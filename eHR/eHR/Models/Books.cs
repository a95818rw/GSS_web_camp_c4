using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHR.Models
{
    //借閱人 借閱人員編號 英文姓名 中文姓名
    public class Books
    {
        /// <summary>
        /// 書本ID
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("書本ID")]
        public int BookID { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        [DisplayName("書名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        [DisplayName("圖書類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookClassID { get; set; }

        /// <summary>
        /// 購書日期
        /// </summary>
        [DisplayName("購書日期")]
        public string BookBoughtDate { get; set; }

        /// <summary>
        /// 作者 
        /// </summary>
        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Author { get; set; }


        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Publisher { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        //[DisplayName("內容簡介")]
        //[Required(ErrorMessage = "此欄位必填")]
        //public string Introduction { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        //[DisplayName("借閱狀態")]
        //[Required(ErrorMessage = "此欄位必填")]
        //public int Age { get; set; }
    }
}