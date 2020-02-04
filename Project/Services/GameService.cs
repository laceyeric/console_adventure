using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using ConsoleAdventure.Project.Controllers;

namespace ConsoleAdventure.Project.Services
{
  public class GameService : IGameService
  {
    private IGame _game { get; set; }
    public bool Playing
    {
      get
      {
        return _game.Playing;
      }
      set { }
    }

    public List<string> Messages { get; set; }

    public GameService()
    {
      _game = new Game();
      _game.Playing = true;
      Messages = new List<string>();
    }
    //travel method
    public void Go(string direction)
    {
      //check does room have an exit in given direction from user input
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        if (_game.CurrentRoom.Exits[direction].IsLocked)
        {
          Messages.Add($"You approach the {_game.CurrentRoom.Exits[direction].Name}. {_game.CurrentRoom.Exits[direction].Description}");
          return;
        }
        Console.Clear();
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        Messages.Add($"You make your way to the {_game.CurrentRoom.Name}. ");
        //check if player is disguised
        if (!(_game.CurrentRoom is PublicSpace) && !_game.CurrentPlayer.WearingUniform)
        {
          //generic death message if not disguised
          Messages.Add("A guard notices you enter the space! Guard: 'Stop right there! Intruder!!' As you turn to run, the enemy troops swarm you and end your fight.");
          return;
        }
        Messages.Add(_game.CurrentRoom.Description);
        return;
      }
      Messages.Add("There doesn't seem to be anything in that direction.");
      return;
    }

    //set up current player starting data
    public void AssignPlayer(string playerName)
    {
      _game.CurrentPlayer.Name = playerName;
    }

    public void PrintIntro()
    {
      Messages.Add("You have the following options available: Press 'Y' for Yes, 'N' for No to answer questions. Press 'Q' to quit the game.  Type 'help' to see all available game commands.");
      Messages.Add(" ");
      Messages.Add("Chief: 'Brave Young Warrior our forces are failing and the enemy grows stronger everyday. I fear if we don't act now our people will be driven from their homes. These dark times have left us with one final course of action. We must cut the head off the snake by assasinating the Dark Lord of Grimtol... Our sources have identified a small tunnel that leads into the rear of the castle.  Will you put your life at risk to save our people?'");
    }

    public void PrintIntro2()
    {
      Messages.Add("Chief: 'Thank you, brave one.  Truly you are our last hope. 'Go east' to enter the hidden tunnel. Be careful and come back victorious!'");
    }
    public void Help()
    {
      Messages.Add("You have the following commands available:");
      Messages.Add("Type 'go' followed by a direction (north, east, south or west) to progress to a different area. EX: 'go east'.");
      Messages.Add("Type 'use' followed by an item name to attempt to use an item. EX: 'use blue towel'.");
      Messages.Add("type 'take' followed by an item name to attempt to place it in your inventory. EX: 'take blue towel'.");
      Messages.Add("Type 'inv' to view your player's inventory.");
      Messages.Add("Type 'look' to see the current area's description again.");
      Messages.Add("Type 'help' to see all available game commands.");
      Messages.Add("Press 'Q' to quit the game.");
    }

    public void Inventory()
    {
      foreach (Item i in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{i.Name} -- {i.Description}");
      }
    }

    public void Look()
    {
      Messages.Add(_game.CurrentRoom.Description);
      Messages.Add("Potentially useful things here:");
      foreach (Item i in _game.CurrentRoom.Items)
      {
        Messages.Add($"{i.Name} -- {i.Description}");
      }
    }

    public void Quit()
    {
    }
    public void Death()
    {
      Messages.Add("You have died.");
      _game.Playing = false;
    }

    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      Messages.Add("Let's try again!");
      _game.Playing = false;
    }

    public void Setup(string playerName)
    {
      throw new System.NotImplementedException();
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      var grabbedItem = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (_game.CurrentRoom.Items.Contains(grabbedItem))
      {
        _game.CurrentPlayer.Inventory.Add(grabbedItem);
        _game.CurrentRoom.Items.RemoveAll(i => i.Name.ToLower() == grabbedItem.Name.ToLower());
        Messages.Add($"You have successfully taken the {grabbedItem.Name}.");
        return;
      }
      Messages.Add($"Failed to find anything useful called {itemName}.");
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {

      var usedItem = _game.CurrentPlayer.Inventory.Find(p => p.Name.ToLower() == itemName);
      if (usedItem == null)
      {
        usedItem = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      }
      if (usedItem == null)
      {
        Messages.Add($"Could not find a useful item called {itemName}.");
        return;
      }
      //unique environment changes for particular items
      //uniform
      if (usedItem.Name.ToLower() == "guard uniform")
      {
        _game.CurrentPlayer.WearingUniform = !_game.CurrentPlayer.WearingUniform;
        if (_game.CurrentPlayer.WearingUniform == false)
        {
          Messages.Add($"Having taken off the uniform, you are no longer disguised!");
        }
        Messages.Add($"Wearing the found uniform, you are now disguised as a local guard");
      }
      // bed
      if (usedItem.Name.ToLower() == "bed" && _game.CurrentPlayer.WearingUniform == false)
      {
        Messages.Add("You hear footsteps approaching the door from the hallway, so you lay in the empty cot, pull the covers up to your chin, and pretend to sleep. Guard: 'It's your turn for watch... Hey, what are you doing in here!? Quick Jenkins, seize him!' You try to scramble to feet to escape the room. Jenkins rushes you with the over-zealous energy of a new recruit and instinctively swings his sword down upon you. You crumple to the floor. The last sound ringing in your ear is of your family pendant bouncing on the cold stone floor that receives your lifeblood.");
        Messages.Add("You have lost the game. Your village will surely be slaughtered by dawn for your attempted treachery.");
        Quit(); //need to flip _playing bool in controller somehow
        return;
      }
      // silver key
      if (usedItem.Name.ToLower() == "silver key" && _game.CurrentRoom.Name.ToLower() == "castle courtyard" && !usedItem.hasBeenUsed)
      {
        Messages.Add($"You've used the silver key and unlocked the door to the War Room.");
        _game.CurrentRoom = _game.CurrentRoom.Exits["east"];
        System.Console.WriteLine(_game.CurrentRoom.Name);
        _game.CurrentRoom.IsLocked = !_game.CurrentRoom.IsLocked;
        usedItem.hasBeenUsed = true;
        _game.CurrentRoom.Description = _game.CurrentRoom.Usables[usedItem];
        Messages.Add(_game.CurrentRoom.Description);
        return;
      }
      // vial
      if (usedItem.Name.ToLower() == "vial" && _game.CurrentRoom.Name.ToLower() == "war room" && !usedItem.hasBeenUsed)
      {
        usedItem.hasBeenUsed = true;
        _game.CurrentRoom.Description = _game.CurrentRoom.Usables[usedItem];
        Messages.Add($"You have used the {usedItem.Name}");
        Messages.Add(_game.CurrentRoom.Description);
        Messages.Add("After discarding the evidence you begin to walk confidently toward the War Room door when you hear a clamor of footsteps and brushing cloth approaching from the other side.  You've got to get out of sight fast!");
        return;
      }
      // window
      if (usedItem.Name.ToLower() == "window" && _game.CurrentRoom.Name.ToLower() == "war room")
      {
        usedItem.hasBeenUsed = true;
        _game.CurrentRoom.Description = _game.CurrentRoom.Usables[usedItem];
        Messages.Add($"You have used the {usedItem.Name}");
        Messages.Add(_game.CurrentRoom.Description);
        VictoryTrigger();
        return;
      }
      usedItem.hasBeenUsed = true;
      _game.CurrentRoom.Description = _game.CurrentRoom.Usables[usedItem];
      Messages.Add($"You have used the {usedItem.Name}");
      Messages.Add(_game.CurrentRoom.Description);

      //check room for usables
      //NO USE
      // for (int i = 0; i < _game.CurrentPlayer.Inventory.Count; i++)
      // {
      //   var item = _game.CurrentPlayer.Inventory[i];

    }
    //at the bottom because, no spoilers.
    public void VictoryTrigger()
    {
      Messages.Add("");
      Messages.Add("Hoisting yourself back up into the room, you make your way across the body-strewn floor to the door.  You peer into the eyes of the Dark Lord and confirm he is gone from this earth.  You exit the War Room, cross the courtyard, and slip into the tunnel opening.  An enormous blow has been landed against your foe!  Tonight you will tell your village leaders and gather forces to scatter the remaining soldiers from the keep in the morning, taking advantage of the confusion!  A glorious victory for you and your people!");
      Messages.Add("CONGRATULATIONS!! YOU WON!");
      _game.Playing = false;
    }
  }
}
