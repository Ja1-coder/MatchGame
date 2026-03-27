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

        private async void SetUpGame()
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

                    string emoji = animalEmoji[index];
                    textBlock.Tag = emoji;
                    textBlock.Text = emoji;

                    animalEmoji.RemoveAt(index);
                }
            }

            mainGrind.IsEnabled = false;

            await Task.Delay(2000);

            foreach (TextBlock textBlock in mainGrind.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Text = "?";
                }
            }

            mainGrind.IsEnabled = true;

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock? lastTextBlockClicked;
        bool findingMath = false;
        bool isProcessing = false;

        private async void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isProcessing || sender is not TextBlock textBlock || textBlock.Text != "?")
            {
                return;
            }

            textBlock.Text = textBlock.Tag.ToString();

            if (!findingMath)
            {
                lastTextBlockClicked = textBlock;
                findingMath = true;
            }
            else if (lastTextBlockClicked != null && textBlock.Tag.ToString() == lastTextBlockClicked.Tag.ToString())
            {
                isProcessing = true;

                matchesFound++;
                await Task.Delay(500);

                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked.Visibility = Visibility.Hidden;

                findingMath = false;
                lastTextBlockClicked = null;

                isProcessing = false;
            }
            else
            {
                isProcessing = true;

                await Task.Delay(500);

                textBlock.Text = "?";

                if (lastTextBlockClicked != null)
                {
                    lastTextBlockClicked.Text = "?";
                }

                findingMath = false;
                lastTextBlockClicked = null;

                isProcessing = false;
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