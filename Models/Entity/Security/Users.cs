using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Entity.Security
{
    public class Users
    {
       public int Id { get; set;  }
       public string UserName { get; set; }
      public string Password { get; set; }
        public string Email { get; set;  }
        public string Code { get; set;  }
        public bool IsConfirm { get; set;  }
        public bool Isonline { get; set;  }
        public DateTime? Lastlogin { get; set;  }
        public DateTime? Lastlogout { get; set;  }
        public bool IsActive { get; set;  }
        public DateTime?  InsertDate { get; set;  }
        public DateTime?  UpdateDate { get; set;  }
        public bool IsDeletet { get; set;  }
        public string Token { get; set; }
        public byte[]  Version { get; set;  }
        public bool isadmin { get;  set; }
    }
}
