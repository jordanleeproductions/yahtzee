using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives; //ToggleButton
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using YahtzeeLibrary;
namespace YahtzeeClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player plyr;
        private TextBox scoringArea;
        private TextBox prevScoringArea;
        private int rollNo;
        

        public MainWindow()
        {
            InitializeComponent();
            plyr = new Player();
            rollNo = 0;
            score.IsEnabled = false;
        
        }

        private void roll_Click(object sender, RoutedEventArgs e)
        {
            score.IsEnabled = true;
            if (rollNo < 3)
            {
                //check togglebuttons for holdState
                for (int i = 0; i < 5; ++i)
                {
                    string str = "hold_" + (i + 1).ToString();
                    object obj = YahtzeeRoot.FindName(str);
                    ToggleButton tb;
                    if (obj is ToggleButton)
                    {
                        tb = (ToggleButton)obj;
                        if (tb.IsChecked == true)
                            plyr.dice.ElementAt(i).HoldState = true;
                        else
                            plyr.dice.ElementAt(i).HoldState = false;

                    }
                }
                plyr.Roll();
                ++rollNo;
                if (rollNo == 3) roll.IsEnabled = false;
                refreshDice();
            }
            
        }


        private void ScoreCardSelection(object sender, RoutedEventArgs e)
        {
            if (prevScoringArea != null)
            {
                prevScoringArea.Background = null;
            }

            scoringArea = (TextBox)sender;

            if (scoringArea.IsEnabled)
            {
                prevScoringArea = scoringArea;

                scoringArea.Background = Brushes.LightGreen;
            }
        }

        private void refreshDice() {
            dice_1.Content = plyr.dice.ElementAt(0).Val.ToString();
            dice_2.Content = plyr.dice.ElementAt(1).Val.ToString();
            dice_3.Content = plyr.dice.ElementAt(2).Val.ToString();
            dice_4.Content = plyr.dice.ElementAt(3).Val.ToString();
            dice_5.Content = plyr.dice.ElementAt(4).Val.ToString();
        }

        private void score_Click(object sender, RoutedEventArgs e)
        {
            //Validate a label is selected
            string nm = scoringArea.Name;
            if (nm == null)
            {
                MessageBox.Show("Error: Please select an scoring area!", "ERROR");
            }
            else
            {
                
                scoringArea.Text = plyr.Calculate(nm).ToString();
                
                //check if upper or lower total reached
                if (plyr.isTotUpper())
                {
                    Upper.Content = plyr.totUpper.ToString();
                    Bonus.Content = plyr.totUpperBonus.ToString();
                    TotalUpper.Content = (plyr.totUpper + plyr.totUpperBonus).ToString();
                    Upper2.Content = (plyr.totUpper + plyr.totUpperBonus).ToString();
                    //check if game over
                    if (plyr.isTotGrand())
                        Total.Content = plyr.totGrand.ToString();

                }

                if (plyr.isTotLower()) {
                    Lower.Content = plyr.totLower.ToString();
                    //check if game over
                    if(plyr.isTotGrand())
                        Total.Content = plyr.totGrand.ToString();
                }

                resetTurn();

            }
        }//score_Click

        private void resetTurn() {
            scoringArea.Background = Brushes.LightGray;
            scoringArea.IsEnabled = false; //can't be selected again
            scoringArea = null;
            rollNo = 0;

            //FIX WITH DATABIND Die.HoldState to togglebuttons
            string str = "hold_";
            for (int i = 0; i < 5; ++i) {
                ToggleButton tb = (ToggleButton)YahtzeeRoot.FindName(str + (1 + i).ToString());
                tb.IsChecked = false;
            }
            //END FIX

            plyr.Reset();
            score.IsEnabled = false;
            roll.IsEnabled = true;
        }
    }
}
