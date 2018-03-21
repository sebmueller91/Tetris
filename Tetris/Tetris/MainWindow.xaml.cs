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

namespace Tetris
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      World.nCols = 14;
      World.nRows = 30;

      this.Height = World.nRows * World.CELL_SIZE;
      this.Width = World.nCols * World.CELL_SIZE;
      
      World.Init(this);
      World.InitWindowGrid();
      World.Visualize();
    }
    public void OnKeyDownHandler(object sender, KeyEventArgs e)
    {
      if(e.Key == Key.R)
      {
        World.Reset();
        return;
      }


      if (e.Key == Key.Space && World.GameLost == false)
      {
        if (World.GamePaused == false)
        {
          World.GamePaused = true;
          World.Timer.Stop();
          MESSAGE.Text = "Game paused\n Press space to continue";
          MESSAGE.FontSize = 20;
          MESSAGE.Visibility = Visibility.Visible;
          

        } else
        {
          World.GamePaused = false;
          World.Timer.Start();
          MESSAGE.Visibility = Visibility.Hidden;
        }
        return;
      }

      if (World.ActiveObject == null) {
        return;
      }

      if (World.GamePaused == false && World.GameLost == false)
      {
        if (e.Key == Key.Left)
        {
          World.ActiveObject.MoveLeft();
        }
        else if (e.Key == Key.Right)
        {
          World.ActiveObject.MoveRight();
        }
        else if (e.Key == Key.Down)
        {
          bool GameLost;
          World.ActiveObject.MoveDown(out GameLost);
        }
        else if (e.Key == Key.Up)
        {
          World.ActiveObject.Rotate();
        }

        World.Visualize();
      }
    }
  }
}
