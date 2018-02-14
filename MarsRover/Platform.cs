using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Platform
    {
        private Position maxPosition;
        private List<Rover> rovers;

        public void Initialize(Position maxPosition)
        {
            if (this.maxPosition != null)
                throw new Exception("Platform is already initialized.");

            this.maxPosition = maxPosition;
            rovers = new List<Rover>();
        }

        public bool IsPositionValid(Position position)
        {
            return position.IsWithin(Position.Origin, maxPosition);
        }

        public bool IsPositionFree(Position position)
        {
            return !rovers.Any(r => r.Position == position);
        }

        public void AddRover(Rover rover)
        {
            rovers.Add(rover);
        }

        public void RemoveRover(Rover rover)
        {
            rovers.Remove(rover);
        }
        public Rover GetRover()
        {
            return rovers.Last();
        }
    }
}
