﻿using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EvilProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("EP_DB", "Users", "id", "email", autoCreateTables: true); 
        }
    }
}