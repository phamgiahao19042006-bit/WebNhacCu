using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDatabase.Interfaces
{
    public interface IMeta
    {
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
    }
}
