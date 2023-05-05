using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;
using System.Linq;

namespace Minesweeper
{
    public class Map
    {
        public static Cell[,]? s_MinesweeperCells;
        public static Button[,]? s_MinesweeperButtons;

        static Window? s_gameWindow;
        static int s_windowHeight = 1000;
        static int s_windowWidth  = 800;

        public static int s_GridDimension = 15;
        static Grid s_minefield = new Grid();

        public static void CreateGame(Window gWindow, int dimension)
        {
            s_GridDimension = dimension;
            s_gameWindow = gWindow;

            // set arrays to s_GridDimension
            s_MinesweeperButtons = new Button[s_GridDimension, s_GridDimension];
            s_MinesweeperCells = new Cell[s_GridDimension, s_GridDimension];
            GameManager.s_BombArray = new bool[s_GridDimension, s_GridDimension];

            GenerateButtonField(s_GridDimension);
            GenerateHeader();
        }

        public static Cell GetCell(int x, int y)
        {
            return s_MinesweeperCells[x, y];
        }

        private static void GenerateHeader()
        {
            // Heading grid
            Grid headingBlock = new Grid();

            // Header text
            TextBlock header = new TextBlock();
            header.Text = "Welcome To the Danger Zone";
            header.FontWeight = FontWeights.Bold;
            header.Foreground = Brushes.Red;
            header.FontSize = 40;
            header.VerticalAlignment = VerticalAlignment.Top;
            header.HorizontalAlignment = HorizontalAlignment.Center;

            // HeadingBlock text
            headingBlock.Background = Brushes.LightSlateGray;
            headingBlock.Width = s_windowWidth;
            headingBlock.Children.Add(header);
            headingBlock.VerticalAlignment = VerticalAlignment.Top;
            headingBlock.HorizontalAlignment = HorizontalAlignment.Center;

            // Create template and add stack, display
            DockPanel dockPanel = new DockPanel();
            DockPanel.SetDock(headingBlock, Dock.Top);
            DockPanel.SetDock(s_minefield, Dock.Bottom);

            dockPanel.Width = s_windowWidth;
            dockPanel.Height = s_windowHeight;

            dockPanel.Children.Add(headingBlock);
            dockPanel.Children.Add(s_minefield);

            dockPanel.VerticalAlignment = VerticalAlignment.Top;
            dockPanel.HorizontalAlignment = HorizontalAlignment.Center;
            dockPanel.Background = Brushes.Lavender;


            // Add the Grid as the Content of the Parent Window Object
            s_gameWindow.Title = "hardcore gamer mode detected";
            s_gameWindow.Content = dockPanel;
            s_gameWindow.Show();
        }

        private static void GenerateButtonField(int dimension)
        {
            // Create the Grid
            s_minefield = new Grid();
            s_minefield.Width = 750;
            s_minefield.Height = 750;
            s_minefield.HorizontalAlignment = HorizontalAlignment.Center;
            s_minefield.VerticalAlignment = VerticalAlignment.Center;
            s_minefield.ShowGridLines = true;

            // Generate bombs 
            int bombsToPlace = (int)Math.Ceiling((double)s_GridDimension * s_GridDimension * 0.18);
            int p = 0;
            bombsToPlace = 1;
            while (p < bombsToPlace)
            {
                Random random = new Random();
                int x = random.Next(0, s_GridDimension - 1);
                int y = random.Next(0, s_GridDimension - 1);

                if (GameManager.s_BombArray[x, y] != true)
                {
                    Trace.WriteLine("Attempting " + p + " Bomb Placement: " + x + " " + y);
                    GameManager.s_BombArray[x, y] = true;
                    p++;
                }
            }

            // Create the Columns and Rows
            for (int i = 0; i < s_GridDimension; i++)
            {
                // Create rows and columns
                s_minefield.ColumnDefinitions.Add(new ColumnDefinition());
                s_minefield.RowDefinitions.Add(new RowDefinition());

                // For each box create cell and button
                for (int j = 0; j < s_GridDimension; j++)
                {
                    // Create Buttons
                    Button btn = new Button();
                    btn.Content = "";
                    btn.FontWeight = FontWeights.Bold;

                    // Create Cell
                    s_MinesweeperCells[i, j] = new Cell(i, j, btn);
                    s_MinesweeperButtons[i, j] = btn;

                    // Set button position
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                    s_minefield.Children.Add(btn);

                    btn.PreviewMouseLeftButtonDown += s_MinesweeperCells[i, j].RevealCell;
                    btn.PreviewMouseRightButtonDown += s_MinesweeperCells[i, j].FlagCell;

                    // Set bomb
                    if (GameManager.s_BombArray[i, j] == true)
                    {
                        btn.PreviewMouseLeftButtonDown -= s_MinesweeperCells[i, j].RevealCell;
                        btn.PreviewMouseLeftButtonDown += s_MinesweeperCells[i, j].ClickedBomb;
                    }
                }
            }
            SetButtons();
        }

        // Set button data
        private static void SetButtons()
        {
            for (int i = 0; i < s_GridDimension; i++)
            {
                // for each box create cell and button
                for (int j = 0; j < s_GridDimension; j++)
                {
                    s_MinesweeperCells[i, j].SetComparison();
                    s_MinesweeperCells[i, j].CalculateNearbyBombs();
                }
            }
        }
    }
}