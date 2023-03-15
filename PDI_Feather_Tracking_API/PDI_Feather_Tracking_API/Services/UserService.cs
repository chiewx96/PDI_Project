using EFWeightScan;
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
            else if(General.LoggedInUser != null)
            {
                return new BooleanMessageModel(false, "User has been signed in");
            }
            else
            {
                var user = General.TryLogin(loginModel.username, loginModel.password, ref dbContext);
                if (user == null)
                {
                    return new BooleanMessageModel(false, "Username / Password is wrong.");
                }
            }
            return new BooleanMessageModel(true, "login success");
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
                dbContext.Users.ForEachAsync(z => z.IsSignedIn = false);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
