using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class UserClaims:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Issuer { get; set; }
    }
}
