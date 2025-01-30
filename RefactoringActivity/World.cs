namespace RefactoringActivity;

public class World
{
    public Dictionary<string, Location> Locations;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        Location start = new("Start", "You are at the starting point of your adventure.");
        Location forest = new("Forest", "You are in a dense, dark forest.");
        Location cave = new("Cave", "You see a dark, ominous cave.");

        start.Exits.Add("north", "Forest");
        forest.Exits.Add("south", "Start");
        forest.Exits.Add("east", "Cave");
        cave.Exits.Add("west", "Forest");

        start.Items.Add("map");
        forest.Items.Add("key");
        forest.Items.Add("potion");
        cave.Items.Add("sword");

        start.Puzzles.Add(new Puzzle("riddle",
            "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));

        Locations.Add("Start", start);
        Locations.Add("Forest", forest);
        Locations.Add("Cave", cave);
    }

    public bool MovePlayer(Player player, string direction)
    {
        if (Locations[player.CurrentLocation].Exits.ContainsKey(direction))
        {
            player.CurrentLocation = Locations[player.CurrentLocation].Exits[direction];
            return true;
        }

        return false;
    }

    public string GetLocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].Description;
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.Description;
        
        details += GetLocationExits(location);
        details += GetLocationItems(location);

        details += GetLocationPuzzles(location);

        return details;
    }

    private static string GetLocationPuzzles(Location location)
    {
        String puzzles = "";
        if (location.Puzzles.Count > 0)
        {
            puzzles += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.Puzzles) 
                puzzles += $"\n- {puzzle.Name}";
        }

        return puzzles;
    }

    private static string GetLocationItems(Location location)
    {
        String items = "";
        if (location.Items.Count > 0)
        {
            items += "\nYou see the following items:";
            foreach (string item in location.Items) 
                items += $"\n- {item}";
        }

        return items;
    }

    private static string GetLocationExits(Location location)
    {
        String exits = "";
        if (location.Exits.Count > 0)
        {
            exits += " Exits lead: ";
            foreach (string exit in location.Exits.Keys)
                exits += exit + ", ";
            exits = exits.Substring(0, exits.Length - 2);
        }

        return exits;
    }

    public bool TakeItem(Player player, string itemName)
    {
        Location location = Locations[player.CurrentLocation];
        if (location.Items.Contains(itemName))
        {
            location.Items.Remove(itemName);
            player.Inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }

        return false;
    }

    public bool UseItem(Player player, string itemName)
    {
        if (player.Inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                UsePotion(player);
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            player.Inventory.Remove(itemName);
            return true;
        }

        return false;
    }

    private static void UsePotion(Player player)
    {
        Console.WriteLine("Ouch! That tasted like poison!");
        player.Health -= 10;
        Console.WriteLine($"Your health is now {player.Health}.");
    }

    public bool SolvePuzzle(Player player, string puzzleName)
    {
        Location location = Locations[player.CurrentLocation];
        Puzzle? puzzle = location.Puzzles.Find(p => p.Name == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.Puzzles.Remove(puzzle);
            return true;
        }

        return false;
    }
}