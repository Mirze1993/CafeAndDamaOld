using MicroORM;
using Model.Entities;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Repostory
{
    public class GamesRepository:CRUD<Games>
    {
        public int GetIdByColumName(string columName,object value)
        {
            string q = $"Select u.Id from AppUser u Where u.{columName} = @{columName}";
            
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var obj = commander.Scaller(q,new List<System.Data.Common.DbParameter>() {commander.SetParametr(columName,value) });
                if (obj != null) return Convert.ToInt32(obj);
                else return default(int);
            }
        }

        public int CountActivGames(int acceptUserId,int requestUserId)
        {
            string q = $"select Count(Id) from Games g where (g.RequestUser={requestUserId} and g.AcceptUser={acceptUserId} and g.Status <> 'Close') or (g.RequestUser={acceptUserId} and g.AcceptUser={requestUserId}) and g.Status <> 'Close'";

            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var obj = commander.Scaller(q);
                return obj != null? Convert.ToInt32(obj): default(int);                
            }
        }

        public List<UIGames> GetUserGames(int id)
        {
            string q = $"select g.*, " +
             $"u.UserName as AcceptUserName, " +
             $"u1.UserName as RequestUserName " +
             $"from Games g " +
             $"LEFT JOIN AppUser u on u.Id = g.AcceptUser " +
             $"LEFT JOIN AppUser u1 on u1.Id = g.RequestUser " +
             $"where g.RequestUser = {id} or g.AcceptUser = {id}";
            
            using (CommanderBase commander = DBContext.CreateCommander())            
                return commander.Reader<UIGames>(q); 
        }

    }
}
