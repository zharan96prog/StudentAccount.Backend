using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAccount.Domain.ApiModel.RequestApiModels
{
    public class MoreParametersModel
    {
        public int numberPage { get; set; }
        public string SearchParameter { get; set; }
    }
}
