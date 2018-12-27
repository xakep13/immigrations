using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class Subscriber : BaseEntity<int>
    {        
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }        
    }
}