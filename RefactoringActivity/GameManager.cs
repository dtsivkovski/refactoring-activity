namespace RefactoringActivity;

public class GameManager
{
    public bool IsRunning;
    public Player Player;
    public World World;
    
    private enum Commands
    {
        Move,
        Take,
        Use,
        Solve,
        Unknown,
    }

    public void RunGame()
    {
        IsRunning = true;
        Player = new Player(100);
        World = new World();

        Console.WriteLine("Welcome to the Text Adventure Game!");
        Console.WriteLine("Type 'help' for a list of commands.");

        while (IsRunning)
        {
            Console.WriteLine();
            Console.WriteLine(World.GetLocationDetails(Player.CurrentLocation));
            Console.Write("> ");
            string input = Console.ReadLine()?.ToLower();
            if (string.IsNullOrEmpty(input)) 
                return;

            if (input == "help")
            {
                PrintAvailableCommands();
            }
            else if (input.StartsWith("go"))
            {
                ExecuteMoveCommand(input);
            }
            else if (input.StartsWith("take"))
            {
                ExecuteTakeCommand(input);
            }
            else if (input.StartsWith("use"))
            {
                ExecuteUseCommand(input);
            }
            else if (input == "inventory")
            {
                Player.ShowInventory();
            }
            else if (input.StartsWith("solve"))
            {
                ExecuteSolveCommand(input);
            }
            else if (input == "quit")
            {
                IsRunning = false;
                Console.WriteLine("Thanks for playing!");
            }
            else
            {
                ClarifyPlayerCommand(Commands.Unknown);
            }
        }
    }

    private void ExecuteMoveCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string direction = parts[1];
            if (World.MovePlayer(Player, direction))
            {
                Console.WriteLine($"You move {direction}.");
            }
            else
            {
                ClarifyPlayerCommand(Commands.Move);
            }
        }
        else
        {
            Console.WriteLine("Move where? (north, south, east, west)");
        }
    }

    private void ExecuteTakeCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
            if (!World.TakeItem(Player, itemName))
            {
                Console.WriteLine($"There is no {itemName} here.");
            }
        }
        else
        {
            ClarifyPlayerCommand(Commands.Take);
        }
    }

    private void ExecuteUseCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
            if (!World.UseItem(Player, itemName))
            {
                Console.WriteLine($"You can't use the {itemName} here.");
            }
        }
        else
        {
            ClarifyPlayerCommand(Commands.Use);
        }
    }

    private void ExecuteSolveCommand(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string puzzleName = parts[1];
            if (World.SolvePuzzle(Player, puzzleName))
            {
                Console.WriteLine($"You solved the {puzzleName} puzzle!");
            }
            else
            {
                Console.WriteLine($"That's not the right solution for the {puzzleName} puzzle.");
            }
        }
        else
        {
            ClarifyPlayerCommand(Commands.Solve);
        }
    }

    private static void PrintAvailableCommands()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("- go [direction]: Move in a direction (north, south, east, west).");
        Console.WriteLine("- take [item]: Take an item from your current location.");
        Console.WriteLine("- use [item]: Use an item in your inventory.");
        Console.WriteLine("- solve [puzzle]: Solve a puzzle in your current location.");
        Console.WriteLine("- inventory: View the items in your inventory.");
        Console.WriteLine("- quit: Exit the game.");
    }

    private static void ClarifyPlayerCommand(Commands commandType)
    {
        if (commandType == Commands.Move)
        {
            Console.WriteLine("Move where? (north, south, east, west)");
        }
        else if (commandType == Commands.Take)
        {
            Console.WriteLine("Take what?");
        }
        else if (commandType == Commands.Use)
        {
            Console.WriteLine("Use what?");
        }
        else if (commandType == Commands.Solve)
        {
            Console.WriteLine("Solve what?");
        }
        else if (commandType == Commands.Unknown)
        {
            Console.WriteLine("Unknown command. Try 'help'.");
        }
    }
}