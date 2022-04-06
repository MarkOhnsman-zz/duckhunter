using duckhunter.models;

namespace duckhunter.services
{
  public class GameService
  {
    private bool playing = true;

    private Room currentRoom;

    private List<Item> inventory = new List<Item>();

    public void Play()
    {
      // start game
      while (playing)
      {
        // get user input
        Console.Write("What do you want to do: ");
        HandleUserInput(Console.ReadLine());
      }
      Console.WriteLine("Goodbye");
    }

    public void HandleUserInput(string input)
    {
      List<string> inputs = input.Split(' ').ToList();
      string action = inputs[0].ToLower();
      string details = "";
      if (inputs.Count > 1)
      {
        inputs.RemoveAt(0);
        details = string.Join(' ', inputs);
      }
      switch (action)
      {
        case "quit":
          playing = false;
          break;
        case "look":
          Look();
          break;
        case "inventory":
          Inventory();
          break;
        case "go":
          if (details.Length == 0)
          {
            System.Console.WriteLine("You must provide a direction");
            break;
          }
          Go(details);
          break;
        case "take":
          if (details.Length == 0)
          {
            System.Console.WriteLine("You must provide an item name");
            break;
          }
          Take(details);
          break;
        default:
          System.Console.WriteLine("Command not recognized");
          break;
      }
    }


    public void Look()
    {
      Console.WriteLine("As you look around you see:");
      Console.WriteLine(currentRoom.Description);
      if (currentRoom.Items.Count > 0)
      {
        System.Console.WriteLine("\nIn the room you see: ");
        foreach (var item in currentRoom.Items)
        {
          System.Console.WriteLine(item.Value.Name);
        }
      }
    }

    public void Go(string direction)
    {
      if (currentRoom.Exits.ContainsKey(direction))
      {
        currentRoom = currentRoom.Exits[direction];
        Look();
      }
      else
      {
        System.Console.WriteLine("You hit your head on the wall, You Can't Go That Way!");
      }
    }

    public void Take(string name)
    {
      if (currentRoom.Items.ContainsKey(name))
      {
        inventory.Add(currentRoom.Items[name]);
        currentRoom.Items.Remove(name);
        System.Console.WriteLine("You take the " + name);
        Look();
      }
      else
      {
        System.Console.WriteLine("You hit your head on the wall, You Can't Go That Way!");
      }
    }


    public void Inventory()
    {
      inventory.ForEach(i =>
      {
        Console.WriteLine(i.Name);
      });
    }

    public GameService()
    {
      Room room1 = new Room("You apear to be outside of an elevator, it is broken, it smells of burnt plastic");
      Room room2 = new Room("It is dark and creepy, there is a body on the floor, they seem to have failed this checkpoint");
      room1.AddRoom("north", room2);
      room2.AddRoom("south", room1);
      room2.Items.Add("duck", new Item("THE GOLDEN DUCK!!!"));
      currentRoom = room1;
    }

  }
}