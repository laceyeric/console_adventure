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
      var village = new PublicSpace("Village", "You race back to your village to update them on your mission");

      var tunnel = new PublicSpace("Hidden Tunnel", "The dark tunnel discovered by your village leaders is tight and filthy. You have come to a grated plate on the eastern wall where light fights its way into the tunnel.  You listen for any noise within the castle, but all seems quiet.");

      var hallway1 = new PublicSpace("Western Hallway", "You find yourself in a small hall leading to the north and south.  There doesn't appear to be anything of interest within the hall itself. The eastern wall has a large arched opening leading into what appears to be a courtyard");

      var barracks = new Room("Barracks", "You see a room with several sleeping guards. The room smells of sweaty men.  The 'bed' closest to you is empty and there are several 'guard uniform's tossed about.");

      var captQuarters = new Room("Captain's Quarters", "As you approach the Captain's quarters you swallow hard and notice your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out upon them.");

      var courtyard = new Room("Castle Courtyard", "You step into the large courtyard located in the middle of the castle grounds.  At its center is a flowing fountain casting moonlight in all directions.  A few guards patrol the area.");

      var guardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and observe that it is empty.  There are a few small 'tool's in the corner and a chair propped against the wall near a stairwell that likely leads to the dungeon.");

      var hallway2 = new Room("Southern Hallway", "You find yourself in a small hall stretching toward entrances at the east and west ends.  There doesn't appear to be anything of interest within the hallway itself. The northern wall has a large arched opening leading into what appears to be a courtyard");

      var dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon you notice a harsh chill to the air.  Landing at the base of the stairs you see the remains of a prisoner that will never find freedom.");

      var squireTower = new Room("Squire's Tower", "As you finish climbing the stairs to the squire tower you see a messenger nestled in his bed. His messenger 'overcoat' is hanging from his bed post.");

      var warRoom = new Room("War Room", "Stepping into the war room you see several maps strewn across a large central table by the cool night breeze coming through a nearby 'window'. On the maps many of the villages have been marked for purification.  You also notice on one side of the table several dishes of prepared food have been set out. Perhaps the war council will be meeting soon...");

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

      // set beginning room
      CurrentRoom = village;

      // create items
      var silverKey = new Item("Silver Key", "A key silver in color. The guard Captain placed it while speaking of prisoners...");
      var note = new Item("Note", "A note from the guard Captain to be delivered to the gate Captain.  His penmanship is surprisingly elegant. He closed the message with 'xoxo'..?");
      var vial = new Item("Vial", "A vial of green liquid taken from a pouch in the Captain's quarters.");
      var uniform = new PlayerItem("Guard Uniform", " A dull uniform made from cheap materials.  These are issued to all the standard castle guards.");
      var hammer = new Item("Tool", "A standard hammer of decent quality");
      var brokenLock = new Item("Broken Lock", "A prisoner shackle lock that no longer functions");
      var overcoat = new Item("Overcoat", "A long, heavy overcoat meant to keep the entire body warm");
      var window = new Item("Window", "A small opening looking out to the eastern gardens");
      var bed = new Item("Bed", "An open bed used by guards in the barracks. Of poor quality but good for a bit of shuteye between long shifts.");
      var pendant = new Item("pendant", "A family heirloom passed down from your village ancestors.");

      // add items to rooms
      captQuarters.Items.Add(silverKey);
      captQuarters.Items.Add(note);
      captQuarters.Items.Add(vial);
      barracks.Items.Add(uniform);
      barracks.Items.Add(bed);
      dungeon.Items.Add(brokenLock);
      guardRoom.Items.Add(hammer);
      squireTower.Items.Add(overcoat);
      warRoom.Items.Add(window);

      // usable changes
      barracks.Usables.Add(uniform, "The sound of you changing didn't seem to stir any sleeping guards.  The rest of the uniforms seem worthless.  The 'bed' closest to you is empty...");
      // how to select which to use?
      // barracks.Usables.Add(bed, "You hear footsteps approaching the door from the hallway, so you lay in the empty cot, pull the covers up to your chin, and pretend to sleep. Guard: 'It's your turn for watch... Hey, what are you doing in here!? Quick Jenkins, seize him!' You try to scramble to feet to escape the room. Jenkins rushes you with the over-zealous energy of a new recruit and instinctively swings his sword down upon you. You crumple to the floor. The last sound ringing in your ear is of your family pendant bouncing on the cold stone floor that receives your lifeblood.");
      barracks.Usables.Add(bed, "Fully uniformed, you lay in the empty cot near the door to better blend in.  Shortly after, footsteps approach the barracks door. Guard: 'Hey Get Up! it's your turn for watch, Go relieve Shigeru in the Guard Room.' You quickly climb out of the bed and head for the door, keeping your head down as you pass the other guards.");

      // player instantiation?
      Player Current = new Player("");
      CurrentPlayer = Current;
      CurrentPlayer.Inventory.Add(pendant);
      CurrentPlayer.Usables.Add(uniform, false); //cannot be an object

    }
    public Game()
    {
      Setup();
    }
  }
}