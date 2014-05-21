using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlueTeamTriviaMaze
{
    /// <summary>
    /// Interaction logic for Maze.xaml
    /// </summary>
    public partial class MazeWindow : Window
    {
        private static MazeWindow _instance;
        public static MazeWindow GetInstance()
        {
            return _instance; 
        }
        


        private Maze _maze;
        public Maze GetMaze()
        {
            return _maze;
        }

        public MazeWindow(int maze_width, int maze_height)
        {
            _instance = this;

            InitializeComponent();

            _maze = new Maze(maze_width, maze_height);

            Content = _maze;

            Width = maze_width * Room.ROOM_SIZE + Door.DOOR_SIZE - 5;
            Height = maze_height * Room.ROOM_SIZE + Door.DOOR_SIZE + 18; // arbitrary value accounts for border padding crap
        }








        /*
        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _questionWindow = new QuestionWindow();
                _questionWindow.Closed += new EventHandler(displayAnswer);

                _questionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void displayAnswer(object sender, EventArgs e)
        {
            if (_questionWindow.Answer == QuestionWindow.ANSWER_CANCELLED)
                lblAnswerVal.Content = "Cancelled!";
            else if (_questionWindow.Answer == QuestionWindow.ANSWER_INCORRECT)
                lblAnswerVal.Content = "Incorrect!";

            else
            {
                lblAnswerVal.Content = "Correct!";

                // animation
                double left = Canvas.GetLeft(rect);
                Anim = new DoubleAnimation(left, 120 + left, new Duration(TimeSpan.FromSeconds(3)));
                rect.BeginAnimation(Canvas.LeftProperty, Anim);
            }

            _questionWindow = null;
        }*/
    }
}
