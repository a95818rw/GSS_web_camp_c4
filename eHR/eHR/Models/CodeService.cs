using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHR.Models
{
    public class CodeService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 取得客戶資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBook(string bookId)
        {
            DataTable dt = new DataTable();
            string sql = @"Select BOOK_ID As CodeId, BOOK_NAME As CodeName 
                           FROM dbo.BOOK_DATA
                           WHERE BOOK_ID != @BookID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookId", bookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得codeTable的部分資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCodeTable(string type)
        {
            DataTable dt = new DataTable();
            string sql = @"Select BOOK_CLASS_NAME As CodeName, BOOK_CLASS_ID As CodeID 
                           From dbo.BOOK_CLASS ";
            //Where BOOK_CLASS_ID = @Type
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.Add(new SqlParameter("@Type", type));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }
        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeId"].ToString() + '-' + row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
}