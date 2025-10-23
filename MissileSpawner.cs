using Godot;
using System;
	
public partial class MissileSpawner : AnimatedSprite2D
{
	public Timer SpawnerTimer;
	public bool SpawnBool = true;
	[Signal]
	public delegate void SpawnMissileEventHandler();
	public float speed = 150;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnerTimer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
        var position = Position;
		position.X += speed * (float)delta;
		Position = position;
    }

	public void OnMissileTimerTimeOut()
	{
		/*
		if (SpawnBool)
		{
			EmitSignal.(SignalName.SpawnMissile);
			SpawnerTimer.WaitTime = 1;
		}
        else
        {
			QueueFree();
        }
		*/
		QueueFree();
	}
}
