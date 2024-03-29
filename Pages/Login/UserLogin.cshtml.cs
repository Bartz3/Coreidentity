using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreIdentity.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CoreIdentity.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        private bool ValidateUser(SiteUser user)
        {
            if ((user.userName == "admin") && (user.password == "abc"))
                return true;
            else
                return false;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, user.userName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
               ClaimsPrincipal(claimsIdentity));
                return RedirectToPage(returnUrl);
            }
            ViewData["Message"] = "Niepoprawne dane";
            return Page();
        }

    }
}
