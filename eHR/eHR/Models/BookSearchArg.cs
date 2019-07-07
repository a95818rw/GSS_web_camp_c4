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
        [DisplayName("書本ID")]
        public string BookId { get; set; }
        [DisplayName("書本名稱")]
        public string BookName { get; set; }
        [DisplayName("圖書類別")]
        public string BookClassName { get; set; }
        [DisplayName("借閱人")]
        public string UserName { get; set; }
        [DisplayName("借閱狀態")]
        public string Status { get; set; }
    }
}