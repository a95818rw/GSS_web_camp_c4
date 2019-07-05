using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHR.Models
{
    public class BookService
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
        /// 新增書籍
        /// </summary>
        /// <param name="Book"></param>
        /// <returns>書本編號</returns>
        public int InsertBook(Models.Books Book)
        {
            string sql = @" INSERT INTO dbo.BOOK_DATA
						 (
							 BOOK_ID, BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, 
                             BOOK_BOUGHT_DATE, BOOK_AUTHOR, BOOK_PUBLISHER
						 )
						VALUES
						(
							 @BOOK_ID, @BOOK_NAME, @BOOK_CLASS_ID, @BOOK_AUTHOR, 
                             @BOOK_BOUGHT_DATE, @BOOK_AUTHOR, @BOOK_PUBLISHER
						)
						Select SCOPE_IDENTITY()";
            int BookId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", Book.BookName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_ID", Book.BookClassID));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", Book.Author));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", Book.Publisher));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", Book.BookBoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", Book.BookClassID));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }
     

        /// <summary>
        /// 依照條件取得書本資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Books> GetBookByCondition(Models.BookSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT  BOOK_ID, BOOK_NAME, BOOK_CLASS_ID, 
                                  BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_AUTHOR, BOOK_PUBLISHER
                           FROM dbo.BOOK_DATA
                           Where 
                                 (BOOK_ID = @BookId or @BookId='') AND
                                 (UPPER(BOOK_NAME) LIKE UPPER('%' + @BookName + '%')or @BookName='') AND
                                 ((BOOK_BOUGHT_DATE BETWEEN @DateStart AND @DateEnd))";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", arg.BookId == null ? string.Empty : arg.BookId));
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@DateStart", arg.BoughtDateStart == null ? "1900/01/01" : arg.BoughtDateStart));
                cmd.Parameters.Add(new SqlParameter("@DateEnd", arg.BoughtDateEnd == null ? "2500/12/31" : arg.BoughtDateEnd));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }

        /// <summary>
        /// 刪除客戶
        /// </summary>
        public void DeleteBookById(string BookId)
        {
            try
            {
                string sql = "Delete FROM dbo.BOOK_DATA Where BookId=@BookId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// Map資料進List
        /// </summary>
        /// <param name="bookData"></param>
        /// <returns></returns>

        private List<Models.Books> MapBookDataToList(DataTable bookData)
        {
            List<Models.Books> result = new List<Books>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Books()
                {
                    BookID = (int)row["BOOK_ID"],
                    BookName = row["BOOK_NAME"].ToString(),
                    BookClassID = row["BOOK_CLASS_ID"].ToString(),
                    BookBoughtDate = row["BOOK_BOUGHT_DATE"].ToString(),
                    Author = row["BOOK_AUTHOR"].ToString(),
                    Publisher = row["BOOK_PUBLISHER"].ToString()
                });
            }
            return result;
        }
    }
}