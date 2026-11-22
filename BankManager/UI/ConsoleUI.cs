/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/22/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 2
 *
 * Purpose: ConsoleUI handles every fixed message the user sees: the
 *          project header, the welcome and instructions, the main menu,
 *          and status messages. Keeping it in one class means the screens
 *          look the same everywhere and the final project can reuse it.
 */

namespace BankManager.UI
{
    public static class ConsoleUI
    {
        public const int Width = 100;

        public static void ShowHeader()
        {
            Line('=');
            Center("SDC320L COURSE PROJECT  |  WEEK 2  |  Software Design, Interfaces, and Polymorphism");
            Center("Cedar Ledger Bank Account Manager");
            Center("Developed by Addison Smith (addsmi1720)");
            Line('=');
        }

        public static void ShowWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("  Welcome to Cedar Ledger Bank's account manager.");
            Console.WriteLine("  \"A penny saved is a penny earned. A penny logged is a penny you can prove.\"");
            Console.WriteLine();
            Console.WriteLine("  How it works:");
            Console.WriteLine("    - Pick an option from the menu by typing its number and pressing Enter.");
            Console.WriteLine("    - Deposits and withdrawals ask you to pick a customer, then an account.");
            Console.WriteLine("    - Each account type follows its own withdrawal rules, and you will see");
            Console.WriteLine("      any fee or penalty before you confirm.");
            Console.WriteLine("    - Press Enter on a blank prompt to cancel and return to the menu.");
            Console.WriteLine("    - Type Q (or Quit / Exit) at the menu to close the application.");
            Console.WriteLine();
        }

        public static void ShowMainMenu()
        {
            Line('-');
            Console.WriteLine("  MAIN MENU");
            Line('-');
            Console.WriteLine("    VIEW");
            Console.WriteLine("    [1]  View all accounts for all customers");
            Console.WriteLine("    [2]  View accounts for one customer");
            Console.WriteLine("    [3]  View customer directory");
            Console.WriteLine("    TRANSACTIONS");
            Console.WriteLine("    [4]  Make a deposit");
            Console.WriteLine("    [5]  Make a withdrawal");
            Console.WriteLine("    [6]  Month-end interest preview");
            Console.WriteLine();
            Console.WriteLine("    [Q]  Quit");
            Line('-');
        }

        public static void ShowSectionTitle(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"  {title.ToUpperInvariant()}");
            Line('-');
        }

        public static void ShowError(string message)
        {
            WriteColor($"  ! {message}", ConsoleColor.Red);
        }

        public static void ShowWarning(string message)
        {
            WriteColor($"  * {message}", ConsoleColor.Yellow);
        }

        public static void ShowSuccess(string message)
        {
            WriteColor($"  + {message}", ConsoleColor.Green);
        }

        public static void ShowGoodbye()
        {
            Console.WriteLine();
            Line('=');
            Center("Thanks for banking with Cedar Ledger. Session closed.");
            Line('=');
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("  Press Enter to return to the menu...");
            Console.ReadLine();
        }

        // Clears the screen when a real console is attached. Redirected output
        // (tests, piping) throws on Clear, so that case is skipped.
        public static void ClearScreen()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }

        public static void Line(char c)
        {
            Console.WriteLine(new string(c, Width));
        }

        private static void Center(string text)
        {
            int pad = Math.Max(0, (Width - text.Length) / 2);
            Console.WriteLine(new string(' ', pad) + text);
        }

        private static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = previous;
        }
    }
}
