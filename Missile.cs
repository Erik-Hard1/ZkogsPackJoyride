using Godot;
using System;

public partial class Missile : StaticBody2D
{
	public float speed = -300;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var position = Position;
		position.X += speed * (float)delta;
		Position = position;
	}
	
	public void Screen_Exited()
    {
		QueueFree();
    }
}
