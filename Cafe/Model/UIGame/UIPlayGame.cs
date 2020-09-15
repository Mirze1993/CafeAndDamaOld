using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIGame
{
    public class UIPlayGame
    {
        public UIPlayGame()
        {

        }
        public UIPlayGame(string gamer1, string gamer2,int id)
        {
            Gamer1 = gamer1; Gamer2 = gamer2;GameId = id;
            Queue = Gamer1;
            WhiteCoordinate = new List<Coordinate>(){
                    new Coordinate(){X=0,Y=0},
                    new Coordinate(){X=2,Y=0},
                    new Coordinate(){X=4,Y=0},
                    new Coordinate(){X=6,Y=0},

                    new Coordinate(){X=1,Y=1},
                    new Coordinate(){X=3,Y=1},
                    new Coordinate(){X=5,Y=1},
                    new Coordinate(){X=7,Y=1},

                    new Coordinate(){X=0,Y=2},
                    new Coordinate(){X=2,Y=2},
                    new Coordinate(){X=4,Y=2},
                    new Coordinate(){X=6,Y=2},
                    };

            BlackCoordinate = new List<Coordinate>(){
                    new Coordinate(){X=1,Y=5},
                    new Coordinate(){X=3,Y=5},
                    new Coordinate(){X=5,Y=5},
                    new Coordinate(){X=7,Y=5},

                    new Coordinate(){X=0,Y=6},
                    new Coordinate(){X=2,Y=6},
                    new Coordinate(){X=4,Y=6},
                    new Coordinate(){X=6,Y=6},

                    new Coordinate(){X=1,Y=7},
                    new Coordinate(){X=3,Y=7},
                    new Coordinate(){X=5,Y=7},
                    new Coordinate(){X=7,Y=7},
                    };
            Move = new Move();
        }
        public List<Coordinate> WhiteCoordinate { get; set; }
        public List<Coordinate> BlackCoordinate { get; set; }
        

        public string Gamer1 { get; set; }
        public string Gamer2 { get; set; }

        public string Queue { get; set; }

        public Move Move { get; set; }

        public int GameId { get; set; }
        
    }
}
