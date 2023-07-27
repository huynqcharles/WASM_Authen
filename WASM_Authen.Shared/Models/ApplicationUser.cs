
using Microsoft.AspNetCore.Identity;

namespace WASM_Authen.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}