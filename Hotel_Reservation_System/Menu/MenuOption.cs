using System;

namespace InitialTestApp
{
    class MenuOption
    {
        public readonly Action doOption;
        private readonly String description;
        private readonly int index;
        private readonly bool requiresYN;

        public MenuOption(String description, int index, bool requiresYN, Action doOption)
        {
            this.description = description;
            this.index = index;
            this.doOption = doOption;
            this.requiresYN = requiresYN;
        }

        // Print out the option's details
        public void PrintOption()
        {
            Console.WriteLine(index + ") " + description);
        }

        // Getter for reuiresYN
        public bool RequiresYN()
        {
            return requiresYN;
        }

    }
}
