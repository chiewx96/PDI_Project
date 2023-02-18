using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF
{
    public class General
    {
        private const string EncryptionKey = "PDI_Feather_Tracking";
        public const string CloseWindow = "CloseWindow";

        public static User? TryLogin(string username, string password, ref FeatherDbContext dbContext)
        {
            if (dbContext != null)
            {
                var targetUser = dbContext.Users.AsNoTracking()
                     .Where(z => z.Username == username)
                     .FirstOrDefault();
                if (targetUser != null && !targetUser.IsSignedIn)
                {
                    var unhashed = Decrypt(targetUser?.Password);
                    return password == unhashed ? targetUser : null;
                }
            }
            return null;
        }

        public static string GenerateRunningNumber(char sku_type_code, string? last_sku_code, int gross_weight)
        {
            string year_code = DateTime.Now.Year.ToString().Substring(2, 2);
            string month_code = ((MonthEnum)DateTime.Now.Month).ToString();
            if (last_sku_code == null || last_sku_code.Substring(0, 1) != month_code || last_sku_code.Substring(1, 2) != year_code)
            {
                // newly deployed || new month || new year
                return $"{month_code}{year_code}{gross_weight}{sku_type_code.ToString()}00001";
            }
            else
            {
                if (int.TryParse(last_sku_code.Substring(7, 5), out int last_running_number))
                {
                    string current_number = (last_running_number + 1).ToString().PadLeft(5, '0');
                    return $"{month_code}{year_code}{gross_weight}{sku_type_code.ToString()}{current_number}";
                }
            }
            return string.Empty;
        }

        #region Encryption
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion
    }
}
