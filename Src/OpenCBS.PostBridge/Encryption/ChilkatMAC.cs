using Chilkat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.PostBridge
{
    public static class ChilkatMAC
    {
        public static string GenerateMac() {
            Crypt2 crypt = new Crypt2();

            bool success = crypt.UnlockComponent("Anything for 30-day trial");
            if (success != true)
            {
                Console.WriteLine(crypt.LastErrorText);
                return "";
            }

            //  Specify 3DES for the encryption algorithm:
            crypt.CryptAlgorithm = "3des";

            //  CipherMode may be "ecb" or "cbc"
            crypt.CipherMode = "cbc";

            //  KeyLength must be 192.  3DES is technically 168-bits;
            //  the most-significant bit of each key byte is a parity bit,
            //  so we must indicate a KeyLength of 192, which includes
            //  the parity bits.
            crypt.KeyLength = 192;

            //  The padding scheme determines the contents of the bytes
            //  that are added to pad the result to a multiple of the
            //  encryption algorithm's block size.  3DES has a block
            //  size of 8 bytes, so encrypted output is always
            //  a multiple of 8.
            crypt.PaddingScheme = 0;

            //  EncodingMode specifies the encoding of the output for
            //  encryption, and the input for decryption.
            //  It may be "hex", "url", "base64", or "quoted-printable".
            crypt.EncodingMode = "hex";

            //  An initialization vector is required if using CBC or CFB modes.
            //  ECB mode does not use an IV.
            //  The length of the IV is equal to the algorithm's block size.
            //  It is NOT equal to the length of the key.
            string ivHex = "0001020304050607";
            crypt.SetEncodedIV(ivHex, "hex");

            //  The secret key must equal the size of the key.  For
            //  3DES, the key must be 24 bytes (i.e. 192-bits).
            string keyHex = "000102030405060708090A0B0C0D0E0F0001020304050607";
            crypt.SetEncodedKey(keyHex, "hex");

            //  Encrypt a string...
            //  The input string is 44 ANSI characters (i.e. 44 bytes), so
            //  the output should be 48 bytes (a multiple of 8).
            //  Because the output is a hex string, it should
            //  be 96 characters long (2 chars per byte).
            string encStr = crypt.EncryptStringENC("The quick brown fox jumps over the lazy dog.");
            Console.WriteLine(encStr);

            //  Now decrypt:
            string decStr = crypt.DecryptStringENC(encStr);
            Console.WriteLine(decStr);
            return decStr;
        }
    }
}
