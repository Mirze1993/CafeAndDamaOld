using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Games:IEntity
    {
        public int Id { get; set; }
        public int RequestUser { get; set; }
        public int AcceptUser { get; set; }
        public string Status { get; set; }
        public int WinUser { get; set; }
    }
}
