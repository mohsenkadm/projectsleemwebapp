using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectsleemwebapp.Models.Entity.Security
{
    public class Posts
    {
       public int post_id { get; set;  }
       public string Item_name { get; set; }
      public string Item_dateils { get; set; }
        public float Item_price { get; set;  }
        public DateTime? item_date { get; set;  }
        public int lastuserid { get; set;  }
        public float lastprice { get; set;  }
        public string image { get; set;  }
        public bool isshow { get; set;  }
        public string username { get; set; }
    }
}
