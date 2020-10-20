using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class PossiblePlace
    {
        UIPlayGame G;
        byte x, y, z;
        public PossiblePlace(UIPlayGame uIPlayGame, byte _x, byte _y, byte _z)
        {
            G = uIPlayGame; x = _x; y = _y; z = _z;
        }

        public UICoordinate White()
        {
            var s = new WhitePossiblePlace(G);
            var q = new WhiteQuenPossiblePlace(G);
            UICoordinate uICoordinate = new UICoordinate();
            if (G.Move.AgainDum)
            {
                if (x == G.Move.NewX && y == G.Move.NewY)
                    if (z > 0) uICoordinate = q.PossibleDum(x, y);
                    else uICoordinate = s.EveryWhereDum(x, y);
                return uICoordinate;
            }

            if (z > 0) uICoordinate = q.PossibleDum(x, y);
            else uICoordinate = s.SimpleDum(x, y);
            if (uICoordinate.DumCoordinates.Count > 0) return uICoordinate;
            if (s.AnyStoneDum() || q.AnyStoneDum()) return default(UICoordinate);
            if (z > 0) return q.PossiblePlace(x, y);
            else return s.PossiblePlace(x, y);
        }

        public UICoordinate BLack()
        {
            var s = new BlackPossiblePlace(G);
            var q = new BlackQuenPossiblePlace(G);
            UICoordinate uICoordinate = new UICoordinate();
            if (G.Move.AgainDum)
            {
                if (x == G.Move.NewX && y == G.Move.NewY)
                    if (z > 0) uICoordinate = q.PossibleDum(x, y);
                    else uICoordinate = s.EveryWhereDum(x, y);
                return uICoordinate;
            }
            if (z > 0) uICoordinate = q.PossibleDum(x, y);
            else uICoordinate = s.SimpleDum(x, y);
            if (uICoordinate.DumCoordinates.Count > 0) return uICoordinate;
            if (s.AnyStoneDum() || q.AnyStoneDum()) return default(UICoordinate);
            if (z > 0) return q.PossiblePlace(x, y);
            else return s.PossiblePlace(x, y);
        }
    }
}
