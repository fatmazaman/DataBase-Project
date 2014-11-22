<%@ Page Title="HumanMicroRNA - Admin Portal" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="Portal.aspx.cs" Inherits="HumanMicroRNA.Admin.Portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellspacing="5px" cellpadding="5px" class="portalTable">
        <tr valign="bottom">
            <td>
                <table class="portalTable" cellspacing="5px" cellpadding="5px">
                    <tr valign="bottom">
                        <td align="center">
                            <asp:Label ID="lblPreferences" runat="server" Text="Preferences" class="portalTitle" /><br />
                            <br />
                            <asp:Image ID="imgPreferences" runat="server" ImageUrl="~/Admin/Assets/Images/preferences.png"
                                Width="70px" Height="70px" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbHeaderPreferences" runat="server" Text="View/Edit Header" /><br />
                            <asp:LinkButton ID="lbFooterPreferences" runat="server" Text="View/Edit Footer" /><br />
                            <asp:LinkButton ID="lbDefaultPagePref" runat="server" Text="View/Edit Default Page" /><br />
                            <asp:LinkButton ID="lbAboutUsPagePref" runat="server" Text="View/Edit About Us Page" /><br />
                            <asp:LinkButton ID="lbContactUsPagePref" runat="server" Text="View/Edit Contact Us Page" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr valign="bottom">
                        <td align="center">
                            <asp:Label ID="lbUsers" runat="server" Text="Users" class="portalTitle" /><br />
                            <br />
                            <asp:Image ID="imgUser" runat="server" ImageUrl="~/Admin/Assets/Images/user.png"
                                Width="70px" Height="70px" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lblAddUsers" runat="server" Text="Add Users" /><br />
                            <asp:LinkButton ID="lblEditUsers" runat="server" Text="Edit Users" /><br />
                            <asp:LinkButton ID="lblViewUsers" runat="server" Text="View Users" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr valign="bottom">
                        <td align="center">
                            <asp:Label ID="lblDataMaintenance" runat="server" Text="Data Maintenance" class="portalTitle" /><br />
                            <br />
                            <asp:Image ID="imgDataMaintenance" runat="server" ImageUrl="~/Admin/Assets/Images/data_transfer.png"
                                Width="70px" Height="70px" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lblViewDataSource" runat="server" Text="View Data Sources" /><br />
                            <asp:LinkButton ID="lblDownloadDataFiles" runat="server" Text="Download Data Files" /><br />
                            <asp:LinkButton ID="lblRunDataReport" runat="server" Text="Run Data Report" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr valign="bottom">
                        <td align="center">
                            <asp:Label ID="lblDatabase" runat="server" Text="Database" class="portalTitle" /><br />
                            <br />
                            <asp:Image ID="imgDatabase" runat="server" ImageUrl="~/Admin/Assets/Images/database_active.png"
                                Width="70px" Height="70px" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lblBackUpTables" runat="server" Text="Back UP Tables" /><br />
                            <asp:LinkButton ID="lblBackUpStoredProc" runat="server" Text="Back UP Stored Procedures" /><br />
                            <asp:LinkButton ID="lblBackUpEntireDb" runat="server" Text="Back UP Entire Database" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
