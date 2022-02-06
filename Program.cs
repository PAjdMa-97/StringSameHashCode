using System;
using System.Text;

namespace SameHashCode
{
    internal class SameHashCode
    {
        public static Random Random = new Random();

        static void Main(string[] args)
        {
            string str1, str2, str3;
            GetTripleStrEqualHashCode(out str1, out str2, out str3);

            Console.WriteLine($"Hash code of \"{str1}\" : {str1.GetHashCode()}");
            Console.WriteLine($"Hash code of \"{str2}\" : {str1.GetHashCode()}");
            Console.WriteLine($"Hash code of \"{str3}\" : {str1.GetHashCode()}");
        }

        /// <summary>
        /// Generate 3 different strings with the same hashcode
        /// </summary>
        /// <param name="str1">First generated string</param>
        /// <param name="str2">Second generated string</param>
        /// <param name="str3">Third generated string</param>
        public static void GetTripleStrEqualHashCode(out string str1, out string str2, out string str3)
        {
            ulong iteration = 0;
            Dictionary<int, string> dict = new Dictionary<int, string>(); // Key : hash code / Value : random string 
            Dictionary<int, (string first, string second)> collisionDict = new Dictionary<int, (string first, string second)>(); // store a couple of string sharing the same hash code

            do
            {
                ++iteration;
                string randomStr = RandomString();
                int hash = randomStr.GetHashCode();

                // find a new string with a hash code already stored
                if (!dict.TryAdd(hash, randomStr) && randomStr != dict[hash])
                {
                    if (collisionDict.ContainsKey(hash))
                    {
                        // a couple already found so randomStr is the third
                        if (randomStr != collisionDict[hash].first && randomStr != collisionDict[hash].second)
                        {
                            str1 = collisionDict[hash].first;
                            str2 = collisionDict[hash].second;
                            str3 = randomStr;
                            Console.WriteLine($"nbr iterations : {iteration}");
                            return; 
                        }
                    }
                    // add a new couple of string with the same hash code
                    else
                    {
                        collisionDict.Add(hash, (dict[hash], randomStr));
                    }
                }
            } while (true);
        }

        /// <summary>
        /// Generate a Random string
        /// </summary>
        /// <returns></returns>
        public static string RandomString()
        {
            StringBuilder builder = new StringBuilder(6);
            for (int i = 0; i < builder.Capacity; i++)
            {
                builder.Append(RandomChar());
            }
            return builder.ToString();
        }

        /// <summary>
        /// Generate a random char
        /// </summary>
        /// <returns></returns>
        public static char RandomChar() => (char)Random.Next((int)'a', (int)'z' + 1);

    } 
}