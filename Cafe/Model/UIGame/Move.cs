using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIGame
{
    public class Move
    {
        public Move()
        {

        }
        public bool AgainDum { get; set; }
        
        public byte OldX { get; set; }
        public byte OldY { get; set; }
        public byte OldZ { get; set; }

        public byte NewX { get; set; }
        public byte NewY { get; set; }
        public byte NewZ { get; set; }

        public byte DumX { get; set; } = 0;
        public byte DumY { get; set; } = 0;
    }
}
