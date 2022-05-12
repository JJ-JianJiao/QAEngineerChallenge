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
        }

        /// <summary>
        /// 
        /// </summary>
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

        private static void TraverseFiles(List<string> fileNames)
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                string filePath = Path.Combine(FILE_FOLDER_PATH, fileNames[i]);
                string[] numsStr = ReadFile(filePath);
                GetFewestRepeatedNumberRes(i + 1, fileNames[i], numsStr);
            }
        }


        private static string[] ReadFile(string filePath) { 
            string[] numsStr = File.ReadAllLines(filePath);
            return numsStr;
        }


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
