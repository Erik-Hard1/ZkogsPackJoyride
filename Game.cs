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
	public int totalSpawns = 0;
	public double SetTimer = 0.0;
	public List<PackedScene> Laser_List = [];
	public List<PackedScene> Star_List = [];
	[Export]
	public PackedScene Spawner { get; set; }
	public Timer SpawnerTimer;
	public Timer SpeedUpTimer;

	public Marker2D LaserSpawn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnerTimer = GetNode<Timer>("SpawnTimer");
		SpeedUpTimer = GetNode<Timer>("SpeedUpTimer");
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

					if (fileName != "laser_patern_template.tscn" && fileName != "LaserPaternTemplate.cs")
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
		foreach (var laser in Laser_List)
		{
			GD.Print(laser);
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
				var star = Star_List[random].Instantiate<LaserPaternTemplate>();
				var position = LaserSpawn.Position;

				position.Y += rand.Next(50, (int)GetViewport().GetVisibleRect().Size.Y - 100);
				star.Position = position;

				star.speed = -speed;

				AddChild(star);

			}
			else
			{
				lastType = "laser";
				totalSpawns += 3;
				GD.Print("Star mode exited");
			}
		}
		if (lastType == "laser")
		{
			//Check if its time for coin heaven
			int chance = typeCounter * 2 + 5;
			if (chance > 25) { chance = 20; }

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
				var laser = Laser_List[random].Instantiate<LaserPaternTemplate>();
				var position = LaserSpawn.Position;

				laser.Position = position;
				laser.speed = -speed;

				totalSpawns += 1;

				AddChild(laser);
			}
			SpawnerTimer.WaitTime = SetTimer;

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
	public void OnSpeedTimerTimeOut()
	{
		/*
        if(speed < 800 && lastType != "star")
        {
			speed += 50;
			GD.Print("Time to pick up the pace! Speed: " + speed);
        }
		*/

		switch (totalSpawns)
		{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 3.0;
				speed = 150;
				SpeedUpTimer.WaitTime = 10;
				break;
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 2.9;
				speed = 175;
				SpeedUpTimer.WaitTime = 10;
				break;
			case 12:
			case 13:
			case 14:
			case 15:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 2.6;
				speed = 200;
				SpeedUpTimer.WaitTime = 8;
				break;
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 2.3;
				speed = 250;
				SpeedUpTimer.WaitTime = 8;
				break;
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 2.0;
				speed = 300;
				SpeedUpTimer.WaitTime = 7;
				break;
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.9;
				speed = 350;
				SpeedUpTimer.WaitTime = 6;
				break;
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.9;
				speed = 350;
				SpeedUpTimer.WaitTime = 6;
				break;
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.8;
				speed = 400;
				SpeedUpTimer.WaitTime = 5;
				break;
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.8;
				speed = 425;
				SpeedUpTimer.WaitTime = 5;
				break;
			case 46:
			case 47:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.7;
				speed = 425;
				SpeedUpTimer.WaitTime = 20;
				break;

			case 55:
			case 56:
			default:

				GD.Print("EHj!" + totalSpawns);
				SetTimer = 1.5;
				speed = 450;
				SpeedUpTimer.WaitTime = 10;
				totalSpawns += 5;
				break;
		
			
		}



	}
}
