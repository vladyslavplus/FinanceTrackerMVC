using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Domain.Entities
{
    public class Users : IdentityUser
    {
        public int TransactionsCount { get; set; } = 0;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
