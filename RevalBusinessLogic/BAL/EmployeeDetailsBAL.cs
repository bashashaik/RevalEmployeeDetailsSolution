using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Revalsys.DataAccess;
using Revalsys.Properties;

namespace Revalsys.BusinessLogic
{
    public class EmployeeDetailsBAL
    {
        #region BindQualificationDetails
        /*
         * Description      :       This method is used to get Qualification details from DAL
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:35 Pm		 	    Creation 
         */

        public List<EmployeeDetailsListDTO> BindQualificationDetails()
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();
            return objEmployeeDetailsDAL.BindQualificationDetails();
        }
        #endregion

        #region BindDesignationDetails
        /*
         * Description      :       This method is used to get Designation details from DAL
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:38 Pm		 	    Creation 
         */

        public List<EmployeeDetailsListDTO> BindDesignationDetails()
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();
            return objEmployeeDetailsDAL.BindDesignationDetails();
        }
        #endregion

        #region BindReportingEmployeeBasedOnDesignationId
        /*
         * Description      :       This method is used to Get the ReportingEmployeeBasedOnDesignationId from DAL
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:43 Pm		 		Creation 
         */

        public List<EmployeeDetailsListDTO> BindReportingEmployeeBasedOnDesignationId(int designationId, long employeeId)
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();

            return objEmployeeDetailsDAL.BindReportingEmployeeBasedOnDesignationId(designationId, employeeId);
        }
        #endregion

        #region InsertEmployee
        /*
         * Description      :       This method is used to INSERT the new Employee
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	12:38 Pm		 		Creation 
         */

        public string InsertEmployeeDetails(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();

            return objEmployeeDetailsDAL.InsertEmployeeDetails(objEmployeeDetailsListDTO);
        }

        #endregion

        #region GetEmployeeDetails
        /*
         * Description      :       This method is used to GET the All EmployeeDetails 
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	01:34 Pm		 		Creation 
         */

        public List<EmployeeDetailsListDTO> GetEmployeeDetails()
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();

            return objEmployeeDetailsDAL.GetEmployeeDetails();
        }
        #endregion

        #region GetEmployeeById
        /*
         * Description      :       This method is used to Get the EmployeeDetails based on Employee Id
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        public EmployeeDetailsListDTO GetEmployeeDetailsById(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();
            return objEmployeeDetailsDAL.GetEmployeeDetailsById(objEmployeeDetailsListDTO);
        }
        #endregion

        #region UpdateEmployeeDetails
        /*
         * Description      :       This method is used to Update the Employee
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	1:30 Pm		 		    Creation 
         */

        public string UpdateEmployeeDetails(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();

            return objEmployeeDetailsDAL.UpdateEmployeeDetails(objEmployeeDetailsListDTO);
        }

        #endregion

        #region DeleteEmployee
        /*
         * Description      :       This method is used to Delete Employee
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	03:23 Pm		 		Creation 
         */

        public string DeleteEmployee(EmployeeDetailsListDTO objEmployeeDetailsListDTO)
        {
            EmployeeDetailsDAL objEmployeeDetailsDAL = new EmployeeDetailsDAL();

            return objEmployeeDetailsDAL.DeleteEmployee(objEmployeeDetailsListDTO);
        }
        #endregion
    }
}
