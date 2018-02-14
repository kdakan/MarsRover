using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Text;

namespace MarsRoverTest
{
    [TestClass]
    public class MarsRoverTest
    {
        [TestMethod]
        public void CommandParser_Should_GenerateCommandsAndOutput()
        {
            var input =
@"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";
            var platformx = new Platform();
            var commands = CommandParser.Parse(input);
            var output = new StringBuilder();
            foreach (var command in commands)
                output.Append(command.Execute(platformx));

            Assert.AreEqual(output.ToString(), "1 3 N\r\n5 1 E\r\n");
        }

        [TestMethod]
        public void Rover_Should_GenerateOutput()
        {
            var platform = new Platform();
            platform.Initialize(new Position(5, 5));
            var rover1 = new Rover(new Position(1, 2), Heading.North, platform);
            rover1.Turn(Rotation.Left);
            rover1.Move();
            rover1.Turn(Rotation.Left);
            rover1.Move();
            rover1.Turn(Rotation.Left);
            rover1.Move();
            rover1.Turn(Rotation.Left);
            rover1.Move();
            rover1.Move();

            var rover2 = new Rover(new Position(3, 3), Heading.East, platform);
            rover2.Move();
            rover2.Move();
            rover2.Turn(Rotation.Right);
            rover2.Move();
            rover2.Move();
            rover2.Turn(Rotation.Right);
            rover2.Move();
            rover2.Turn(Rotation.Right);
            rover2.Turn(Rotation.Right);
            rover2.Move();

            var output = rover1.PrintPositionAndHeading() + rover2.PrintPositionAndHeading();

            Assert.AreEqual(output, "1 3 N\r\n5 1 E\r\n");
        }
    }
}

