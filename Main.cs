using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Main : Node
{
	[Signal]
	public delegate void PlayEventHandler();
	public Node GameScene;
	public CanvasLayer TitleScreen;
	public CanvasLayer DeathScreen;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		GameScene = GetNode<Node>("Game");
		TitleScreen = GetNode<CanvasLayer>("MainMenu");
		DeathScreen = GetNode<CanvasLayer>("DeathScreen");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayButtonPressed()
	{
		//EmitSignal(SignalName.Play);
		GameScene.GetTree().Paused = false;
		TitleScreen.Visible = false;
	}

	public void DeathMenu()
	{
		DeathScreen.Visible = true;
	}
	
	public void RestartButtonPressed()
    {
        GetTree().CallDeferred("reload_current_scene");
    }
}
