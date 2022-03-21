using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMW.Models
{
    public static class Description
    {
        /// <summary>
        /// used if operation succeeded
        /// </summary>
        public static string Succeeded = "Succeeded";
        /// <summary>
        /// used if throw exceptions
        /// </summary>
        public static string Failed = "Failed";
        /// <summary>
        /// used if the selected object not found
        /// </summary>
        public static string NotFound = "Not Found";
        /// <summary>
        /// used if the CRUD operations not Complete 
        /// </summary>
        public static string NotComplete = "Not Complete";

    }

}