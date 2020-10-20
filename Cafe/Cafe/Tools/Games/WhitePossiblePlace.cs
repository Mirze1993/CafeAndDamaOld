using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class WhitePossiblePlace :BasePossiblePlace
    {
      
        public WhitePossiblePlace(UIPlayGame g)
        {
            playGame = g;
            allCoordinates = new List<Coordinate>();
            allCoordinates.AddRange(playGame.WhiteCoordinate);
            allCoordinates.AddRange(playGame.BlackCoordinate);
        }
        

        public UICoordinate PossiblePlace(byte x, byte y)
        {
            UICoordinate uICoordinate=new UICoordinate();
            
            uICoordinate = SimpleDum(x, y);
            if (uICoordinate.PossibleCoordinates.Count > 0) return uICoordinate;            
            return SimpleMove(x, y);
        }

        UICoordinate SimpleMove(byte x, byte y)
        {
            UICoordinate ui = new UICoordinate();

            //right down
            if (Check(allCoordinates, (byte)(x + 1), (byte)(y + 1)) == Possison.no)
                ui.PossibleCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
            //left down  
            if (Check(allCoordinates, (byte)(x - 1), (byte)(y + 1)) == Possison.no)
                ui.PossibleCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));

            return ui;
        }

        public UICoordinate SimpleDum(byte x, byte y)
        {
            UICoordinate uICoordinate = new UICoordinate();

            //right down
            if (Check(playGame.BlackCoordinate, (byte)(x + 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
                }
            }

            //left down  
            if (Check(playGame.BlackCoordinate, (byte)(x - 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));
                }
            }
            return uICoordinate;
        }


        public UICoordinate EveryWhereDum(byte x, byte y)
        {
            UICoordinate uICoordinate = new UICoordinate();
            

            //right down
            if (Check(playGame.BlackCoordinate, (byte)(x + 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
                }
            }

            //left down  
            if (Check(playGame.BlackCoordinate, (byte)(x - 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));
                }
            }
            //right up
            if (Check(playGame.BlackCoordinate, (byte)(x + 1), (byte)(y - 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y - 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y - 1)));
                }
            }
            //left up  
            if (Check(playGame.BlackCoordinate, (byte)(x - 1), (byte)(y - 1))==Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y - 2))==Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y - 1)));
                }
            }
            return uICoordinate;
        }

        public bool AnyStoneDum()
        {
            foreach (var item in playGame.WhiteCoordinate)
            {
                if(item.Z==0)
                if (SimpleDum(item.X, item.Y).PossibleCoordinates.Count > 0) return true;
            }
            return false;
        }
    }
}
