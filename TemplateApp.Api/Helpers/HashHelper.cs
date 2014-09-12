using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace TemplateApp.Api.Helpers
{
    /// <summary>
    /// A helper class to simplify the generation of hash keys.
    /// </summary>
    public static class HashHelper
    {
        #region public methods

        /// <summary>
        /// Generates a base64 encoded SHA1 hash from the input string.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        public static string GenerateSHA1(string input)
        {
            // extract password as unmanaged byte array
            SHA1Managed sha = new SHA1Managed();
            try
            {
                // generate hash using SHA1 provider
                byte[] passwordData = Encoding.UTF8.GetBytes(input);
                byte[] hashedPasswordData = sha.ComputeHash(passwordData);
                string hashedPassword = Convert.ToBase64String(
                    hashedPasswordData);

                // return hash
                return hashedPassword;
            }

            // ensure the hash is cleared from memory
            finally
            {
                sha.Clear();
            }
        }

        /// <summary>
        /// Generates a hash key using the HMAC SHA256 algorithm, using UTF8 
        /// encoding to generate the binary data used to generate the hash.
        /// </summary>
        /// <param name="input">The string being hashed.</param>
        /// <param name="key">The digest key used to generate the hash.</param>
        public static string GenerateHMACSHA256(string input, string key)
        {
            return GenerateHMACSHA256(input, key, Encoding.UTF8);
        }

        /// <summary>
        /// Generates a hash key using the HMAC SHA256 algorithm, using the 
        /// specified encoding to generate the binary data used for the hash.
        /// </summary>
        /// <param name="input">The string being hashed.</param>
        /// <param name="key">The digest key used to generate the hash.</param>
        /// <param name="encoding">
        /// The encoding used to convert strings when hashing.
        /// </param>
        public static string GenerateHMACSHA256(string input, string key,
            Encoding encoding)
        {
            // create sha
            HMACSHA256 sha = new HMACSHA256(encoding.GetBytes(key));

            // compute hash
            sha.ComputeHash(encoding.GetBytes(input));
            byte[] hash = sha.Hash;

            // build output string
            int hashLength = hash.Length;
            StringBuilder buffer = new StringBuilder(hashLength);
            for (int i = 0; i < hashLength; ++i)
            {
                byte hashValue = hash[i];
                buffer.Append(hashValue.ToString("X2"));
            }

            // return hash value
            return buffer.ToString();
        }

        public static string GenerateMD5(string input)
        {
            return GenerateMD5(input, Encoding.UTF8);
        }

        public static string GenerateMD5(string input, Encoding encoding)
        {
            // get bytes for input
            byte[] inputData = encoding.GetBytes(input);

            // generate hash data
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashData = md5.ComputeHash(inputData);

            // return hash balue
            string hash = BitConverter.ToString(hashData);
            return Regex.Replace(hash, "-", "");
        }

        #endregion

    }  // class HashHelper

}