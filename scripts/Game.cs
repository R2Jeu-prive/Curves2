using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	public static Game Instance { get; private set; }

	private PackedScene TurnSmallScene = GD.Load<PackedScene>("res://scenes/pieces/turn_small.tscn");
	private PackedScene TurnBigScene = GD.Load<PackedScene>("res://scenes/pieces/turn_big.tscn");
	private Plane floorPlane = new Plane(0,1,0,0);

	public List<Turn> PlacedTurns;
	public Turn HeldTurn;


	public override void _Ready()
	{
		Instance = this;
		PlacedTurns = new List<Turn>();
		HeldTurn = null;
	}

	public void CreateTurn(bool big){
		Turn newTurn = (big ? TurnBigScene : TurnSmallScene).Instantiate() as Turn;
		newTurn.big = big;
		GetTree().Root.AddChild(newTurn);
		HeldTurn = newTurn;
	}

	public Vector3 GetSelectedPos(bool snapToCenter){
		Vector2 mousePos = GetViewport().GetMousePosition();
		Camera3D cam = GetTree().Root.GetCamera3D();
		Vector3? potentialIntersection = floorPlane.IntersectsRay(cam.Position, cam.ProjectRayNormal(mousePos));

		if(!potentialIntersection.HasValue){throw new Exception("Could not intersect ray with floor plane");}
		Vector3 intersection = potentialIntersection.Value;

		if(snapToCenter){
			intersection.X = Mathf.RoundToInt(intersection.X + 0.5f) - 0.5f;
			intersection.Z = Mathf.RoundToInt(intersection.Z + 0.5f) - 0.5f;
		}else{
			intersection.X = Mathf.RoundToInt(intersection.X);
			intersection.Z = Mathf.RoundToInt(intersection.Z);
		}
		return intersection;
	}

	public void CheckWinCondition(){
		Tip start = GetTree().Root.GetNode<Tip>("Game/Level/Start");
		Tip end = GetTree().Root.GetNode<Tip>("Game/Level/End");
		Vector2I current = GetTipPos(start);
		Vector2I endTip = GetTipPos(end);
		List<Vector2I[]> connections = PlacedTurns.ConvertAll(t => t.GetTips());
		GD.Print("CheckWinCondition !");
		GD.Print(connections.Count);
		while(true){
			GD.Print(current);

			bool foundNext = false;
			List<Vector2I[]> connectionsLeft = new List<Vector2I[]>();
			foreach (Vector2I[] c in connections)
			{
				if(c[0] == current && !foundNext){
					foundNext = true;
					current = c[1];
					continue;
				}
				if(c[1] == current && !foundNext){
					foundNext = true;
					current = c[0];
					continue;
				}
				connectionsLeft.Add(c);
			}
			if(!foundNext){GD.Print("Failed"); return;} //Could not continue path
			if(current == endTip){break;} //Finished path

			connections = connectionsLeft;
		}
		
		//Player has won
		GD.Print("GG !");
	}

	public static Vector2I GetTipPos(Tip tip){

		Vector2I pos = new Vector2I(Mathf.RoundToInt(tip.Position.X), Mathf.RoundToInt(tip.Position.Z));
		int rot = ((Mathf.RoundToInt(tip.RotationDegrees.Y) % 360) + 360) % 360;
		if(rot == 0){return pos + new Vector2I(0,-1);}
		else if(rot == 90){return pos + new Vector2I(-1,0);}
		else if(rot == 180){return pos + new Vector2I(0,1);}
		else if(rot == 270){return pos + new Vector2I(1,0);}
		else{throw new Exception("Could not GetTipRelPos for a non cardinal direction of Start or End");}
	}
}
