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
	public static bool[,] s_FlagArray;
    public static bool[,] s_BombArray;

    TextBox chooseDimension = new TextBox();

    public static Grid s_MenuGrid;

    static Window s_minesweeperWindow = Application.Current.MainWindow;

    static string s_loseEffectString = "C:\\Users\\pranc\\Dropbox\\_Employment\\_The Software Institute 17-04-2023\\C# Projects\\Minesweeper\\wilhelmScream.mp3";
    static string s_winSoundEffect = "C:\\Users\\pranc\\Dropbox\\_Employment\\_The Software Institute 17-04-2023\\C# Projects\\Minesweeper\\winSound.mp3";
    static string s_winVideoString = "C:\\\\Users\\\\pranc\\\\Dropbox\\\\_Employment\\\\_The Software Institute 17-04-2023\\\\C# Projects\\\\Minesweeper\\\\winVideo.mp4";
    static string s_loseVideoString = "C:\\\\Users\\\\pranc\\\\Dropbox\\\\_Employment\\\\_The Software Institute 17-04-2023\\\\C# Projects\\\\Minesweeper\\\\loseVideo.mp4";

    MediaPlayer winnerPlayer = new MediaPlayer();
    MediaPlayer winVideo = new MediaPlayer();
    MediaElement mediaElement   = new MediaElement();
    MediaPlayer loserPlayer = new MediaPlayer();
    VideoDrawing videoDrawer = new VideoDrawing();

    static Uri s_winEffectUri = new Uri(s_winSoundEffect);
    static Uri s_winVideoUri = new Uri(s_winVideoString);
    static Uri s_loseEffectUri = new Uri(s_loseEffectString);
    static Uri s_loseVideoUri = new Uri(s_loseVideoString);

    public GameManager()
	{
		
	}

	public void EndGame(bool win)
	{

		if (win)
		{
            winnerPlayer.Volume = 0.25;
            winnerPlayer.Open(s_winEffectUri);
            winnerPlayer.Play();

            s_minesweeperWindow.Content = null;

            MainWindow.game.DisplayStartScreen();

            DisplayWinScreen();
        }
		else
		{
            loserPlayer.Open(s_loseEffectUri);
            loserPlayer.Play();

            s_minesweeperWindow.Content = null;

            MainWindow.game.DisplayStartScreen();
        }
	}

    private void DisplayWinScreen()
    {
        DisplayVideoPopup(s_winVideoUri);
    }

    private void DisplayLoseScreen()
    {
        DisplayVideoPopup(s_loseVideoUri);
    }

    private void DisplayVideoPopup(Uri videoUri)
    {
        Window popup = new Window();

        winVideo.Open(videoUri);

        mediaElement.LoadedBehavior = MediaState.Manual;
        mediaElement.Source         = videoUri;

        popup.Content = mediaElement;
        popup.Show();

        mediaElement.Play();
    }

    public void DisplayStartScreen()
    {
        Grid myGrid     = new Grid();
        myGrid.Width    = 700;
        myGrid.Height   = 700;
        myGrid.HorizontalAlignment  = HorizontalAlignment.Center;
        myGrid.VerticalAlignment    = VerticalAlignment.Center;

        // Create columns and rows
        for (int i = 0; i < 5; i++)
        {
            myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            myGrid.RowDefinitions.Add(new RowDefinition());
        }

        // create buttons
        Button randomGameGenerator  = new Button();
        Button fileGameGenerator    = new Button();
        chooseDimension             = new TextBox();

        // set text
        fileGameGenerator.Content   = "I do literally\n  nothing.\n\n Just like my\n   creator.";
        fileGameGenerator.FontSize  = 20;

        randomGameGenerator.Content     = " Randomly\nGenerated\n    Game";
        randomGameGenerator.FontSize    = 20;

        chooseDimension.Text        = "Type your map size here.\n\n10 to 50";
        chooseDimension.FontSize    = 20;
        chooseDimension.HorizontalContentAlignment  = HorizontalAlignment.Center;
        chooseDimension.VerticalContentAlignment    = VerticalAlignment.Center;
        chooseDimension.TextWrapping                = TextWrapping.Wrap;

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
        s_minesweeperWindow.Content = myGrid;
        s_minesweeperWindow.Show();
    }

    private void AttemptGameInitialisation(object sender, RoutedEventArgs e)
    {
        // when clicked, pass the textfiel if it is an integer
        // if not an integer, display "pick a real number, fool"
        try
        {
            int dimension;

            Int32.TryParse(chooseDimension.Text.ToString(), out dimension);

            s_FlagArray = new bool[dimension, dimension];

            if (dimension > 9 && dimension <= 50)
            {
                Map.CreateGame(s_minesweeperWindow, dimension);
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
}
