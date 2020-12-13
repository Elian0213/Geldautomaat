using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Library.Helpers
{
    public class PasswordHelper
    {
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public class generatedCode
        {
            public string Code { get; set; }
            public string Hash { get; set; }
        }

        /**
         * Returns a randomly generated 4-digit string
        **/
        public dynamic randomPincode()
        {
            string code = new Random().Next(1000, 9999).ToString();

            return new generatedCode{
                Code = code,
                Hash = ComputeSha256Hash(code),
            };
        }
    }
}
