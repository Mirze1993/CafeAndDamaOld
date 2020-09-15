using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class WhitePossiblePlace
    {
        UIPlayGame playGame;
        List<Coordinate> allCoordinates;
        public WhitePossiblePlace(UIPlayGame g)
        {
            playGame = g;
            allCoordinates = new List<Coordinate>();
            allCoordinates.AddRange(playGame.WhiteCoordinate);
            allCoordinates.AddRange(playGame.BlackCoordinate);
        }
        

        public List<Coordinate> getSimple(byte x, byte y)
        {
            List<Coordinate> listDum = SimpleDum(x, y);
            if (listDum.Count > 0) return listDum;
            if (Dum()) return listDum;
            return SimpleMove(x, y);
        }

        List<Coordinate> SimpleMove(byte x, byte y)
        {
            List<Coordinate> listKorgeri = new List<Coordinate>();

            //right down
            if (Check(allCoordinates, (byte)(x + 1), (byte)(y + 1)))
                listKorgeri.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
            //left down  
            if (Check(allCoordinates, (byte)(x - 1), (byte)(y + 1)))
                listKorgeri.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));

            return listKorgeri;
        }

        List<Coordinate> SimpleDum(byte x, byte y)
        {
            List<Coordinate> listKorgeri = new List<Coordinate>();

            //right down
            if (!Check(playGame.BlackCoordinate, (byte)(x + 1), (byte)(y + 1)))
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y + 2)))
                    listKorgeri.Add(new Coordinate((byte)(x + 2), (byte)(y + 2)));
            }

            //left down  
            if (!Check(playGame.BlackCoordinate, (byte)(x - 1), (byte)(y + 1)))
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y + 2)))
                    listKorgeri.Add(new Coordinate((byte)(x - 2), (byte)(y + 2)));
            }

            return listKorgeri;
        }


        bool Dum()
        {
            foreach (var item in playGame.WhiteCoordinate)
            {
                if (SimpleDum(item.X, item.Y).Count > 0) return true;
            }
            return false;
        }


        bool Check(List<Coordinate> coordinates, byte x, byte y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) return false;
            foreach (var item in coordinates)
            {
                if (item.X == x && item.Y == y) return false;
            }
            return true;
        }
    }
}
