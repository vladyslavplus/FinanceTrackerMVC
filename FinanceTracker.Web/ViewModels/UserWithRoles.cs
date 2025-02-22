using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Web.ViewModels
{
    public class UserWithRoles
    {
        public Users User { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public string RolesString => Roles.Count > 0 ? string.Join(", ", Roles) : "No roles";
    }
}
