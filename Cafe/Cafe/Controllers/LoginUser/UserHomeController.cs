using System;
using System.Linq;
using System.Security.Claims;
using Cafe.Repostory;
using Cafe.Tools;
using Cafe.Tools.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIGame;
using Newtonsoft.Json;

namespace Cafe.Controllers.LoginUser
{
    [Authorize]
    [OnlineUserFilter]
    public class UserHomeController : Controller
    {
        GamesRepository repository;
        public UserHomeController( )
        {
            repository = new GamesRepository();
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult GameRoom()
        {
            ViewBag.ActiveUser = OnlineUsers.Users;
            return View();
        }

        [HttpPost]
        public string RequestGame(string username)
        {
            var userid=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;            

            int acceptUserId = repository.GetIdByColumName("UserName",username);
            int requestUserId =userid != null? requestUserId = Convert.ToInt32(userid):0;
            if (acceptUserId <=0||requestUserId<=0) return "user not found";
            

            if (repository.CountActivGames(acceptUserId,requestUserId) > 0) return "game exsist";


            int gameId = repository.Insert(new Games() {
                AcceptUser = acceptUserId,
                RequestUser = requestUserId,
                Status = GameStatus.Wait
            });

            return "success";
        }

        [HttpPost]
        public string CheckGamesDB()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            int id = userid != null? id = Convert.ToInt32(userid):0;
            if (id <= 0) return "error";

            var games = repository.GetUserGames(id);
            return JsonConvert.SerializeObject(games);
        }

        [HttpPost]
        public string RejectRequest(int id)
        {
            return repository.Delet(id) ? "succes" : "unsuccess";
        }

        [HttpPost]
        public string AcceptRequest(int id,string reqU,string accU)
        {
            var games= repository.GetByColumName("Id",id);
            if (games.Count<1) return "game not found";
            var game = games.FirstOrDefault();
            game.Status = GameStatus.Start;
            var b=repository.Update(game,id);
            if (!b) return "unsucces";
            var a= new PlayGameRepository().Insert(new SrzJson().srz(new UIPlayGame(reqU, accU, id)));
            return a>0?"success": "unsuccess";
        }
    }
}