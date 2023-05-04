using Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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
namespace Minesweeper
{

    public partial class MainWindow : Window
    {
        List<object> startMenuObjects = new List<object>();
        TextBox chooseDimension = new TextBox();
        public static Grid myGrid;
        public static GameManager game = new GameManager();
        public MainWindow()
        {
            InitializeComponent();

            // Display Start Screen

            game.DisplayStartScreen();

        }

    }
}
