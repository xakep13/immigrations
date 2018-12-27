﻿using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class DocFile : BaseEntity<int>
    {        
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }
     
        public string File { get; set; }
        public Double Size { get; set; }
        public string Extension { get; set; }

        public int DocId { get; set; }

        public virtual Doc Doc { get; set; }
    }
}