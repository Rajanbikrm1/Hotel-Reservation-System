using System;
using System.Runtime.InteropServices;

namespace InitialTestApp
{
    public static class UserInput
    {
        // Set up console printing to allow underlined text
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        const string UNDERLINE = "\x1B[4m";
        const string RESET = "\x1B[0m";

        static UserInput()
        {
            // Set up console to allow special text
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            uint mode;
            GetConsoleMode(handle, out mode);
            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            SetConsoleMode(handle, mode);
        }

        // Print a prompt for the user
        public static void PromptUser(bool showDefualtPrompt, String message = "")
        {
            if (showDefualtPrompt)
            {
                Console.Write(">> ");
            } else
            {
                Console.Write(message);
            }
        }

        // Test if a string is an integer
        public static bool IsInt(string input)
        {
            int i = 0;
            return int.TryParse(input, out i);
        }

        // Test if a string is a long
        public static bool IsLong(string input)
        {
            long i = 0;
            return long.TryParse(input, out i);
        }

        // Handle taking in user input as an integer
        public static int HandleUserInputInt()
        {
            string input = Console.ReadLine();
            if (!IsInt(input) || Equals(input, ""))
            {
                return -1;  // Was not possible to get an integer
            }
            return Convert.ToInt32(input);
        }

        // Handle taking in user input as an long
        public static long HandleUserInputLong()
        {
            string input = Console.ReadLine();
            if (!IsLong(input) || Equals(input, ""))
            {
                return -1;  // Was not possible to get a long
            }
            return Convert.ToInt64(input);
        }

        // Handle taking in user input as an string
        public static String HandleUserInputString()
        {
            string input = Console.ReadLine();
            if (Equals(input, ""))
            {
                return "";  // Was a null value
            }
            return input;
        }

        // Simply prompt the user to press enter to continue and wait for enter
        public static void EnterToContinue()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine(); // Will pause until user presses enter
        }

        // Handle prompting the user with a Yes or No option dialog
        // *Note: Y is underlined, which is the defualt, so if there is no user input it returns TRUE
        public static bool HandleYNPrompt(String message = "")
        {
            Console.Write((message == "" ? "Do you want to continue?" : message) + " (" + UNDERLINE + "Y" + RESET + "/N): ");
            String ynChoice = Console.ReadLine();

            return (ynChoice.ToUpper() == "Y" | ynChoice.ToUpper() == "");
        }
    }
}
