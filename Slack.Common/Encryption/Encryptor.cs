using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Slack.Common.Encryption
{
    public class Encryptor
    {
        private RSACryptoServiceProvider rsa;

        public Encryptor()
        {
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = "EncryptKey";

            rsa = new RSACryptoServiceProvider(csp);
        }

        public string Encrypt(string value)
             => Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(value), true));

        public string Decrypt(string value)
            => Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(value), true));
    }
}
