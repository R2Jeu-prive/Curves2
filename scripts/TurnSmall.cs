using Godot;

public partial class TurnSmall : Node3D
{
	Vector3 wantedPos;
	Area3D area;
	MeshInstance3D mesh;
	private float slideSpeed = 20f;
	Material trackTexture = GD.Load<Material>("res://assets/track.tres");
	Material trackTextureOverlapp = GD.Load<Material>("res://assets/track_red.tres");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Yo !");
		wantedPos = Game.Instance.GetSelectedPos(true);
		Position = wantedPos;
		area = GetNode<Area3D>("Area3D");
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(Game.Instance.HeldTurnSmall != this){return;}
		wantedPos = Game.Instance.GetSelectedPos(true);

		Vector3 movement = (wantedPos - Position).LimitLength(slideSpeed * (float)delta);
		Position += movement;

		if(Input.IsActionJustPressed("rotate")){
			RotationDegrees = new Vector3(RotationDegrees.X, RotationDegrees.Y + 90, RotationDegrees.Z);
		}

		mesh.SetSurfaceOverrideMaterial(0, area.HasOverlappingAreas() ? trackTextureOverlapp : trackTexture);
	}
}
