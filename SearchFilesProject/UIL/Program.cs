using System;
using System.IO;
using BLL;
using BOL;


namespace UIL
{
    class Program
    {
        /// <summary>
        /// this function is for the menu and checking the inputs-if its ok it calls the needed functions
        /// </summary>
        public static void programRun()
        {
            DbManager.IsFullComputerSearch = false;
            Console.WriteLine("1.Search file on the entire computer\n2.Search file in a specific folder\n3.exit");
            ConsoleKeyInfo k = Console.ReadKey();

            //exit
            if (k.Key == ConsoleKey.NumPad3 || k.Key == ConsoleKey.D3)
            {
                return;
            }
            string folder = "C:";
            string fileName = "";

            //search at the all computer,later it will call the nedded function to make the search
            if (k.Key == ConsoleKey.NumPad1 || k.Key == ConsoleKey.D1)
            {
                Console.Clear();
                DbManager.IsFullComputerSearch = true;
                Console.WriteLine("enter the file name or part of the name yo want to find");
                fileName = Console.ReadLine();
            }

            //search in a specific folder, sets the folder path and the wnated file string later it will call the nedded function
            if (k.Key == ConsoleKey.NumPad2 || k.Key == ConsoleKey.D2)
            {
                //the parameter tell if its the first time at the loop to show the right message to the user.
                bool isFirstTime = true;
                do
                {
                    Console.Clear();
                    if (!isFirstTime)
                    {
                        Console.WriteLine("folder does not exists, please try another path");
                        folder = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("enter the folder path you want to find files at");
                        folder = Console.ReadLine();
                    }

                    isFirstTime = false;

                }
                while (!Directory.Exists(folder));

                Console.WriteLine("enter the file name or part of the name yo want to find");
                fileName = Console.ReadLine();
            }






            int searchID = DbManager.UpdateSearchDetails(new SearchDetailsModel { SearchFolder = folder, SearchString = fileName });
            //creating the SearchRsultModer and sends it to the DB manager and there it will be entered to the DB,and cheks if there are results for the search
            Action<string> callback = (file) =>
             {

                 Console.WriteLine(file);
                 DbManager.UpdateDbResults(new SearchResultsModel
                 {
                     FileName = Path.GetFileName(file),
                     FilePath = file,
                     SearchId = searchID
                 });

             };

            SearchDetailsManager.FilesSearchHandler += callback;

            //callind the search function a for every driver was found
            if (k.Key == ConsoleKey.NumPad1 || k.Key == ConsoleKey.D1)
            {
                //this parameter gets all of the drivers at the computer in case the user want to serch a file in the all compuer
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (var drive in allDrives)
                {

                    SearchDetailsManager.getFilesRecursive(fileName,drive.Name );
                }
            }
            else//calls the search function for the specific folder
            {
                SearchDetailsManager.getFilesRecursive(fileName, folder);
            }
                

            Console.WriteLine("Search has ended\npress 1 to make a new search, press any other key to exit");

            k = Console.ReadKey();
            //if the user wants to make a new search, i clean the delegate and the function calls herself again.
            if (k.Key == ConsoleKey.NumPad1 || k.Key == ConsoleKey.D1)
            {
                SearchDetailsManager.FilesSearchHandler -= callback;
                Console.Clear();
                programRun();
            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine("welcome");
            programRun();

        }
    }
}





