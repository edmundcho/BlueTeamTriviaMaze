using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

namespace BlueTeamTriviaMaze
{
    /// <summary>
    /// Maze - the container for a Player to navigate through a collection 
    ///        of Rooms, each composed of doors that will only open if a
    ///        question is answered correctly.
    /// </summary>
    



    public class Maze : Canvas
    {
        private Room[,] _rooms;



        // Helper function. Returns the room given via the array indicies [y,x], or null if out of bounds.
        private Room GetRoom(int x, int y)
        {
            if (y < 0 || x < 0 || y >= _rooms.GetLength(0) || x >= _rooms.GetLength(1))
                return null;

            return _rooms[y, x];
        }



        public Maze(int width, int height, int entrance_x, int entrance_y, int[,] exits_xy)
        {
            // create each of the rooms
            _rooms = new Room[height, width];
            ArrayList doorsList = new ArrayList();


            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    // build each Room composed of 4 Doors (NSEW) and add it to the Maze.
                    //
                    // Note: this will only create new Doors if the neighboring Room that will share the
                    // same door hasn't already created one for them to share.
                    Room neighbor = null;



                    // NORTH DOOR
                    Door northDoor = null;
                    if (y > 0) // omit north doors on top-most rooms
                    {
                        neighbor = GetRoom(x, y - 1);  // (0,0) is the top-left most room, so y-1 means get the neighboring room 'north' of here
                        northDoor = neighbor != null ? neighbor.SouthDoor : new Door(x, y - 0.5f);

                        if (neighbor == null)          // if a new door was just created, add the door to the temporary collection.
                            doorsList.Add(northDoor);  // it will be added to the Maze later (for painting)
                    }



                    // SOUTH DOOR
                    Door southDoor = null;
                    if (y < height - 1) // omit south doors on bottom-most rooms
                    {
                        neighbor = GetRoom(x, y + 1);
                        southDoor = neighbor != null ? neighbor.NorthDoor : new Door(x, y + 0.5f); // position door south = y+0.5

                        if (neighbor == null)
                            doorsList.Add(southDoor);
                    }



                    // EAST DOOR
                    Door eastDoor = null;
                    if (x < width - 1) // omit east doors on right-most rooms
                    {
                        neighbor = GetRoom(x + 1, y);
                        eastDoor = neighbor != null ? neighbor.WestDoor : new Door(x + 0.5f, y); // position door east = x+0.5

                        if (neighbor == null)
                            doorsList.Add(eastDoor);
                    }



                    // WEST DOOR
                    Door westDoor = null;
                    if (x > 0) // omit west doors on left-most rooms
                    {
                        neighbor = GetRoom(x - 1, y);
                        westDoor = neighbor != null ? neighbor.EastDoor : new Door(x - 0.5f, y); // position door west = x-0.5

                        if (neighbor == null)
                            doorsList.Add(westDoor);
                    }



                    // Determine the Room type based this Room's location- this figures out if this Room is the entrance, an exit or a normal room
                    Room.Type room_type = Room.Type.Normal;
                    if (x == entrance_x && y == entrance_y) // check if its the entrance
                        room_type = Room.Type.Entrance;
                    

                    else // check if this Room is an exit room
                        for (int i = 0; i < exits_xy.GetLength(0) && room_type == Room.Type.Normal; ++i)
                            if (x == exits_xy[i, 0] && y == exits_xy[i, 1])
                                room_type = Room.Type.Exit;




                    // create and store a new Room, composed of all its
                    // appropriate doors (either freshly-generated or as taken from the neighboring rooms)
                    _rooms[y, x] = new Room(x, y, room_type, northDoor, southDoor, eastDoor, westDoor);


                    // Finally, add that new Room as a child of the Maze (canvas) so it may be drawn
                    Children.Add(_rooms[y, x].Drawable);


                } // end for (width)
            } // end for (height)



            // Add all the doors as children of the Maze (canvas) for drawing
            //
            // Note: this is done LAST like this so the Doors are drawn on top the Rooms
            foreach (Door door in doorsList)
                Children.Add(door);


        } // end public Maze(...)



        public void Win()
        {
            IsEnabled = false;

            MessageBox.Show("You Win!", "Winner!");
            
            MazeWindow.GetInstance().Close();
        }

        public void Lose()
        {
            IsEnabled = false;

            MessageBox.Show("You Lose!", "Loser!");

            MazeWindow.GetInstance().Close();
        }
    }
}
