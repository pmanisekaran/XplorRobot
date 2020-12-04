namespace TestRobot
{
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
}
