using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eHR.Models
{
    public class EmployeeService
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
        /// 新增員工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>員工編號</returns>
        public int InsertEmployee(Models.Employees employee)
        {
            string sql = @" INSERT INTO HR.Employees
						 (
							 FirstName, LastName, Title, TitleOfCourtesy, Gender, ManagerID, 
                             HireDate, BirthDate, 
                             Address, City, Country, Phone, 
                             MonthlyPayment, YearlyPayment
						 )
						VALUES
						(
							 @EmployeeFirstName,@EmployeeLastName, @JobTitle, @TitleOfCourtesy, @Gender, @ManagerId, 
                             @HireDate, @BirthDate,
                             @Address, @City, @Country, @Phone, 
                             @MonthlyPayment, @YearlyPayment
						)
						Select SCOPE_IDENTITY()";
            int EmployeeId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeFirstName", employee.EmployeeFirstName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeLastName", employee.EmployeeLastName));
                cmd.Parameters.Add(new SqlParameter("@JobTitle", employee.JobTitleId));
                cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", employee.TitleOfCourtesy));
                cmd.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", employee.BirthDate));
                cmd.Parameters.Add(new SqlParameter("@Address", employee.Address));
                cmd.Parameters.Add(new SqlParameter("@City", employee.City));
                cmd.Parameters.Add(new SqlParameter("@Gender", employee.GenderId == null ? (Object)DBNull.Value : employee.GenderId));
                cmd.Parameters.Add(new SqlParameter("@Country", employee.Country));
                cmd.Parameters.Add(new SqlParameter("@ManagerId", employee.ManagerId == null ? (Object)DBNull.Value : employee.ManagerId));
                cmd.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                cmd.Parameters.Add(new SqlParameter("@MonthlyPayment", employee.MonthlyPayment == null ? (Object)DBNull.Value : employee.MonthlyPayment));
                cmd.Parameters.Add(new SqlParameter("@YearlyPayment", employee.YearlyPayment == null ? (Object)DBNull.Value : employee.YearlyPayment));
                EmployeeId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return EmployeeId;
        }
     

        /// <summary>
        /// 依照條件取得客戶資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Employees> GetEmployeeByCondtioin(Models.EmployeeSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT e.EmployeeID, e.FirstName, e.LastName, e.Title AS JobTitle, ctj.CodeVal AS JobTitleId, 
                                  e.TitleOfCourtesy, CONVERT( varchar(12), HireDate, 23) AS HireDate, 
                                  CONVERT( varchar(12), BirthDate, 23) AS BirthDate, 
                                  DATEPART(yyyy, GETDATE()) - YEAR(e.BirthDate) AS Age, e.Address, e.City, e.Country,
                                  e.Gender AS GenderId, ctg.CodeVal AS Gender, e.ManagerID, 
                                  e.Phone, e.MonthlyPayment, e.YearlyPayment
                           FROM HR.Employees as e 
	                       LEFT JOIN dbo.CodeTable as ctj
	                           ON (e.Title = ctj.CodeId AND ctj.CodeType = 'TITLE')
	                       LEFT JOIN dbo.CodeTable as ctg
	                           ON (e.Gender = ctg.CodeId)
                           Where (e.EmployeeID = @EmployeeId OR @EmployeeId='') AND
                                 (UPPER(e.FirstName + ' ' + e.LastName) LIKE UPPER('%' + @EmployeeName + '%')or @EmployeeName='') AND
                                 (e.Title = @JobTitleId OR @JobTitleId='')AND
                                 ((e.HireDate BETWEEN @HireDateStart AND @HireDateEnd))";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", arg.EmployeeId == null ? string.Empty : arg.EmployeeId));
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", arg.EmployeeName == null ? string.Empty : arg.EmployeeName));
                cmd.Parameters.Add(new SqlParameter("@JobTitleId", arg.JobTitleId == null ? string.Empty : arg.JobTitleId));
                cmd.Parameters.Add(new SqlParameter("@HireDateStart", arg.HireDateStart == null ? "1900/01/01" : arg.HireDateStart));
                cmd.Parameters.Add(new SqlParameter("@HireDateEnd", arg.HireDateEnd == null ? "2500/12/31" : arg.HireDateEnd));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapEmployeeDataToList(dt);
        }

        /// <summary>
        /// 刪除客戶
        /// </summary>
        public void DeleteEmployeeById(string EmployeeId)
        {
            try
            {
                string sql = "Delete FROM HR.Employees Where EmployeeID=@EmployeeId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", EmployeeId));
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
        /// <param name="employeeData"></param>
        /// <returns></returns>

        private List<Models.Employees> MapEmployeeDataToList(DataTable employeeData)
        {
            List<Models.Employees> result = new List<Employees>();
            foreach (DataRow row in employeeData.Rows)
            {
                result.Add(new Employees()
                {
                    EmployeeId = (int)row["EmployeeID"],
                    EmployeeFirstName = row["FirstName"].ToString(),
                    EmployeeLastName = row["LastName"].ToString(),
                    JobTitleId = row["JobTitleId"].ToString(),
                    JobTitle = row["JobTitle"].ToString(),
                    TitleOfCourtesy = row["TitleOfCourtesy"].ToString(),
                    HireDate = row["HireDate"].ToString(),
                    BirthDate = row["BirthDate"].ToString(),
                    Age = (int)row["Age"],
                    Address = row["Address"].ToString(),
                    City = row["City"].ToString(),
                    Country = row["Country"].ToString(),
                    GenderId = row["GenderId"].ToString(),
                    Gender = row["Gender"].ToString(),
                    ManagerId = row["ManagerID"].ToString(),
                    Phone = row["Phone"].ToString(),
                    MonthlyPayment = row["MonthlyPayment"].ToString(),
                    YearlyPayment = row["YearlyPayment"].ToString()
                });
            }
            return result;
        }
    }
}