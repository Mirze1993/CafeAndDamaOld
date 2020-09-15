using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIGames
    {
        public int Id { get; set; }
        public int RequestUser { get; set; }
        public string RequestUserName { get; set; }
        public int AcceptUser { get; set; }
        public string AcceptUserName { get; set; }
        public string Status { get; set; }
        public int WinUser { get; set; }
    }
}
