<%@ Page Title="Admin Portal - Add User" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="HumanMicroRNA.Admin.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                Add New User</h1>
                        </td>
                    </tr>
                </table>
                <hr />
            </td>
        </tr>
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td width="20%">
                            First Name:
                        </td>
                        <td width="80%">
                            <asp:TextBox ID="tbFirstName" runat="server" Width="60%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Last Name:
                        </td>
                        <td>
                            <asp:TextBox ID="tbLastName" runat="server" Width="60%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email Address:
                        </td>
                        <td>
                            <asp:TextBox ID="tbEmailAddress" runat="server" Width="85%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td width="30%">
                            Username:
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="tbUsername" runat="server" Width="70%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Enter Password:
                        </td>
                        <td>
                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="70%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Re-enter Password:
                        </td>
                        <td>
                            <asp:TextBox ID="tbReEnterPassword" runat="server" TextMode="Password" Width="70%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdblStatus" runat="server" TextAlign="Left" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">
                                    <img src="Assets/Images/active.png" alt="Active" />
                                </asp:ListItem>
                                <asp:ListItem Value="0">
                                    <img src="Assets/Images/inactive.png" alt="Inactive" />
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <br />
                <asp:Button ID="btnCreateUser" runat="server" Text="Create User" class="AddUserCreateUser"
                    OnClick="btnCreateUser_Click" />
                <asp:Button ID="btnGoBack" runat="server" Text="Go Back" class="AddUserGoBack" PostBackUrl="~/Admin/Portal.aspx" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:BulletedList ID="blValidationMessage" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
