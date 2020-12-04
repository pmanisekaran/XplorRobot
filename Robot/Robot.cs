using System;
using System.Collections.Generic;

namespace Robot
{
	public class Command
	{
		public readonly string actionName;
		public Command(string actionName)
		{
			this.actionName = actionName;
		}
	}
	public class PlaceCommand : Command
	{
		public int x, y;
		public Direction direction = Direction.North;

		public PlaceCommand(int x, int y, Direction direction) : base("PLACE")
		{
			this.x = x;
			this.y = y;
			this.direction = direction;

		}

	}



	public class Robot
	{

		private List<Command> Commands;
		private int xCurrent = 0;
		private int yCurrent = 0;
		const int xMax = 4; // 5 x 5 table
		const int yMax = 4;// 5 x 5 table
		const int xMin = 0;// 5 x 5 table
		const int yMin = 0;// 5 x 5 table

		private Direction direction = Direction.North;
		public Robot(List<Command> commands)
		{
			this.xCurrent = -1;
			this.yCurrent = -1;
			this.direction = Direction.North;

			if (commands == null || commands.Count == 0)
				throw new Exception("atleast one command must be supplied ");
			if (commands[0].actionName != "PLACE")
				throw new Exception("First command must always be PLACE ");
			PlaceCommand p = commands[0] as PlaceCommand;
			if (p != null)
			{
				if (p.x < 0 || p.x > 4 || p.y < 0 || p.y > 0)
					throw new Exception("Inital place command must be on the table");
			}
			this.Commands = commands;
		}

		public void ExceuteCommands()
		{

			foreach (var c in Commands)
			{

				switch (c.actionName)
				{
					case "PLACE":
						PlaceCommand(c);
						break;
					case "MOVE":
						Move();
						break;
					case "LEFT":
					case "RIGHT":
						ChangeDirection(c);
						break;
				}

			}


		}

		private bool isValidCommand(int x, int y)
		{
			if (x < xMin || x > xMax || y < yMin || y > yMax)
				return false;
			return true;

		}
		private void PlaceCommand(Command c)
		{
			var placeCommand = (c as PlaceCommand);
			if (placeCommand == null)
				throw new Exception("PLACE action must be PlaceCommand object");
			if (!isValidCommand(placeCommand.x, placeCommand.y))
				return;
			this.xCurrent = placeCommand.x;
			this.yCurrent = placeCommand.y;
			this.direction = placeCommand.direction;
		}
		private void ChangeDirection(Command c)
		{
			if (c.actionName == "LEFT")
			{
				//if north facing left will make you face west
				if (this.direction == Direction.North)
					this.direction = Direction.West;

				//if north facing west will make you face south
				if (this.direction == Direction.West)
					this.direction = Direction.South;

				//if north facing south left will make you face east
				if (this.direction == Direction.South)
					this.direction = Direction.East;
				//if north facing east , left will make you face north
				if (this.direction == Direction.East)
					this.direction = Direction.North;

			}
			if (c.actionName == "RIGHT")
			{
				//if north facing left will make you face east
				if (this.direction == Direction.North)
					this.direction = Direction.East;

				//if north facing west will make you face north
				if (this.direction == Direction.West)
					this.direction = Direction.North;

				//if north facing south left will make you face west
				if (this.direction == Direction.South)
					this.direction = Direction.West;
				//if north facing east , left will make you face north
				if (this.direction == Direction.East)
					this.direction = Direction.South;

			}

		}



		private string Report()
		{
			return $"x is {this.xCurrent} y is {this.yCurrent} direction facing is {this.direction.ToString()}";
		}

		private void Move()
		{
			if (this.direction == Direction.East && isValidCommand(this.xCurrent + 1, this.yCurrent))
				this.xCurrent += 1;
			if (this.direction == Direction.West && isValidCommand(this.xCurrent - 1, this.yCurrent))
				this.xCurrent -= 1;

			if (this.direction == Direction.North && isValidCommand(this.xCurrent, this.yCurrent + 1))
				this.yCurrent += 1;
			if (this.direction == Direction.West && isValidCommand(this.xCurrent, this.yCurrent - 1))
				this.yCurrent -= 1;


		}

	}

	public enum Direction
	{
		North,
		South,
		East,
		West

	}
}
