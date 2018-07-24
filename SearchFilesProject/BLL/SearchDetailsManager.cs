using System;
using System.IO;

namespace BLL
{
    /// <summary>
    /// this class deals with searches, calling the needed functions to find the wanted file.
    /// </summary>
    public static class SearchDetailsManager
    {
        public static event Action<string> FilesSearchHandler;

        /// <summary>
        /// this function searchs the file(wanted file) in a directory(sDir)
        /// action recusivly-for every new directory found
        /// the funcrtion calls herself again to search at the new directory was found
        /// </summary>
        /// <param name="wantedFile">a string that represnts a file name or part of the file name the user want to find</param>
        /// <param name="sDir">a string that represents the path that the file need to be searched in</param>
        public static void getFilesRecursive(string wantedFile, string sDir)
        {
            try
            {
                foreach (var file in Directory.GetFiles(sDir))
                {
                    if (Path.GetFileName(file).ToLower().Contains(wantedFile.ToLower()))
                    {
                        SearchFileByEvent(file);
                    }
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    getFilesRecursive(wantedFile, d);
                }
                
            }
            catch (Exception e)
            {
             //Console.WriteLine(e.Message + "-no acsses promition");
            }
        }

        /// <summary>
        /// for every file that found OK, the function invokes it to the event.
        /// </summary>
        public static void SearchFileByEvent(string file)
        {
           
                FilesSearchHandler?.Invoke(file);
        }
    }

}

