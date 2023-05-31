using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Utilities
{
    internal class Command
    {
        public string name;
        public Action doAction;

        public Command(string name, Action doAction)
        {
            this.name = name;
            this.doAction = doAction;
        }
    }

    internal class CommandHandler
    {

        private static List<Command> commands = new List<Command>();

        public static void AddCommand(string command, Action doAction)
        {
            commands.Add(new Command(command.ToLower(), doAction));
        }

        // Return false if a command was not found, true if it was
        //  Additionally, call the corresponding function if found
        public static bool HandleCommand(string command)
        {
            foreach (Command cmd in commands)
            {
                if (Equals(command.ToLower(), cmd.name))
                {
                    cmd.doAction();
                    return true;
                }
            }

            return false;
        }
    }
}
