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

    public List<string> Messages { get; set; }

    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
    }
    public void Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        Console.Clear();
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        Messages.Add($"You make your way to the {_game.CurrentRoom.Name}. ");
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
      Messages.Add("Chief: 'Thank you, brave one.  Truly you are our last hope. Go east to enter the hidden tunnel. Be careful and come back victorious!'");
    }
    public void Help()
    {
      Messages.Add("You have the following commands available:");
      Messages.Add("Type 'go' followed by a direction (north, east, south or west) to progress to a different area. EX: 'go east'.");
      Messages.Add("Type 'use' followed by an item name to attempt to use an item. EX: 'use towel'.");
      Messages.Add("type 'take' followed by an item name to attempt to place it in your inventory. EX: 'take towel'.");
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
    }

    public void Quit()
    {
      Messages.Add("You have chosen to give up your quest.");
    }

    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
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
      // is the item typed in either room or player inventory?
      var playerItem = _game.CurrentPlayer.Inventory.Find(p => p.Name == itemName);
      var roomItem = _game.CurrentRoom.Items.Find(i => i.Name == itemName);
      if (itemName == "guard uniform" || itemName == "Guard Uniform")
      {
        Messages.Add("You grab one of the uniforms nearby to better disguise yourself.");
      }

      // handle non matches of either list first
      // if (playerItem.Name.ToLower() != itemName || roomItem.Name.ToLower() != itemName)
      // {
      // Messages.Add($"Could not find an item to use called {itemName}.");
      // }
      // else if (itemName == playerItem.Name.ToLower())
      // {
      //   switch (playerItem)
      //   {

      //   }
      // }
    }
  }
}