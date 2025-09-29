using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	public int starCount = 0;
	public int speed = 150;
	public int score = 0;
	public List<> Laser_List = [];
	[Export]
	public PackedScene Spawner { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		using var dir = DirAccess.Open("res://Laser_Paterns");
		if (dir != null)
		{
			dir.ListDirBegin();
			string fileName = dir.GetNext();
			while (fileName != "")
			{
				if (dir.CurrentIsDir())
				{
					GD.Print($"Found directory: {fileName}");
				}
				else
				{
					GD.Print($"Found file: {fileName}");
					ResourceLoader.Load<PackedScene>("res://scene.tscn")
				}
				fileName = dir.GetNext();
			}
		}
		else
		{
			GD.Print("An error occurred when trying to access the path.");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		score += speed;
		GD.Print(Math.Floor(score * 0.001));
	}
	public void StarPickUp()
	{
		starCount++;
		GD.Print("Stars" + starCount);
	}

	public void OnSpawnTimerTimeOut()
	{

		var thing = Spawner.Instantiate<TileMapLayer>();
		AddChild(thing);

		// Create the objects.
		var node = new Node2D();
		var body = new RigidBody2D();
		var collision = new CollisionShape2D();

		// Create the object hierarchy.
		body.AddChild(collision);
		node.AddChild(body);

		// Change owner of `body`, but not of `collision`.
		body.Owner = node;
		var scene = new PackedScene();

		// Only `node` and `body` are now packed.
		Error result = scene.Pack(node);
		if (result == Error.Ok)
		{
			Error error = ResourceSaver.Save(scene, "res://path/name.tscn"); // Or "user://..."
			if (error != Error.Ok)
			{
				GD.PushError("An error occurred while saving the scene to disk.");
			}
		}
	}

	public void DirContents(string path)
	{
		using var dir = DirAccess.Open(path);
		if (dir != null)
		{
			dir.ListDirBegin();
			string fileName = dir.GetNext();
			while (fileName != "")
			{
				if (dir.CurrentIsDir())
				{
					GD.Print($"Found directory: {fileName}");
				}
				else
				{
					GD.Print($"Found file: {fileName}");
				}
				fileName = dir.GetNext();
			}
		}
		else
		{
			GD.Print("An error occurred when trying to access the path.");
		}
	}

}
