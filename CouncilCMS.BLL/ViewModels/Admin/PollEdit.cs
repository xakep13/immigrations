using Bissoft.CouncilCMS.Core.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Admin
{
    public class PollEdit
    {
        public int Id { get; set; }

        public string NameRu { get; set; }
        public string NameUk { get; set; }
        public string NameEn { get; set; }
        
        public bool ShowPoll { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int VoiсeCount { get; set; }
    }
}
