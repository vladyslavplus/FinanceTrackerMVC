using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.ViewModels
{
    public class AddOrEditUserViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string SelectedRole { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
    }

}
