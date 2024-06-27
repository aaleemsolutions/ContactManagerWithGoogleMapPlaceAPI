using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Common.Model
{
    public abstract class BaseDTO
    {
       
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public BaseDTO()
        {
            this.CreatedBy = string.Empty;
        }
    }
}
