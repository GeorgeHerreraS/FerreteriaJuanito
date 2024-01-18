using System.Security.Cryptography;
using System.Text;

namespace FerreteriaJuanito.Clases.Utils
{
    public class Encripta
    {
        //base publica
        public string AES256_LOGIN_Key = "eXSQ6pSMaax9fA8dJLURCWn79jMPskCj";
        public string AES256_USER_Key = "hUf3eof71VCa7IIjFlNewZ73yBWjckdX";

        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] streams = sha256.ComputeHash(encoding.GetBytes(str)); ;
            StringBuilder sb = new StringBuilder();
            // streams = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < streams.Length; i++)
            {
                sb.AppendFormat("{0:X2}", streams[i]);
            }
            return sb.ToString();

        }

        public string AES256_Encritar(string key, string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string AES256_Desencriptar(string key, string textoCifrado)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(textoCifrado);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string CorregirToken(string token)
        {
            return token.Replace("%2F", "/");
        }

    }
}
