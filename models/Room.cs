namespace duckhunter.models
{
  public class Room
  {
    public string Description { get; private set; }
    public Dictionary<string, Room> Exits { get; private set; }
    public Dictionary<string, Item> Items { get; set; }

    public Dictionary<Item, KeyValuePair<string, Room>> LockedRooms { get; set; }


    public void AddRoom(string direction, Room room)
    {
      Exits.Add(direction, room);
    }

    public Room(string description)
    {
      Exits = new Dictionary<string, Room>();
      Items = new Dictionary<string, Item>();
      Description = description;
    }

  }
}