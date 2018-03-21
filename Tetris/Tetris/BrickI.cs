using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
  class BrickI : Brick
  {
    public BrickI()
    {
      Coordinates = new Coord[4];
      Coordinates[0] = new Coord(-1, 0);
      Coordinates[1] = new Coord(0, 0);
      Coordinates[2] = new Coord(1, 0);
      Coordinates[3] = new Coord(2, 0);

      InitPosition();
    }
  }
}
