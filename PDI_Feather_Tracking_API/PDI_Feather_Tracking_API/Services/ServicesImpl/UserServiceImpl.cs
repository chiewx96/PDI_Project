using EFWeightScan;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;
using PDI_Feather_Tracking_API.Services.Services;

namespace PDI_Feather_Tracking_API.Services.ServicesImpl
{
    public class UserServiceImpl : UserService
    {
        private PDIFeatherTrackingDbContext dbContext;

        public UserServiceImpl(PDIFeatherTrackingDbContext dbContext)
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
                var user = General.TryLogin(loginModel.username, loginModel.password, ref dbContext);
                if (user == null)
                {
                    return new BooleanMessageModel(false, "Username / Password is wrong.");
                }
            }
            return new BooleanMessageModel(true, "login success");
        }
    }
}
