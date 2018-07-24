using BOL;
using DAL;
using System;

namespace BLL
{
    /// <summary>
    /// this class deals with the DB actions, insert new results and searches.
    /// </summary>
    public static class DbManager
    {
        /// <summary>
        /// this parmater becomes true if the user asked for search in the entire computer,
        /// if its true, the wanted folder at the SearchDetail obj becomes "my computer" 
        /// </summary>
        public static bool IsFullComputerSearch { get; set; } = false;

        /// <summary>
        /// this function gets a SearchDetailsModel parameter and inserts it to the DB,
        /// returns the ID of the object so the results that were found for this search will be relaited to it by having the same SearchID
        /// 
        /// </summary>
        /// <param name="searchDetails"> a SearchDetailsModel parameter that need to be enterd to the DB</param>
        /// <returns></returns>
        /// 
   
        public static int UpdateSearchDetails(SearchDetailsModel searchDetails)
        {
            if (IsFullComputerSearch == true)
            {
                searchDetails.SearchFolder = "My Computer";
            }
            
            try
            {
                using (SearchDBEntities db = new SearchDBEntities())
                {

                    SearchDetail s = new SearchDetail();
                    s.SearchFolder = searchDetails.SearchFolder;
                    s.SearchString = searchDetails.SearchString;
                    db.SearchDetails.Add(s);
                    db.SaveChanges();
                    return s.SearchId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }


        /// <summary>
        /// this function gets a SearchResultsModel parameter and inserts it into the DB,
        /// activated for every new result was found
        /// </summary>
        /// <param name="searchResult"> a SearchResultsModel parameter to enter to the DB</param>
        public static void UpdateDbResults(SearchResultsModel searchResult)
        {
            try
            {
                using (SearchDBEntities db = new SearchDBEntities())
                {

                    SearchResult searchResults = new SearchResult();

                    searchResults.SearchId = searchResult.SearchId;
                    searchResults.FilePath = searchResult.FilePath;
                    searchResults.FileName = searchResult.FileName;

                    db.SearchResults.Add(searchResults);
                    db.SaveChanges();
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



    }
}
