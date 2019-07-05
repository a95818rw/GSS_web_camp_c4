using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eHR.Models
{

    public class Employees
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("員工編號")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 員工姓名(First Name)
        /// </summary>
        [DisplayName("員工姓名(First Name)")]
        [Required(ErrorMessage = "此欄位必填")]
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// 員工姓名(Last Name)
        /// </summary>
        [DisplayName("員工姓名(Last Name)")]
        [Required(ErrorMessage = "此欄位必填")]
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DisplayName("職稱")]
        public string JobTitle { get; set; }

        [DisplayName("職稱-Id")]
        [Required(ErrorMessage = "此欄位必填")]
        public string JobTitleId { get; set; }

        /// <summary>
        /// 稱謂
        /// </summary>
        [DisplayName("稱謂")]
        [Required(ErrorMessage = "此欄位必填")]
        public string TitleOfCourtesy { get; set; }


        /// <summary>
        /// 任職日期
        /// </summary>
        [DisplayName("任職日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string HireDate { get; set; }

        /// <summary>
        /// 生日日期
        /// </summary>
        [DisplayName("生日日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BirthDate { get; set; }

        /// <summary>
        /// 年齡
        /// </summary>
        [DisplayName("年齡")]
        [Required(ErrorMessage = "此欄位必填")]
        public int Age { get; set; }


        /// <summary>
        /// 國家
        /// </summary>
        [DisplayName("國家")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Country { get; set; }

        /// <summary>
        /// 城市代號
        /// </summary>
        [DisplayName("城市代號")]
        [Required(ErrorMessage = "此欄位必填")]
        public string City { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [DisplayName("性別")]
        public string Gender { get; set; }

        [DisplayName("性別-Id")]
        public string GenderId { get; set; }
        /// <summary>
        /// 電話號碼
        /// </summary>
        [DisplayName("電話號碼")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Address { get; set; }

        /// <summary>
        /// 直屬主管
        /// </summary>
        [DisplayName("直屬主管")]
        public string ManagerId { get; set; }

        /// <summary>
        /// 月薪
        /// </summary>
        [DisplayName("月薪")]
        public string MonthlyPayment { get; set; }

        /// <summary>
        /// 年薪
        /// </summary>
        [DisplayName("年薪")]
        public string YearlyPayment { get; set; }
    }
}