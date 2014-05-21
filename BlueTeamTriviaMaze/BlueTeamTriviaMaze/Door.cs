using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlueTeamTriviaMaze
{
    public class Door : Button
    {
        public const int DOOR_SIZE = 20;



        public enum State {Closed, Locked, Opened};

        private State _state;

        private void SetState(State new_state)
        {
            _state = new_state;

            if (_state == State.Closed)
            {
                Content = "+";
                Background = Brushes.LightGray;
                Foreground = Brushes.Black;
            }
            else if (_state == State.Locked)
            {
                Content = "X";
                Background = Brushes.Transparent;
                Foreground = Brushes.DarkRed;
            }
            else if (_state == State.Opened)
            {
                Content = "";
                Background = Brushes.Transparent;
                Foreground = Brushes.Black;
            }
        } // end SetState(new_state)



        public Door(float x_index, float y_index)
        {
            FontWeight = System.Windows.FontWeights.ExtraBold;
            MinWidth = MaxWidth = MinHeight = MaxHeight = Width = Height = DOOR_SIZE;

            IsEnabled = false;  // all Doors start disabled, the parent Rooms for this door will enable/disable this as the player enter/exits

            SetState(State.Closed);

            // this ABSOLUTELY positions the door
            Canvas.SetLeft(this, x_index * Room.ROOM_SIZE + Room.ROOM_SIZE/2 - DOOR_SIZE/2); // +ROOM_SIZE/2 to move the door onto the N,S,E,W edges of the room
            Canvas.SetTop(this, y_index * Room.ROOM_SIZE + Room.ROOM_SIZE/2 - DOOR_SIZE/2);  // -DOOR_SIZE/2 to center the door itself on the edge
        }
    }
}
