using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
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

            Console.Write(output.ToString());
            Console.ReadKey();

        }
    }
}
