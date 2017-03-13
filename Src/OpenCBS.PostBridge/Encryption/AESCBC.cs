using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge.Encryption
{
    public class AESCBC
    {
        private static byte[] EncryptBytes(byte[] key, byte[] plaintext)
        {
            using (var cipher = new RijndaelManaged { Key = key })
            {
                using (var encryptor = cipher.CreateEncryptor())
                {
                    var ciphertext = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);

                    // IV is prepended to ciphertext
                    return cipher.IV.Concat(ciphertext).ToArray();
                }
            }
        }

        private  static byte[] DecryptBytes(byte[] key, byte[] packed)
        {
            using (var cipher = new RijndaelManaged { Key = key })
            {
                int ivSize = cipher.BlockSize / 8;

                cipher.IV = packed.Take(ivSize).ToArray();

                using (var encryptor = cipher.CreateDecryptor())
                {
                    return encryptor.TransformFinalBlock(packed, ivSize, packed.Length - ivSize);
                }
            }
        }

        private  static byte[] AddMac(byte[] key, byte[] data)
        {
            using (var hmac = new HMACSHA256(key))
            {
                var macBytes = hmac.ComputeHash(data);

                // HMAC is appended to data
                return data.Concat(macBytes).ToArray();
            }
        }

        private  static bool BadMac(byte[] found, byte[] computed)
        {
            int mismatch = 0;

            // Aim for consistent timing regardless of inputs
            for (int i = 0; i < found.Length; i++)
            {
                mismatch += found[i] == computed[i] ? 0 : 1;
            }

            return mismatch != 0;
        }

        private  static byte[] RemoveMac(byte[] key, byte[] data)
        {
            using (var hmac = new HMACSHA256(key))
            {
                int macSize = hmac.HashSize / 8;

                var packed = data.Take(data.Length - macSize).ToArray();

                var foundMac = data.Skip(packed.Length).ToArray();

                var computedMac = hmac.ComputeHash(packed);

                if (BadMac(foundMac, computedMac))
                {
                    throw new Exception("Bad MAC");
                }

                return packed;
            }
        }

        private  static List<byte[]> DeriveTwoKeys(string password)
        {
            var salt = "12345678";//new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var saltBytes = Encoding.ASCII.GetBytes(salt);

            var kdf = new Rfc2898DeriveBytes(password, saltBytes, 10000);

            var bytes = kdf.GetBytes(32); // Two keys 128 bits each

            return new List<byte[]> { bytes.Take(16).ToArray(), bytes.Skip(16).ToArray() };
        }

        public static byte[] EncryptString(string password, String message)
        {
            var keys = DeriveTwoKeys(password);

            var plaintext = Encoding.UTF8.GetBytes(message);

            var packed = EncryptBytes(keys[0], plaintext);

            return AddMac(keys[1], packed);
        }

        public static String DecryptString(string password, byte[] secret)
        {
            var keys = DeriveTwoKeys(password);

            var packed = RemoveMac(keys[1], secret);

            var plaintext = DecryptBytes(keys[0], packed);

            return Encoding.UTF8.GetString(plaintext);
        }
       
    }
}
