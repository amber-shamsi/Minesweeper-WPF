using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using Minesweeper;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Automation;

public class GameManager
{
	public static bool[,] flagArray;
    public static bool[,] bombArray;

    TextBox chooseDimension = new TextBox();

    public static Grid myGrid;

    static Window minesweeperWindow = Application.Current.MainWindow;

    static string loseEffectString = "C:\\Users\\pranc\\Dropbox\\_Employment\\_The Software Institute 17-04-2023\\C# Projects\\Minesweeper\\wilhelmScream.mp3";
    static string winSoundEffect = "C:\\Users\\pranc\\Dropbox\\_Employment\\_The Software Institute 17-04-2023\\C# Projects\\Minesweeper\\winSound.mp3";
    static string winVideoString = "C:\\\\Users\\\\pranc\\\\Dropbox\\\\_Employment\\\\_The Software Institute 17-04-2023\\\\C# Projects\\\\Minesweeper\\\\winVideo.mp4";
    static string loseVideoString = "C:\\\\Users\\\\pranc\\\\Dropbox\\\\_Employment\\\\_The Software Institute 17-04-2023\\\\C# Projects\\\\Minesweeper\\\\loseVideo.mp4";

    MediaPlayer winnerPlayer    = new MediaPlayer();
    MediaPlayer winVideo        = new MediaPlayer();
    MediaElement mediaElement   = new MediaElement();
    MediaPlayer loserPlayer     = new MediaPlayer();
    VideoDrawing videoDrawer    = new VideoDrawing();

    static Uri winEffectUri     = new Uri(winSoundEffect);
    static Uri winVideoUri      = new Uri(winVideoString);
    static Uri loseEffectUri    = new Uri(loseEffectString);
    static Uri loseVideoUri     = new Uri(loseVideoString);

    public GameManager()
	{
		
	}

	public void EndGame(bool win)
	{

		if (win)
		{
            winnerPlayer.Volume = 0.25;
            winnerPlayer.Open(winEffectUri);
            winnerPlayer.Play();

            minesweeperWindow.Content = null;

            MainWindow.game.DisplayStartScreen();

            DisplayWinScreen();
        }
		else
		{
            loserPlayer.Open(loseEffectUri);
            loserPlayer.Play();

            minesweeperWindow.Content = null;

            MainWindow.game.DisplayStartScreen();
        }
	}

    private void DisplayWinScreen()
    {
        DisplayVideoPopup(winVideoUri);
    }

    private void DisplayLoseScreen()
    {
        DisplayVideoPopup(loseVideoUri);
    }

    private void DisplayVideoPopup(Uri videoUri)
    {
        Window popup = new Window();

        winVideo.Open(videoUri);

        mediaElement.LoadedBehavior = MediaState.Manual;
        mediaElement.Source = videoUri;

        popup.Content = mediaElement;
        popup.Show();

        mediaElement.Play();
    }

    public void DisplayStartScreen()
    {
        Grid myGrid = new Grid();
        myGrid.Width = 700;
        myGrid.Height = 700;
        myGrid.HorizontalAlignment = HorizontalAlignment.Center;
        myGrid.VerticalAlignment = VerticalAlignment.Center;

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
        fileGameGenerator.Content = "I do literally\n  nothing.\n\n Just like my\n   creator.";
        fileGameGenerator.FontSize = 20;

        randomGameGenerator.Content = " Randomly\nGenerated\n    Game";
        randomGameGenerator.FontSize = 20;

        chooseDimension.Text = "Type your map size here.\n\n10 to 50";
        chooseDimension.FontSize = 20;
        chooseDimension.HorizontalContentAlignment = HorizontalAlignment.Center;
        chooseDimension.VerticalContentAlignment = VerticalAlignment.Center;
        chooseDimension.TextWrapping = TextWrapping.Wrap;

        // set click function
        randomGameGenerator.Click += AttemptGameInitialisation;

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

    private void AttemptGameInitialisation(object sender, RoutedEventArgs e)
    {
        // when clicked, pass the textfiel if it is an integer
        // if not an integer, display "pick a real number, fool"
        try
        {
            int dimension;
            Int32.TryParse(chooseDimension.Text.ToString(), out dimension);
            flagArray = new bool[dimension, dimension];
            if (dimension > 9 && dimension <= 50)
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
            chooseDimension.Text = "try again";
        }

    }

    // check dimensions are accetptable ie 
    private static void StartButtonClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
