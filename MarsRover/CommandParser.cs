using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class CommandParser
    {
        public static List<IPlatformCommand> Parse(string input)
        {
            var commands = new List<IPlatformCommand>();

            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("input", $"Empty input.");

            var lines = input.Split(new[] { '\r', '\n' }).Where(l => !string.IsNullOrEmpty(l)).Select(l => l.Trim()).ToArray();
            int currentRoverIndex = -1;
            for(int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                var onLineNumber = $"on line {i}";
                if (line.Length == 0)
                    throw new ArgumentException("input", $"Empty line not allowed {onLineNumber}.");

                var parts = line.Split();
                
                if (char.IsDigit(line[0]))
                {
                    if (parts.Count() < 2 || parts.Count() > 3)
                    {
                        throw new ArgumentException("input", $"Line starting with digit must have either two or three parts {onLineNumber}.");
                    }

                    int x;
                    if (!int.TryParse(parts[0], out x) || x < 0)
                        throw new ArgumentException("input", $"Cannot parse positive integer from {parts[0]} {onLineNumber}.");
                    int y;
                    if (!int.TryParse(parts[1], out y) || y < 0)
                        throw new ArgumentException("input", $"Cannot parse positive integer from {parts[1]} {onLineNumber}.");

                    if (parts.Count() == 2)
                    {
                        if (i != 0)
                            throw new ArgumentException("input", $"Platform initialization should be on the first line {onLineNumber}.");

                        //Platform.Instance.Initialize(new Position(x, y));
                        commands.Add(new PlatformInitializeCommand(new Position(x, y)));
                    }
                    else
                    {
                        if (parts[2].Length != 1 || !"NESW".Contains(parts[2]))
                            throw new ArgumentException("input", $"Rover initialization line should have either N, E, S, or W on the last part {onLineNumber}.");

                        var heading = Heading.North;
                        switch (parts[2][0])
                        {
                            case 'N':
                                heading = Heading.North;
                                break;
                            case 'E':
                                heading = Heading.East;
                                break;
                            case 'S':
                                heading = Heading.South;
                                break;
                            case 'W':
                                heading = Heading.West;
                                break;
                        }

                        if (currentRoverIndex != -1)
                            commands.Add(new PrintPositionAndHeadingRoverCommand());

                        currentRoverIndex++;
                        commands.Add(new NewRoverCommand(new Position(x, y), heading));
                        //currentRover = new Rover(new Position(x, y), heading);
                    }
                }
                else
                {
                    foreach(var c in line)
                    {
                        switch (c)
                        {
                            case 'L':
                                commands.Add(new RotateRoverCommand(Rotation.Left));
                                break;
                            case 'R':
                                commands.Add(new RotateRoverCommand(Rotation.Right));
                                break;
                            case 'M':
                                commands.Add(new MoveRoverCommand());
                                break;
                            default:
                                throw new ArgumentException("input", $"Rover rotation/move line should have either L, R, or M characters {onLineNumber}.");
                        }

                    }
                    

                }
            }

            if (currentRoverIndex != -1)
                commands.Add(new PrintPositionAndHeadingRoverCommand());

            return commands;
        }
    }
}
