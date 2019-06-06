﻿using System;
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
        Random rnd = new Random();

        GameStatus gameStatus;
        SnakeParts snakePart = new SnakeParts();

        int foodRow;
        int foodCol;

        Direction snakeDirection;

        int points = 0;

        public MainWindow()
        {
            InitializeComponent();
            DrawBoardBackground();
            InitFood();
            InitSnake();
            ChangePoints(0);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();

            rnd = new Random();

            ChangeGameStatus(GameStatus.Ongoing);
        }

        private void DrawBoardBackground()
        {
            SolidColorBrush color1 = Brushes.LightGreen;
            SolidColorBrush color2 = Brushes.LimeGreen;

            for (int row = 0; row < CellCount; row++)
            {
                SolidColorBrush color = row % 2 == 0 ? color1 : color2;

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

        private void ChangeGameStatus(GameStatus newGameStatus)
        {
            gameStatus = newGameStatus;
            lblGameStatus.Content =
                $"Status:{gameStatus}";
        }

        private void ChangePoints(int newPoints)
        {
            points = newPoints;
            lblPoints.Content =
                $"Points: {points}";
        }

        private void InitFood()
        {
            foodShape.Height = CellSize;
            foodShape.Width = CellSize;
            foodRow = rnd.Next(0, CellCount);
            foodCol = rnd.Next(0, CellCount);
            SetShape(foodShape, foodRow, foodCol);
        }

        private void InitSnake()
        {
            snakeShape.Height = CellSize;
            snakeShape.Width = CellSize;
            int index = CellCount / 2;
            snakePart.Row = index;
            snakePart.Col = index;
            SetShape(snakeShape, snakePart.Row, snakePart.Col);

            ChangeSnakeDirection(Direction.Up);
        }

        private void ChangeSnakeDirection(Direction direction)
        {
            snakeDirection = direction;
            lblSnakeDirection.Content =
                $"Direction: {direction}";
        }

        private void MoveSnake(Direction direction)
        {

            switch (snakeDirection)
            {
                case Direction.Up:
                    snakePart.Row--;
                    break;
                case Direction.Left:
                    snakePart.Col--;
                    break;
                case Direction.Down:
                    snakePart.Row++;
                    break;
                case Direction.Right:
                    snakePart.Col++;
                    break;
            }

            bool food = 
                snakePart.Row == foodRow &&
                snakePart.Col == foodCol;
            if(food)
            {
                ChangePoints(points + 1);
                InitFood();
            }

            SetShape(snakeShape, snakePart.Row, snakePart.Col);           
        }
        private void SetShape(Shape shape, int row, int col)
        {
            double top = row * CellSize;
            double left = col * CellSize;

            Canvas.SetTop(shape, top);
            Canvas.SetLeft(shape, left);
        }

        private void SnakeParts()
        {
            List<int> a = new List<int>();
            a.Add(1);
            a.Add(2);
            a.Add(3);

            int temp = a[2];
            a.RemoveAt(2);
            a.Insert(0, temp);

            Console.WriteLine(String.Join(",", a));
        }
            
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(gameStatus != GameStatus.Ongoing)
            {
                return;
            }
            MoveSnake(snakeDirection);
        }                      
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameStatus != GameStatus.Ongoing)
            {
                return;
            }

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
            ChangeSnakeDirection(direction);
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
