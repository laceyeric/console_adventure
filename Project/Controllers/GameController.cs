using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();
    private bool _playing = true;
    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      System.Console.WriteLine("What is your name?");
      string playerName = Console.ReadLine();
      System.Console.WriteLine($"Welcome to the game, {playerName}. ");
      _gameService.PrintIntro();

      // looping game
      while (_playing)
      {
        PrintMessages();
        GetUserInput();
      }
      Console.Clear();
      System.Console.WriteLine("End of Game");
    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"

      switch (command)
      {
        case "y":
        case "yes":
          _gameService.PrintIntro2();
          PrintMessages();
          break;
        case "n":
        case "no":
        case "q":
        case "quit":
          _playing = false;
          //   _gameService.Quit();
          break;
        default:
          break;
      }
    }

    //NOTE this should print your messages for the game.
    public void PrintMessages()
    {
      foreach (string message in _gameService.Messages)
      {
        System.Console.WriteLine(message);
      }
      _gameService.Messages.Clear();
    }

  }
}