<%@ Page Title="Admin Portal  - View Users" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="HumanMicroRNA.Admin.ViewUsers" %>

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
                                View Users</h1>
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
