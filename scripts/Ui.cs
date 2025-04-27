using Godot;

public partial class Ui : Node
{
	public override void _Ready()
	{
		GetNode<Button>("%ButtonTurnSmall").Pressed += () => Game.Instance.CreateTurn(false);
		GetNode<Button>("%ButtonTurnBig").Pressed += () => Game.Instance.CreateTurn(true);
	}
}
