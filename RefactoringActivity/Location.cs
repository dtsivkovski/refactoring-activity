namespace RefactoringActivity;

public class Location
{
    public string Name;
    public string Description;
    public Dictionary<string, string> Exits;
    public List<string> Items;
    public List<Puzzle> Puzzles;

    public enum DefaultLocations
    {
        Start,
        Forest,
        Cave
    }
    
    public Location(string name, string description)
    {
        Name = name;
        Description = description;
        Exits = new Dictionary<string, string>();
        Items = new List<string>();
        Puzzles = new List<Puzzle>();
    }

    public static Location NewDefaultLocation(DefaultLocations loc)
    {
        Location location;
        if (loc == DefaultLocations.Start)
        {
            location = new("Start", "You are at the starting point of your adventure.");
            location.Exits.Add("north", "Forest");
            location.Items.Add("map");
            location.Puzzles.Add(new Puzzle("riddle",
                "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));
        }
        else if (loc == DefaultLocations.Forest)
        {
            location = new("Forest", "You are in a dense, dark forest.");
            location.Exits.Add("south", "Start");
            location.Exits.Add("east", "Cave");
            location.Items.Add("key");
            location.Items.Add("potion");
        }
        else if (loc == DefaultLocations.Cave)
        {
            location = new("Cave", "You see a dark, ominous cave.");
            location.Exits.Add("west", "Forest");
            location.Items.Add("sword");
        }
        else
        {
            location = new("Unknown", "Unknown Location");
        }

        return location;
    }
}