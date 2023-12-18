using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockPaperScissorsAI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (MessageBox.Show("Do you want to learn AI?", "Learn", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Neuron neuron = new Neuron();
                Neuron.NeuronLearn(Rock, Scissors, Paper);
            }
        }

        private void Rock_Click(object sender, RoutedEventArgs e)
        {
            IsWin(Rock);
        }

        private void Scissors_Click(object sender, RoutedEventArgs e)
        {
            IsWin(Scissors);
        }

        private void Paper_Click(object sender, RoutedEventArgs e)
        {
            IsWin(Paper);
        }

        void IsWin(Button button)
        {
            RPS player = Neuron.NeuronPlay(button);
            RPS optionAI;
            Button buttonAI = new Button();
            Random rand = new Random();
            switch (rand.Next(0, 3))
            {
                case 1:
                    buttonAI = Rock;
                    break;
                case 2:
                    buttonAI = Scissors;
                    break;
                case 3:
                    buttonAI = Paper;
                    break;
            }
            optionAI = Neuron.NeuronPlay(buttonAI);
            Mystery.Background = buttonAI.Background;

            switch (RPS.Compare(player, optionAI))
            {
                case -1:
                    if (MessageBox.Show("Fail!\nWant play again?", "Fail", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    else
                        Application.Current.Shutdown();
                    break;
                case 0:
                    if (MessageBox.Show("Tie!\nWant play again?", "Tie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    else
                        Application.Current.Shutdown();
                    break;
                case 1:
                    if (MessageBox.Show("You are winer!\nWant play again?", "Win", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                    else
                        Application.Current.Shutdown();
                    break;
            }
        }
    }
}
