using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIGame
{
    public class Coordinate
    {
        public byte X { get; set; }
        public byte Y { get; set; }

        public Coordinate()
        {

        }

        public Coordinate(byte x, byte y)
        {
            X = x; Y = y;
        }
    }
}
