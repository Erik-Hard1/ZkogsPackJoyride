using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

public partial class Game : Node
{
	public int starCount = 0;
	public int speed = 150;
	public int score = 0;

	public int typeCounter = 0;
	public string lastType = "laser";
	public List<PackedScene> Laser_List = [];
	public List<PackedScene> Star_List = [];
	[Export]
	public PackedScene Spawner { get; set; }

	public Marker2D LaserSpawn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//LASER PATERN LIST
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
					
					if (fileName != "laser_patern_template.tscn")
					{
						GD.Print($"Found file: {fileName}");
						Laser_List.Add(ResourceLoader.Load<PackedScene>($"res://Laser_Paterns/{fileName}"));
					}

				}
				fileName = dir.GetNext();
			}
		}
		else
		{
			GD.Print("An error occurred when trying to access the path.");
		}
		//STAR PATERN LIST
		using var dirStar = DirAccess.Open("res://Star_Paterns");
		if (dir != null)
		{
			dirStar.ListDirBegin();
			string fileName = dirStar.GetNext();
			while (fileName != "")
			{
				if (dirStar.CurrentIsDir())
				{
					GD.Print($"Found directory: {fileName}");
				}
				else
				{
					GD.Print($"Found file: {fileName}");
					if (fileName != "laser_patern_template.tscn")
					{
						Star_List.Add(ResourceLoader.Load<PackedScene>($"res://Star_Paterns/{fileName}"));
					}

				}
				fileName = dirStar.GetNext();
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
		//GD.Print(Math.Floor(score * 0.001));
	}
	public void StarPickUp()
	{
		starCount++;
		GD.Print("Stars" + starCount);
	}

	public void OnSpawnTimerTimeOut()
	{
		GD.Print("Spawning TinmoeOut");
		Random rand = new Random();


		if (lastType == "star")
		{
			if (typeCounter > 0)
			{
				typeCounter -= 1;
				int random = rand.Next(Star_List.Count);
				var star = Star_List[random].Instantiate<TileMapLayer>();
				var position = LaserSpawn.Position;

				position.Y += rand.Next(50, (int)GetViewport().GetVisibleRect().Size.Y - 100);
				star.Position = position;


				AddChild(star);

			}
			else
			{
				lastType = "laser";
				GD.Print("Star mode exited");
			}
		}
		if (lastType == "laser")
		{
			//Check if its time for coin heaven
			int chance = typeCounter * 5 + 5;
			if (chance > 44) { chance = 33; }

			if (rand.Next(100) < chance)
			{
				lastType = "star";
				typeCounter = rand.Next(2, typeCounter + 2);
				if (typeCounter > 15) { typeCounter = 10; }
				GD.Print("Star mode entered");
			}
			else
			{
				//Spawn laser
				typeCounter += 1;
				int random = rand.Next(Laser_List.Count);
				var laser = Laser_List[random].Instantiate<TileMapLayer>();
				var position = LaserSpawn.Position;

				laser.Position = position;
				//laser.speed = -speed;

				AddChild(laser);
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
