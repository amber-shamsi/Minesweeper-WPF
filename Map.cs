using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;
using Minesweeper;
using System.Linq;

public class Map
{
    public static Cell[,] minesweeperCells;
    public static Button[,] minesweeperButtons;
    
    static int windowHeight = 1000;
    static int windowWidth = 800;
    static bool usingTextFile = false;
    public static int gridDimension = 15;
    static Grid minefield = new Grid();
    static Window gameWindow;
    public Map()
	{

	}

    public static void CreateGame(Window gWindow, int dimension)
    {
        gameWindow = gWindow;

        GenerateButtonField(dimension);
        GenerateHeader();
    }

    public static Cell GetCell(int x, int y)
    {
        return minesweeperCells[x, y];
    }
    private static void GenerateHeader()
    {
        // Headline text
        TextBlock header = new TextBlock();
        header.Text = "Welcome To the Danger Zone";
        header.FontWeight = FontWeights.Bold;
        header.Foreground = Brushes.Red;
        header.FontSize = 40;
        header.Height = 55;
        header.Background = Brushes.Green;
        header.VerticalAlignment = VerticalAlignment.Top;
        header.HorizontalAlignment = HorizontalAlignment.Center;


        // header grid
        Grid heading = new Grid();
        heading.Width = windowWidth;
        // heading.Height = 100;
        heading.Background = Brushes.Yellow;
        heading.VerticalAlignment = VerticalAlignment.Top;
        heading.HorizontalAlignment = HorizontalAlignment.Center;
        heading.Children.Add(header);

        // create template and add stack, display
        DataTemplate dataTemplate = new DataTemplate();
        DockPanel dockPanel = new DockPanel();
        DockPanel.SetDock(heading, Dock.Top);
        DockPanel.SetDock(minefield, Dock.Bottom);
        dockPanel.Children.Add(heading);
        dockPanel.Children.Add(minefield);

        dockPanel.VerticalAlignment = VerticalAlignment.Top;
        dockPanel.HorizontalAlignment = HorizontalAlignment.Center;



        // Add the Grid as the Content of the Parent Window Object
        gameWindow.Title = "hardcore gamer mode detected";
        gameWindow.Content = dockPanel;
        gameWindow.Show();
    }

    private static void GenerateButtonField(int dimension)
    {
        gridDimension = dimension;
        minesweeperCells = new Cell[gridDimension, gridDimension];
        minesweeperButtons = new Button[gridDimension, gridDimension];
        GameManager.bombArray = new bool[gridDimension, gridDimension];

        // Create the Grid
        minefield.Width = 750;
        minefield.Height = 750;
        minefield.HorizontalAlignment = HorizontalAlignment.Center;
        minefield.VerticalAlignment = VerticalAlignment.Bottom;
        minefield.ShowGridLines = true;

        // Generate bombs 
        int bombsToPlace = (int)(Math.Ceiling(((double)gridDimension * gridDimension * 0.2)));
        int p = 0;
        while (p < bombsToPlace)
        {
            Random random = new Random();
            int x = random.Next(0, gridDimension - 1);
            int y = random.Next(0, gridDimension - 1);

            if (GameManager.bombArray[x,y] != true)
            {
                Trace.WriteLine("Attempting " + p + " Bomb Placement: " + x + " " + y);
                GameManager.bombArray[x, y] = true;
                p++;
            }
            
        }

        // Create the Columns and Rows
        for (int i = 0; i < gridDimension; i++)
        {
            // create rows and columns
            minefield.ColumnDefinitions.Add(new ColumnDefinition());
            minefield.RowDefinitions.Add(new RowDefinition());

            // for each box create cell and button
            for (int j = 0; j < gridDimension; j++)
            {
                
                // create button
                Button btn = new Button();
                // create Cell
                minesweeperCells[i, j] = new Cell(i, j, btn);
                minesweeperButtons[i, j] = btn;
                btn.Content = "";
                // Set position of button
                Grid.SetColumn(btn, j);
                Grid.SetRow(btn, i);
                minefield.Children.Add(btn);
                btn.PreviewMouseLeftButtonDown += minesweeperCells[i, j].RevealCell;
                btn.PreviewMouseRightButtonDown += minesweeperCells[i, j].FlagCell;
                if (GameManager.bombArray[i,j] == true)
                {
                    btn.PreviewMouseLeftButtonDown -= minesweeperCells[i, j].RevealCell;
                    btn.PreviewMouseLeftButtonDown += minesweeperCells[i, j].ClickedBomb;
                }
            }
        }
        SetButtons();
    }

    private static void SetButtons()
    {
        for (int i = 0; i < gridDimension; i++)
        {
            // for each box create cell and button
            for (int j = 0; j < gridDimension; j++)
            {
                minesweeperCells[i, j].SetComparison();
                minesweeperCells[i, j].CalculateNearbyBombs();
            }
        }
    }
}
