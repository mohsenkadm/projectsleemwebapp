using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Entity.Security
{
    public class postuser
    {
       public int postuser_id { get; set;  }
       public int User_id { get; set; }
        public int post_id { get; set;  }
        public float price_item { get; set;  }
        public DateTime? data_insert { get; set;  }
        public string username { get; set; }
    }
}
