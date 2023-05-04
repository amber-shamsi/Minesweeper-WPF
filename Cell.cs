using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Windows.Input;
using System.Runtime.CompilerServices;

public class Cell
{

    bool isBomb;
    Cell[] adjacent = new Cell[8];
    public int locationX;
    public int locationY;
    static int adjacentBombs = 0;
    Button button;
    int comparisonCase;
    bool isRevealed;
    bool isFlagged;
    static int foundX;
    static int foundY;

    int x1;
    int y1;

    int x2;
    int y2;

    int x3;
    int y3;

    public Cell(int x, int y, Button btn)
	{
        locationX = x;
        locationY = y;
        isBomb = false;
        button = btn;
        isRevealed = false;
        comparisonCase = 0;

        x1 = locationX + 1;
        y1 = locationY + 1;
        
        x2 = locationX - 1;
        y2 = locationY - 1;

        x3 = locationX;
        y3 = locationY;

        isFlagged = false;

    }

    public void CheckAdjacent(object sender, MouseButtonEventArgs e)
    {
        Trace.WriteLine("clackd");
        RevealButton();      
        
    }

    private void HoverCells()
    {
        throw new NotImplementedException();
    }

    public void FlagCell(object sender, MouseButtonEventArgs e)
    {
        isFlagged = !isFlagged;
        if (isFlagged)
        {
            button.Content = "|>";
        }
        else
        {
            button.Content = "|>";
        }
    }

    private static Cell GetCell(Button btn)
    {

        try {
            for (int i = 0; i < Map.gridDimension; i++)
            {
                for (int j = 0; j < Map.gridDimension; j++)
                {
                    if (Map.minesweeperButtons[i, j] == btn)
                    {
                        Trace.WriteLine(i.ToString() + j.ToString());
                        foundX = i;
                        foundY = j;
                        return Map.GetCell(i, j);
                       
                    }
                }
            }
            return null;
        }
        catch { return null; }
        
    }

    void CalculateNearbyBombs()
    {
        if(isRevealed)
        {
            return;
        }

        isRevealed = true;

        adjacentBombs = 0;
        
        comparisonCase = 0;
        
        // good
        if (x3 > 0 && x3 < Map.gridDimension - 1 && y3 > 0 && y3 < Map.gridDimension - 1)
        {
            comparisonCase = 1;
        }
        // x min 
        else if (x3 == 0 && y3 > 0 && y3 < Map.gridDimension - 1)
        {
            comparisonCase = 2;
        }
        // x max
        else if (x3 == Map.gridDimension - 1 && y3 > 0 && y3 < Map.gridDimension - 1)
        {
            comparisonCase = 3;
        }
        // y min
        else if (y3 == 0 && x3 > 0 && x3 < Map.gridDimension - 1)
        {
            comparisonCase = 4;
        }
        // y is max
        else if (x3 < Map.gridDimension - 1 && x3 > 0 && y3 == Map.gridDimension - 1)
        {
            comparisonCase = 5;
        }
        // x min y min
        else if (x3 == 0 && y3 == 0)
        {
            comparisonCase = 6;
        }
        // x max y max
        else if (x3 == Map.gridDimension - 1 && y3 == Map.gridDimension - 1)
        {

            comparisonCase = 7;
        }

        switch (comparisonCase)
        {
            case 0:
                break;
            // if x is fine and y is fine
            case 1:
                // check all x-1
                if (Map.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all x values
                if (Map.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all x+1
                if (Map.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x min
            case 2:
                // check all x
                if (Map.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all x+1
                if (Map.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x max
            case 3:
                // check all x-1
                if (Map.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all x values
                if (Map.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                break; 
            // y min
            case 4:
                // check all y values
                if (Map.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all y+1
                if (Map.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                break; 
            // y max
            case 5:
                // check all y-1
                if (Map.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all y values
                if (Map.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                if (Map.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x min y min
            case 6:
                    // check x+1 y values
                    if (Map.bombArray[x1, y3])
                    {
                        adjacentBombs++;
                    }
                    // check x+1 y+1
                    if (Map.bombArray[x1, y1])
                    {
                        adjacentBombs++;
                    }
                    // x y+1
                    if (Map.bombArray[x3, y1])
                    {
                        adjacentBombs++;
                    }
                break; 
            // x max y max
            case 7:
                // check x y-1 values
                if (Map.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check x
                if (Map.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // x y+1
                if (Map.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                break;
        }
        
        button.Content = adjacentBombs.ToString();

        if (adjacentBombs == 0)
        {
            NoneAdjacent();   
        }
    }

    private Button GetButton(int foundX, int foundY)
    {
        return Map.GetCell(foundX, foundY).button;
    }

    void NoneAdjacent()
    {
        switch (comparisonCase)
        {
            case 0:
                break;
            // if x is fine and y is fine
            case 1:
                // check all x-1
                Map.minesweeperCells[x2, y1].RevealButton();
                Map.minesweeperCells[x2, y2].RevealButton();     
                Map.minesweeperCells[x2, y3].RevealButton();
                // check all x
                Map.minesweeperCells[x3, y1].RevealButton();
                Map.minesweeperCells[x3, y2].RevealButton(); 
                // check all x+1
                Map.minesweeperCells[x1, y1].RevealButton();
                Map.minesweeperCells[x1, y2].RevealButton();
                Map.minesweeperCells[x1, y3].RevealButton();
                break;
            // x min
            case 2:
                // check all x
                Map.minesweeperCells[x3, y1].RevealButton();
                Map.minesweeperCells[x3, y2].RevealButton();
                // check all x+1
                Map.minesweeperCells[x1, y1].RevealButton();
                Map.minesweeperCells[x1, y2].RevealButton();
                Map.minesweeperCells[x1, y3].RevealButton();
                break;
            // x max
            case 3:
                // check all x-1
                Map.minesweeperCells[x2, y1].RevealButton();
                Map.minesweeperCells[x2, y2].RevealButton();
                Map.minesweeperCells[x2, y3].RevealButton();
                // check all x
                Map.minesweeperCells[x3, y1].RevealButton();
                Map.minesweeperCells[x3, y2].RevealButton();
                break;
            // y min
            case 4:
                // check all y values
                Map.minesweeperCells[x1, y3].RevealButton();
                Map.minesweeperCells[x2, y3].RevealButton();
                // check all y+1
                Map.minesweeperCells[x1, y1].RevealButton();
                Map.minesweeperCells[x2, y1].RevealButton();
                Map.minesweeperCells[x3, y1].RevealButton();
                break;
            // y max
            case 5:
                // check all y+1
                Map.minesweeperCells[x1, y2].RevealButton();
                Map.minesweeperCells[x2, y2].RevealButton();
                Map.minesweeperCells[x3, y2].RevealButton();
                // check all y values
                Map.minesweeperCells[x1, y3].RevealButton();
                Map.minesweeperCells[x2, y3].RevealButton();
                break;
            // x min y min
            case 6:
                // check x+1 y values
                Map.minesweeperCells[x1, y3].RevealButton();
                // check x+1 y+1
                Map.minesweeperCells[x1, y1].RevealButton();
                // x y+1
                Map.minesweeperCells[x3, y1].RevealButton();
                break;
            // x max y max
            case 7:
                // check x y-1 values
                Map.minesweeperCells[x3, y2].RevealButton();
                // check x
                Map.minesweeperCells[x2, y3].RevealButton();
                // x y+1
                Map.minesweeperCells[x2, y2].RevealButton();
                break;
        }
    }



    public void RevealButton()
    {
        CalculateNearbyBombs();
        
        button.Background = Brushes.LightGoldenrodYellow;
    }

    public void ClickedBomb(object sender, RoutedEventArgs e)
    {
        if (!isFlagged)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.Blue;
            Trace.WriteLine("Bomb idiot");
        }
        
    }


    public void PlaceBomb(Button element)
    {
        isBomb = true;
        // element.MouseDown -= CheckAdjacent;
        // element.MouseDown += ClickedBomb;
    }

    public bool CheckBomb()
    {
        return isBomb;
    }

}
