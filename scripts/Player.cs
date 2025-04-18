using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class Player : Camera3D
{

	PackedScene TurnSmallScene = GD.Load<PackedScene>("res://scenes/turn_small.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(Input.IsActionJustPressed("right_click")){
			TurnSmall newTurn = TurnSmallScene.Instantiate() as TurnSmall;
			GetTree().Root.AddChild(newTurn);
			Game.Instance.HeldTurn = newTurn;
		}

		if(Input.IsActionJustReleased("right_click") && Game.Instance.HeldTurn != null){
			Game.Instance.PlacedTurns.Add(Game.Instance.HeldTurn);
			Game.Instance.HeldTurn = null;
		}
	}
}
