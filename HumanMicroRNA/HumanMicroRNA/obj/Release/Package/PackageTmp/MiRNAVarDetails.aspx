<%@ Page Title="Human MicroRNA-Var Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MiRNAVarDetails.aspx.cs" Inherits="HumanMicroRNA.MiRNAVarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="90%" cellpadding="1" cellspacing="0" align="center">
        <tr>
            <td colspan="2">
                <fieldset runat="server">
                    <legend runat="server">Stem-loop Sequence<asp:Label ID="lblMiRNAIDsl" runat="server" /></legend>
                    <table cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblAccession" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:HyperLink ID="hlAccession" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblMiRNAID" runat="server" Text="miRNA ID" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:Label ID="lblMiRNAIDValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblMiRNADesc" runat="server" Text="Description" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:Label ID="lblMiRNADescValue" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td width="10%" align="left">
                                <asp:Label ID="lblSequence" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:TextBox ID="tbSequenceValue" runat="server" ReadOnly="true" BorderStyle="Solid"
                                    BorderColor="Gray" BorderWidth="1px" Style="overflow: hidden;" ForeColor="#696969"
                                    TextMode="MultiLine" Wrap="true" Width="98%" Height="50px" Font-Names="Helvetica Neue , Lucida Grande, Segoe UI, Arial, Helvetica, Verdana, sans-serif" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td width="10%" align="left">
                                <asp:Label ID="lblComment" runat="server" Text="Comment" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:TextBox ID="tbCommentValue" runat="server" ReadOnly="true" BorderStyle="Solid"
                                    BorderColor="Gray" BorderWidth="1px" Style="overflow: hidden;" ForeColor="#696969"
                                    TextMode="MultiLine" Wrap="true" Width="98%" Height="50px" Font-Names="Helvetica Neue , Lucida Grande, Segoe UI, Arial, Helvetica, Verdana, sans-serif" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td width="50%">
                <fieldset id="Fieldset1" runat="server">
                    <legend id="Legend1" runat="server">Mature Sequence<asp:Label ID="lblMiRNA5p" runat="server" /></legend>
                    <table>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblAccession5p" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblAccession5pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblPreviousID5p" runat="server" Text="Previous ID" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblPreviousID5pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence5pStart" runat="server" Text="Sequence Start" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence5pStartValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence5pEnd" runat="server" Text="Sequence End" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence5pEndValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence5p" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence5pValue" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td width="50%">
                <fieldset id="Fieldset2" runat="server">
                    <legend id="Legend2" runat="server">Mature Sequence<asp:Label ID="lblMiRNA3p" runat="server" /></legend>
                    <table>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblAccession3p" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblAccession3pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblPreviousID3p" runat="server" Text="Previous ID" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblPreviousID3pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence3pStart" runat="server" Text="Sequence Start" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence3pStartValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence3pEnd" runat="server" Text="Sequence End" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence3pEndValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" align="left">
                                <asp:Label ID="lblSequence3p" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="65%" align="left">
                                <asp:Label ID="lblSequence3pValue" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:BulletedList ID="blVardIDs" runat="server" DisplayMode="HyperLink">
                </asp:BulletedList>
            </td>
        </tr>
    </table>
</asp:Content>
