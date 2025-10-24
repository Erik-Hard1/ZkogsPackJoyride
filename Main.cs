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
	public CanvasLayer PauseScreen;

	public bool paused = false;
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		GameScene = GetNode<Node>("Game");
		TitleScreen = GetNode<CanvasLayer>("MainMenu");
		DeathScreen = GetNode<CanvasLayer>("DeathScreen");
		PauseScreen = GetNode<CanvasLayer>("PauseMenu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
			PauseButtonPressed();
        }
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

	public void PauseButtonPressed()
	{
		if (DeathScreen.Visible == false && TitleScreen.Visible == false)
		{
			if (!paused)
			{
				GameScene.GetTree().Paused = true;
				PauseScreen.Visible = true;
				paused = true;
			}
			else
			{
				GameScene.GetTree().Paused = false;
				PauseScreen.Visible = false;
				paused = false;
			}
		}

	}
}
