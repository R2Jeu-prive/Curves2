using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	public static Game Instance { get; private set; }

	public List<TurnSmall> PlacedTurnsSmall;
	public List<TurnBig> PlacedTurnsBig;
	public TurnSmall HeldTurnSmall;
	public TurnBig HeldTurnBig;

	private Plane floorPlane = new Plane(0,1,0,0);
	private Vector3I wantedGridPos;


	public override void _Ready()
	{
		Instance = this;
		PlacedTurnsSmall = new List<TurnSmall>();
		PlacedTurnsBig = new List<TurnBig>();
		HeldTurnSmall = null;
		HeldTurnBig = null;
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
		DebugDraw3D.DrawSphere(intersection, 0.2f);
		return intersection;
	}
}
