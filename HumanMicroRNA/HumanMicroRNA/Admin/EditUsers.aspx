<%@ Page Title="Admin Portal  - Edit Users" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="EditUsers.aspx.cs" Inherits="HumanMicroRNA.Admin.EditUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Assets/JScript/ModalPopUp.js" type="text/javascript"></script>
    <script src="Assets/JQuery/jquery.min.js" type="text/javascript"></script>
    <script src="Assets/JScript/EditUsers.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dvPopup" style="display: none; width: 600px; height: 230px; border: 4px solid #2B547E;
        background-color: #FFFFFF;">
        <table width="100%" cellpadding="2px" cellspacing="0px" border="0">
            <tr valign="middle">
                <td width="50%" class="table_forgot_pssd_td" align="left">
                    <asp:Label ID="lblResetPassword" runat="server" Text="Edit User" />
                </td>
                <td width="50%" class="table_forgot_pssd_td" align="right">
                    <a href="#" onclick="HideModalPopup('dvPopup'); return false;">[Close Window]</a>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <h2>
                        Editing User</h2>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td>
                                First Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbFirstName" runat="server" />
                            </td>
                            <td>
                                Username
                            </td>
                            <td>
                                <asp:TextBox ID="tbUsername" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last Name
                            </td>
                            <td>
                                <asp:TextBox ID="tbLastName" runat="server" />
                            </td>
                            <td>
                                Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbPassword" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email Address:
                            </td>
                            <td>
                                <asp:TextBox ID="tbEmailAddress" runat="server" />
                            </td>
                            <td>
                                Re-enter Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbRePassword" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="Go Back" class="AddUserGoBack" PostBackUrl="~/Admin/EditUsers.aspx" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="3" cellspacing="0" width="100%">
        <tr valign="middle">
            <td colspan="2">
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr valign="middle">
                        <td align="center" width="5%">
                            <img src="Assets/Images/add_user.png" />
                        </td>
                        <td align="left" width="95%">
                            <h1>
                                Edit Users</h1>
                        </td>
                    </tr>
                </table>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lvUsers" runat="server" DataSourceID="GetAllUsers">
                    <ItemTemplate>
                        <tr>
                            <td class="lvFirstTD">
                                <input type="image" src="Assets/Images/user_edit.png" 
                                onclick='ShowModalPopup("dvPopup"); ShowElements(); return false;' />
                            </td>
                            <td class="lvFirstTD">
                                <%#Eval("user_first_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_last_name")%>
                            </td>
                            <td align="center">
                                <%# Eval("user_email_address_text")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_name")%>
                            </td>
                            <td align="center">
                                <img src='<%# GetImageSatus(Eval("user_act_flag").ToString()) %>' width="16" height="15" />
                            </td>
                            <td align="center">
                                <%#Eval("user_created_dtm")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_created_by_name")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color: #E0E0E0;">
                            <td class="lvFirstTD">
                                <asp:ImageButton ID="ibEdit" runat="server" ImageUrl="~/Admin/Assets/Images/user_edit.png"
                                    CommandArgument='<%#Eval("user_lku_id")%>' />
                            </td>
                            <td class="lvFirstTD">
                                <%#Eval("user_first_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_last_name")%>
                            </td>
                            <td align="center">
                                <%# Eval("user_email_address_text")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_name")%>
                            </td>
                            <td align="center">
                                <img src='<%# GetImageSatus(Eval("user_act_flag").ToString()) %>' width="16" height="15" />
                            </td>
                            <td align="center">
                                <%#Eval("user_created_dtm")%>
                            </td>
                            <td align="center">
                                <%#Eval("user_created_by_name")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center">
                                    <b>There exists no users in the database.</b>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table cellpadding="0" cellspacing="0" align="center" class="lvTable">
                            <tr style="background-color: #006699">
                                <th class="lvHeader">
                                    <asp:Label ID="lblEdit" runat="server" Text="Edit" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblUsername" runat="server" Text="Username" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblCreatedBy" runat="server" Text="Created On" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblCreatedOn" runat="server" Text="Created By" Font-Bold="true" />
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="GetAllUsers" runat="server" SelectMethod="GetAllUsers"
                    TypeName="HumanMicroRNA.BusinessLayer.Users.BusUsers"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <br />
                <asp:Button ID="btnGoBack" runat="server" Text="Go Back" class="AddUserGoBack" PostBackUrl="~/Admin/Portal.aspx" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
