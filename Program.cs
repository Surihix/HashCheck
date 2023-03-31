using System;
using System.IO;
using System.Security.Cryptography;

namespace HashCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Specify two files as arguments");
                Console.ReadLine();
                Environment.Exit(0);
            }

            var inFile_1 = args[0];
            var inFile_2 = args[1];

            CheckIfFileExists(inFile_1);
            CheckIfFileExists(inFile_2);

            var f1Hash = "";
            var f2Hash = "";

            ComputeHash(inFile_1, ref f1Hash);
            Console.WriteLine("");
            ComputeHash(inFile_2, ref f2Hash);

            Console.WriteLine("");
            if (f1Hash.Equals(f2Hash))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hashes match", Console.ForegroundColor);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hashes do not match", Console.ForegroundColor);
            }

            Console.ReadLine();
        }

        static void CheckIfFileExists(string inFileName)
        {
            if (!File.Exists(inFileName))
            {
                Console.WriteLine("Specified file " + inFileName + " does not exist");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        static void ComputeHash(string inFileName, ref string hashString)
        {
            var fName = Path.GetFileName(inFileName);

            Console.WriteLine("Computing hash for " + fName + "....");
            using (FileStream fs = new FileStream(inFileName, FileMode.Open, FileAccess.Read))
            {
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    var hashBuffer = mySHA256.ComputeHash(fs);
                    hashString = BitConverter.ToString(hashBuffer).Replace("-", "");
                    Console.WriteLine(hashString);
                }
            }
        }
    }
}