using System.Linq.Expressions;
using Godot;

public partial class TurnSmall : Node3D
{
	Vector3I wantedPos;
	private float slideSpeed = 30f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Yo !");
		Vector3I? wantedPosOrNull = Game.Instance.GetSelectedPos();
		if(wantedPosOrNull == null){ return; }
		wantedPos = wantedPosOrNull.Value;
		Position = wantedPos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(this != Game.Instance.HeldTurn){
			wantedPos = new Vector3I(Mathf.RoundToInt(Position.X), 0, Mathf.RoundToInt(Position.Z));
		}else{
			Vector3I? wantedPosOrNull = Game.Instance.GetSelectedPos();
			if(wantedPosOrNull == null){ return; }
			wantedPos = wantedPosOrNull.Value;
		}

		Vector3 movement = (wantedPos - Position).LimitLength(slideSpeed * (float)delta);
		Position += movement;
	}
}
