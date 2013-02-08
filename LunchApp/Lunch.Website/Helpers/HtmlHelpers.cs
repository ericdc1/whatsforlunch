using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Security.Cryptography;

 
namespace System.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Md5Hash(this HtmlHelper helper, string value)
        {
            return new MvcHtmlString(CalculateMd5Hash(value));
        }

        public static string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }

}
