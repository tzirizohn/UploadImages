﻿using System.Web;
using System.Web.Mvc;

namespace Homework_4_01_19
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
