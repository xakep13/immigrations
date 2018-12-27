using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels.Public
{
    public class CmsPoll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int VoiсeCount { get; set; }

        public bool IsActive { get; set; }

        public List<CmsQuestion> CmsQuestions { get; set; }
    }
}
