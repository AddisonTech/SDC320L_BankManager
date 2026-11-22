/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: InputHelper reads and validates everything the user types. It
 *          keeps asking until it gets a usable value, so the rest of the
 *          program never has to deal with bad input. Blank input on the
 *          number and amount prompts means "cancel."
 */

using System.Globalization;

namespace BankManager.UI
{
    public static class InputHelper
    {
        private static readonly string[] QuitWords = { "Q", "QUIT", "EXIT" };
        private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

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

        // Reads a whole number between min and max. Blank input returns null.
        public static int? ReadIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                int? value = ReadInt($"{prompt} ({min}-{max})");
                if (value == null || (value >= min && value <= max))
                {
                    return value;
                }

                ConsoleUI.ShowError($"Please pick a number from {min} to {max}.");
            }
        }

        // Reads a dollar amount greater than zero with at most two decimal
        // places. Accepts "250", "250.00", or "$250.00". Blank returns null.
        public static decimal? ReadAmount(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt} (or press Enter to cancel): $");
                string input = (Console.ReadLine() ?? string.Empty).Trim().TrimStart('$').Replace(",", "");

                if (input.Length == 0)
                {
                    return null;
                }

                if (!decimal.TryParse(input, NumberStyles.Number, Usd, out decimal amount))
                {
                    ConsoleUI.ShowError("Please enter a dollar amount, like 250 or 250.00.");
                }
                else if (amount <= 0)
                {
                    ConsoleUI.ShowError("The amount must be greater than zero.");
                }
                else if (decimal.Round(amount, 2) != amount)
                {
                    ConsoleUI.ShowError("Amounts can't go past cents (two decimal places).");
                }
                else
                {
                    return amount;
                }
            }
        }

        // Y/N confirmation. Keeps asking until it gets a Y or an N.
        public static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"  {prompt} (Y/N): ");
                string input = (Console.ReadLine() ?? string.Empty).Trim().ToUpperInvariant();

                if (input == "Y" || input == "YES")
                {
                    return true;
                }
                if (input == "N" || input == "NO")
                {
                    return false;
                }

                ConsoleUI.ShowError("Please answer Y or N.");
            }
        }
    }
}
