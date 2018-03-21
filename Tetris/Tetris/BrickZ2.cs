using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
  class BrickZ2 : Brick
  {
    public BrickZ2()
    {
      Coordinates = new Coord[4];
      Coordinates[0] = new Coord(1, 0);
      Coordinates[1] = new Coord(1, 1);
      Coordinates[2] = new Coord(0, 1);
      Coordinates[3] = new Coord(0, 2);

      InitPosition();
    }
  }
}
