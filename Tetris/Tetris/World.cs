using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tetris
{
  class World
  {

    #region Variables
    public static Brush[] Colors = new Brush[]
    {
      Brushes.Purple,
      Brushes.Blue,
      Brushes.Green,
      Brushes.Red,
      Brushes.Yellow,
      Brushes.Cyan,
      Brushes.White
    };

    private static Random rand = new Random();

    public static DispatcherTimer Timer, TimeMeasurement;

    private static int secCounter = 0;

    public static Block[,] Grid { get; set; }
    public static int nCols { get; set; }
    public static int nRows { get; set; }
    public static Brick ActiveObject { get; set; }
    public static Brick NextActiveObject { get; set; }

    private static MainWindow mw;

    private static int[] levelPoints = new int[] { 40, 100, 300, 1200 };

    public const int CELL_SIZE = 20;

    public static int PreviewSize { get; set; }

    private static Label[,] Labels, previewLabels;

    private static int IntervalLength = 200;

    private static int m_Points;
    public static int Points
    {
      get
      {
        return m_Points;
      }
      set
      {
        m_Points = value;
        mw.SCORE.Text = m_Points.ToString("000 000 000");
      }
    }

    private static int m_destroyed_rows;
    public static int Destroyed_Rows
    {
      get
      {
        return m_destroyed_rows;
      }
      set
      {
        m_destroyed_rows = value;
        mw.ROWS_DESTROYED.Text = m_destroyed_rows.ToString();
      }
    }

    public static bool GameLost { get; set; }

    public static bool GamePaused { get; set; }

    private static int m_Level;
    public static int Level
    {
      get
      {
        return m_Level;
      }
      set
      {
        m_Level = value;
        mw.LEVEL.Text = m_Level.ToString("0");
      }
    }
    #endregion Variables

    internal static void Reset()
    {
      for (int i = 0; i < nRows; i++)
      {
        for (int j = 0; j < nCols; j++)
        {
          Grid[i, j].Color = Brushes.Black;
          Grid[i, j].Filled = false;
        }
      }
      for (int i = 0; i < PreviewSize; i++)
      {
        for (int j = 0; j < PreviewSize; j++)
        {
          previewLabels[i, j].Background = Brushes.Black;
        }
      }
      ActiveObject = null;
      NextActiveObject = null;

      Visualize();

      secCounter = 0;
      NextActiveObject = null;
      Points = 0;
      IntervalLength = 200;
      Destroyed_Rows = 0;

      GameLost = false;
      Level = 0;

      mw.MESSAGE.Text = "Press space to start";
      mw.MESSAGE.Visibility = Visibility.Visible;
      mw.MESSAGE.FontSize = 25;
      mw.MESSAGE.Foreground = Brushes.Orange;

      Timer.Tick -= TimeStep;
      TimeMeasurement.Tick -= UpdateTime;
      Timer = null;
      TimeMeasurement = null;
      
      World.Init(mw);      
    }

    public static void DrawPreview()
    {
      Coord[] coordinates = NextActiveObject.Normalize();

      for (int i = 0; i < PreviewSize; i++)
      {
        for (int j = 0; j < PreviewSize; j++)
        {
          previewLabels[i, j].Background = Brushes.Black;
        }
      }

      for (int i = 0; i < coordinates.Length; i++)
      {
        previewLabels[PreviewSize-(coordinates[i].X+ PreviewSize/2), coordinates[i].Y+ PreviewSize/2].Background = NextActiveObject.Color;
      }
    }

    public static Brick CreateObject()
    {
      Brick newObject = null;

      switch (rand.Next(0, 7))
      {

        case 0:
          newObject = new BrickL();
          newObject.Color = World.Colors[0];
          break;
        case 1:
          newObject = new BrickI();
          newObject.Color = World.Colors[1];
          break;
        case 2:
          newObject = new BrickT();
          newObject.Color = World.Colors[2];
          break;
        case 3:
          newObject = new BrickSq();
          newObject.Color = World.Colors[3];
          break;
        case 4:
          newObject = new BrickZ();
          newObject.Color = World.Colors[4];
          break;
        case 5:
          newObject = new BrickZ2();
          newObject.Color = World.Colors[5];
          break;
        case 6:
          newObject = new BrickL2();
          newObject.Color = World.Colors[6];
          break;
        default:
          MessageBox.Show("oops");
          break;
      }

      newObject.RotateBrickBy(rand.Next(0, 4) * (Math.PI / 2.0));

      return newObject;
    }
    public static void TimeStep(object sender, EventArgs e)
    {
      if (GamePaused == true || GameLost == true)
      {
        return;
      }
      if (ActiveObject == null)
      {
        if (NextActiveObject == null)
        {
          ActiveObject = CreateObject();
          NextActiveObject = CreateObject();
        } else
        {
          ActiveObject = NextActiveObject;
          NextActiveObject = CreateObject();
        }
        DrawPreview();
      }
      else
      {
        bool isGameOver;
        if (ActiveObject.MoveDown(out isGameOver) == false)
        {
          if (isGameOver)
          {
            Timer.Stop();
            GameLost = true;
            mw.MESSAGE.Text = "GAME OVER";
            mw.MESSAGE.FontSize = 35;
            mw.MESSAGE.Foreground = Brushes.Red;
            mw.MESSAGE.Visibility = Visibility.Visible;

            //Highscores w = new Highscores();
            //w.Show();
            return;
          }
          ActiveObject.Merge();
          ActiveObject = null;
          int n_rows_deleted = CleanFullRows();
          if (n_rows_deleted > 0)
          {
            Points += n_rows_deleted * Level * levelPoints[n_rows_deleted - 1];
            Destroyed_Rows += n_rows_deleted;
          }
        }
      }

      Visualize();
    }

    public static void Visualize()
    {
      for (int i = 0; i < nRows; i++)
      {
        for (int j = 0; j < nCols; j++)
        {
          // draw background
          Labels[i, j].Background = Brushes.Black;
          if (Grid[i, j].Filled == true)
          {
            // draw world block
            Labels[i, j].Background = Grid[i, j].Color;
          }
        }
      }

      // draw active object
      if (ActiveObject != null)
      {
        for (int i = 0; i < ActiveObject.Coordinates.Length; i++)
        {
          if (ActiveObject.Coordinates[i].X < nRows)
          {
            Labels[ActiveObject.Coordinates[i].X, ActiveObject.Coordinates[i].Y].Background = ActiveObject.Color;
          }
        }
      }
    }

    /// <summary>
    /// returns the numbre of columns that have been deleted
    /// </summary>
    /// <returns></returns>
    public static int CleanFullRows()
    {
      int rowsDeleted = 0;

      for (int row = 0; row < nRows; row++)
      {
        // check if column is full
        bool full = true;
        for (int col = 0; col < nCols; col++)
        {
          if (Grid[row, col].Filled == false)
          {
            full = false;
            continue;
          }
        }

        if (full == true)
        {
          rowsDeleted++;
          for (int i = row + 1; i < nRows; i++)
          {
            for (int j = 0; j < nCols; j++)
            {
              Grid[i - 1, j].Filled = Grid[i, j].Filled;
              Grid[i - 1, j].Color = Grid[i, j].Color;

              Grid[i, j].Filled = false;
              Grid[i, j].Color = Brushes.Black;
            }
          }
          row--;
        }
      }

      return rowsDeleted;
    }

    public static void UpdateTime(object sender, EventArgs e)
    {
      if (GamePaused == true || GameLost == true)
      {
        return;
      }

      secCounter++;
      if ((secCounter%100) % 60 == 0)
      {
        secCounter = secCounter + 40;
      }


      //change timer interval every n seconds (e.g. 30)
      if ((secCounter % 100) % 30 == 0)
      {
        //TimeSpan newTimeInterval = new TimeSpan(250000);
        IntervalLength  = Convert.ToInt32(Convert.ToDouble(IntervalLength)*0.85);
        Timer.Interval = new TimeSpan(0, 0, 0, 0, IntervalLength);
        Level++;
      }

      mw.TIME.Text = secCounter.ToString("00:00");
    }

    public static void Init(MainWindow main_window)
    {
      mw = main_window;

      mw.TIME.Text = secCounter.ToString("00:00");

      TimeMeasurement = new DispatcherTimer();
      TimeMeasurement.Tick += UpdateTime;
      TimeMeasurement.Interval = new TimeSpan(0, 0, 0, 0, 1000);

      Timer = new DispatcherTimer();
      Timer.Tick += new EventHandler(TimeStep);
      Timer.Interval = new TimeSpan(0, 0, 0, 0, IntervalLength);

      Grid = new Block[nRows, nCols];
      for (int i = 0; i < nRows; i++)
      {
        for (int j = 0; j < nCols; j++)
        {
          Grid[i, j] = new Block();
          Grid[i, j].Filled = false;
          Grid[i, j].Color = Brushes.Black;
        }
      }

      PreviewSize = 7;

      GameLost = false;
      GamePaused = true;

      Destroyed_Rows = 0;
      Points = 0;
      Level = 1;

      Timer.Start();
      TimeMeasurement.Start();
    }

    public static void InitWindowGrid()
    {
      mw.PREVIEW_GRID.Rows = PreviewSize;
      mw.PREVIEW_GRID.Columns = PreviewSize;

      mw.PREVIEW_GRID.Width = PreviewSize * 15;
      mw.PREVIEW_GRID.Height = PreviewSize * 15;

      previewLabels = new Label[PreviewSize, PreviewSize];

      for (int i = 0; i < PreviewSize; i++)
      {
        for (int j = 0; j < PreviewSize; j++)
        {
          previewLabels[i, j] = new Label();
          previewLabels[i, j].BorderBrush = Brushes.Silver;
          previewLabels[i, j].BorderThickness = new Thickness(0.5);
          mw.PREVIEW_GRID.Children.Add(previewLabels[i,j]);
          previewLabels[i, j].Background = Brushes.Black;
        }
      }

      mw.MY_GRID.Rows = nRows;
      mw.MY_GRID.Columns = nCols;

      mw.Width = nCols * CELL_SIZE + (nCols * CELL_SIZE) / 2;

      Labels = new Label[nRows, nCols];

      for (int i = nRows - 1; i >= 0; i--)
      {
        for (int j = 0; j < nCols; j++)
        {
          Label Label = new Label();

          Label.BorderBrush = Brushes.Silver;
          Label.BorderThickness = new Thickness(0.5);
          mw.MY_GRID.Children.Add(Label);
          Labels[i, j] = Label;
        }
      }
    }
  }
}
