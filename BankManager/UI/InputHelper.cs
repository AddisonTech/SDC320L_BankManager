/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
 *
 * Purpose: InputHelper reads and validates everything the user types. It
 *          keeps asking until it gets a usable value, so the rest of the
 *          program never has to deal with bad input. Later weeks add
 *          decimal (dollar amount) and text prompts here.
 */

namespace BankManager.UI
{
    public static class InputHelper
    {
        private static readonly string[] QuitWords = { "Q", "QUIT", "EXIT" };

        // Returns the menu choice in upper case, or "Q" for any quit word
        public static string ReadMenuChoice(string prompt, params string[] validChoices)
        {
            while (true)
            {
                Console.Write($"  {prompt}: ");
                string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpperInvariant();

                if (QuitWords.Contains(input))
                {
                    return "Q";
                }

                if (validChoices.Contains(input))
                {
                    return input;
                }

                ConsoleUI.ShowError($"\"{input}\" is not an option. Choose {string.Join(", ", validChoices)}, or Q to quit.");
            }
        }

        // Reads a whole number. Blank input returns null so the caller can cancel.
        public static int? ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt} (or press Enter to cancel): ");
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (input.Length == 0)
                {
                    return null;
                }

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                ConsoleUI.ShowError("Please enter a whole number.");
            }
        }
    }
}
