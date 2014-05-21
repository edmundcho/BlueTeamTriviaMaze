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
        public static int ROOM_SIZE = 75;

        public Shape Drawable;

        public Door WestDoor { get; private set; }
        public Door EastDoor { get; private set; }
        public Door NorthDoor { get; private set; }
        public Door SouthDoor { get; private set; }

        public Room(int x, int y, Door north, Door south, Door east, Door west)
        {
            // store all the doors- some can be null for edge cases
            NorthDoor = north;
            SouthDoor = south;
            EastDoor = east;
            WestDoor = west;


            // create what the door will look like- an outlined rectangle
            Drawable = new Rectangle();

            Canvas.SetLeft(Drawable, x * ROOM_SIZE);
            Canvas.SetTop(Drawable, y * ROOM_SIZE);

            //Drawable.StrokeThickness = 1; // thick walls
            Drawable.Stroke = Brushes.Black;
            Drawable.Fill = Brushes.LightGray;

            Drawable.Width = Drawable.MinWidth = Drawable.MaxWidth = Drawable.Height = Drawable.MinHeight = Drawable.MaxHeight = ROOM_SIZE;
        }
    }
}
