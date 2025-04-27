using Godot;
using System;
using System.Runtime.Serialization;

public partial class Turn : Node3D
{
	Area3D area;
	MeshInstance3D mesh;
	Material trackTexture = GD.Load<Material>("res://assets/track.tres");
	Material trackTextureOverlapp = GD.Load<Material>("res://assets/track_red.tres");
	
	Vector3 wantedPos;
	private float slideSpeed = 20f;
	public bool big;
	private bool justPlaced = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area = GetNode<Area3D>("Area3D");
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");

		wantedPos = Game.Instance.GetSelectedPos(!big);
		Position = wantedPos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector3 movement = (wantedPos - Position).LimitLength(slideSpeed * (float)delta);
		Position += movement;

		if(justPlaced && wantedPos - Position == Vector3.Zero){
			justPlaced = false;
			Game.Instance.PlacedTurns.Add(this);
			Game.Instance.CheckWinCondition();
		}

		mesh.SetSurfaceOverrideMaterial(0, area.HasOverlappingAreas() ? trackTextureOverlapp : trackTexture);

		if(Game.Instance.HeldTurn != this){return;}

		if(Input.IsActionJustReleased("left_click")){
			Game.Instance.HeldTurn = null;
			justPlaced = true; //will wait for piece to glide in place before CheckWinCondition
			if(area.HasOverlappingAreas()){
				QueueFree();
			}
			return;
		}

		wantedPos = Game.Instance.GetSelectedPos(!big);


		if(Input.IsActionJustPressed("right_click")){
			RotationDegrees = new Vector3(RotationDegrees.X, RotationDegrees.Y - 90f, RotationDegrees.Z);
		}
	}

	public Vector2I[] GetTips(){
		Vector3 a, b;
		if(Mathf.Abs(RotationDegrees.Y) % 180f > 45f && Mathf.Abs(RotationDegrees.Y) % 180f < 135f){
			a = new Vector3(1f, 0, 1f);
			b = new Vector3(-1f, 0, -1f);
		}else{
			a = new Vector3(-1f, 0, 1f);
			b = new Vector3(1f, 0, -1f);
		}
		if(!big){a/=2;b/=2;}
		a += Position;
		b += Position;

		Vector2I tipA = new Vector2I(Mathf.RoundToInt(a.X), Mathf.RoundToInt(a.Z));
		Vector2I tipB = new Vector2I(Mathf.RoundToInt(b.X), Mathf.RoundToInt(b.Z));
        
		return [tipA, tipB];
	}
}
