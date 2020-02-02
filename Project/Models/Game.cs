using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      // rooms initiated
      var village = new Room("Village", "You race back to your village to update them on your mission");

      var tunnel = new Room("Hidden Tunnel", "The dark tunnel discovered by your village leaders is tight and filthy. You have come to a grated on the eastern wall where light fights its way into the tunnel.  You listen for any noise on the opposite side, but all seems quiet.");

      var hallway1 = new Room("Western Hallway", "You find yourself in a small hall stretching to the north and south.  There doesn't appear to be anything of interest here. The eastern wall has a larger arched opening leading into what appears to be a courtyard");

      var barracks = new Room("Barracks", "You see a room with several sleeping guards. The room smells of sweaty men.  The bed closest to you is empty and there are several uniforms tossed about.");

      var captQuarters = new Room("Captain's Quarters", "As you approach the Captain's quarters you swallow hard and notice your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out upon them.");

      var courtyard = new Room("Castle Courtyard", "You step into the large courtyard located in the middle of the castle grounds.  At its center is a flowing fountain casting moonlight in all directions.  A few guards patrol the area.");

      var guardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and observe that it is empty.  There are a few small tools in the corner and a chair propped against the wall near a stairwell that likely leads to the dungeon.");

      var hallway2 = new Room("Souther Hallway", "You find yourself in a small hall stretching to the east and west.  There doesn't appear to be anything of interest here. The northern wall has a larger arched opening leading into what appears to be a courtyard");

      var dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon you notice a harsh chill to the air.  Landing at the base of the stairs you see the remains of a prisoner that will never find freedom.");

      var squireTower = new Room("Squire's Tower", "As you finish climbing the stairs to the squire tower you see a messenger nestled in his bed. His messenger overcoat is hanging from his bed post.");

      var warRoom = new Room("War Room", "Stepping into the war room you see several maps spread across tables. On the maps many of the villages have been marked for purification.  You also notice on one side of the tables that several dishes of prepared food have been set out. Perhaps the war council will be meeting soon...");

      var throneRoom = new Room("Throne Room", "As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on his throne you see the Dark Lord.  The Dark Lord shouts at you demanding why you dared to interrupt him during his Ritual of Evil Summoning... Dumbfounded, you mutter an incoherent response. Becoming more enraged the Dark Lord complains that you just ruined his concentration and he will now have to start the ritual over.  Quickly striding towards you he smirks at you.  'At least I know I have a sacrificial volunteer.' he whispers to you, plunging his jewel encrusted dagger into your heart as your world slowly fades away...");

      // create room exits
      village.Exits.Add("east", tunnel);
      tunnel.Exits.Add("west", village);
      tunnel.Exits.Add("east", hallway1);
      hallway1.Exits.Add("north", barracks);
      hallway1.Exits.Add("south", captQuarters);
      hallway1.Exits.Add("east", courtyard);
      hallway1.Exits.Add("west", tunnel);
      hallway2.Exits.Add("west", captQuarters);
      hallway2.Exits.Add("east", guardRoom);
      hallway2.Exits.Add("north", courtyard);
      barracks.Exits.Add("south", hallway1);
      captQuarters.Exits.Add("north", hallway1);
      captQuarters.Exits.Add("east", hallway2);
      guardRoom.Exits.Add("west", hallway2);
      guardRoom.Exits.Add("north", dungeon);
      dungeon.Exits.Add("south", guardRoom);
      courtyard.Exits.Add("west", hallway1);
      courtyard.Exits.Add("south", hallway2);
      courtyard.Exits.Add("north", throneRoom);
      courtyard.Exits.Add("east", warRoom);
      throneRoom.Exits.Add("south", courtyard);
      squireTower.Exits.Add("west", warRoom);
      warRoom.Exits.Add("east", squireTower);
      warRoom.Exits.Add("west", courtyard);

      // create items
      var silverKey = new Item("Silver Key", "A key silver in color. The guard Captain placed it while speaking of prisoners...");
      var note = new Item("Capt Note", "A note from the guard Captain to be delivered to the gate Captain.  His penmanship is surprisingly elegant. He closed the message with 'xoxo'..?");
      var vial = new Item("vial", "A vial of green liquid taken from a pouch in the Captain's quarters.");
      var guardUniform = new Item("Guard Uniform", " A dull uniform made from cheap materials.  These are issued to all the standard castle guards.");
      var hammer = new Item("Hammer", "A standard hammer of decent quality");
      var brokenLock = new Item("Broken Lock", "A prisoner shackle lock that no longer functions");
      var overcoat = new Item("Messenger Overcoat", "A long, heavy overcoat meant to keep the entire body warm");
      var window = new Item("Window", "A small opening looking out to the eastern gardens");
      var bed = new Item("Bed", "An open bed used by guards in the barracks. Of poor quality but good for a bit of shuteye between long shifts.");

      // add items to rooms
      captQuarters.Items.Add(silverKey);
      captQuarters.Items.Add(note);
      captQuarters.Items.Add(vial);
      barracks.Items.Add(guardUniform);
      barracks.Items.Add(bed);
      dungeon.Items.Add(brokenLock);
      guardRoom.Items.Add(hammer);
      squireTower.Items.Add(overcoat);
      warRoom.Items.Add(window);


    }
  }
}