using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        public Position Position { get; private set; }
        public Heading Heading { get; private set; }
        public Platform Platform { get; private set; }

        public Rover(Position position, Heading heading, Platform platform)
        {
            if (!platform.IsPositionValid(position))
                throw new ArgumentException("position", $"{position} is not valid.");

            if (!platform.IsPositionFree(position))
                throw new ArgumentException("position", $"{position} is not free.");

            Position = position;
            Heading = heading;
            Platform = platform;
            Platform.AddRover(this);
        }

        public void Turn(Rotation rotation)
        {
            int headingInt = (int)Heading + (int)rotation + 4;
            Heading = (Heading) (headingInt % 4);
        }

        public void Move()
        {
            var x = Position.X;
            var y = Position.Y;
            switch (Heading)
            {
                case Heading.North:
                    y++;
                    break;
                case Heading.East:
                    x++;
                    break;
                case Heading.South:
                    y--;
                    break;
                case Heading.West:
                    x--;
                    break;
            }

            var position = new Position(x, y);

            if (!Platform.IsPositionValid(position))
                throw new ArgumentException("position", $"{position} is not valid. Cannot move towards {Heading} from current position {Position}.");

            if (!Platform.IsPositionFree(position))
                throw new ArgumentException("position", $"{position} is not free. Cannot move towards {Heading} from current position {Position}.");

            Position = position;
        }

        public string PrintPositionAndHeading()
        {
            var headingString = "";
            switch (Heading)
            {
                case Heading.North:
                    headingString = "N";
                    break;
                case Heading.East:
                    headingString = "E";
                    break;
                case Heading.South:
                    headingString = "S";
                    break;
                case Heading.West:
                    headingString = "W";
                    break;
            }
            return $"{Position.X} {Position.Y} {headingString}\r\n";
        }

    }
}
