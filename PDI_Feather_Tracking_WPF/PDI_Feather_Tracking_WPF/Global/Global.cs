using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Model;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF
{
    public class General
    {
        public const string CloseWindow = "CloseWindow";

        public const string StopWeighting = "StopWeighting";

        public const string PrintLabelCommand = "PDI_PL_Feather";

        public const string RefreshUserAccess = "Refresh_User_Access";

        public const string RecordCommand = "*()record_current_result!@#";

        public static User? TryLogin(string username, string password, ref FeatherDbContext dbContext)
        {
            if (dbContext != null)
            {
                var targetUser = dbContext.Users.AsNoTracking()
                     .Where(z => z.Username == username)
                     .FirstOrDefault();
                if (targetUser != null)
                {
                    var unhashed = EncryptionHelper.Decrypt(targetUser?.Password);
                    targetUser.UserLevel = dbContext.UserLevels.AsNoTracking().Where(x=>x.Id == targetUser.UserLevelId).First();
                    return password == unhashed ? targetUser : null;
                }
            }
            return null;
        }

        public static string GenerateRunningNumber(char sku_type_code, string? last_sku_code, decimal gross_weight)
        {
            string year_code = DateTime.Now.Year.ToString().Substring(2, 2);
            string month_code = ((MonthEnum)DateTime.Now.Month).ToString();
            if (last_sku_code == null || last_sku_code == String.Empty || last_sku_code.Substring(0, 1) != month_code || last_sku_code.Substring(1, 2) != year_code)
            {
                // newly deployed || new month || new year
                return $"{month_code}{year_code}{gross_weight.ToString().PadLeft(3, '0')}{sku_type_code.ToString()}00001";
            }
            else
            {
                if (int.TryParse(last_sku_code.Substring(7, 5), out int last_running_number))
                {
                    string current_number = (last_running_number + 1).ToString().PadLeft(5, '0');
                    return $"{month_code}{year_code}{gross_weight.ToString().PadLeft(3, '0')}{sku_type_code.ToString()}{current_number}";
                }
            }
            return string.Empty;
        }

        public static bool CheckAccessibility(User? currentUser, ModuleEnum? moduleEnum)
        {
            if (moduleEnum == null || currentUser == null || currentUser.UserLevel == null)
                return false;
            try
            {
                var _access_json = currentUser.UserLevel.ModuleAccess;
                List<ModuleAccess> _access = JsonConvert.DeserializeObject<List<ModuleAccess>>(_access_json);
                ModuleAccess _target = _access.Where(x => x.Module.Id == (int)moduleEnum).First();
                return _target.Status == 1 ? true : false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(nameof(CheckAccessibility));
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public static double RandomNumberBetween(double minValue, double maxValue)
        {
            Random random = new Random();
            var next = random.NextDouble();
            return minValue + (next * (maxValue - minValue));
        }

        public static void SendNotifcation(string message)
        {
            Messenger.Default.Send(new NotificationViewModel()
            {
                Message = message
            });
            Messenger.Default.Send(new LoggerModel() { Message = message });
        }
    }
}
