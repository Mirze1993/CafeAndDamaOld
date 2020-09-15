using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLCafe.Interface;
using Cafe.Tools.Games;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIGame;
using Newtonsoft.Json;

namespace Cafe.Controllers.Game
{
    public class GameController : Controller
    {
        IPlayGameRepository repository;
        public GameController(IPlayGameRepository gameRepository)
        {
            repository = gameRepository;
        }
        public IActionResult Start(int id)
        {
            //var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={id}");
            ViewBag.GameId = id;
            return View();
        }

        [HttpPost]
        public string StartGame(int id)
        {
            var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={id}");
            return JsonConvert.SerializeObject(pg.FirstOrDefault());
        }

        [HttpPost]
        public string PossiblePlace(int x,int y,int gameId)
        {
            var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={gameId}").FirstOrDefault();
            List<Coordinate> coordinates=new List<Coordinate>();
            UIPlayGame uIPlaygame = new SrzJson().desrz(pg);

            if (pg.Gamer1 == pg.Queue)
                coordinates = new WhitePossiblePlace(uIPlaygame).getSimple(Convert.ToByte(x), Convert.ToByte(y));

            else if (pg.Gamer2 == pg.Queue) coordinates = new BlackPossiblePlace(uIPlaygame).getSimple(Convert.ToByte(x), Convert.ToByte(y));
            
            return JsonConvert.SerializeObject(coordinates);
        }

        [HttpPost]
        public string Move(int gameID,int oldX,int oldY,int newX,int newY)
        {
            var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={gameID}").FirstOrDefault();
            var uiGame = new SrzJson().desrz(pg);
            var moveItem = new MoveItem(uiGame, Convert.ToByte(oldX), Convert.ToByte(oldY)
               , Convert.ToByte(newX), Convert.ToByte(newY));

            if (uiGame.Gamer1 == uiGame.Queue)
            {
                uiGame = moveItem.MoveWhite();
                uiGame.Queue = uiGame.Gamer2;                
            }
            else if (uiGame.Gamer2 == uiGame.Queue)
            {
                uiGame = moveItem.MoveBlack();
                uiGame.Queue = uiGame.Gamer1;                
            }
            if (!repository.Update(new SrzJson().srz(uiGame),pg.Id )) return "";

            return JsonConvert.SerializeObject(uiGame.Move);
        }


        [HttpPost]
        public string DumMove(int gameID, int oldX, int oldY, int newX, int newY)
        {
            var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={gameID}").FirstOrDefault();
            var uiGame = new SrzJson().desrz(pg);
            var moveItem = new MoveItem(uiGame, Convert.ToByte(oldX), Convert.ToByte(oldY)
               , Convert.ToByte(newX), Convert.ToByte(newY));

            if (uiGame.Gamer1 == uiGame.Queue)
            {
                uiGame = moveItem.DumWhite();
                uiGame.Queue = uiGame.Gamer2;
            }
            else if (uiGame.Gamer2 == uiGame.Queue)
            {
                uiGame = moveItem.DumBlack();
                uiGame.Queue = uiGame.Gamer1;
            }
            if (!repository.Update(new SrzJson().srz(uiGame), pg.Id)) return "";
            
            return JsonConvert.SerializeObject(uiGame.Move);
        }

        [HttpPost]
        public string CkeckGame(int gameId)
        {
            var pg = repository.ExecuteReader<PlayGame>($"Select * from PlayGame p where p.GameId={gameId}").FirstOrDefault();
           
            return JsonConvert.SerializeObject(pg);
        }

    }
}