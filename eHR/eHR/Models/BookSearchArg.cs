using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHR.Models
{
    public class BookSearchArg
    {
        [DisplayName("書本編號")]
        public string BookId { get; set; }
        [DisplayName("書本名稱")]
        public string BookName { get; set; }
        [DisplayName("購書起日")]
        public string BoughtDateStart { get; set; }
        [DisplayName("購書迄日")]
        public string BoughtDateEnd { get; set; }
    }
}