using Godot;

public partial class Tip : Node3D
{
	Area3D area;
	MeshInstance3D mesh;
	Material trackTexture = GD.Load<Material>("res://assets/track.tres");
	Material trackTextureOverlapp = GD.Load<Material>("res://assets/track_red.tres");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area = GetNode<Area3D>("Area3D");
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		mesh.SetSurfaceOverrideMaterial(0, area.HasOverlappingAreas() ? trackTextureOverlapp : trackTexture);
	}
}
