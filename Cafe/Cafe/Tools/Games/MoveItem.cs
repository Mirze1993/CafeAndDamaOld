using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class MoveItem
    {
        public MoveItem(UIPlayGame g, byte oldX, byte oldY, byte newX, byte newY)
        {
            G = g;
            OldX = oldX;
            OldY = oldY;
            NewX = newX;
            NewY = newY;
            G.Move = new Move()
            {
                NewX = newX,
                NewY = newY,
                OldX = oldX,
                OldY = oldY
            };
        }

        public UIPlayGame G { get; }
        public byte OldX { get; }
        public byte OldY { get; }
        public byte NewX { get; }
        public byte NewY { get; }

        public UIPlayGame MoveBlack()
        {
            for (int i = 0; i < G.BlackCoordinate.Count; i++)
            {
                if (G.BlackCoordinate[i].X == OldX && G.BlackCoordinate[i].Y == OldY)
                { G.BlackCoordinate[i].X = NewX; G.BlackCoordinate[i].Y = NewY; }
            }
            return G;
        }

        public UIPlayGame MoveWhite()
        {
            for (int i = 0; i < G.WhiteCoordinate.Count; i++)
            {
                if (G.WhiteCoordinate[i].X == OldX && G.WhiteCoordinate[i].Y == OldY)
                { G.WhiteCoordinate[i].X = NewX; G.WhiteCoordinate[i].Y = NewY; }
            }
            return G;
        }

        internal UIPlayGame DumWhite()
        {
            byte x1 = (byte)((OldX + NewX) / 2);
            byte y1 = (byte)((OldY + NewY) / 2);
            G.BlackCoordinate.RemoveAt(G.BlackCoordinate.FindIndex(c => c.X == x1 && c.Y == y1));

            return MoveWhite();
        }

        internal UIPlayGame DumBlack()
        {
            byte x1 = (byte)((OldX + NewX) / 2);
            byte y1 = (byte)((OldY + NewY) / 2);
            G.WhiteCoordinate.RemoveAt(G.WhiteCoordinate.FindIndex(c => c.X == x1 && c.Y == y1));
            return MoveBlack();
        }
    }
}
