using StudentAccount.DataAccess.Entity;
using System.Collections.Generic;

namespace StudentAccount.Domain.ApiModel.RequestApiModels
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }

        public List<AppUser> Users { get; set; }
        public static int DefaultPageSize { get; set; } = 3;
    }
}
