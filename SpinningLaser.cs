using Godot;
using System;

public partial class SpinningLaser : StaticBody2D
{
	
	public float speed = -150;
	public double rotationSpeed = 0.05;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var position = Position;
		position.X += speed * (float)delta;

		

		Rotate((float)rotationSpeed);

		Position = position;
	}
}
