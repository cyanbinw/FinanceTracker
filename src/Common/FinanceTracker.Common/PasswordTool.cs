// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

public static class Program
{
    public string GeneratePassword()
    {
        return GeneratePassword(16);
    }

    static string GeneratePassword(int length)
    {
        const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string specialChars = "~!@#()-_=+[]{}.?";

        string allChars = lowerCase + upperCase + digits + specialChars;
        char[] password = new char[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            Random random = new Random();
            HashSet<int> uniqueNumbers = new HashSet<int>();

            while (uniqueNumbers.Count < 4)
            {
                int randomNumber = random.Next(0, 16);
                uniqueNumbers.Add(randomNumber);
            }

            var list = new List<int>(uniqueNumbers);
            // Ensure the password contains at least one of each category
            password[list[0]] = lowerCase[GetRandomIndex(rng, lowerCase.Length)];
            password[list[1]] = upperCase[GetRandomIndex(rng, upperCase.Length)];
            password[list[2]] = digits[GetRandomIndex(rng, digits.Length)];
            password[list[3]] = specialChars[GetRandomIndex(rng, specialChars.Length)];

            // Fill the remaining characters randomly
            for (int i = 0; i < length; i++)
            {
                if (password[i] == ' ' || password[i] == '\0')
                    password[i] = allChars[GetRandomIndex(rng, allChars.Length)];
            }
        }

        // Shuffle the password to randomize character positions
        return new string(password.OrderBy(_ => Guid.NewGuid()).ToArray());
    }

    static int GetRandomIndex(RandomNumberGenerator rng, int maxExclusive)
    {
        byte[] buffer = new byte[4];
        rng.GetBytes(buffer);
        uint randomValue = BitConverter.ToUInt32(buffer, 0);
        return (int)(randomValue % maxExclusive);
    }
}
