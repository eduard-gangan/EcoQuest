using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoQuest
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "map", "east", "south", "west", "look", "back", "quit", "reputation", "balance", "inventory", "sail", "dump", "help", "sort", "pick", "talk", "fishsort", "showcase", "descend", "analyze", "ascend", "read", "end" };

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
