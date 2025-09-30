using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	public int starCount = 0;
	public int speed = 150;
	public int score = 0;
	public List<PackedScene> Laser_List = [];
	[Export]
	public PackedScene Spawner { get; set; }

	public Marker2D LaserSpawn;
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
					Laser_List.Add(ResourceLoader.Load<PackedScene>("res://scene.tscn"));
				}
				fileName = dir.GetNext();
			}
		}
		else
		{
			GD.Print("An error occurred when trying to access the path.");
		}

		LaserSpawn = GetNode<Marker2D>("SpawnMarker");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		score += speed;
		//sGD.Print(Math.Floor(score * 0.001));
	}
	public void StarPickUp()
	{
		starCount++;
		GD.Print("Stars" + starCount);
	}

	public void OnSpawnTimerTimeOut()
	{
		GD.Print("Spawning Laser TinmoeOut");
		Random rand = new Random();
		int random = rand.Next(Laser_List.Count);

		var laser = Laser_List[random].Instantiate<TileMapLayer>();
		var position = LaserSpawn.Position;

		AddChild(laser);

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
