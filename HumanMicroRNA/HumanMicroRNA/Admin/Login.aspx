<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HumanMicroRNA.Admin.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Assets/JScript/ModalPopUp.js" type="text/javascript"></script>
    <script src="Assets/JQuery/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dvPopup" style="display: none; width: 400px; height: 200px; border: 4px solid #2B547E;
        background-color: #FFFFFF;">
        <table width="100%" cellpadding="2px" cellspacing="0px" border="0">
            <tr valign="middle">
                <td width="50%" class="table_forgot_pssd_td" align="left">
                    <asp:Label ID="lblResetPassword" runat="server" Text="Password Reset" />
                </td>
                <td width="50%" class="table_forgot_pssd_td" align="right">
                    <a href="#" onclick="HideModalPopup('dvPopup'); return false;">[Close Window]</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    If you forgot your password, or your account is locked, you’ll need to reset your
                    password. It’s easy. Please complete the step below.
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr valign="bottom">
                            <td width="80%" align="center">
                                Email Address
                                <asp:TextBox ID="tbEmailAddress" runat="server" CssClass="text_box_email" />
                            </td>
                            <td width="20%" align="center">
                                <asp:Button ID="btnSubmitReset" runat="server" Text="Submit" CausesValidation="false"
                                    CssClass="submit_button" OnClick="btnSubmitReset_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table class="login_page_table" align="center">
        <tr>
            <td>
                <fieldset>
                    <legend>Logon Page</legend>
                    <table class="login_table" align="center">
                        <tr>
                            <td width="20%" align="left">
                                <asp:Label ID="lblUserName" runat="server" Text="Username" />
                            </td>
                            <td width="70%" align="right">
                                <asp:TextBox ID="tbUserName" runat="server" Width="100%" />
                            </td>
                            <td width="10%" align="right">
                                <asp:RequiredFieldValidator ControlToValidate="tbUserName" Display="Static" ErrorMessage="*"
                                    runat="server" ID="vUserName" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" align="left">
                                <asp:Label ID="lblPassword" runat="server" Text="Password" />
                            </td>
                            <td width="70%" align="right">
                                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="100%" />
                            </td>
                            <td width="10%" align="right">
                                <asp:RequiredFieldValidator ControlToValidate="tbPassword" Display="Static" ErrorMessage="*"
                                    runat="server" ID="vUserPass" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="90%" colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" width="50%">
                                            <asp:LinkButton ID="lbForgotPassword" runat="server" Text="Forgot Password?" OnClientClick="ShowModalPopup('dvPopup'); return false;" />
                                        </td>
                                        <td align="right" width="50%">
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" class="login_button" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%">
                            </td>
                        </tr>
                    </table>
                    <p>
                        <asp:Label ID="lblErrorMessage" ForeColor="red" Font-Size="10" runat="server" /></p>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
