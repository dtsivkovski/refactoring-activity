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
        Location start = Location.NewDefaultLocation(Location.DefaultLocations.Start);
        Location forest = Location.NewDefaultLocation(Location.DefaultLocations.Forest);
        Location cave = Location.NewDefaultLocation(Location.DefaultLocations.Cave);

        Locations.Add(start.Name, start);
        Locations.Add(forest.Name, forest);
        Locations.Add(cave.Name, cave);
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