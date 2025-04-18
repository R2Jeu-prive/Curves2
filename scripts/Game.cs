using Godot;
using System.Collections.Generic;

public partial class Game : Node
{
	public static Game Instance { get; private set; }

	public List<TurnSmall> PlacedTurns;
	public TurnSmall HeldTurn;

	private Plane floorPlane = new Plane(0,1,0,0);
	private Vector3I wantedGridPos;


	public override void _Ready()
	{
		Instance = this;
		PlacedTurns = new List<TurnSmall>();
		HeldTurn = null;
	}

	public Vector3I? GetSelectedPos(){
		Vector2 mousePos = GetViewport().GetMousePosition();
		Camera3D cam = GetTree().Root.GetCamera3D();
		Vector3? intersection = floorPlane.IntersectsRay(cam.Position, cam.ProjectRayNormal(mousePos));

		if(!intersection.HasValue){return null;}
		wantedGridPos = new Vector3I(Mathf.RoundToInt(intersection.Value.X), 0, Mathf.RoundToInt(intersection.Value.Z));

		DebugDraw3D.DrawSphere(wantedGridPos, 0.2f);

		return wantedGridPos;
	}
}
