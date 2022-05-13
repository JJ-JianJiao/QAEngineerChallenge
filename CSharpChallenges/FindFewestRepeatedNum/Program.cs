using System;
using System.Collections.Generic;
using System.IO;

namespace FindFewestRepeatedNum
{
    internal class Program
    {
        //store the "Relative Path" of the resource folder
        static string FILE_FOLDER_PATH = Directory.GetCurrentDirectory() + "\\src\\";
        static string FOLDER_NOT_EXIST = "The folder does not exists";
        static string FILES_NOT_VALID = "There is no valid file in the folder";

        static void Main(string[] args)
        {
            List<string> fileNames = GetAllFileNames(FILE_FOLDER_PATH);

            if (ValidationFileNames(fileNames)) {
                TraverseFiles(fileNames);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Returns a string List of all the *.txt file's names under the specified folder
        /// </summary>
        /// <param name="folderPath">The path of the specified folder.</param>
        private static List<string> GetAllFileNames(string folderPath)
        {
            List<string> fileNameList = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(folderPath);

            if (!folder.Exists)
            {
                return null;
            }

            foreach (FileInfo file in folder.GetFiles("*.txt"))
            {
                fileNameList.Add(file.Name);
            }
            return fileNameList;
        }

        /// <summary>
        /// validate the List of file names
        /// </summary>
        /// <param name="fileNames">the string list of file names</param>
        /// <returns>A boolean value, True means is valid, False means is invalid.</returns>
        private static bool ValidationFileNames(List<string> fileNames)
        {
            if (fileNames == null)
            {
                Console.WriteLine(FOLDER_NOT_EXIST);
                return false;
            }
            else if (fileNames.Count == 0)
            {
                Console.WriteLine(FILES_NOT_VALID);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Travers all files and get the results.
        /// </summary>
        /// <param name="fileNames">the List of file names</param>
        private static void TraverseFiles(List<string> fileNames)
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                string filePath = Path.Combine(FILE_FOLDER_PATH, fileNames[i]);
                string[] numsStr = ReadFile(filePath);
                GetFewestRepeatedNumberRes(i + 1, fileNames[i], numsStr);
            }
        }

        /// <summary>
        /// Get all data of the specified txt file
        /// </summary>
        /// <param name="filePath">The path of the specified file.</param>
        /// <returns>A string arry, Each line of the file is an element of the array</returns>
        private static string[] ReadFile(string filePath) { 
            string[] numsStr = File.ReadAllLines(filePath);
            return numsStr;
        }

        /// <summary>
        /// print out the result of the fewest repeated number of times for the specified txt file
        /// Format is: 1: File: 1.txt, Number: 32, Repetead: 3 times
        /// </summary>
        /// <param name="index">the index of the txt file</param>
        /// <param name="fileName">the name of the txt file</param>
        /// <param name="numsStr">A string arry, Each line of the file is an element of the array</param>
        private static void GetFewestRepeatedNumberRes(int index, string fileName, string[] numsStr)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < numsStr.Length; i++)
            {
                if (!dict.ContainsKey(numsStr[i]))
                {
                    dict.Add(numsStr[i], 1);
                }
                else
                {
                    dict[numsStr[i]]++;
                }
            }

            int minTimes = int.MaxValue;
            string numStr = "";
            foreach (var item in dict)
            {
                if (item.Value < minTimes)
                {
                    minTimes = item.Value;
                    //numsStr = item.Key[0];
                    numStr = item.Key;
                }
                else if (item.Value == minTimes) {
                    numStr = int.Parse(numStr) < int.Parse(item.Key)?numStr:item.Key;
                }
            }

            Console.WriteLine("{0}:File:{1}, Number:{2}, Repetead:{3} times", index,fileName, numStr,minTimes);
        }
    }
}
