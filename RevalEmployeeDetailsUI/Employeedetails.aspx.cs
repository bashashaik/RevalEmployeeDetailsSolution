using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Revalsys.BusinessLogic;
using Revalsys.Properties;
using System.IO;

namespace RevalEmployeeDetailsUI
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region !IsPostBack
            /*Actions to be performed when page loads for the first time (!IsPostBack) */
            if (!IsPostBack)
            {
                btnResetEmployee.Style.Add("margin", "30px");
                imgProfileImage.Attributes.Add("Display", "none");
                BindDesignationAndQualificationDetailsToDDLControl();
                GetEmployeeDetails();
            }
            #endregion
        }

        #region ddlDesignation_SelectedIndexChanged
        /*
         * Description      :       This method is used to bind the reporting employees to the 
         *                          ddlReportingEmployee control
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 		     December 10, 2015			         Creation 
         */

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReportingEmployee();
        }
        #endregion

        #region BindDesignationAndQualificationDetailsToDDLControl
        /*
         * Description      :       This method is used to bind the Designation List and Qualification List to the 
         *                          ddlDesignation and ddlQualification controls
         *                              
         *  Version  	  Author        			   Date            		  	            Remarks       
            -- -----------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 		     December 29, 2015  11:29 Am            Creation 
         */

        private void BindDesignationAndQualificationDetailsToDDLControl()
        {
            EmployeeDetailsBAL objEmployeeDetailBAL = new EmployeeDetailsBAL();
            List<EmployeeDetailsListDTO> lstDesignationDetails = objEmployeeDetailBAL.BindDesignationDetails();
            List<EmployeeDetailsListDTO> lstQualificationDetails = objEmployeeDetailBAL.BindQualificationDetails();
            ddlDesignation.DataSource = lstDesignationDetails;
            ddlDesignation.DataValueField = "DesignationId";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlQualification.DataSource = lstQualificationDetails;
            ddlQualification.DataValueField = "QualificationId";
            ddlQualification.DataTextField = "Qualification";
            ddlQualification.DataBind();
            ddlQualification.Items.Insert(0, new ListItem("-- Select --", "0"));
            objEmployeeDetailBAL = null;
        }
        #endregion

        #region Butoon Insert Employee Clicked Event
        /*
         * Description      :       This method is called on button clicked event.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        protected void btnInsertEmployee_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InsertEmployee":
                    InsertEmployee();
                    break;
                case "UpdateEmployee":
                    UpdateEmployee();
                    break;
            }
        }
        #endregion

        #region Button Reset Clicked Event
        /*
         * Description      :       This method is called when reset button is clicked.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        protected void btnResetEmployee_Click(object sender, EventArgs e)
        {
            ClearEmployeeTable();
        }
        #endregion

        #region InsertEmployee
        /*
         * Description      :       This method is used to Insert new employee into database. receives parameters
         *                  :       from form controls
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ----------------------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	11:58 Am		 	    Creation 
         */

        private void InsertEmployee()
        {
            if (Page.IsValid)
            {
                EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;
                EmployeeDetailsBAL objEmployeeDetailsBAL = null;
                lblEmailValidation.Visible = false;
                lblEmployeeNumberValidation.Visible = false;
                lblStatus.Text = string.Empty;
                objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
                objEmployeeDetailsListDTO.EmployeeName = txtEmployeeName.Text.Trim();
                if (Convert.ToInt16(ddlDesignation.SelectedItem.Value) != 0)
                {
                    objEmployeeDetailsListDTO.DesignationId = Convert.ToInt16(ddlDesignation.SelectedItem.Value);
                }
                if (ddlReportingEmployee.Enabled && Convert.ToInt64(ddlReportingEmployee.SelectedItem.Value) != 0)
                {
                    objEmployeeDetailsListDTO.ReportingEmployeeId = Convert.ToInt64(ddlReportingEmployee.SelectedItem.Value);
                }
                else
                {
                    objEmployeeDetailsListDTO.ReportingEmployeeId = Convert.ToInt64(null);
                }
                objEmployeeDetailsListDTO.Salary = decimal.Parse(txtSalary.Text.Trim());
                objEmployeeDetailsListDTO.Email = txtEmail.Text.Trim();
                objEmployeeDetailsListDTO.MobileNumber = txtMobileNumber.Text;
                if (Convert.ToInt16(ddlQualification.SelectedItem.Value) != 0)
                {
                    objEmployeeDetailsListDTO.QualificationId = Convert.ToInt16(ddlQualification.SelectedItem.Value);

                } if (fuProfileImage.HasFile)
                {
                    objEmployeeDetailsListDTO.ProfileImage = "~/Images/" + Path.GetFileName(fuProfileImage.PostedFile.FileName);
                    string fileName = Path.GetFileName(fuProfileImage.PostedFile.FileName);
                    fuProfileImage.SaveAs(Server.MapPath("~/Images/") + fileName);

                }
                else
                {
                    objEmployeeDetailsListDTO.ProfileImage = "~/Images/" + "default.jpg";
                }
                objEmployeeDetailsListDTO.EmployeeNumber = txtEmployeeNumber.Text;
                objEmployeeDetailsBAL = new EmployeeDetailsBAL();
                string strStatus = objEmployeeDetailsBAL.InsertEmployeeDetails(objEmployeeDetailsListDTO);
                
                switch (strStatus)
                {
                    case "Inserted successfully.":
                        ClearEmployeeTable();
                        lblStatus.Text = strStatus;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        break;
                    case "Employee number already exists.":
                        lblEmployeeNumberValidation.Visible = true;
                        break;
                    case "Email already exist.":
                        lblEmailValidation.Visible = true;
                        break;
                    default:
                        lblStatus.Text = strStatus;
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        break;
                }
                GetEmployeeDetails();

            }
        }
        #endregion

        #region GetEmployeeDetails
        /*
         * Description      :       This method is used to bind the Employee details to grid.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	2:34 Pm		 		    Creation 
         */

        public void GetEmployeeDetails()
        {
            List<EmployeeDetailsListDTO> lstEmployeeDetailsListDTO = null;
            EmployeeDetailsBAL objEmployeeDetailsBAL = new EmployeeDetailsBAL();
            lstEmployeeDetailsListDTO = objEmployeeDetailsBAL.GetEmployeeDetails();
            gvEmployees.DataSource = lstEmployeeDetailsListDTO;
            gvEmployees.DataBind();
            objEmployeeDetailsBAL = null;
            lstEmployeeDetailsListDTO = null;
        }
        #endregion

        #region PageIndexChanging
        /*
         * Description      :       This method is Fired when user clicks on next Page
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 10, 2015	05:04 Pm		 		Creation 
         */

        protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //ClearEmployeeTable();
            gvEmployees.PageIndex = e.NewPageIndex;
            GetEmployeeDetails();
        }
        #endregion

        #region RowEditing
        /*
         * Description      :       This method is fired when the edit button is clicked
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	10:34 Am		 		Creation 
         */

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int editIndex = e.NewEditIndex;

            EmployeeDetailsBAL objEmployeeDetailsBAL = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();

            ClearEmployeeTable();
            objEmployeeDetailsListDTO.EmployeeId = (long)gvEmployees.DataKeys[editIndex].Value;
            hfEmployeeId.Value = Convert.ToString(objEmployeeDetailsListDTO.EmployeeId);
            objEmployeeDetailsBAL = new EmployeeDetailsBAL();
            objEmployeeDetailsListDTO = objEmployeeDetailsBAL.GetEmployeeDetailsById(objEmployeeDetailsListDTO);
            txtEmployeeName.Text = objEmployeeDetailsListDTO.EmployeeName;
            BindDesignationAndQualificationDetailsToDDLControl();
            ddlDesignation.SelectedValue = objEmployeeDetailsListDTO.DesignationId.ToString();
            BindReportingEmployee(objEmployeeDetailsListDTO.EmployeeId);
            if (ddlReportingEmployee.Items.Count > 1)
            {
                ddlReportingEmployee.SelectedValue = objEmployeeDetailsListDTO.ReportingEmployeeId.ToString();
            }
            if (objEmployeeDetailsListDTO.ReportingEmployeeId == 0)
            {
                ddlReportingEmployee.SelectedIndex = 0;
                //lblReportingEmployeeValidation.Visible = true;
            }
            txtSalary.Text = Convert.ToString(objEmployeeDetailsListDTO.Salary);
            txtEmail.Text = objEmployeeDetailsListDTO.Email;
            txtMobileNumber.Text = objEmployeeDetailsListDTO.MobileNumber;
            ddlQualification.SelectedValue = objEmployeeDetailsListDTO.QualificationId.ToString();
            imgProfileImage.Attributes["Display"] = "inline";
            imgProfileImage.ImageUrl = objEmployeeDetailsListDTO.ProfileImage;
            imgProfileImage.Visible = true;
            txtEmployeeNumber.Text = objEmployeeDetailsListDTO.EmployeeNumber;
            btnInsertEmployee.Text = "Update";
            btnInsertEmployee.CommandName = "UpdateEmployee";
            objEmployeeDetailsBAL = null;
            objEmployeeDetailsListDTO = null;
        }
        #endregion

        #region UpdateEmployee
        /*
         * Description      :       This method is used to Update the Employee Details
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	02:06 Pm		 		Creation 
         */

        private void UpdateEmployee()
        {
            if (Page.IsValid)
            {
                EmployeeDetailsBAL objEmployeeDetailsBAL = null;
                EmployeeDetailsListDTO objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
                lblEmailValidation.Visible = false;
                lblEmployeeNumberValidation.Visible = false;
                lblStatus.Text = string.Empty;
                objEmployeeDetailsListDTO.EmployeeId = Convert.ToInt64(hfEmployeeId.Value);
                objEmployeeDetailsListDTO.EmployeeName = txtEmployeeName.Text;
                objEmployeeDetailsListDTO.DesignationId = Convert.ToInt16(ddlDesignation.SelectedItem.Value);
                if (ddlReportingEmployee.Enabled && Convert.ToInt64(ddlReportingEmployee.SelectedItem.Value) != 0)
                {
                    objEmployeeDetailsListDTO.ReportingEmployeeId = Convert.ToInt64(ddlReportingEmployee.SelectedItem.Value);
                }
                else
                {
                    objEmployeeDetailsListDTO.ReportingEmployeeId = Convert.ToInt64(null);
                }
                objEmployeeDetailsListDTO.Salary = decimal.Parse(txtSalary.Text);
                objEmployeeDetailsListDTO.Email = txtEmail.Text;
                objEmployeeDetailsListDTO.MobileNumber = txtMobileNumber.Text;
                objEmployeeDetailsListDTO.QualificationId = Convert.ToInt16(ddlQualification.SelectedItem.Value);

                if (fuProfileImage.HasFile)
                {
                    fuProfileImage.SaveAs(Server.MapPath("~/Images/") + Path.GetFileName(fuProfileImage.PostedFile.FileName));
                    objEmployeeDetailsListDTO.ProfileImage = "~/Images/" + Path.GetFileName(fuProfileImage.PostedFile.FileName);
                }
                else
                {
                    objEmployeeDetailsListDTO.ProfileImage = "~/Images/" + Path.GetFileName(imgProfileImage.ImageUrl);
                }
                objEmployeeDetailsListDTO.EmployeeNumber = txtEmployeeNumber.Text;
                objEmployeeDetailsBAL = new EmployeeDetailsBAL();
                string strStatus = objEmployeeDetailsBAL.UpdateEmployeeDetails(objEmployeeDetailsListDTO);
                //ClearEmployeeTable();
                //lblStatus.Text = strStatus;

                switch (strStatus)
                {
                    case "Updated successfully.":
                        ClearEmployeeTable();
                        lblStatus.Text = strStatus;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        break;
                    case "Employee number already exists.":
                        lblEmployeeNumberValidation.Visible = true;
                        btnInsertEmployee.CommandName = "UpdateEmployee";
                        btnInsertEmployee.Text = "Update";
                        hfEmployeeId.Value = objEmployeeDetailsListDTO.EmployeeId.ToString();
                        break;
                    case "Email already exist.":
                        lblEmailValidation.Visible = true;
                        break;
                    default:
                        lblStatus.Text = strStatus;
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        break;
                }

                GetEmployeeDetails();
                objEmployeeDetailsBAL = null;
                objEmployeeDetailsListDTO = null;

            }
        }
        #endregion

        #region BindReportingEmployee
        /*
         * Description      :       This method is used to bind the reporting employees to the 
         *                          ddlReportingEmployee control
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	12:53 Pm		 		Creation 
         */

        private void BindReportingEmployee(params object[] objParams)
        {
            List<EmployeeDetailsListDTO> reportingEmployeeDetailsList = null;
            ListItem designationSelectedItem = ddlDesignation.SelectedItem;
            int designationSelectedValue = Convert.ToInt16(designationSelectedItem.Value);
            if (designationSelectedValue != 0 && designationSelectedValue != 1)
            {
                EmployeeDetailsBAL employeeDetailBAL = new EmployeeDetailsBAL();
                lblReportingEmployeeValidation.Visible = false;
                if (objParams.Length > 0)
                {
                    reportingEmployeeDetailsList = employeeDetailBAL.BindReportingEmployeeBasedOnDesignationId(designationSelectedValue, Convert.ToInt64(objParams[0]));
                }
                else if (!string.IsNullOrEmpty(hfEmployeeId.Value) && Convert.ToInt64(hfEmployeeId.Value) != 0)
                {
                    reportingEmployeeDetailsList = employeeDetailBAL.BindReportingEmployeeBasedOnDesignationId(designationSelectedValue, Convert.ToInt64(hfEmployeeId.Value));
                }
                else
                {
                    reportingEmployeeDetailsList = employeeDetailBAL.BindReportingEmployeeBasedOnDesignationId(designationSelectedValue, 0);

                } if (reportingEmployeeDetailsList.Count > 0)
                {
                    ddlReportingEmployee.DataSource = reportingEmployeeDetailsList;
                    ddlReportingEmployee.DataValueField = "ReportingEmployeeId";
                    ddlReportingEmployee.DataTextField = "ReportingEmployee";
                    ddlReportingEmployee.DataBind();
                    ddlReportingEmployee.Items.Insert(0, new ListItem("-- Select --", "0"));
                    ddlReportingEmployee.Enabled = true;
                }
                else
                {
                    ddlReportingEmployee.ClearSelection();
                    ddlReportingEmployee.SelectedValue = "0";
                    ddlReportingEmployee.Enabled = false;
                    lblReportingEmployeeValidation.Visible = true;
                }
                employeeDetailBAL = null;
            }
            else
            {
                ddlReportingEmployee.Items.Clear();
                ddlReportingEmployee.Items.Add(new ListItem("-- Select --", "0"));
                ddlReportingEmployee.SelectedIndex = 0;
                ddlReportingEmployee.Enabled = false;
                lblReportingEmployeeValidation.Visible = true;
            }
        }
        #endregion

        #region RowDelete Event
        /*
            * Description      :       This method is fired when user clicks on delete butoon. This method handels delete action
            *                          
            *  Version  	  Author        			   Date            		  	         Remarks       
            -- ------------------------------------------------------------------------------------------------------------------
            *  1.0		    Shaik Nazeer	 	 December 11, 2015	03:15 Pm		 		Creation 
            */

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            EmployeeDetailsBAL objEmployeeDetailsBAL = null;
            EmployeeDetailsListDTO objEmployeeDetailsListDTO = null;
            ClearEmployeeTable();
            long employeeId = (long)gvEmployees.DataKeys[rowIndex].Value;
            objEmployeeDetailsListDTO = new EmployeeDetailsListDTO();
            objEmployeeDetailsListDTO.EmployeeId = employeeId;
            objEmployeeDetailsBAL = new EmployeeDetailsBAL();
            string strStatus = objEmployeeDetailsBAL.DeleteEmployee(objEmployeeDetailsListDTO);
            //lblStatus.Text = strStatus;
            switch (strStatus)
            {
                case "Deleted successfully.":
                    lblStatus.Text = strStatus;
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    break;
                case "Employee can't delete due to dependency.":
                    lblDeleteValidation.Visible = true;
                    lblDeleteValidation.ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    lblStatus.Text = strStatus;
                    break;
            }
            GetEmployeeDetails();
            objEmployeeDetailsBAL = null;
            objEmployeeDetailsListDTO = null;

        }
        #endregion

        #region cvFuProfileImage_ServerValidate
        /*
         * Description      :       This method is used to validate the uploaded file
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ---------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 11, 2015	04:41 Pm		 		Creation 
         */

        protected void cvFuProfileImage_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (fuProfileImage.HasFile)
            {
                string format = Path.GetExtension(fuProfileImage.PostedFile.FileName);
                if (format == ".jpg" || format == ".png" || format == ".jpeg" || format == ".bmp")
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    imgProfileImage.Visible = false;
                    imgProfileImage.Attributes["Display"] = "none";
                }
            }
            else if (btnInsertEmployee.CommandName == "UpdateEmployee")
            {
                args.IsValid = true;
            }
        }
        #endregion

        #region ClearEmployeeTable
        /*
         * Description      :       This method is used to clear Data in tblEmployee
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 14, 2015	6:48 Pm		 		    Creation 
         */
        
        private void ClearEmployeeTable()
        {
            txtEmployeeName.Text = string.Empty;
            BindDesignationAndQualificationDetailsToDDLControl();
            ddlReportingEmployee.SelectedIndex = 0;
            ddlReportingEmployee.Enabled = false;
            lblReportingEmployeeValidation.Visible = false;
            txtSalary.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lblEmailValidation.Visible = false;
            txtMobileNumber.Text = string.Empty;
            imgProfileImage.ImageUrl = string.Empty;
            imgProfileImage.Visible = false;
            imgProfileImage.Attributes["Display"] = "none";
            txtEmployeeNumber.Text = string.Empty;
            lblEmployeeNumberValidation.Visible = false;
            btnInsertEmployee.Text = "Insert";
            btnInsertEmployee.CommandName = "InsertEmployee";
            lblStatus.Text = string.Empty;
            lblDeleteValidation.Visible = false;
            hfEmployeeId.Value = string.Empty;
        }
        #endregion

        #region Email Domain Validation
        /*
         * Description : This method is used to validate Email domain.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- -----------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 15, 2015	12:02 Pm		 		Creation 
         */

        protected void cvTxtEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            List<string> lstDomains = null;

            string strEmail = txtEmail.Text;
            string strDomain = strEmail.Substring(strEmail.IndexOf('@'));
            lstDomains = new List<string>() { "@gmail.com", "@yahoo.com", "@revalsys.com" };
            if (lstDomains.Contains(strDomain))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
            lstDomains = null;
        }
        #endregion

        #region Grid RowDataBound event
        /*
         * Description      :       This method is called before binding each row to the grid.
         *                          
         *  Version  	  Author        			   Date            		  	         Remarks       
            -- ------------------------------------------------------------------------------------
         *  1.0		    Shaik Nazeer	 	 December 17, 2015	04:39 Pm		 		Creation 
         */

        protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbDelete = (LinkButton)e.Row.Cells[11].Controls[0];
                lbDelete.OnClientClick = "return confirm('Are you sure to delete "+e.Row.Cells[1].Text +" ?');";

            }
        }
        #endregion
    }
}