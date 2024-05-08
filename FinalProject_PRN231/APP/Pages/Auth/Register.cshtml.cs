using APP.IServices;
using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APP.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IAccountServices accountServices;
        public RegisterModel(IAccountServices accountServices, ILogger<IndexModel> logger)
        {
            this.accountServices = accountServices;
            _logger = logger;
        }
        public OperationResult OperationResult { get; set; }
        [BindProperty(SupportsGet = true)]
        public RegisterCreationModel RegisterCreationModel { get; set; }
        public string Msg { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public async Task OnPostCreate()
        {
            OperationResult = await accountServices.Register(RegisterCreationModel);
        }
    }
}
