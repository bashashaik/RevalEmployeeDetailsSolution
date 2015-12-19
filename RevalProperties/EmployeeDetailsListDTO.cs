using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Revalsys.Properties
{
    
    public class EmployeeDetailsListDTO
    {
        public long EmployeeId { set; get; }
        public int CompanyId { set; get; }
        public int SiteId { set; get; }
        public int DepartmentId { set; get; }
        public int DesignationId { set; get; }
        public long ReportingEmployeeId { set; get; }
        public int QualificationId { set; get; }
        public string EmployeeName { set; get; }
        public decimal Salary { set; get; }
        public string Email { set; get; }
        public string MobileNumber { set; get; }
        public string ProfileImage { set; get; }
        public string EmployeeNumber { set; get; }
        public string Tag { set; get; }
        public string Comments { set; get; }
        public bool DisplayOnWeb { set; get; }
        public bool IsPublished { set; get; }
        public int SortOrder { set; get; }
        public string IPAddress { set; get; }
        public string CreatedBy { set; get; }
        public DateTime DateCreated { set; get; }
        public string UpdatedBy { set; get; }
        public DateTime LastUpdated { set; get; }
        public string DeletedBy { set; get; }
        public DateTime DateDeleted { set; get; }
        public string PublishedBy { set; get; }
        public DateTime DatePublished { set; get; }
        public string Designation { set; get; }
        public string Qualification { set; get; }
        public string ReportingEmployee { set; get; }

        #region EmployeeDetailsDTO Default Constructor
        /*
        * Description   : This method is the default constructor for EmployeeDetailsListDTO
        *                 it initializes the properties with default values.
        *                          
        *  Version  	  Author        			   Date            		  	         Remarks       
           -- ------------------------------------------------------------------------------------
        *  1.0		    Shaik Nazeer	 	 December 10, 2015	10:34 Am		 		Creation 
        */

        public EmployeeDetailsListDTO()
        {
            EmployeeId = 0;
            CompanyId = 1;
            SiteId = 1;
            DepartmentId = 1;
            DesignationId = 0;
            ReportingEmployeeId = 1;
            QualificationId = 0;
            EmployeeName = string.Empty;
            Salary = 0;
            Email = string.Empty;
            MobileNumber = string.Empty;
            ProfileImage = string.Empty;
            EmployeeNumber = string.Empty;
            Tag = string.Empty;
            Comments = string.Empty;
            DisplayOnWeb = true;
            IsPublished = true;
            SortOrder = 1;
            IPAddress = string.Empty;
            CreatedBy = string.Empty;
            DateCreated = DateTime.Now;
            UpdatedBy = string.Empty;
            LastUpdated = DateTime.Now;
            DeletedBy = string.Empty;
            DateDeleted = DateTime.Now;
            PublishedBy = string.Empty;
            DatePublished = DateTime.Now;
            Designation = string.Empty;
            Qualification = string.Empty;
            ReportingEmployee = string.Empty;
        }
        #endregion
    }

}
