using Godot;

public partial class Player : Camera3D
{

	PackedScene TurnSmallScene = GD.Load<PackedScene>("res://scenes/turn_small.tscn");
	PackedScene TurnBigScene = GD.Load<PackedScene>("res://scenes/turn_big.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(Input.IsActionJustPressed("small_turn") && Game.Instance.HeldTurnSmall == null && Game.Instance.HeldTurnBig == null){
			GD.Print("Pressed small turn");
			TurnSmall newTurn = TurnSmallScene.Instantiate() as TurnSmall;
			GetTree().Root.AddChild(newTurn);
			Game.Instance.HeldTurnSmall = newTurn;
		}

		if(Input.IsActionJustReleased("click") && Game.Instance.HeldTurnSmall != null){
			GD.Print("Let go of small turn");
			Game.Instance.PlacedTurnsSmall.Add(Game.Instance.HeldTurnSmall);
			Game.Instance.HeldTurnSmall = null;
		}

		if(Input.IsActionJustPressed("big_turn") && Game.Instance.HeldTurnSmall == null && Game.Instance.HeldTurnBig == null){
			GD.Print("Pressed big turn");
			TurnBig newTurn = TurnBigScene.Instantiate() as TurnBig;
			GetTree().Root.AddChild(newTurn);
			Game.Instance.HeldTurnBig = newTurn;
		}

		if(Input.IsActionJustReleased("click") && Game.Instance.HeldTurnBig != null){
			GD.Print("Let go of big turn");
			Game.Instance.PlacedTurnsBig.Add(Game.Instance.HeldTurnBig);
			Game.Instance.HeldTurnBig = null;
		}
	}
}
