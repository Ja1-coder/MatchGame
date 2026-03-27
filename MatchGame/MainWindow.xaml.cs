using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();


            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            float currentTime = tenthsOfSecondsElapsed / 10F;
            timeTextBlock.Text = currentTime.ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();

                if (matchesFound == 8 && currentTime < GameInfo.BestTime)
                {
                    GameInfo.BestTime = currentTime;
                    timeTextBlock.Text = currentTime.ToString("0.0s - Novo Recorde!");
                }

                MessageBoxResult result = MessageBox.Show("Parabéns! Voltar ao menu?", "Fim de Jogo", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) {
                    WindowMenu menu = new WindowMenu();
                    menu.Show();
                    this.Close();
                }
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐵", "🐵",
                "🐶", "🐶",
                "🐺", "🐺",
                "🦄", "🦄",
                "🦁", "🦁",
                "🦝", "🦝",
                "🐭", "🐭",
                "🐮", "🐮",
            };

            Random random = new Random();

            foreach(TextBlock textBlock in mainGrind.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock? lastTextBlockClicked;
        bool findingMath = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock textBlock)
                return;

            if (!findingMath)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMath = true;
            }
            else if (lastTextBlockClicked != null && textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMath = false;
            }
            else
            {
                if (lastTextBlockClicked != null)
                {
                    lastTextBlockClicked.Visibility = Visibility.Visible;
                }

                findingMath = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
               SetUpGame();
            }
        }
    }
}