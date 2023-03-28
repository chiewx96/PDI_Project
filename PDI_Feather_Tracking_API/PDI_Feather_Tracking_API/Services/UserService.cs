using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;

namespace PDI_Feather_Tracking_API.Services
{
    public class UserService
    {
        private PDIFeatherTrackingDbContext dbContext;

        public UserService(PDIFeatherTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BooleanMessageModel TryLogin(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.username) || string.IsNullOrEmpty(loginModel.password))
            {
                return new BooleanMessageModel(false, "Invalid login credentials");
            }
            else
            {
                var response = General.TryLogin(loginModel.username, loginModel.password, ref dbContext);
                if (response == null)
                {
                    return new BooleanMessageModel(false, "Username / Password is wrong.");
                }
                else if(response != null && response.GetValueOrDefault<string, object>("user") == null)
                {
                    return new BooleanMessageModel(false, "You have no access on this module.");
                }
                return new BooleanMessageModel(true, response);
            }
        }

        public BooleanMessageModel Logout()
        {
            if (General.Logout(ref dbContext))
            {
                return new BooleanMessageModel(true, "User signed out");
            }
            return new BooleanMessageModel(false, "Sign out operation fail. Contact admin.");
        }

        public bool LogoutAll()
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
