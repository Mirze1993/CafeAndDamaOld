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
        
        public byte OldX { get; set; }
        public byte OldY { get; set; }
        public byte NewX { get; set; }
        public byte NewY { get; set; }
    }
}
