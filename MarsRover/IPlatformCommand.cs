using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public interface IPlatformCommand
    {
        string Execute(Platform platform);
    }

    public class PlatformInitializeCommand : IPlatformCommand
    {
        private Position maxPosition;

        public PlatformInitializeCommand(Position maxPosition)
        {
            this.maxPosition = maxPosition;
        }

        public string Execute(Platform platform)
        {
            platform.Initialize(maxPosition);
            return string.Empty;
        }
    }

    public class NewRoverCommand : IPlatformCommand
    {
        private Position position;
        private Heading heading;

        public NewRoverCommand(Position position, Heading heading)
        {
            this.position = position;
            this.heading = heading;
        }

        public string Execute(Platform platform)
        {
            new Rover(position, heading, platform);
            return string.Empty;
        }
    }

    public class RotateRoverCommand : IPlatformCommand
    {
        private Rotation rotation;

        public RotateRoverCommand(Rotation rotation)
        {
            this.rotation = rotation;
        }

        public string Execute(Platform platform)
        {
            var rover = platform.GetRover();
            rover.Turn(rotation);
            return string.Empty;
        }
    }

    public class MoveRoverCommand : IPlatformCommand
    {
        public string Execute(Platform platform)
        {
            var rover = platform.GetRover();
            rover.Move();
            return string.Empty;
        }
    }

    public class PrintPositionAndHeadingRoverCommand : IPlatformCommand
    {
        public string Execute(Platform platform)
        {
            var rover = platform.GetRover();
            return rover.PrintPositionAndHeading();
        }
    }
}
