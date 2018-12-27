using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.Entities.Entities
{
    public class Poll : BaseAuditionEntity
    {   
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int VoiсeCount { get; set; }

        public virtual List<Question> Questions { get; set; }       
    }
}
