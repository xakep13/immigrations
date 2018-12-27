using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class FacebookPostEntity : FacebookPostModel
    {

        public string access_token { get; set; }

        public FacebookPostEntity() { }

        public FacebookPostEntity(FacebookPostModel parent)
        {
            this.actions = parent.actions;
            this.caption = parent.caption;
            this.description = parent.description;
            this.link = parent.link;
            this.message = parent.message;
            this.name = parent.name;
            this.privacy = parent.privacy;
            this.source = parent.source;
        }
    }

    public class FacebookPostModel
    {
        public string message { get; set; }
        //public string picture { get; set; }  
        public string link { get; set; }
        public string name { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public string actions { get; set; }
        public string privacy { get; set; }
    }
}
