using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramDraw.Models
{
    public class UserModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> UniqueTaggedUsers { get; set; }

        public int Score { get; set; }
    }
}
