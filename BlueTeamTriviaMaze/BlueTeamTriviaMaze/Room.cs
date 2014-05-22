using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace BlueTeamTriviaMaze
{
    public class Room
    {
        public static int ROOM_SIZE = 100;

        public enum Type { Normal, Entrance, Exit }
        public enum State { NotVisited, Visited }

        private Type _type;
        private State _state;

        public Shape Drawable;

        public Door WestDoor { get; private set; }
        public Door EastDoor { get; private set; }
        public Door NorthDoor { get; private set; }
        public Door SouthDoor { get; private set; }

        new public Type GetType() { return _type; }
        public State GetState() { return _state; }
        public void SetState(State state)
        {
            _state = state;

            if (_state == State.Visited)
                Drawable.Fill = Brushes.GhostWhite;
            
            else if (_state == State.NotVisited)
                Drawable.Fill = Brushes.DarkGray;
        }

        public void SetDoorsEnabled(bool enabled)
        {
            if (NorthDoor != null)
                NorthDoor.IsEnabled = enabled;
            if (SouthDoor != null)
                SouthDoor.IsEnabled = enabled;
            if (EastDoor != null)
                EastDoor.IsEnabled = enabled;
            if (WestDoor != null)
                WestDoor.IsEnabled = enabled;
        }

        public Room(int x, int y, Type type, Door north, Door south, Door east, Door west)
        {
            _type = type;

            // store all the doors- some can be null for edge cases
            NorthDoor = north;
            SouthDoor = south;
            EastDoor = east;
            WestDoor = west;


            // create what the door will look like- an outlined rectangle
            Drawable = new Rectangle();

            Canvas.SetLeft(Drawable, x * ROOM_SIZE);
            Canvas.SetTop(Drawable, y * ROOM_SIZE);

            if (_type == Type.Exit)
                Drawable.Stroke = Brushes.LimeGreen;
            else
                Drawable.Stroke = Brushes.Black;

            Drawable.Width = Drawable.MinWidth = Drawable.MaxWidth = Drawable.Height = Drawable.MinHeight = Drawable.MaxHeight = ROOM_SIZE;


            // all rooms start as not visited
            SetState(State.NotVisited);


        } // end public Room(...)
    }
}
