using APP.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BusinessObject.RequestModel.Account;
using BusinessObject.Common;

namespace APP.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IAccountServices accountServices;
        public LoginModel(IAccountServices accountServices, ILogger<IndexModel> logger)
        {
            this.accountServices = accountServices;
            _logger = logger;
        }
        [BindProperty(SupportsGet = true)]
        public LoginQueryModel LoginQueryModel { get; set; }
        public string Msg { get; set; } = string.Empty;
        public async Task OnGetAsync()
        {
            Msg = TempData["msg"] as string;
            TempData["msg"] = string.Empty;
        }
        public async Task<IActionResult> OnPost()
        {
            var rs = await accountServices.Login(LoginQueryModel);
            if (rs == null)
            {
                TempData["msg"] = "Login Failed. Try again";
                return RedirectToPage("Login");
            }
            else
            {
                var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, rs.Email.ToString().Trim()),
                new Claim(ClaimTypes.Role, ((int)rs.Role).ToString().Trim()),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                if(rs.Role == Role.Admin)
                {
                    return RedirectToPage("../Admin/Dashboard/Index");
                }
                return RedirectToPage("../Index");
            }
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Auth/Login");

        }
    }
}
