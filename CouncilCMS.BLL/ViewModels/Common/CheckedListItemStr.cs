using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CheckedListItemStr
    {
        public string Value { get; set; }

        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public int Level { get; set; }
        public bool Allowed { get; set; }
    }
}
