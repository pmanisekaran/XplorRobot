using System;
using System.Collections.Generic;

namespace TestRobot
{
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
			//if (commands[0].actionName != "PLACE")
			//	throw new Exception("First command must always be PLACE ");
			//PlaceCommand p = commands[0] as PlaceCommand;
			//if (p != null)
			//{
			//	if (p.x < 0 || p.x > 4 || p.y < 0 || p.y > 0)
			//		throw new Exception("Inital place command must be on the table");
			//}
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
					case "REPORT":
						Report();
						break;
				}

			}


		}

		public static bool isValidCommand(int x, int y)
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

		private bool isRobotOnTheTable()
		{
			if (xCurrent < xMin || xCurrent > xMax || yCurrent < yMin || yCurrent > yMax)
				return false;
			else
				return true;
		}

		private void ChangeDirection(Command c)
		{
			//if not on the table ignore the command
			if (!isRobotOnTheTable())
				return;

			switch (this.direction)
			{
				case Direction.North:
					if (c.actionName == "LEFT")
						direction = Direction.West;
					else
						direction = Direction.East;
					break;
				case Direction.South:
					if (c.actionName == "LEFT")
						direction = Direction.East;
					else
						direction = Direction.West;
					break;
				case Direction.East:
					if (c.actionName == "LEFT")
						direction = Direction.North;
					else
						direction = Direction.South;
					break;
				case Direction.West:
					if (c.actionName == "LEFT")
						direction = Direction.South;
					else
						direction = Direction.North;
					break;
			}
		}



		private void Report()
		{
			Console.WriteLine($"x is {this.xCurrent} y is {this.yCurrent} direction facing is {this.direction.ToString()}");
		}

		private void Move()
		{

			//if not on the table ignore the command
			if (!isRobotOnTheTable())
				return;


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
