using PDI_Feather_Tracking_API.Models.RequestModel;
using PDI_Feather_Tracking_API.Models.ResponseModel;

namespace PDI_Feather_Tracking_API.Services.Services
{
    public interface UserService
    {
        BooleanMessageModel TryLogin(LoginModel model);
    }
}
