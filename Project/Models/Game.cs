using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }
    public bool Playing { get; set; } = true;

    //NOTE Make yo rooms here...
    public void Setup()
    {

      // rooms initiated
      var village = new PublicSpace("Village", "You race back to your village to update them on your mission");

      var tunnel = new PublicSpace("Hidden Tunnel", "The dark tunnel discovered by your village leaders is tight and filthy. You have come to a grated plate on the eastern wall where light fights its way into the tunnel.  You listen for any noise within the castle, but all seems quiet.");

      var hallway1 = new PublicSpace("Western Hallway", "You find yourself in a small hall leading to the north and south.  There doesn't appear to be anything of interest within the hall itself. The eastern wall has a large arched opening leading into what appears to be a courtyard");

      var barracks = new PublicSpace("Barracks", "You see a room with several sleeping guards. The room smells of sweaty men.  The 'bed' closest to you is empty and there are several 'guard uniform's tossed about.");

      var captQuarters = new Room("Captain's Quarters", "As you approach the Captain's quarters you swallow hard and notice your lips are dry.  Stepping into the room you see a few small tables and maps of the countryside sprawled out upon them. A soldier, presumably the Captain stops pacing the far wall and sizes you up upon entry. Captain: 'A new recruit, huh? I'd send you to the guard room where I didn't think you could screw anything up, but apparently we have special guests coming in this late hour' He slams a key down onto the table, crinkling maps and flattening a scroll. Captain: 'I want this night to stay quiet and simple...the last thing I need is to clean up any mistakes!'  He pens a quick 'note' and tosses it on the table in front of you. Captain: 'Go fetch a messenger squire for me and have him take this note to the Gate Captain immediately.  I'll rouse the other guards to secure their welcome once within.'  With that, he grabbed his coat and took two lumbering strides out the door.  A quick survey of the room lets you see not only has he left a 'silver key' on the table beside the 'note', but some interesting vials of liquid fill a pouch along the back wall where he was pacing.");

      var courtyard = new Room("Castle Courtyard", "You step into the large courtyard located in the middle of the castle grounds.  At its center is a flowing fountain casting moonlight in all directions.  A few guards patrol the area.");

      var guardRoom = new Room("Guard Room", "Pushing open the door of the guard room, you look around and observe that it is empty.  There are a few small 'tool's in the corner and a chair propped against the wall near a stairwell that likely leads to the dungeon.");

      var hallway2 = new PublicSpace("Southern Hallway", "You find yourself in a small hall stretching toward entrances at the east and west ends.  There doesn't appear to be anything of interest within the hallway itself. The northern wall has a large arched opening leading into what appears to be a courtyard");

      var dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon you notice a harsh chill to the air.  Landing at the base of the stairs you see the remains of a prisoner that will never find freedom.");

      var squireTower = new Room("Squire's Tower", "As you finish climbing the stairs to the squire tower you see a messenger nestled in his bed. His messenger 'overcoat' is hanging from his bed post.");

      var warRoom = new LockedRoom("War Room", "A large ornate door stands tightly shut with a silver lion's head below the handle. Within its mouth, a keyhole.");

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
      barracks.Usables.Add(bed, "Fully uniformed, you lay in the empty cot near the door to better blend in.  Shortly after, footsteps approach the barracks door. Guard: 'Hey Get Up! it's your turn for watch, Go relieve Shigeru in the Guard Room.' You quickly climb out of the bed and head for the door, keeping your head down as you pass the other guards.");
      courtyard.Usables.Add(silverKey, "Stepping into the war room you see several maps strewn across a large central table by the cool night breeze coming through a nearby 'window'. On the maps many of the villages have been marked for purification.  You also notice on one side of the table several dishes of prepared food have been set out. Perhaps the war council will be meeting soon...A narrow stair on the eastern wall winds up to the squire quarters.");
      //need this duplicated so that the description can be displayed from silver key after entering war room as current room
      warRoom.Usables.Add(silverKey, "Stepping into the war room you see several maps strewn across a large central table by the cool night breeze coming through a nearby 'window'. On the maps many of the villages have been marked for purification.  You also notice on one side of the table several dishes of prepared food have been set out. Perhaps the war council will be meeting soon...A narrow stair on the eastern wall winds up to the squire quarters.");
      warRoom.Usables.Add(vial, "As you stare around the room you realize the vial is likely the same deadly poison that the Dark Lord's troops have been putting on their arrowheads...the same arrows that have felled so many of those close to you. Inspecting the refreshment layout, you drain every last drop from the vial into the most ornate cups, then toss the empty vial out the window.");
      warRoom.Usables.Add(window, "You grab the outer ledge of the stone opening in the wall, and hop out of the room.  As you gauge the short fall to the brush below you can hear the war council jovially speaking with each other as they enter the room.  The Dark Lord's rasp-filled cackle cutting through the rest.  As much as your adrenaline wishes you to flee you can't help but to continue to listen.  Clanging of glasses, an enthusiastic toast is bellowed. Some more talk about the need to destroy the 'inconveniences' surrounding the castle's lands.  Then, amidst a self-indulging monologue, the Dark Lord begins to cough. It thickens.  You can hear the concern in the other councilmembers as their steps scurry towards him.  You can hear as the rest of the group begins to struggle breathing as well.  Your knuckles turn white as you soak in their desperate gasps for air.  Worried they may try and run for help, you pull yourself up and peer back into the room, but they have all fallen still on the floor.");

      // player instantiation?
      Player Current = new Player("");
      CurrentPlayer = Current;
      CurrentPlayer.Inventory.Add(pendant);
      CurrentPlayer.Usables.Add(uniform.Name, false); //cannot be an object

      Playing = true;

    }
    public Game()
    {
      Setup();
    }
  }
}