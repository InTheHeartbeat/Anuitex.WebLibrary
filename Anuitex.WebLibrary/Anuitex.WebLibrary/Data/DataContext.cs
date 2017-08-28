using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anuitex.WebLibrary.Data
{
    public class DataContext
    {
        public static DataContext Context => GetInstance();
        public Account CurrentUser { get; set; }

        private static DataContext _instance;

        public LibraryDataContext LibraryDataContext = new LibraryDataContext();

        private static DataContext GetInstance()
        {
            return _instance ?? (_instance = new DataContext());
        }
    }
}