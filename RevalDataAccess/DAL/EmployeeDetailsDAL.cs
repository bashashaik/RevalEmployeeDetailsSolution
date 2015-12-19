using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Revalsys.Properties;
using System.Configuration;
using Revalsys.Common;

namespace Revalsys.DataAccess
{
    public class EmployeeDetailsDAL
    {
        string strConnectionString = ConfigurationManager.ConnectionStrings["strConnectionString"].ConnectionString;
        #region BindQualificationDetails
        /*-----------------------------------------------------------------------------------------------------------
         * Description      :       This method is used to get the Qualification details form tblQualification table.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ------------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 08, 2015	10:34 Am		 		Creation 
         */

        public List<EmployeeDetailsListDTO> BindQualificationDetails()
        {
            List<EmployeeDetailsListDTO> lstEmployeeDetailsListDTO = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            SqlDataReader sdr = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspGetQualificationDetails", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sdr = sqlCmd.ExecuteReader();
                    lstEmployeeDetailsListDTO = new List<EmployeeDetailsListDTO>();
                    while (sdr.Read())
                    {
                        objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
                        objEmployeeDetailsListDTO.QualificationId = (int)sdr[0];
                        objEmployeeDetailsListDTO.Qualification = (string)sdr[1];
                        lstEmployeeDetailsListDTO.Add(objEmployeeDetailsListDTO);
                        objEmployeeDetailsListDTO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCmd = null;
                sqlConnection = null;
                sdr = null;
            }
            return lstEmployeeDetailsListDTO;
        }
        #endregion

        #region BindDesignationDetails
        /*
         * Description      :       This method is used to get the Designation details
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        public List<EmployeeDetailsListDTO> BindDesignationDetails()
        {
            List<EmployeeDetailsListDTO> lstEmployeeDetailsListDTO = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            SqlDataReader sdr = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspGetDesignationDetails", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sdr = sqlCmd.ExecuteReader();
                    lstEmployeeDetailsListDTO = new List<EmployeeDetailsListDTO>();
                    while (sdr.Read())
                    {
                        objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
                        objEmployeeDetailsListDTO.DesignationId = (int)sdr[0];
                        objEmployeeDetailsListDTO.Designation = (string)sdr[1];
                        lstEmployeeDetailsListDTO.Add(objEmployeeDetailsListDTO);
                        objEmployeeDetailsListDTO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCmd = null;
                sqlConnection = null;
                sdr = null;
            }
            return lstEmployeeDetailsListDTO;
        }
        #endregion

        #region BindReportingEmployeeBasedOnDesignationId
        /*
         * Description      :       This method is used to bind reporting employees based on designation id
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */
        public List<EmployeeDetailsListDTO> BindReportingEmployeeBasedOnDesignationId(int designationId, long employeeId)
        {
            List<EmployeeDetailsListDTO> lstEmployeeDetailsListDTO = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;
            SqlDataReader sdr = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspGetReportingEmployeeBasedOnDesignationId", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@DesignationId", designationId);
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    sqlConnection.Open();
                    sdr = sqlCmd.ExecuteReader();
                    lstEmployeeDetailsListDTO = new List<EmployeeDetailsListDTO>();
                    while (sdr.Read())
                    {
                        objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
                        objEmployeeDetailsListDTO.ReportingEmployeeId = (long)sdr[0];
                        objEmployeeDetailsListDTO.ReportingEmployee = (string)sdr[1];
                        lstEmployeeDetailsListDTO.Add(objEmployeeDetailsListDTO);
                        objEmployeeDetailsListDTO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;
                sdr = null;
            }
            return lstEmployeeDetailsListDTO;
        }

        #endregion

        #region InsertEmployeeDetails
        /*
         * Description      :       This method is used to New Employee into NazeerDB database tblNazeerEmployee table
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- --------------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:57 Pm		 		Creation 
         */

        public string InsertEmployeeDetails(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            string strReturnValue = string.Empty;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspInsertEmployeeDetails", sqlConnection);

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ComapanyId", objEmployeeDetailsListDTO.CompanyId);
                    sqlCmd.Parameters.AddWithValue("@SiteId", objEmployeeDetailsListDTO.SiteId);
                    sqlCmd.Parameters.AddWithValue("@DepartmentId", objEmployeeDetailsListDTO.DepartmentId);
                    sqlCmd.Parameters.AddWithValue("@DesignationId", objEmployeeDetailsListDTO.DesignationId == 0 ? (object)(DBNull.Value) : objEmployeeDetailsListDTO.DesignationId);
                    sqlCmd.Parameters.AddWithValue("@ReportingEmployeeId", objEmployeeDetailsListDTO.ReportingEmployeeId == 0 ? (object)(DBNull.Value) : objEmployeeDetailsListDTO.ReportingEmployeeId);
                    sqlCmd.Parameters.AddWithValue("@QualificationId", objEmployeeDetailsListDTO.QualificationId == 0 ? (object)(DBNull.Value) : objEmployeeDetailsListDTO.QualificationId);
                    sqlCmd.Parameters.AddWithValue("@EmployeeName", objEmployeeDetailsListDTO.EmployeeName);
                    sqlCmd.Parameters.AddWithValue("@Salary", objEmployeeDetailsListDTO.Salary);
                    sqlCmd.Parameters.AddWithValue("@Email", objEmployeeDetailsListDTO.Email);
                    sqlCmd.Parameters.AddWithValue("@MobileNumber", objEmployeeDetailsListDTO.MobileNumber);
                    sqlCmd.Parameters.AddWithValue("@ProfileImage", objEmployeeDetailsListDTO.ProfileImage);
                    sqlCmd.Parameters.AddWithValue("@EmployeeNumber", string.IsNullOrEmpty(objEmployeeDetailsListDTO.EmployeeNumber) ? (object)DBNull.Value : objEmployeeDetailsListDTO.EmployeeNumber);
                    sqlCmd.Parameters.AddWithValue("@Tag", objEmployeeDetailsListDTO.Tag);
                    sqlCmd.Parameters.AddWithValue("@Comments", objEmployeeDetailsListDTO.Comments);
                    sqlCmd.Parameters.AddWithValue("@DisplayOnWeb", objEmployeeDetailsListDTO.DisplayOnWeb);
                    sqlCmd.Parameters.AddWithValue("@IsPublished", objEmployeeDetailsListDTO.IsPublished);
                    sqlCmd.Parameters.AddWithValue("@SortOrder", objEmployeeDetailsListDTO.SortOrder);
                    sqlCmd.Parameters.AddWithValue("@IPAddress", objEmployeeDetailsListDTO.IPAddress);
                    sqlCmd.Parameters.AddWithValue("@CreatedBy", objEmployeeDetailsListDTO.CreatedBy);
                    sqlCmd.Parameters.AddWithValue("@DateCreated", objEmployeeDetailsListDTO.DateCreated);
                    sqlCmd.Parameters.AddWithValue("@UpdatedBy", objEmployeeDetailsListDTO.UpdatedBy);
                    sqlCmd.Parameters.AddWithValue("@LastUpdated", objEmployeeDetailsListDTO.LastUpdated);
                    sqlCmd.Parameters.AddWithValue("@DeletedBy", objEmployeeDetailsListDTO.DeletedBy);
                    sqlCmd.Parameters.AddWithValue("@DateDeleted", objEmployeeDetailsListDTO.DateDeleted);
                    sqlCmd.Parameters.AddWithValue("@PublishedBy", objEmployeeDetailsListDTO.PublishedBy);
                    sqlCmd.Parameters.AddWithValue("@DatePublished", objEmployeeDetailsListDTO.DatePublished);
                    sqlConnection.Open();
                    int nou = sqlCmd.ExecuteNonQuery();
                    if (nou != -1 && nou != 0)
                    {
                        strReturnValue = "Inserted successfully.";
                    }

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("EmployeeNumber"))
                {
                    strReturnValue = "Employee number already exists.";
                }
                else if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("Email"))
                {
                    strReturnValue = "Email already exist.";
                }
                else
                {
                    strReturnValue = "Some error has occured, employee details not inserted.";
                }
            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;
            }

            return strReturnValue;
        }
        #endregion

        #region GetEmployeeDetailsById
        /*
         * Description      :       This method is used to get employee details based on employee id.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        public EmployeeDetailsListDTO GetEmployeeDetailsById(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            SqlDataReader sdr = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspGetEmployeeDetailsByEmployeeId", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", objEmployeeDetailsListDTO.EmployeeId);
                    sqlCmd.Parameters.AddWithValue("@CompanyId", objEmployeeDetailsListDTO.CompanyId);
                    sqlCmd.Parameters.AddWithValue("@SiteID", objEmployeeDetailsListDTO.SiteId);
                    sqlCmd.Parameters.AddWithValue("@DepartmentId", objEmployeeDetailsListDTO.DepartmentId);
                    sqlConnection.Open();
                    sdr = sqlCmd.ExecuteReader();

                    if (sdr.Read())
                    {
                        objEmployeeDetailsListDTO.EmployeeName = (string)sdr["EmployeeName"];
                        objEmployeeDetailsListDTO.DesignationId = sdr["DesignationId"] == DBNull.Value ? 0 : (int)sdr["DesignationId"];
                        objEmployeeDetailsListDTO.ReportingEmployeeId = sdr["ReportingEmployeeId"] == DBNull.Value ? 0 : (long)sdr["ReportingEmployeeId"];
                        objEmployeeDetailsListDTO.QualificationId = sdr["QualificationId"] == DBNull.Value ? 0 : (int)sdr["QualificationId"];
                        objEmployeeDetailsListDTO.Salary = decimal.Parse((string)sdr["Salary"]);
                        objEmployeeDetailsListDTO.Email = (string)sdr["Email"];
                        objEmployeeDetailsListDTO.MobileNumber = (string)sdr["MobileNumber"];
                        objEmployeeDetailsListDTO.ProfileImage = (string)sdr["ProfileImage"];
                        objEmployeeDetailsListDTO.EmployeeNumber = sdr["EmployeeNumber"] == DBNull.Value ? string.Empty : sdr["EmployeeNumber"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;
                sdr = null;
            }
            return objEmployeeDetailsListDTO;
        }
        #endregion

        #region UpdateEmployeeDetails
        /*
         * Description      :       This method is used to Update Employee Details based on CompanyId, SiteId, DepartmentId, EmployeeId
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -------------------------------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	1:35 Pm		 		Creation 
         */

        public string UpdateEmployeeDetails(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            string strReturnValue = string.Empty;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspUpdateEmployeeDetails", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@CompanyId", objEmployeeDetailsListDTO.CompanyId);
                    sqlCmd.Parameters.AddWithValue("@SiteID", objEmployeeDetailsListDTO.SiteId);
                    sqlCmd.Parameters.AddWithValue("@DepartmentId", objEmployeeDetailsListDTO.DepartmentId);
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", objEmployeeDetailsListDTO.EmployeeId);
                    sqlCmd.Parameters.AddWithValue("@EmployeeName", objEmployeeDetailsListDTO.EmployeeName);
                    sqlCmd.Parameters.AddWithValue("@DesignationId", objEmployeeDetailsListDTO.DesignationId == 0 ? (Object)DBNull.Value : objEmployeeDetailsListDTO.DesignationId);
                    sqlCmd.Parameters.AddWithValue("@ReportingEmployeeId", objEmployeeDetailsListDTO.ReportingEmployeeId == 0 ? (Object)DBNull.Value : objEmployeeDetailsListDTO.ReportingEmployeeId);
                    sqlCmd.Parameters.AddWithValue("@Salary", objEmployeeDetailsListDTO.Salary);
                    sqlCmd.Parameters.AddWithValue("@Email", objEmployeeDetailsListDTO.Email);
                    sqlCmd.Parameters.AddWithValue("@MobileNumber", objEmployeeDetailsListDTO.MobileNumber);
                    sqlCmd.Parameters.AddWithValue("@QualificationId", objEmployeeDetailsListDTO.QualificationId == 0 ? (Object)DBNull.Value : objEmployeeDetailsListDTO.QualificationId);
                    sqlCmd.Parameters.AddWithValue("@ProfileImage", objEmployeeDetailsListDTO.ProfileImage);
                    sqlCmd.Parameters.AddWithValue("@EmployeeNumber", objEmployeeDetailsListDTO.EmployeeNumber);

                    sqlConnection.Open();
                    int nou = sqlCmd.ExecuteNonQuery();
                    if (nou > 0)
                    {
                        strReturnValue = "Updated successfully.";
                    }
                    else
                    {
                        strReturnValue = "Update failed.";
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("EmployeeNumber"))
                {
                    strReturnValue = "Employee number already exists.";
                }
                else if (ex.Message.Contains("UNIQUE") && ex.Message.Contains("Email"))
                {
                    strReturnValue = "Email already exist.";
                }

            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;

            }

            return strReturnValue;
        }
        #endregion

        #region DeleteEmployee
        /*
         * Description      :       This method is used to Delete Employee in tblNazeerEmployee Table
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	03:27 Pm		 		Creation 
         */

        public string DeleteEmployee(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            string strReturnValue = string.Empty;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspDeleteEmployee", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", objEmployeeDetailsListDTO.EmployeeId);
                    sqlCmd.Parameters.AddWithValue("@ComapanyId", objEmployeeDetailsListDTO.CompanyId);
                    sqlCmd.Parameters.AddWithValue("@SiteId", objEmployeeDetailsListDTO.SiteId);
                    sqlCmd.Parameters.AddWithValue("@DepartmentId", objEmployeeDetailsListDTO.DepartmentId);
                    sqlConnection.Open();
                    int nou = sqlCmd.ExecuteNonQuery();
                    if (nou > 0)
                    {
                        strReturnValue = "Deleted successfully.";
                    }
                    else
                    {
                        strReturnValue = "Employee can't delete due to dependency.";
                    }
                }
            }
            catch (Exception ex)
            {
                strReturnValue = "Deletion failed, " + ex.Message + ".";
            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;
            }
            return strReturnValue;
        }
        #endregion

        #region GetEmployeeDetails
        /*
         * Description      :       This method is used to GetEmployeeDetails from tblNazeerEmployee table
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- --------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:38 Pm		 		Creation 
         */

        public List<EmployeeDetailsListDTO> GetEmployeeDetails()
        {
            List<EmployeeDetailsListDTO> lstEmployeeDetailsList = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCmd = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;
            SqlDataAdapter sdaGetEmployees = null;
            DataTable dtGetEmployees = null;
            List<DataRow> lstRows = null;

            try
            {
                using (sqlConnection = new SqlConnection(strConnectionString))
                {
                    sqlCmd = new SqlCommand("uspGetEmployeeDetails", sqlConnection);
                    objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@CompanyId", objEmployeeDetailsListDTO.CompanyId);
                    sqlCmd.Parameters.AddWithValue("@SiteID", objEmployeeDetailsListDTO.SiteId);
                    sqlCmd.Parameters.AddWithValue("@DepartmentId", objEmployeeDetailsListDTO.DepartmentId);
                    sdaGetEmployees = new SqlDataAdapter(sqlCmd);
                    dtGetEmployees = new DataTable();
                    sdaGetEmployees.Fill(dtGetEmployees);
                    if (dtGetEmployees != null && dtGetEmployees.Rows.Count > 0)
                    {
                        lstRows = new List<DataRow>(dtGetEmployees.Select());
                        lstEmployeeDetailsList = (List<EmployeeDetailsListDTO>)CommonDAL.ConvertToList<EmployeeDetailsListDTO>(lstRows);
                    }
                    if (lstEmployeeDetailsList != null && lstEmployeeDetailsList.Count > 0)
                    {
                        return lstEmployeeDetailsList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection = null;
                sqlCmd = null;
                sdaGetEmployees = null;
                dtGetEmployees = null;
                lstRows = null;
                objEmployeeDetailsListDTO = null;
            }
        }
        #endregion
    }
}
