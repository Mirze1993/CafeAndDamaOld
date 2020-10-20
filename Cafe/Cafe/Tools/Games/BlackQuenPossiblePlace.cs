using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class BlackQuenPossiblePlace : BasePossiblePlace
    {
        public BlackQuenPossiblePlace(UIPlayGame g)
        {
            playGame = g;
            allCoordinates = new List<Coordinate>();
            allCoordinates.AddRange(playGame.WhiteCoordinate);
            allCoordinates.AddRange(playGame.BlackCoordinate);
        }
        UICoordinate uICoordinate = new UICoordinate();

        public bool AnyStoneDum()
        {
            foreach (var item in playGame.BlackCoordinate)
            {
                if (item.Z == 1)
                    if (PossibleDum(item.X, item.Y).DumCoordinates.Count > 0) return true;
            }
            return false;
        }

        public UICoordinate PossiblePlace(byte x, byte y)
        {
            UICoordinate uICoordinate = PossibleDum(x, y);
            if (uICoordinate.DumCoordinates.Count > 0) return uICoordinate;

            #region singlePossision            
            uICoordinate.PossibleCoordinates.AddRange(SingleRD(x, y));
            uICoordinate.PossibleCoordinates.AddRange(SingleRU(x, y));
            uICoordinate.PossibleCoordinates.AddRange(SingleLD(x, y));
            uICoordinate.PossibleCoordinates.AddRange(SingleLU(x, y));
            return uICoordinate;
            #endregion
        }

        public UICoordinate PossibleDum(byte x, byte y)
        {
            var rd = DumRightDown(x, y);
            var ru = DumRightUp(x, y);
            var ld = DumLeftDown(x, y);
            var lu = DumLeftUp(x, y);

            UICoordinate uICoordinate = new UICoordinate();
            #region DumPossision            
            if (rd != null)
            {
                uICoordinate.DumCoordinates.Add(rd);
                uICoordinate.PossibleCoordinates.AddRange(SingleRD(rd.X, rd.Y));
            }

            if (ru != null)
            {
                uICoordinate.DumCoordinates.Add(ru);
                uICoordinate.PossibleCoordinates.AddRange(SingleRU(ru.X, ru.Y));
            }

            if (ld != null)
            {
                uICoordinate.DumCoordinates.Add(ld);
                uICoordinate.PossibleCoordinates.AddRange(SingleLD(ld.X, ld.Y));
            }

            if (lu != null)
            {
                uICoordinate.DumCoordinates.Add(lu);
                uICoordinate.PossibleCoordinates.AddRange(SingleLU(lu.X, lu.Y));
            }

            #endregion

            return uICoordinate;
        }

        #region SingleMove       
        List<Coordinate> SingleRD(byte x, byte y)
        {
            var list = new List<Coordinate>();
            for (int i = 0; i < 8; i++)
            {
                if (x >= 7 || y >= 7) break;
                if (Check(allCoordinates, (byte)(x + 1), (byte)(y + 1)) == Possison.no)
                    list.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
                else break;
                x++; y++;
            }
            return list;
        }

        List<Coordinate> SingleLD(byte x, byte y)
        {
            var list = new List<Coordinate>();
            for (int i = 0; i < 8; i++)
            {
                if (x <= 0 || y >= 7) break;
                if (Check(allCoordinates, (byte)(x - 1), (byte)(y + 1)) == Possison.no)
                    list.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));
                else break;
                x--; y++;
            }
            return list;
        }

        List<Coordinate> SingleRU(byte x, byte y)
        {
            var list = new List<Coordinate>();
            for (int i = 0; i < 8; i++)
            {
                if (x >= 7 || y <= 0) break;
                if (Check(allCoordinates, (byte)(x + 1), (byte)(y - 1)) == Possison.no)
                    list.Add(new Coordinate((byte)(x + 1), (byte)(y - 1)));
                else break;
                x++; y--;
            }
            return list;
        }

        List<Coordinate> SingleLU(byte x, byte y)
        {
            var list = new List<Coordinate>();
            for (int i = 0; i < 8; i++)
            {
                if (x <= 0 || y <= 0) break;
                if (Check(allCoordinates, (byte)(x - 1), (byte)(y - 1)) == Possison.no)
                    list.Add(new Coordinate((byte)(x - 1), (byte)(y - 1)));
                else break;
                x--; y--;
            }
            return list;
        }
        #endregion

        #region DumMove
        Coordinate DumRightDown(byte x, byte y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x >= 6 || y >= 6) break;
                if (Check(playGame.WhiteCoordinate, (byte)(x + 1), (byte)(y + 1)) == Possison.yes)
                    if (Check(allCoordinates, (byte)(x + 2), (byte)(y + 2)) == Possison.no)
                        return new Coordinate((byte)(x + 1), (byte)(y + 1));
                x++; y++;
            }
            return null;
        }

        Coordinate DumLeftDown(byte x, byte y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x <= 1 || y >= 6) break;
                if (Check(playGame.WhiteCoordinate, (byte)(x - 1), (byte)(y + 1)) == Possison.yes)
                    if (Check(allCoordinates, (byte)(x - 2), (byte)(y + 2)) == Possison.no)
                        return new Coordinate((byte)(x - 1), (byte)(y + 1));
                x--; y++;
            }
            return null;
        }

        Coordinate DumRightUp(byte x, byte y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x >= 6 || y <= 1) break;
                if (Check(playGame.WhiteCoordinate, (byte)(x + 1), (byte)(y - 1)) == Possison.yes)
                    if (Check(allCoordinates, (byte)(x + 2), (byte)(y - 2)) == Possison.no)
                        return new Coordinate((byte)(x + 1), (byte)(y - 1));
                x++; y--;
            }
            return null;
        }

        Coordinate DumLeftUp(byte x, byte y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (x <= 1 || y <= 1) break;
                if (Check(playGame.WhiteCoordinate, (byte)(x - 1), (byte)(y - 1)) == Possison.yes)
                    if (Check(allCoordinates, (byte)(x - 2), (byte)(y - 2)) == Possison.no)
                        return new Coordinate((byte)(x - 1), (byte)(y - 1));
                x--; y--;
            }
            return null;
        }
        #endregion


    }
}
