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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double CellSize = 30D;
        const int CellCount = 16;
        DispatcherTimer timer;

        Snake snake;
        

        public MainWindow()
        {
            InitializeComponent();
            DrawBoardBackground();
            snake = new Snake(snakeShape, CellSize, CellCount);
            

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            snake.Move();
        }
        

        private void DrawBoardBackground()
        {
            SolidColorBrush color1 = Brushes.LightGreen;
            SolidColorBrush color2 = Brushes.LimeGreen;

            for (int row = 0; row < CellCount; row++)
            {
                SolidColorBrush color = row % 2 == 0 ? color1 : color2;
                if (row % 2 == 0)
                {

                }
                for (int col = 0; col < CellCount; col++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = CellSize;
                    r.Height = CellSize;
                    r.Fill = color;
                    Canvas.SetTop(r, row * CellSize);
                    Canvas.SetLeft(r, col * CellSize);
                    board.Children.Add(r);

                    color = color == color1 ? color2 : color1;
                }

            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Direction direction;
            switch (e.Key)
            {
                case Key.Up:
                    direction = Direction.Up;
                    break;
                case Key.Down:
                    direction = Direction.Down;
                    break;
                case Key.Left:
                    direction = Direction.Left;
                    break;
                case Key.Right:
                    direction = Direction.Right;
                    break;
                default:
                    return;
            }
            snake.ChangeDirection(direction);
            /*if (e.Key == Key.Up)
            {
                direction = Direction.Up;
            }
            else if (e.Key == Key.Down)
            {
                direction = Direction.Down;
            }
            else if (e.Key == Key.Right)
            {
                direction = Direction.Right;
            }
            else if (e.Key == Key.Left)
            {
                direction = Direction.Left;
            }
            else
            {
                return;
            }
 
            MoveSnake(direction);*/
        }

        
    }
}
