using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLCafe.ConcreateRepository;
using BLCafe.Interface;
using Cafe.Tools;
using Cafe.Tools.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.UIEntites;
using Model.UIGame;
using Newtonsoft.Json;

namespace Cafe.Controllers.LoginUser
{
    [Authorize]
    [OnlineUserFilter]
    public class UserHomeController : Controller
    {
        IGamesRepository repository;
        public UserHomeController(IGamesRepository gamesRepository )
        {
            repository = gamesRepository;
        }

        public IActionResult Index()
        {
            ViewBag.ActiveUser = OnlineUsers.Users;
            return View();
        }

        [HttpPost]
        public string RequestGame(string username)
        {
            var userid=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var obj = repository.ExecuteScaler($"Select u.Id from AppUser u Where u.UserName='{username}'");
            int acceptUserId = 0;
            int requestUserId = 0;
            if (obj != null) acceptUserId=Convert.ToInt32(obj);
            if (userid != null) requestUserId = Convert.ToInt32(userid);
            if (acceptUserId <=0||requestUserId<=0) return "user not found";
            var checkagain = repository.ExecuteReader<Games>($"select * from Games g where (g.RequestUser={requestUserId} and g.AcceptUser={acceptUserId}) or (g.RequestUser={acceptUserId} and g.AcceptUser={requestUserId})");
            if (checkagain.Count > 0) return "game exsist";
            int gameId=repository.Insert(new Games() {
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
            int id = 0;
            if (userid != null) id = Convert.ToInt32(userid);
            if (id <= 0) return "error";
            var games=repository.ExecuteReader<UIGames>
                ($"select g.*, " +
                $"u.UserName as AcceptUserName, " +
                $"u1.UserName as RequestUserName " +
                $"from Games g " +
                $"LEFT JOIN AppUser u on u.Id = g.AcceptUser " +
                $"LEFT JOIN AppUser u1 on u1.Id = g.RequestUser " +
                $"where g.RequestUser = {id} or g.AcceptUser = {id}");
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
            var games= repository.GetById(id);
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