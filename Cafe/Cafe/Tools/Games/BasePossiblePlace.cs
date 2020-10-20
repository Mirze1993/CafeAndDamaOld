using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class BasePossiblePlace
    {
        internal UIPlayGame playGame;
        internal List<Coordinate> allCoordinates;
        public BasePossiblePlace(UIPlayGame uIPlayGame)
        {
            playGame = uIPlayGame;
            allCoordinates = new List<Coordinate>();
            allCoordinates.AddRange(uIPlayGame.WhiteCoordinate);
            allCoordinates.AddRange(uIPlayGame.BlackCoordinate);
        }
        public BasePossiblePlace()
        {

        }



        internal Possison Check(List<Coordinate> coordinates, byte x, byte y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) return Possison.outoff;
            foreach (var item in coordinates)
            {
                if (item.X == x && item.Y == y) return Possison.yes;
            }
            return Possison.no;
        }

        
    }

    public enum Possison
    {
         yes,
         no,
         outoff
    }
}
