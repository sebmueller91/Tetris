using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris
{
  abstract class Brick
  {
    public Coord[] Coordinates { get; set; }
    public Brush Color { get; set; }

    /// <summary>
    /// returns false if it touches the grid, true otherwise
    /// </summary>
    /// <returns></returns>
    public bool MoveDown(out bool isGameOver)
    {
      isGameOver = false;

      for (int i = 0; i < Coordinates.Length; i++)
      {
        if (Coordinates[i].X <= 0 
          || (Coordinates[i].X-1 < World.nRows && World.Grid[Coordinates[i].X - 1, Coordinates[i].Y].Filled == true))
        {
          for (int j = 0; j < Coordinates.Length; j++)
          {
            if (Coordinates[j].X >= World.nRows)
            {
              isGameOver = true;
            }
          }
          return false;
        }
      }

      for (int i = 0; i < Coordinates.Length; i++)
      {
        Coordinates[i].X -= 1;
        //Coordinates[i].X = Coordinates[i].X - 1; // TODO
      }

      return true;
    }

    public void InitPosition()
    {
      for (int i = 0; i < Coordinates.Length; i++)
      {
        Coordinates[i] += new Tetris.Coord(World.nRows, World.nCols / 2 - 1); 
      }
    }

    public Coord[] Normalize()
    {
      Coord[] newCoordinates = new Coord[Coordinates.Length];

      for (int i = 0; i < Coordinates.Length; i++)
      {
        newCoordinates[i] = Coordinates[i] - new Tetris.Coord(World.nRows, World.nCols / 2 - 1);
      }

      return newCoordinates;
    }

    public bool MoveLeft()
    {
      for (int i = 0; i < Coordinates.Length; i++)
      {
        if (Coordinates[i].Y <= 0 || (Coordinates[i].Y > 0 
          && (Coordinates[i].X < World.nRows && World.Grid[Coordinates[i].X, Coordinates[i].Y-1].Filled == true)))
        {
          return false;
        }
      }

      for (int i = 0; i < Coordinates.Length; i++)
      {
        Coordinates[i].Y -= 1;
      }

      return true;
    }

    public bool MoveRight()
    {
      for (int i = 0; i < Coordinates.Length; i++)
      {
        if (Coordinates[i].Y >= World.nCols-1 || (Coordinates[i].Y < World.nCols-1
         && (Coordinates[i].X < World.nRows && World.Grid[Coordinates[i].X, Coordinates[i].Y + 1].Filled == true)))
        {
          return false;
        }
      }

      for (int i = 0; i < Coordinates.Length; i++)
      {
        Coordinates[i].Y += 1;
      }

      return true;
    }

    public void Merge()
    {
      for (int i = 0; i < Coordinates.Length; i++)
      {
        World.Grid[Coordinates[i].X, Coordinates[i].Y].Filled = true;
        World.Grid[Coordinates[i].X, Coordinates[i].Y].Color = this.Color;
      }
    }

    public bool Rotate()
    {

      if (this.GetType().ToString().Contains("BrickSq")) {
        return true;
      }

      return RotateBrickBy(Math.PI / 2.0);
    }

    /// <summary>
    /// Returns false if the rotated brick collides with the world, true otherwise
    /// If false is returned, no action on the coordinates is performed
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public bool RotateBrickBy(double angle)
    {
      Coord[] newCoordinates = new Coord[Coordinates.Length];

      double mX = 0, mY = 0;
      for (int i = 0; i < Coordinates.Length; i++)
      {
        newCoordinates[i] = new Coord(0,0);
        mX += Coordinates[i].X;
        mY += Coordinates[i].Y;
      }
      mX /= ((double)Coordinates.Length);
      mY /= ((double)Coordinates.Length);

      for (int i = 0; i < Coordinates.Length; i++)
      {
        int tmpX = Coordinates[i].X - Convert.ToInt32(mX);
        int tmpY = Coordinates[i].Y - Convert.ToInt32(mY);

        newCoordinates[i].X = Convert.ToInt32(Math.Cos(angle)) * tmpX - Convert.ToInt32(Math.Sin(angle)) * tmpY;
        newCoordinates[i].Y = Convert.ToInt32(Math.Sin(angle)) * tmpX + Convert.ToInt32(Math.Cos(angle)) * tmpY;

        newCoordinates[i].X += Convert.ToInt32(mX);
        newCoordinates[i].Y += Convert.ToInt32(mY);

        if (newCoordinates[i].X < 0 || newCoordinates[i].Y < 0 || newCoordinates[i].Y >= World.nCols)
        {
          return false;
        }

        if (newCoordinates[i].X < World.nRows && World.Grid[newCoordinates[i].X, newCoordinates[i].Y].Filled == true) {
          return false;
        }
      }

      Coordinates = newCoordinates;
      return true;
    }
  }
}
