using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;

namespace BlueTeamTriviaMaze
{
    /// <summary>
    /// Player - the entity that moves between Rooms through Doors
    ///          in the Maze in the 4 cardinal directions (N,S,E,W).
    /// </summary>



    public class Player
    {
        private const int PLAYER_SIZE = 50;

        private Room _currentRoom;
        private int _currentKeys;

        public Shape Drawable { get; private set; }

        public Player(Room start_room, int num_keys)
        {
            _currentKeys = num_keys;


            // Load the player drawable
            Drawable = new Rectangle();
            Drawable.Width = Drawable.Height = PLAYER_SIZE;
            Drawable.StrokeThickness = 3.0;
            Drawable.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/player.png")));

            // position the player initially
            Canvas.SetLeft(Drawable, Canvas.GetLeft(_currentRoom.Drawable) + Room.ROOM_SIZE / 2 - Drawable.Width / 2);
            Canvas.SetTop(Drawable, Canvas.GetTop(_currentRoom.Drawable) + Room.ROOM_SIZE / 2 - Drawable.Height / 2);

            EnterRoom(start_room);
        }

        private void EnterRoom(Room target_room)
        {
            DependencyProperty property = null;
            int start = 0, end = 0;


            // if we are coming from a room, we can animate
            if (_currentRoom != null) { 
                _currentRoom.SetDoorsEnabled(false); // disable the old room's doors

                // figure out what property (direction) to animate- Left or Top
                property = Canvas.GetTop(target_room.Drawable) == Canvas.GetTop(_currentRoom.Drawable) ? Canvas.LeftProperty : Canvas.TopProperty;

            }

            _currentRoom = target_room;
            _currentRoom.SetDoorsEnabled(true); // enable the new room's doors
            _currentRoom.SetState(Room.State.Visited);


            // animate the player's position moving it toward the new location if we were in a room prior
            if (property != null)
                Drawable.BeginAnimation(property, new DoubleAnimation(start, end, new Duration(TimeSpan.FromSeconds(2))));
        }
    }
}
