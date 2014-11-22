<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HumanMicroRNA._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" cellspacing="1" cellpadding="5">
        <tr>
            <td>
                <p style="text-align: justify;">
                    MicroRNAs (miRNAs) are studied as key genetic elements that regulate the gene expression
                    involved in different human diseases. Clinical sequence based variations like copy
                    number variations (CNVs) affect miRNA biogenesis, dosage and target recognition
                    that may represent potentially functional variants and relevant target bindings.
                </p>
                <p style="text-align: justify;">
                    To systematically analyze miRNA-related CNVs and their effects on related genes,
                    a user-friendly free online database was developed to provide further analysis of
                    co-localization of miRNA loci with human genome CNV regions. Further analysis pipelines
                    such as miRNA-target to estimate the levels or locations of variations for genetic
                    duplications, insertions or deletions were also offered. Such information could
                    support the simulation of miRNA-target interactions. It also provided additional
                    clinical data curation related to miRNAs, their binding sites, genome mutations,
                    and diseases.
                </p>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
