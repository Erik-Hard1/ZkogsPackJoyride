using Godot;
using System;

public partial class Zkogs : CharacterBody2D
{
	[Signal]
	public delegate void StarPickUpEventHandler();
	[Signal]
	public delegate void DieEventHandler();
	public const float Speed = 300.0f;
	public const float JumpVelocity = -50.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)1.38 * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionPressed("Fly"))
		{
			velocity.Y += JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
		//	velocity.X = direction.X * Speed;
		}
		else
		{
		//	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	public void OnStarZoneAreaEntered(Area2D area2d)
	{
		if (area2d is Star)
		{
			//GD.Print("Star Picked Up");
			EmitSignal(SignalName.StarPickUp);
			area2d.QueueFree();
		}
	}

	public void OnHitBoxBodyEntered(Area2D area2d)
	{
		//GD.Print("Collision");
		if (area2d.GetParent() is Beam || area2d.GetParent() is SpinningLaser || area2d.GetParent() is FastSpinningLaser || area2d.GetParent() is CounterSpinningLaser || area2d.GetParent() is Missile)
		{
			GD.Print("Die");
			EmitSignal(SignalName.Die);
		}
	}

}
