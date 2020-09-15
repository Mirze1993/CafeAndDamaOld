using Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIGame
{
    public class SrzJson
    {
       
        public PlayGame srz(UIPlayGame playGame)
        {
            PlayGame p = new PlayGame();
            p.Gamer1 = playGame.Gamer1;
            p.Gamer2 = playGame.Gamer2;
            p.Queue = playGame.Queue;
            p.GameId = playGame.GameId;
            p.WhiteCoordinate = JsonConvert.SerializeObject(playGame.WhiteCoordinate);
            p.BlackCoordinate = JsonConvert.SerializeObject(playGame.BlackCoordinate);
            p.Move = JsonConvert.SerializeObject(playGame.Move);
            return p;

        }

        public UIPlayGame desrz(PlayGame playGame)
        {
            UIPlayGame p = new UIPlayGame();
            p.Gamer1 = playGame.Gamer1;
            p.Gamer2 = playGame.Gamer2;
            p.Queue = playGame.Queue;
            p.GameId = playGame.GameId;
            p.WhiteCoordinate = JsonConvert.DeserializeObject<List<Coordinate>>(playGame.WhiteCoordinate);
            p.BlackCoordinate = JsonConvert.DeserializeObject<List<Coordinate>>(playGame.BlackCoordinate);
            p.Move = JsonConvert.DeserializeObject<Move>(playGame.Move);
            return p;
        }
    }
}
