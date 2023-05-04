using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using Minesweeper;

public class GameManager
{
	public static bool[,] flagArray;
    public static bool[,] bombArray;

    TextBox chooseDimension = new TextBox();
    List<object> startMenuObjects = new List<object>();
    List<object> gameObjects = new List<object>();
    List<object> endGameObjects = new List<object>();

    public static Grid myGrid;
    static Window minesweeperWindow = Application.Current.MainWindow;

    public GameManager()
	{
		
	}

	public void EndGame(bool win)
	{
		if (win)
		{

		}
		else
		{

		}
	}

    public void DisplayStartScreeen()
    {
        Grid myGrid = new Grid();
        myGrid.Width = 700;
        myGrid.Height = 700;
        myGrid.HorizontalAlignment = HorizontalAlignment.Center;
        myGrid.VerticalAlignment = VerticalAlignment.Bottom;
        myGrid.ShowGridLines = true;

        // Create columns and rows
        for (int i = 0; i < 5; i++)
        {
            myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            myGrid.RowDefinitions.Add(new RowDefinition());
        }

        // create buttons
        Button randomGameGenerator = new Button();
        Button fileGameGenerator = new Button();
        chooseDimension = new TextBox();

        // set text
        fileGameGenerator.Content = "NOT YET IMPLEMENTED";
        randomGameGenerator.Content = "Randomly Generated Game";
        chooseDimension.Text = "Type your map size here.\n10 to 50";
        chooseDimension.FontSize = 20;
        chooseDimension.TextWrapping = TextWrapping.Wrap;
        // set click function
        randomGameGenerator.Click += AttemptGameInitialisation;
        fileGameGenerator.Click += AttemptFileInitialisation;

        // set location
        Grid.SetColumn(randomGameGenerator, 1);
        Grid.SetColumn(fileGameGenerator, 3);
        Grid.SetColumn(chooseDimension, 2);

        Grid.SetRow(fileGameGenerator, 2);
        Grid.SetRow(randomGameGenerator, 2);
        Grid.SetRow(chooseDimension, 2);

        // Add elements to grid children
        myGrid.Children.Add(randomGameGenerator);
        myGrid.Children.Add(fileGameGenerator);
        myGrid.Children.Add(chooseDimension);

        // Add grid to window content
        minesweeperWindow.Content = myGrid;
        minesweeperWindow.Show();

        // add elements to list
        startMenuObjects.Add(randomGameGenerator);
        startMenuObjects.Add(fileGameGenerator);
        startMenuObjects.Add(chooseDimension);
    }

    private void AttemptFileInitialisation(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AttemptGameInitialisation(object sender, RoutedEventArgs e)
    {
        // when clicked, pass the textfiel if it is an integer
        // if not an integer, display "pick a real number, fool"
        try
        {
            int dimension;
            Int32.TryParse(chooseDimension.Text.ToString(), out dimension);

            if (dimension > 0 && dimension <= 50)
            {
                Map.CreateGame(minesweeperWindow, dimension);
            }
            else
            {
                chooseDimension.Text = "Put a better number idiot";
            }

        }
        catch
        {
            chooseDimension.Text = "Put a better number idiot";
        }

    }


    // check dimensions are accetptable ie 
    private static void StartButtonClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
