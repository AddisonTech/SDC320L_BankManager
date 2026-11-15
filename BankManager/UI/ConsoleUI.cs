/*
 * Name:    Addison Smith (addsmi1720)
 * Date:    11/15/2026
 * Course:  SDC320L Advanced Object-Oriented Programming Using C# Lab
 * Project: Cedar Ledger Bank Account Manager - Week 1
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
            Center("SDC320L COURSE PROJECT  |  WEEK 1  |  Inheritance, Composition, and User Interaction");
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
            Console.WriteLine("    - Every screen returns you to this menu when you're done.");
            Console.WriteLine("    - Type Q (or Quit / Exit) at the menu to close the application.");
            Console.WriteLine();
        }

        public static void ShowMainMenu()
        {
            Line('-');
            Console.WriteLine("  MAIN MENU");
            Line('-');
            Console.WriteLine("    [1]  View all accounts for all customers");
            Console.WriteLine("    [2]  View accounts for one customer");
            Console.WriteLine("    [3]  View customer directory");
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
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ! {message}");
            Console.ForegroundColor = previous;
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
    }
}
