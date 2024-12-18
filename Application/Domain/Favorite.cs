using Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class Favorite : BaseDataModel
    {      
        public long DomainId { get; set; }  
        public DomainCheck Domain { get; set; }
    }
}
