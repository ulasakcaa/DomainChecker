using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class DomainCheck : BaseDataModel
    {    
        public string DomainName { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastCheckedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
