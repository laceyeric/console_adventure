namespace ConsoleAdventure.Project.Models
{
  class LockedRoom : Room
  {

    public LockedRoom(string name, string description) : base(name, description)
    {
      IsLocked = true;
    }
  }
}