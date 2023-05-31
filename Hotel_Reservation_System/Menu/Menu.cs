using System;
using System.Collections.Generic;

namespace InitialTestApp
{
    /* How to use:
     * In order to add a new menu: Add a menu type to the MenuType enum
     * 
     * In order to add options for each menu: Add something simmilar to the following example
     * 
     * Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "This is the first option", false, doOption: () => {
     *      // Add functionality here
     *      Console.WriteLine("This is just a test");
     * });
     * 
    */

    public static class Menu
    {
        public enum MenuType
        {
            MAIN_MENU,
            SECONDARY_MENU,
            THIRD_MENU,
            REPORT_MENU,
            RESERVATION_MENU,
        }

        // Creates an array for indexes for each menu
        private static readonly int[] indexes = new int[Enum.GetValues(typeof(MenuType)).Length];
        // Creates an array of lists of the options for each menu
        private static readonly List<MenuOption>[] menuOptions = new List<MenuOption>[Enum.GetValues(typeof(MenuType)).Length];

        static Menu()
        {
            // Setup initial list of menu options
            for (int i = 0; i < Enum.GetValues(typeof(MenuType)).Length; i++)
            {
                menuOptions[i] = new List<MenuOption>();
            }
        }

        // Add a menu option to the menu list
        public static void AddMenuOption(MenuType menuType, String description, bool requiresYN, Action doOption)
        {
            menuOptions[(int)menuType].Add(new MenuOption(description, indexes[(int)menuType]++, requiresYN, doOption));
        }

        // Print out all options for the specified menuType
        public static void PrintMenu(MenuType menuType)
        {
            // Loop through all menu options and print their index and value
            foreach (MenuOption option in menuOptions[(int)menuType])
            {
                option.PrintOption();
            }
        }

        // Call "doOption" method for the selected option
        public static void HandleOption(MenuType menuType, int option)
        {
            if (option == -1)
            {
                ErrorHandler.HandleError(ErrorHandler.ErrorType.INVALID_INPUT);
                return;
            }

            // Get the currently selected option
            MenuOption selectedOption = menuOptions[(int)menuType][option];

            if (selectedOption.RequiresYN())
            {
                if (!UserInput.HandleYNPrompt())    // If selected anything other than Y
                {
                    return; // Don't call option's function
                }
            }
            selectedOption.doOption();
        }
    }
}
