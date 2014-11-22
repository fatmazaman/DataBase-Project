using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanMicroRNA.BusinessLayer.Base
{
    public class BusConstants
    {
        /// <summary>
        /// The abbreviation for the Homo Sapiens term
        /// </summary>
        public const string HomoSapiensAbr = "hsa";

        public static string GetVarLink(string varID)
        {
            return "http://www.ncbi.nlm.nih.gov/sites/entrez?db=dbvar&cmd=DetailsSearch&term=" + varID + "&save_search=true";
        }
        public static string GetGeneLink(string geneID)
        {
            return "http://www.ncbi.nlm.nih.gov/gene/?term=" + geneID;
        }


        #region FTP Links
        public const string miRNAURLLink = @"http://www.mirbase.org/cgi-bin/mirna_entry.pl?acc=";
        public const string VarURLLink = @"http://www.ncbi.nlm.nih.gov/dbvar/variants/";
        public const string MIRNAFTP = @"ftp://mirbase.org/pub/mirbase/CURRENT/database_files/";
        public const string MIRNAFTPCurrent = @"ftp://mirbase.org/pub/mirbase/CURRENT/";
        public const string SNPFTP = @"ftp://ftp.ncbi.nih.gov/snp/organisms/human_9606/database/organism_data/";
        public const string VarFTP = @"ftp://ftp.ncbi.nlm.nih.gov/pub/dbVar/data/Homo_sapiens/by_assembly/NCBI36/gvf/"; 
        #endregion

        #region Pages Link
        public const string snpTempDirPath = @"~\Admin\TempDocuments\SNP\";
        public const string miRNATempDirPath = @"~\Admin\TempDocuments\miRNA\";
        public static string varTempDirPath = @"~\Admin\TempDocuments\VAR\";
        public const string geneTempDirPath = @"~\Admin\TempDocuments\Gene\"; 
        public const string TempDocumentsDirPath = @"~\Admin\TempDocuments\";
        #endregion

        #region Excel Sheet Names
        public const string miRNAFileExt = ".txt"; 
        #endregion

        #region DataBase Table Names
        public const string miRNASheetName = "miRNA";

        public const string miRNAExcelTbl = "mirna_excel";

        public const string miRNAVarTable = "hmrna_var_assn";

        public const string dbVarTbl = "db_var";

        public const string snpTableName = "SNPContigLoc";

        public const string mirnaTable = "mirna";

        public const string miRFamily = "mir_family_info";

        public const string predictedTarget = "predicted_target";
        #endregion

        #region Database Schema names
        public const string SchemaRNA = "rna";

        public const string SchemaSNP = "snp";

        public const string SchemaVar = "dbvar";

        public const string Schemdbo = "dbo";
        #endregion

        /// <summary>
        /// The following class holds the pages URL used
        /// throughout the application.
        /// </summary>
        public class PageNavigation
        {
            public const string AdminLoginPage = "~/Admin/Login.aspx";
            public const string AdminMyPortalPage = "~/Admin/Portal.aspx";
            public const string AdminDataMaintenancePage = "~/Admin/DataMaintenance.aspx";
        }
        /// <summary>
        /// The following class holds the FTP Source
        /// names for identification between ftp links.
        /// </summary>
        public class FTPSource
        {
            public const string SNP = "SNP";
            public const string VAR = "VAR";
            public const string MiRNA = "MIRNA";
            public const string Gene = "GENE";
        }
        /// <summary>
        /// The following class is a representation of an enumeration list which represents
        /// the name of the files located on the dbVar ftp server.
        /// </summary>
        public class VarFile
        {
            public const string dbVarFile = "NCBI36.remap.all.germline.ucsc.gvf";
        }
        /// <summary>
        /// The following class is a representation of an enumeration list which represents
        /// the name of the files located on the dbSNP ftp server.
        /// </summary>
        public class SNPFile
        {
            public const string SNPContigLoc = "b135_SNPContigLoc_37_3.bcp";

        }
        /// <summary>
        /// The following class is a representation of an enumeration list which represents
        /// the name of the files located on the miRNA ftp server.
        /// </summary>
        public class miRNAFiles
        {
            public const string miRNAXlsx = "miRNA.xlsx.gz";
            public const string miRNAXls = "miRNA.xls.gz";
            public const string dead_mirna = "dead_mirna.txt";
            public const string experiment_pre_read = "experiment_pre_read.txt";
            public const string experiment = "experiment.txt";
            public const string literature_references = "literature_references.txt";
            public const string mature_pre_read = "mature_pre_read.txt";
            public const string mature_read_count_by_experiment = "mature_read_count_by_experiment.txt";
            public const string mature_read_count = "mature_read_count.txt";
            public const string mirna_2_prefam = "mirna_2_prefam.txt";
            public const string mirna_chromosome_build = "mirna_chromosome_build.txt";
            public const string mirna_context = "mirna_context.txt";
            public const string mirna_database_links = "mirna_database_links.txt";
            public const string mirna_literature_references = "mirna_literature_references.txt";
            public const string mirna_mature = "mirna_mature.txt";
            public const string mirna_pre_mature = "mirna_pre_mature.txt";
            public const string mirna_pre_read = "mirna_pre_read.txt";
            public const string mirna_prefam = "mirna_prefam.txt";
            public const string mirna_read_count_by_experiment = "mirna_read_count_by_experiment.txt";
            public const string mirna_read_count = "mirna_read_count.txt";
            public const string mirna_read_experiment_count = "mirna_read_experiment_count.txt";
            public const string mirna_read = "mirna_read.txt";
            public const string mirna_species = "mirna_species.txt";
            public const string mirna_target_links = "mirna_target_links.txt";
            public const string mirna_target_url = "mirna_target_url.txt";
            public const string mirna = "mirna.txt";
        }
        /// <summary>
        /// The following class is a representation of an enumeration list which represents
        /// the name of the files located on the miRNA ftp server.
        /// </summary>
        public class miRTarget
        {
            public const string miRFamilyInfo = "miR_Family_Info.txt";
            public const string predictedTargetsInfo = "Predicted_Targets_Info.txt";
        }
        public const string HMiRNAsqvURL = @"http://131.247.3.121/HumanMicroRNA/admin/Login.aspx?id=";
        /// <summary>
        /// The following method is designed in order to generate a body message for the
        /// reset email going out to the user.
        /// </summary>
        /// <param name="firstLastName">string firstLastName</param>
        /// <param name="passwordRequestLink">string passwordRequestLink</param>
        /// <returns>Returns the body message of an email as a string.</returns>
        public static string GenerateBodyMessage(string firstLastName, string passwordRequestLink)
        {
            return @"
                    <table cellspacing='0' cellpadding='5' width='70%' align='left'>
                        <tr>
                            <td align='left'>
                                Hello " + firstLastName + @",<br /><br />
                                Our system indicated that you have requested a password reset for your HMiRNAsqv account. 
                                To get back into your HMiRNAsqv account, you will need to create a new password.<br /><br />
                                To reset your password, please follow the instructions below:<br />
                                <ul>
                                    <li>
                                        Click the link provided below in this email in order to access the reset link.
                                    </li>
                                    <li>
                                        Once you open the link and you are on the page, follow the instructions on the reset page.
                                    </li>
                                </ul><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Click on the link to reset your password: " + passwordRequestLink + @"
                                <br /><br />
                                If you feel you have received this email in error, or you did not ask us for help with your password, please disregard this email.
                                <br /><br /><br />
                            </td>
                        </tr>
                    <table>";
        }
    }
}
