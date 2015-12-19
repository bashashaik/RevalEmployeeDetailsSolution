<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employeedetails.aspx.cs" Inherits="RevalEmployeeDetailsUI._Default" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/mystyle.css" rel="stylesheet" />
    <title>Revalsys</title>
    <script type="text/javascript">
        function SetScrollEvent() {
            window.scrollTo(0, 0);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="AddEmployee">
            <center>
                <h3 style="text-align: center; font-size: 30px; color: #5F0B0B">Employee Registration Form</h3>
                <div id="EmployeeUpdateTable" style="margin: 0 auto; width: 980px;">
                    <table id="tblEmployee" style="width: 655px; margin-left: 100px;">
                        <tr>
                            <td style="width: 155px">
                                <asp:Label ID="lblEmployeeName" Text="Employee Name: " runat="server" /><span class="requiredIdentifier">*</span>
                            </td>
                            <td style="width: 500px">
                                <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="64" CssClass="textBoxClass" />
                                <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ErrorMessage="Employee name is required." ControlToValidate="txtEmployeeName" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revTxtEmployeeName" runat="server" ControlToValidate="txtEmployeeName" ValidationExpression="^[a-zA-Z\s\.]+$" Display="Dynamic" ErrorMessage="Invalid name."></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesignationId" runat="server" Text="Designation: " />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" CssClass="ddlClass">
                                    <asp:ListItem>-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblReportingEmployeeId" runat="server" Text="Reporting Employee: " />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReportingEmployee" runat="server" Enabled="false" CssClass="ddlClass">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblReportingEmployeeValidation" runat="server" Visible="false" Text="Reporting employee does't exist." ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSalary" runat="server" Text="Salary: " /><span class="requiredIdentifier">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSalary" runat="server" MaxLength="12" CssClass="textBoxClass" />
                                <asp:RequiredFieldValidator ID="rfvTxtSalary" runat="server" ControlToValidate="txtSalary" ForeColor="Red" ErrorMessage="Salary is required." Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revTxtSalary" runat="server" ControlToValidate="txtSalary" ErrorMessage="Invalid salary." Display="Dynamic" ForeColor="Red" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text="Email-Id: " /><span class="requiredIdentifier">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="128" CssClass="textBoxClass" />
                                <asp:RequiredFieldValidator ID="rfvTxtEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email-Id is required." ForeColor="Red" />
                                <%--<asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ErrorMessage="Invalid e-mail." ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                <asp:CustomValidator ID="cvTxtEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Email." ValidateEmptyText="true" OnServerValidate="cvTxtEmail_ServerValidate"></asp:CustomValidator>
                                <asp:RegularExpressionValidator ID="revTxtEmail2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ErrorMessage="In-valid e-mail." ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(co|com|co.in|in))+)$"></asp:RegularExpressionValidator>
                                <asp:Label ID="lblEmailValidation" runat="server" Text="Email already exist." Visible="false" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number: " /><span class="requiredIdentifier">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10" CssClass="textBoxClass" />
                                <asp:RequiredFieldValidator ID="rfvTxtMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Mobile number is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revTxtMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Invalid mobile number." Display="Dynamic" ValidationExpression="^[789]\d{9}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQualificationId" runat="server" EnableViewState="true" Text="Qualification: " />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="ddlClass">
                                    <asp:ListItem>-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProfileImage" runat="server" Text="Profile Image: " />
                            </td>
                            <td>
                                <asp:FileUpload ID="fuProfileImage" runat="server" Width="180px" />
                                <asp:RegularExpressionValidator ID="Revfupimage" ForeColor="Red" runat="server" ControlToValidate="fuProfileImage" ErrorMessage="In-valid image file." ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="cvFuProfileImage" runat="server" ControlToValidate="fuProfileImage" ErrorMessage="Invalid file format." Display="Dynamic" ForeColor="Red" OnServerValidate="cvFuProfileImage_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>
                                <asp:Image ID="imgProfileImage" runat="server" Visible="false" Width="100px" Height="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee Number:" /><span class="requiredIdentifier">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmployeeNumber" runat="server" MaxLength="32" CssClass="textBoxClass" />
                                <asp:RequiredFieldValidator ID="rfvTxtEmployeeNumber" runat="server" ControlToValidate="txtEmployeeNumber" Display="Dynamic" ErrorMessage="Employee number is required." ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revTxtEmployeeNumber" runat="server" ControlToValidate="txtEmployeeNumber" Display="Dynamic" ValidationExpression="^(?=.*[0-9])([a-zA-Z0-9\-]+)$" ErrorMessage="Invalid employee number." ForeColor="Red"></asp:RegularExpressionValidator>
                                <asp:Label ID="lblEmployeeNumberValidation" runat="server" Text="Employee number already exists." Visible="false" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnInsertEmployee" runat="server" CommandName="InsertEmployee" Text="Insert" OnCommand="btnInsertEmployee_Command" CssClass="ButtonStyle" />
                                <asp:Button ID="btnResetEmployee" runat="server" Text="Reset" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnResetEmployee_Click" CssClass="ButtonStyle" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true" ForeColor="Red" />
                    <asp:HiddenField ID="hfEmployeeId" runat="server" Visible="false" />
                </div>

                <br />
                <br />
                <br />

                <asp:Label ID="lblDeleteValidation" runat="server" Text="Employee can't delete due to dependency." Visible="False" Style="margin-left: 620px;" Font-Bold="True" />
                <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" EnableModelValidation="True" Height="100px" Width="961px" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvEmployees_PageIndexChanging" OnRowEditing="gvEmployees_RowEditing" DataKeyNames="EmployeeId" OnRowDeleting="gvEmployees_RowDeleting" EmptyDataText="Records are not available." OnRowDataBound="gvEmployees_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id" ShowHeader="False" Visible="False" />
                        <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName" />
                        <asp:BoundField HeaderText="Designation" DataField="Designation" />
                        <asp:BoundField HeaderText="Reporting Employee" DataField="ReportingEmployee" />
                        <asp:BoundField HeaderText="Salary" DataField="Salary" DataFormatString="{0:f2}" />
                        <asp:BoundField HeaderText="Email-Id" DataField="Email" />
                        <asp:BoundField HeaderText="Mobile Number" DataField="MobileNumber" />
                        <asp:BoundField HeaderText="Qualification" DataField="Qualification" />
                        <asp:TemplateField HeaderText="Profile Image">
                            <ItemTemplate>
                                <asp:Image ID="Label1" runat="server" ImageUrl='<%# Bind("ProfileImage") %>' Width="100px" Height="100px"></asp:Image>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Employee Number" DataField="EmployeeNumber" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataRowStyle ForeColor="#A55129" />
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </center>
            <br />
        </div>
    </form>
    <script type="text/javascript">
        var textBoxes = document.getElementsByClassName("textBoxClass");
        for (var i = 0; i < textBoxes.length; i++) {
            textBoxes[i].addEventListener("focus", function () { this.style.border = '1px solid #81DAF5' });
            textBoxes[i].addEventListener("blur", function () { this.style.border = '1px solid #E7A586' });
        }
        var dropBox = document.getElementsByClassName("ddlClass");
        for (var i = 0; i < dropBox.length; i++) {
            dropBox[i].addEventListener("focus", function () { this.style.border = '1px solid #81DAF5' });
            dropBox[i].addEventListener("blur", function () { this.style.border = '1px solid #E7A586' });
        }

        
    </script>
</body>
</html>
