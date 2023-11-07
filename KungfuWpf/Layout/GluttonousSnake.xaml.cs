using KungfuWpf.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using static KungfuWpf.Layout.GluttonousSnake;
using static System.Net.Mime.MediaTypeNames;

namespace KungfuWpf.Layout
{
    /// <summary>
    /// GluttonousSnake.xaml 的交互逻辑
    /// </summary>
    public partial class GluttonousSnake : Window
    {

        const int SnakeSquareSize = 20;
        const int SnakeStartLength = 3;
        const int SnakeStartSpeed = 400;
        const int SnakeSpeedThreshold = 100;
        const int MaxHighscoreListEntryCount = 5;

        private Random rnd = new Random();

        private UIElement snakeFood = null;
        private SolidColorBrush foodBrush = Brushes.Red;

        private SolidColorBrush snakeBodyBrush = Brushes.Green;
        private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
        private List<SnakePart> snakeParts = new List<SnakePart>();

        public enum SnakeDirection { Left, Right, Up, Down }
        private SnakeDirection snakeDirection = SnakeDirection.Right;
        private int snakeLength;
        private int currentScore = 0;

        private DispatcherTimer gameTickTimer = new DispatcherTimer();

        private SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        public ObservableCollection<SnakeHighscore> HighscoreList { get; set; } = new ObservableCollection<SnakeHighscore>();

        public GluttonousSnake()
        {
            InitializeComponent();
            gameTickTimer.Tick += GameTickTimer_Tick;
            LoadHighscoreList();
        }

        private void GameTickTimer_Tick(object? sender, EventArgs e)
        {
            MoveSnake();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
        }

        /// <summary>
        /// 画布区域绘制
        /// </summary>
        private void DrawGameArea()
        {
            int row = (int)(this.GameArea.Width / SnakeSquareSize);
            int column = (int)(this.GameArea.Height / SnakeSquareSize);

            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < column; i++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                    };

                    if (j % 2 == 0)
                    {
                        rect.Fill = i % 2 == 0 ? Brushes.Black : Brushes.White;
                    }
                    else
                    {
                        rect.Fill = i % 2 == 0 ? Brushes.White : Brushes.Black;
                    }

                    GameArea.Children.Add(rect);
                    Canvas.SetTop(rect, SnakeSquareSize * j);
                    Canvas.SetLeft(rect, i * SnakeSquareSize);
                }
            }
        }

        /// <summary>
        /// 画蛇
        /// </summary>
        private void DrawSnake()
        {
            foreach (var snakePart in snakeParts)
            {
                if (snakePart.UiElement is null)
                {
                    snakePart.UiElement = new Rectangle
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = snakePart.IsHead ? snakeHeadBrush : snakeBodyBrush
                    };
                    GameArea.Children.Add(snakePart.UiElement);
                    Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y);
                    Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X);
                }
            }
        }

        private void MoveSnake()
        {
            while (snakeParts.Count >= snakeLength)
            {
                GameArea.Children.Remove(snakeParts[0].UiElement);
                snakeParts.RemoveAt(0);
            }

            foreach (SnakePart snakePart in snakeParts)
            {
                (snakePart.UiElement as Rectangle).Fill = snakeBodyBrush;
                snakePart.IsHead = false;
            }

            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
            double nextX = snakeHead.Position.X;
            double nextY = snakeHead.Position.Y;

            switch (snakeDirection)
            {
                case SnakeDirection.Left:
                    nextX -= SnakeSquareSize;
                    break;
                case SnakeDirection.Right:
                    nextX += SnakeSquareSize;
                    break;
                case SnakeDirection.Up:
                    nextY -= SnakeSquareSize;
                    break;
                case SnakeDirection.Down:
                    nextY += SnakeSquareSize;
                    break;
            }

            snakeParts.Add(new SnakePart()
            {
                Position = new Point(nextX, nextY),
                IsHead = true
            });

            DrawSnake();
            DoCollisionCheck();
        }

        /// <summary>
        /// 游戏开始，设置基础参数
        /// </summary>
        private void StartNewGame()
        {
            //隐藏游戏画布上方的所有信息
            bdrWelcomeMessage.Visibility = Visibility.Collapsed;
            bdrHighscoreList.Visibility = Visibility.Collapsed;
            bdrEndOfGame.Visibility = Visibility.Collapsed;

            //清除蛇身所有元素
            foreach (SnakePart snakeBodyPart in snakeParts)
            {
                if (snakeBodyPart.UiElement != null)
                {
                    GameArea.Children.Remove(snakeBodyPart.UiElement);
                }
            }
            snakeParts.Clear();

            //移除食物元素
            if (snakeFood != null)
            {
                GameArea.Children.Remove(snakeFood);
            }

            //蛇身初始长度
            snakeLength = SnakeStartLength;

            //蛇身初始移动方向
            snakeDirection = SnakeDirection.Right;
            snakeParts.Add(new SnakePart() { Position = new Point(SnakeSquareSize * 5, SnakeSquareSize * 5) });

            //定时器执行间隔
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);

            //开始画蛇  
            DrawSnake();

            //画食物
            DrawSnakeFood();

            //小蛇动起来
            gameTickTimer.IsEnabled = true;
        }


        /// <summary>
        /// 随机获取一个坐标值，用于设置食物位置
        /// </summary>
        /// <returns></returns>
        private Point GetNextFoodPosition()
        {
            int maxX = (int)(GameArea.ActualWidth / SnakeSquareSize);
            int maxY = (int)(GameArea.ActualHeight / SnakeSquareSize);
            int foodX = rnd.Next(0, maxX) * SnakeSquareSize;
            int foodY = rnd.Next(0, maxY) * SnakeSquareSize;

            foreach (SnakePart snakePart in snakeParts)
            {
                if ((snakePart.Position.X == foodX) && (snakePart.Position.Y == foodY))
                    return GetNextFoodPosition();
            }

            return new Point(foodX, foodY);
        }

        /// <summary>
        /// 画食物所在具体的位置
        /// </summary>
        private void DrawSnakeFood()
        {
            Point foodPosition = GetNextFoodPosition();
            snakeFood = new Ellipse()
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = foodBrush
            };
            GameArea.Children.Add(snakeFood);
            Canvas.SetTop(snakeFood, foodPosition.Y);
            Canvas.SetLeft(snakeFood, foodPosition.X);
        }

        /// <summary>
        /// 监听界面按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            SnakeDirection originalSnakeDirection = snakeDirection;
            switch (e.Key)
            {
                case Key.Up:
                    if (snakeDirection != SnakeDirection.Down)
                        snakeDirection = SnakeDirection.Up;
                    break;
                case Key.Down:
                    if (snakeDirection != SnakeDirection.Up)
                        snakeDirection = SnakeDirection.Down;
                    break;
                case Key.Left:
                    if (snakeDirection != SnakeDirection.Right)
                        snakeDirection = SnakeDirection.Left;
                    break;
                case Key.Right:
                    if (snakeDirection != SnakeDirection.Left)
                        snakeDirection = SnakeDirection.Right;
                    break;
                case Key.Space:
                    StartNewGame();
                    break;
            }
            if (snakeDirection != originalSnakeDirection)
                MoveSnake();
        }

        /// <summary>
        /// 碰撞监测
        /// </summary>
        private void DoCollisionCheck()
        {
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            if ((snakeHead.Position.X == Canvas.GetLeft(snakeFood)) && (snakeHead.Position.Y == Canvas.GetTop(snakeFood)))
            {
                EatSnakeFood();
                return;
            }

            if ((snakeHead.Position.Y < 0) || (snakeHead.Position.Y >= GameArea.ActualHeight) ||
            (snakeHead.Position.X < 0) || (snakeHead.Position.X >= GameArea.ActualWidth))
            {
                EndGame();
            }

            foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((snakeHead.Position.X == snakeBodyPart.Position.X) && (snakeHead.Position.Y == snakeBodyPart.Position.Y))
                    EndGame();
            }
        }

        /// <summary>
        /// 迟到食物
        /// </summary>
        private void EatSnakeFood()
        {
            speechSynthesizer.SpeakAsync("yummy");

            snakeLength++;
            currentScore++;
            int timerInterval = Math.Max(SnakeSpeedThreshold, (int)gameTickTimer.Interval.TotalMilliseconds - (currentScore * 5));
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            GameArea.Children.Remove(snakeFood);
            DrawSnakeFood();
            UpdateGameStatus();
        }

        private void UpdateGameStatus()
        {
            this.tbStatusScore.Text = currentScore.ToString();
            this.tbStatusSpeed.Text = gameTickTimer.Interval.TotalMilliseconds.ToString();
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        private void EndGame()
        {
            bool isNewHighscore = false;
            if (currentScore > 0)
            {
                int lowestHighscore = (this.HighscoreList.Count > 0 ? this.HighscoreList.Min(x => x.Score) : 0);
                if ((currentScore > lowestHighscore) || (this.HighscoreList.Count < MaxHighscoreListEntryCount))
                {
                    bdrNewHighscore.Visibility = Visibility.Visible;
                    txtPlayerName.Focus();
                    isNewHighscore = true;
                }
            }
            if (!isNewHighscore)
            {
                tbFinalScore.Text = currentScore.ToString();
                bdrEndOfGame.Visibility = Visibility.Visible;
            }
            gameTickTimer.IsEnabled = false;

            SpeakEndOfGameInfo(isNewHighscore);
        }


        /// <summary>
        /// 窗体拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnShowHighscoreList_Click(object sender, RoutedEventArgs e)
        {
            bdrWelcomeMessage.Visibility = Visibility.Collapsed;
            bdrHighscoreList.Visibility = Visibility.Visible;
        }

        private void BtnAddToHighscoreList_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = 0;
            // Where should the new entry be inserted?
            if ((this.HighscoreList.Count > 0) && (currentScore < this.HighscoreList.Max(x => x.Score)))
            {
                SnakeHighscore justAbove = this.HighscoreList.OrderByDescending(x => x.Score).First(x => x.Score >= currentScore);
                if (justAbove != null)
                    newIndex = this.HighscoreList.IndexOf(justAbove) + 1;
            }
            // Create & insert the new entry
            this.HighscoreList.Insert(newIndex, new SnakeHighscore()
            {
                PlayerName = txtPlayerName.Text,
                Score = currentScore
            });
            // Make sure that the amount of entries does not exceed the maximum
            while (this.HighscoreList.Count > MaxHighscoreListEntryCount)
                this.HighscoreList.RemoveAt(MaxHighscoreListEntryCount);

            SaveHighscoreList();

            bdrNewHighscore.Visibility = Visibility.Collapsed;
            bdrHighscoreList.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 加载历史排行榜
        /// </summary>
        private void LoadHighscoreList()
        {
            if (File.Exists("snake_highscorelist.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>));
                using (Stream reader = new FileStream("snake_highscorelist.xml", FileMode.Open))
                {
                    List<SnakeHighscore> tempList = (List<SnakeHighscore>)serializer.Deserialize(reader);
                    this.HighscoreList.Clear();
                    foreach (var item in tempList.OrderByDescending(x => x.Score))
                        this.HighscoreList.Add(item);
                }
            }
        }

        /// <summary>
        /// 保存排行榜
        /// </summary>
        private void SaveHighscoreList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<SnakeHighscore>));
            using (Stream writer = new FileStream("snake_highscorelist.xml", FileMode.Create))
            {
                serializer.Serialize(writer, this.HighscoreList);
            }
        }

        private void SpeakEndOfGameInfo(bool isNewHighscore)
        {
            PromptBuilder promptBuilder = new PromptBuilder();

            promptBuilder.StartStyle(new PromptStyle()
            {
                Emphasis = PromptEmphasis.Reduced,
                Rate = PromptRate.Slow,
                Volume = PromptVolume.ExtraLoud
            });
            promptBuilder.AppendText("oh no");
            promptBuilder.AppendBreak(TimeSpan.FromMilliseconds(200));
            promptBuilder.AppendText("you died");
            promptBuilder.EndStyle();

            if (isNewHighscore)
            {
                promptBuilder.AppendBreak(TimeSpan.FromMilliseconds(500));
                promptBuilder.StartStyle(new PromptStyle()
                {
                    Emphasis = PromptEmphasis.Moderate,
                    Rate = PromptRate.Medium,
                    Volume = PromptVolume.Medium
                });
                promptBuilder.AppendText("new high score:");
                promptBuilder.AppendBreak(TimeSpan.FromMilliseconds(200));
                promptBuilder.AppendTextWithHint(currentScore.ToString(), SayAs.NumberCardinal);
                promptBuilder.EndStyle();
            }
            speechSynthesizer.SpeakAsync(promptBuilder);
        }
    }
}
