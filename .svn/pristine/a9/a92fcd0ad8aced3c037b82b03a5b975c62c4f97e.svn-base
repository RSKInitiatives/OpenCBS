namespace OpenCBS.Manager
{
    public class Hasher
    {
        public static bool Validate(string plainText, string hashedText)
        {
            if (string.IsNullOrWhiteSpace(plainText) || string.IsNullOrWhiteSpace(hashedText))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(plainText, hashedText);
        }

        public static string Get(string text)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(text, salt);
        }
    }
}
