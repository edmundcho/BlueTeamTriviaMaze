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
        }

        public Door(float x_index, float y_index)
        {
            FontWeight = System.Windows.FontWeights.ExtraBold;
            MinWidth = MaxWidth = MinHeight = MaxHeight = Width = Height = DOOR_SIZE;

            IsEnabled = false;

            SetState(State.Locked);

            // position the door
            Canvas.SetLeft(this, x_index * Room.ROOM_SIZE - DOOR_SIZE / 2);
            Canvas.SetTop(this, y_index * Room.ROOM_SIZE - DOOR_SIZE / 2);
        }
    }
}
