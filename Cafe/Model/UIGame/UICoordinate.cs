using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIGame
{
    public class UICoordinate
    {
        public List<Coordinate> PossibleCoordinates { get; set; }
        public List<Coordinate> DumCoordinates { get; set; }
        public UICoordinate()
        {
            PossibleCoordinates = new List<Coordinate>();
            DumCoordinates = new List<Coordinate>();
        }
    }
}
