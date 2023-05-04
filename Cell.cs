﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using Minesweeper;

public class Cell
{
    Button button;
    
    // Cell[] adjacent = new Cell[8]; // implement to shortcut calculation of adjacent
    // int foundX;
    // int foundY;
    public int locationX;
    public int locationY;

    int adjacentBombs = 0;
    int comparisonCase = 0;
    
    bool isRevealed;
    bool isFlagged;
    bool isBomb;

    int x1;
    int x2;
    int x3;
    
    int y1;
    int y2;
    int y3;



    public Cell(int x, int y, Button btn)
	{
        button = btn;

        locationX = x;
        locationY = y;

        isBomb = false;
        isRevealed = false;
        isFlagged = false;

        comparisonCase = 0;

        x1 = locationX + 1;
        y1 = locationY + 1;
        
        x2 = locationX - 1;
        y2 = locationY - 1;

        x3 = locationX;
        y3 = locationY;
    }

    public void SetComparison()
    {
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
        // x max y min
        else if (x3 == Map.gridDimension - 1 && y3 == 0)
        {
            comparisonCase = 8;
        }
        // y max x min
        else if (y3 == Map.gridDimension - 1 && x3 == 0)
        {
            comparisonCase = 9;
        }
    }

    public void CheckAdjacent(object sender, MouseButtonEventArgs e)
    {
        RevealButton();      
    }

    public void FlagCell(object sender, MouseButtonEventArgs e)
    {
        
        if (!isFlagged && !isRevealed)
        {
            isFlagged = !isFlagged;
            button.Content = "|>";
        }
        else if (isFlagged && !isRevealed)
        {
            isFlagged = !isFlagged;
            button.Content = "";
        }
    }
    /*
    private Cell GetCell(Button btn)
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
    }*/

    public void CalculateNearbyBombs()
    {
        adjacentBombs = 0;
        switch (comparisonCase)
        {
            case 0:
                adjacentBombs = 99;
                break;
            // if x is fine and y is fine
            case 1:
                // check all x-1
                if (GameManager.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all x values
                if (GameManager.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all x+1
                if (GameManager.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x min
            case 2:
                // check all x
                if (GameManager.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all x+1
                if (GameManager.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x max
            case 3:
                // check all x-1
                if (GameManager.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all x values
                if (GameManager.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                break; 
            // y min
            case 4:
                // check all y values
                if (GameManager.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check all y+1
                if (GameManager.bombArray[x1, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                break; 
            // y max
            case 5:
                // check all y-1
                if (GameManager.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check all y values
                if (GameManager.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                break; 
            // x min y min
            case 6:
                    // check x+1 y values
                    if (GameManager.bombArray[x1, y3])
                    {
                        adjacentBombs++;
                    }
                    // check x+1 y+1
                    if (GameManager.bombArray[x1, y1])
                    {
                        adjacentBombs++;
                    }
                    // x y+1
                    if (GameManager.bombArray[x3, y1])
                    {
                        adjacentBombs++;
                    }
                break; 
            case 7:
                // check x y-1 values
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check x
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // x y+1
                if (GameManager.bombArray[x2, y2])
                {
                    adjacentBombs++;
                }
                break;
            // x max y min
            case 8:
                // check x-1 y++
                if (GameManager.bombArray[x2, y1])
                {
                    adjacentBombs++;
                }
                // check x-1 y
                if (GameManager.bombArray[x2, y3])
                {
                    adjacentBombs++;
                }
                // check x and y++
                if (GameManager.bombArray[x3, y1])
                {
                    adjacentBombs++;
                }
                break;
            case 9:
                // x min y max
                // check y x++
                if (GameManager.bombArray[x1, y3])
                {
                    adjacentBombs++;
                }
                // check y-1  x
                if (GameManager.bombArray[x3, y2])
                {
                    adjacentBombs++;
                }
                // check y-1 x++
                if (GameManager.bombArray[x1, y2])
                {
                    adjacentBombs++;
                }
                break;

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
            // x max y min
            case 8:
                // check x-1 y++
                Map.minesweeperCells[x2, y1].RevealButton();
                // check x-1 y
                Map.minesweeperCells[x2, y3].RevealButton();
                // check x and y++
                Map.minesweeperCells[x3, y1].RevealButton();
                break;
            case 9:
                // x min y max
                // check y x++
                Map.minesweeperCells[x1, y3].RevealButton();
                // check y-1  x
                Map.minesweeperCells[x3, y2].RevealButton();
                // check y-1 x++
                Map.minesweeperCells[x3, y2].RevealButton();
                break;
        }
    }



    public void RevealButton()
    {
        if (isRevealed)
        {
            return;
        }
        button.Background = Brushes.LightGoldenrodYellow;
        button.Content = adjacentBombs.ToString();
        isRevealed = true;
        if (adjacentBombs == 0)
        {
            NoneAdjacent();
        }
    }

    public void ClickedBomb(object sender, RoutedEventArgs e)
    {
        if (!isFlagged)
        {
            Button btn = sender as Button;
            btn.Foreground = Brushes.Blue;
            btn.Content = "AAAAAAAAAAAAAAAAAAAAAAAAA";
            MainWindow.game.EndGame(false);
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
