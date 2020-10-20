using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Tools.Games
{
    public class MoveItem
    {
        public MoveItem(UIPlayGame g, byte oldX, byte oldY, byte oldZ, byte newX, byte newY)
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
                OldY = oldY,
                OldZ = oldZ,
                NewZ = oldZ
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
                {
                    G.BlackCoordinate[i].X = NewX;
                    G.BlackCoordinate[i].Y = NewY;
                    if (NewY == 0)
                    {
                        G.BlackCoordinate[i].Z = 1; G.Move.NewZ = 1;
                    }
                    ;
                }
            }
            return G;
        }

        public UIPlayGame MoveWhite()
        {
            for (int i = 0; i < G.WhiteCoordinate.Count; i++)
            {
                if (G.WhiteCoordinate[i].X == OldX && G.WhiteCoordinate[i].Y == OldY)
                {
                    G.WhiteCoordinate[i].X = NewX;
                    G.WhiteCoordinate[i].Y = NewY;
                    if (NewY == 7)
                    {
                        G.WhiteCoordinate[i].Z = 1; G.Move.NewZ = 1;
                    }
                }
            }
            return G;
        }

        internal UIPlayGame DumWhite()
        {
            byte x1 = OldX;
            byte y1 = OldY; 

            var x2 = Math.Sign(NewX - OldX);
            var y2 = Math.Sign(NewY - OldY);
            while (x1 != NewX && y1 != NewY)
            {
                var i = G.BlackCoordinate.FindIndex(c => c.X == x1 && c.Y == y1);
                if (i > -1)
                {
                    G.Move.DumX = x1;
                    G.Move.DumY = y1;
                    G.BlackCoordinate.RemoveAt(i);
                    break;
                }
                x1 =(byte)(x1+ x2);
                y1 =(byte)(y1+ y2);
            }


            var i1 = new WhitePossiblePlace(G).EveryWhereDum(NewX, NewY).DumCoordinates.Count;
            var i2 = new WhiteQuenPossiblePlace(G).PossibleDum(NewX, NewY).DumCoordinates.Count;
            if (i1 > 0 || i2 > 0)
                G.Move.AgainDum = true;
            else { G.Queue = G.Gamer2; G.Move.AgainDum = false; }
            return MoveWhite();
        }

        internal UIPlayGame DumBlack()
        {
            byte x1 = OldX;
            byte y1 = OldY;

            var x2 = Math.Sign(NewX - OldX);
            var y2 = Math.Sign(NewY - OldY);
            while (x1 != NewX && y1 != NewY)
            {
                var i = G.WhiteCoordinate.FindIndex(c => c.X == x1 && c.Y == y1);
                if (i > -1)
                {
                    G.Move.DumX = x1;
                    G.Move.DumY = y1;
                    G.WhiteCoordinate.RemoveAt(i);
                    break;
                }
                x1 = (byte)(x1 + x2);
                y1 = (byte)(y1 + y2);
            }

            var i1 = new BlackPossiblePlace(G).EveryWhereDum(NewX, NewY).DumCoordinates.Count;
            var i2=new BlackQuenPossiblePlace(G).PossibleDum(NewX, NewY).DumCoordinates.Count;
            if (i1 > 0||i2>0)
                G.Move.AgainDum = true;
            else { G.Queue = G.Gamer1; G.Move.AgainDum = false; }
            return MoveBlack();
        }
    }
}
