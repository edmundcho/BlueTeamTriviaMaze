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

        private Room GetRoom(int y, int x)
        {
            if (y < 0 || x < 0 || y >= _rooms.GetLength(0) || x >= _rooms.GetLength(1))
                return null;

            return _rooms[y, x];
        }

        public Maze(int width, int height)
        {
            // create each of the rooms
            _rooms = new Room[height, width];
            ArrayList doorsList = new ArrayList();

            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                {
                    // create this rooms doors, BUT only after we check the neighboring Room's doors for if this door is already created, then we just steal the reference to it if so
                    Room neighbor = null;


                    // NORTH DOOR
                    Door northDoor = null;
                    if (y > 0)
                    {
                        neighbor = GetRoom(y - 1, x);
                        northDoor = neighbor != null ? neighbor.SouthDoor : new Door(x, y - 0.5f);
                        if (neighbor == null) doorsList.Add(northDoor); // add door as child (for painting) if it was just created
                    }


                    // SOUTH DOOR
                    Door southDoor = null;
                    if (y < height - 1)
                    {
                        neighbor = GetRoom(y + 1, x);
                        southDoor = neighbor != null ? neighbor.NorthDoor : new Door(x, y + 0.5f);
                        if (neighbor == null) doorsList.Add(southDoor);
                    }


                    // EAST DOOR
                    Door eastDoor = null;
                    if (x < width - 1)
                    {
                        neighbor = GetRoom(y, x + 1);
                        eastDoor = neighbor != null ? neighbor.WestDoor : new Door(x + 0.5f, y);
                        if (neighbor == null) doorsList.Add(eastDoor);
                    }


                    // WEST DOOR
                    Door westDoor = null;
                    if (x > 0)
                    {
                        neighbor = GetRoom(y, x - 1);
                        westDoor = neighbor != null ? neighbor.EastDoor : new Door(x - 0.5f, y);
                        if (neighbor == null) doorsList.Add(westDoor);
                    }


                    // store the new room, composed of all its appropriate doors (either freshly-generated or stolen from the neighboring room)
                    _rooms[y, x] = new Room(x, y, northDoor, southDoor, eastDoor, westDoor);
                    Children.Add(_rooms[y, x].Drawable); // add the room as a child to the maze
                }


            // add all the doors as children LAST (z-ordering)
            foreach (Door door in doorsList)
                Children.Add(door);
        }


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
