using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PDI_Feather_Tracking_API.Models;
using PDI_Feather_Tracking_API.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_API
{
    public class General
    {
        public const string EncryptionKey = "PDI_Feather_Tracking";

        public const string TokenKey = "PDI_Feather_Tracking_API_1234567890_";

        private const int LoginExpirationMinutes = 30;

        public static Dictionary<string, object> TryLogin(string username, string password, ref PDIFeatherTrackingDbContext dbContext)
        {
            if (dbContext != null)
            {
                var targetUser = dbContext.Users
                     .Where(z => z.Username == username)
                     .FirstOrDefault();
                if (targetUser != null)
                {
                    var unhashed = Decrypt(targetUser?.Password);
                    if (password == unhashed)
                    {
                        dbContext.SaveChanges();
                        string token = TokenService.GenerateToken(targetUser.Id.ToString(), targetUser.Username);
                        return new Dictionary<string, object>()
                        {
                            { "user", targetUser },
                            { "token", token},
                        };
                    }
                }
            }
            return null;
        }

        public static bool Logout(ref PDIFeatherTrackingDbContext dbContext)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

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

    }
}
