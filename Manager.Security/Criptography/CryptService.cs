using System.Security.Cryptography;
using System.Text;
using Manager.Core.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Manager.Security.Criptography
{
    public sealed class CryptService
    {
        /// <summary>
        /// Verify if hashes key match.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashToCompare"></param>
        /// <param name="salt"></param>
        /// <returns>True if matches, false otherwise</returns>
        public bool CheckHash(string password, byte[] hashToCompare, byte[] salt = null)
        {
            byte[] sentPassword = salt.IsNull() ? SHA256Hash(password) : PBKDF2Hash(password, salt);

            return CompareHash(sentPassword, hashToCompare);
        }

        /// <summary>
        /// Generate a random sequence 128-bits of nonzero value.
        /// </summary>
        /// <returns>Byte array</returns>
        public byte[] CreateSalt()
        {
            byte[] salt = new byte[128 / 8];
            
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// Performs hash value using the SHA1 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A hash key</returns>
        public byte[] SHA256Hash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            return SHA256.Create().ComputeHash(passwordBytes);
        }

        /// <summary>
        /// Performs key derivation using the PBKDF2 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>The derived key</returns>
        public byte[] PBKDF2Hash(string password, byte[] salt)
        {
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            );
            
            return hashed;
        }

        /// <summary>
        /// Compare the values of the two hashes key.
        /// </summary>
        /// <param name="sentHash">The value sended</param>
        /// <param name="compareHash">The value to compare</param>
        /// <returns>True if matches, false otherwise</returns>
        private bool CompareHash(byte[] sentHash, byte[] compareHash)
        {
            for (int x = 0; x < sentHash.Length; x++)
            {
                if (sentHash[x] != compareHash[x])
                {
                    return false;
                }
            }

            return true;
        }
    }
}