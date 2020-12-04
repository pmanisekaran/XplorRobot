using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TestRobot
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Command> commands = GetCommandsfromTextinput();
			Robot r = new Robot(commands);
			r.ExceuteCommands();
			Console.ReadKey();
		}

		private static List<Command> GetCommandsfromTextinput()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "TestRobot.TestInput.txt";
			List<Command> list = new List<Command>();
			var names = assembly.GetManifestResourceNames();

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{

				Command c = null;
				Direction d = Direction.East;
				while (!reader.EndOfStream)
				{
					string s = reader.ReadLine().Trim();
					if (s.StartsWith("PLACE"))
					{
						var array = s.Split(new char[] { ' ' })[1].Split(new char[] { ',' });

						switch (array[2])
						{
							case "NORTH":
								d = Direction.North;
								break;
							case "SOUTH":
								d = Direction.South;
								break;
							case "WEST":
								d = Direction.West;
								break;
							case "EAST":
								d = Direction.East;
								break;
						}
						var x = int.Parse(array[0]);
						var y = int.Parse(array[1]);
						c = new PlaceCommand(int.Parse(array[0]), int.Parse(array[1]), d);

					}
					else
					{
						c = new Command(s);
					}
					list.Add(c);

				}
			}

			return list;
		}
	}
}
