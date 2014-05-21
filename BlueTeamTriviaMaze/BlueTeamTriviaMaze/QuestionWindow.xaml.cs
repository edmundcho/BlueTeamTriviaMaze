using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlueTeamTriviaMaze
{
    /// <summary>
    /// Interaction logic for Question.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public enum Result { Cancelled, Correct, Incorrect };

        public const int TYPE_TRUE_FALSE = 0,
            TYPE_MULTIPLE_CHOICE = 1;



        private string _guess;
        TriviaItem _triviaItem;

        public Result Answer { get; private set; }



        public QuestionWindow()
        {
            InitializeComponent();

            Answer = Result.Cancelled;
            
            _triviaItem = new TriviaItem();
            
            questionLayout();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_guess.Equals(_triviaItem.Answer))
                Answer = Result.Correct;
            else
                Answer = Result.Incorrect;

            this.Close();
        }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.IsChecked.Value)
                _guess = (string)rb.Content;

            btnSubmit.IsEnabled = true;
        }

        private void questionLayout()
        {
            lblQuestion.Content = _triviaItem.Question;
            rbOptionOne.Content = _triviaItem.DummyAnswer[0];
            rbOptionTwo.Content = _triviaItem.Answer;
            rbOptionThree.Content = _triviaItem.DummyAnswer[1];
            rbOptionFour.Content = _triviaItem.DummyAnswer[2];

            if (_triviaItem.Type == TYPE_TRUE_FALSE)
            {
                cvsQuestion.Children.Remove(rbOptionThree);
                cvsQuestion.Children.Remove(rbOptionFour);
            }
        }
    }
}
