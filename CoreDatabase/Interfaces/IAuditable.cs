using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDatabase.Interfaces
{
    public interface IAuditable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
