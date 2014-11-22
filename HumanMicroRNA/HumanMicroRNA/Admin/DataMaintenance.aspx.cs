using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using HumanMicroRNA.BusinessLayer.Base;
using System.Net;
using System.IO.Compression;
using HumanMicroRNA.BusinessLayer.MicroRNA;
using System.Web.Hosting;
using System.Threading;
using iSharpToolkit.Data.Helper;
using iSharpToolkit.Extensions;
using HumanMicroRNA.BusinessLayer.SNP;
using HumanMicroRNA.BusinessLayer.Var;
using System.Web.Security;
using System.Web.Services;

namespace HumanMicroRNA.Admin
{
    public partial class DataMaintenance : System.Web.UI.Page
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie == null)
                Response.Redirect(BusConstants.PageNavigation.AdminLoginPage + "?url=" +
                    BusConstants.PageNavigation.AdminDataMaintenancePage.Replace("~/Admin/", string.Empty));
        }
        #endregion

        #region Events
        /// <summary>
        /// The following event is designed in order to download the data from the
        /// ftp sources.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnDownloadData_Click(object sender, EventArgs e)
        {
            DirectoriesValidation();

            //Download the files from the MiRNA and dbSNP FTP server.
            DownloadMiRNAFilesFromFTP();
            //DownloadSNPFileFromFTP();
            DownloadVarFilesFromFTP();

            //Decompress the files downloaded from the FTP servers.
            DecompressMiRNAFiles(BusConstants.FTPSource.MiRNA, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.SNP, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.VAR, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.Gene, "zip");
        }
        #endregion

        #region WebMethods
        [WebMethod]
        public static bool UpdateMiRNAData()
        {
            List<KeyValuePair<string, bool>> UpdateSuccessfulList = new List<KeyValuePair<string, bool>>();

            if (Directory.Exists(HostingEnvironment.MapPath(BusConstants.TempDocumentsDirPath)))
            {
                #region Parse & Import dbVar
                ParseAndImportdbVarData(UpdateSuccessfulList);
                #endregion

                #region Parse & Import MiRNA
                ParseAndImportMiRNAFromExcel(UpdateSuccessfulList);
                ParseAndImportAllMiRNA(UpdateSuccessfulList);
                #endregion

                #region Parse & Import Genes
                ParseAndImportGene(UpdateSuccessfulList);
                #endregion

                if (UpdateSuccessfulList.Where(a => a.Value == false).Count() > 0)
                    return false;
                else
                {
                    AssignVarToMiRNA();
                    return true;
                }
            }
            else
                return false;
        }
        /// <summary>
        /// The following webmethod is designed to be executed when the user
        /// clicks on the download data button. The method will then download
        /// the data files from various sources and decompress them.
        /// </summary>
        /// <returns>Returns a boolean value indicating wether the process was successful.</returns>
        [WebMethod]
        public static bool DownloadData()
        {
            DirectoriesValidation();

            //Download the files from the MiRNA and dbSNP FTP server.
            DownloadMiRNAFilesFromFTP();
            //DownloadSNPFileFromFTP();
            DownloadVarFilesFromFTP();

            //Decompress the files downloaded from the FTP servers.
            DecompressMiRNAFiles(BusConstants.FTPSource.MiRNA, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.SNP, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.VAR, "gz");
            DecompressMiRNAFiles(BusConstants.FTPSource.Gene, "zip");

            return true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The following method is designed in order to parse and import all of the
        /// MiRNA files in the directory into the database.
        /// </summary>
        /// <param name="UpdateSuccessfulList">List(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        private static void ParseAndImportAllMiRNA(List<KeyValuePair<string, bool>> UpdateSuccessfulList)
        {
            string[] txtFiles =
                        Directory.GetFiles(HostingEnvironment.MapPath(BusConstants.miRNATempDirPath), "*.txt");

            if (txtFiles.Count() > 0)
            {
                Array.Sort(txtFiles);

                foreach (string item in txtFiles)
                {
                    string[] Records = System.IO.File.ReadAllLines(item);

                    if (Records.Count() <= 0)
                        continue;

                    ParseAndImportMiRNAFiles(UpdateSuccessfulList, item, Records);

                }
            }
        }
        /// <summary>
        /// The following method is designed in order to parse and import the data
        /// in the MiRNA files passed to it it into the database.
        /// </summary>
        /// <param name="UpdateSuccessfulList">Lis(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        /// <param name="item">string item</param>
        /// <param name="Records">string[] Records</param>
        /// <param name="miRNADt">ref DataTable miRNADt</param>
        /// <param name="tableName">ref string tableName</param>
        /// <param name="successful">ref bool successful</param>
        /// <param name="MiRNAExcelDeleted">ref bool MiRNAExcelDeleted</param>
        private static void ParseAndImportMiRNAFiles(List<KeyValuePair<string, bool>> UpdateSuccessfulList, string item, string[] Records)
        {
            bool MiRNAExcelDeleted = false;
            bool successful = false;

            DataTable miRNADt = new DataTable();
            string tableName = string.Empty;

            switch (item.Replace(HostingEnvironment.MapPath(BusConstants.miRNATempDirPath), string.Empty))
            {
                case BusConstants.miRNAFiles.mirna:
                    miRNADt = ParseMiRNA(Records, BusConstants.miRNAFiles.mirna);

                    tableName = miRNADt.TableName.ToString().Replace(".txt", string.Empty);
                    MiRNAExcelDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + tableName);
                    if (MiRNAExcelDeleted)
                    {
                        successful = BusMicroRNA.ImportMiRNA(miRNADt, tableName, true, false);
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, successful));
                    }
                    else
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, false));
                    break;
                case BusConstants.miRNAFiles.mirna_chromosome_build:
                    miRNADt = ParseMiRNA(Records, BusConstants.miRNAFiles.mirna_chromosome_build);

                    tableName = miRNADt.TableName.ToString().Replace(".txt", string.Empty);
                    MiRNAExcelDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + tableName);
                    if (MiRNAExcelDeleted)
                    {
                        successful = BusMicroRNA.ImportMiRNA(miRNADt, tableName, true, false);
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, successful));
                    }
                    else
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, false));
                    break;
                case BusConstants.miRNAFiles.mirna_mature:
                    miRNADt = ParseMiRNA(Records, BusConstants.miRNAFiles.mirna_mature);

                    tableName = miRNADt.TableName.ToString().Replace(".txt", string.Empty);
                    MiRNAExcelDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + tableName);
                    if (MiRNAExcelDeleted)
                    {
                        successful = BusMicroRNA.ImportMiRNA(miRNADt, tableName, true, false);
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, successful));
                    }
                    else
                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(tableName, false));
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// The following method is designed in order to parse and import the gene
        /// information into the database.
        /// </summary>
        /// <param name="UpdateSuccessfulList">List(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        private static void ParseAndImportGene(List<KeyValuePair<string, bool>> UpdateSuccessfulList)
        {
            string TempGeneDirPath = HostingEnvironment.MapPath(BusConstants.geneTempDirPath);
            if (Directory.Exists(TempGeneDirPath))
            {
                string[] geneTextFiles = Directory.GetFiles(HostingEnvironment.MapPath(BusConstants.geneTempDirPath), "*.txt");
                if (geneTextFiles.Count() > 0)
                {
                    foreach (string item in geneTextFiles)
                    {
                        string[] Records = System.IO.File.ReadAllLines(item);
                        DataTable geneDt = new DataTable();
                        string tableName = string.Empty;
                        bool successful = false;

                        switch (item.Replace(HostingEnvironment.MapPath(BusConstants.geneTempDirPath), string.Empty))
                        {
                            case BusConstants.miRTarget.miRFamilyInfo:
                                geneDt = ParseGene(Records, BusConstants.miRTarget.miRFamilyInfo);
                                if (geneDt.Rows.Count > 0)
                                {
                                    bool geneDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + BusConstants.miRFamily);

                                    if (geneDeleted)
                                    {
                                        successful = BusMicroRNA.ImportGeneFamilyInfo(geneDt, false);
                                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.miRFamily, successful));
                                    }
                                    else
                                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.miRFamily, false));
                                }
                                break;
                            case BusConstants.miRTarget.predictedTargetsInfo:
                                geneDt = ParseGene(Records, BusConstants.miRTarget.predictedTargetsInfo);
                                if (geneDt.Rows.Count > 0)
                                {
                                    bool geneDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + BusConstants.predictedTarget);
                                    if (geneDeleted)
                                    {
                                        successful = BusMicroRNA.ImportGeneFamilyInfo(geneDt, false);
                                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.predictedTarget, successful));
                                    }
                                    else
                                        UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.predictedTarget, false));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to parse and import the MiRNA data
        /// from an excel spreadsheet into the database.
        /// </summary>
        /// <param name="UpdateSuccessfulList">List(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        private static void ParseAndImportMiRNAFromExcel(List<KeyValuePair<string, bool>> UpdateSuccessfulList)
        {
            DataTable getMiRNADataFromExcel = GetmiRNADataFromExcel();

            if (getMiRNADataFromExcel.Rows.Count > 1)
            {
                bool MiRNAExcelDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + BusConstants.miRNAExcelTbl);
                if (MiRNAExcelDeleted)
                {
                    bool successfulInsert =
                        BusMicroRNA.ImportMiRNA(getMiRNADataFromExcel, BusConstants.miRNAExcelTbl, false, true);
                    UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.miRNAExcelTbl, successfulInsert));
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to parse and import the dbVar records
        /// into the database.
        /// </summary>
        /// <param name="UpdateSuccessfulList">List(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        //private static void ParseAndImportDbVar(List<KeyValuePair<string, bool>> UpdateSuccessfulList)
        //{
        //    DataTable dbVarParsedData = ParseAndImportdbVarData();

        //    if (dbVarParsedData.Rows.Count > 1)
        //    {
        //        bool deleteAllVarFromTable = BusVar.DeleteAllVarFromTable();
        //        if (deleteAllVarFromTable)
        //        {
        //            bool successfulInsert =
        //                 BusVar.ImportVar(dbVarParsedData, BusConstants.dbVarTbl, false, true);
        //            UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.dbVarTbl, successfulInsert));
        //        }
        //    }
        //}

        /// <summary>
        /// The following method is designed in order to parse the gene records into
        /// a datatable.
        /// </summary>
        /// <param name="geneRecords">string[] geneRecords</param>
        /// <param name="tableName">string tableName</param>
        /// <returns>Returns a datatable with the parsed gene records.</returns>
        private static DataTable ParseGene(string[] geneRecords, string tableName)
        {
            DataTable dtGene = new DataTable();
            dtGene.TableName = tableName;

            switch (tableName)
            {
                case BusConstants.miRTarget.miRFamilyInfo:
                    if (geneRecords.Count() > 0 && geneRecords != null)
                    {
                        dtGene.Columns.Add("miRFamily", typeof(string));
                        dtGene.Columns.Add("Seedm8", typeof(string));
                        dtGene.Columns.Add("SpeciesID", typeof(Int32));
                        dtGene.Columns.Add("miRBaseID", typeof(string));
                        dtGene.Columns.Add("MatureSequence", typeof(string));
                        dtGene.Columns.Add("FamilyConservation", typeof(int));
                        dtGene.Columns.Add("miRBaseAccession", typeof(string));

                        foreach (string item in geneRecords)
                        {
                            string[] itemSplit = item.Split('\t');

                            if (itemSplit.Count() == dtGene.Columns.Count)
                                if (itemSplit[3].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                                    dtGene.Rows.Add(itemSplit);
                        }
                    }
                    return dtGene;
                case BusConstants.miRTarget.predictedTargetsInfo:
                    DataTable geneInformation = BusMicroRNA.GetAllmiRFamilyInfo();

                    if (geneInformation.Rows.Count > 0)
                    {

                        bool geneDeleted = BusMicroRNA.DeleteMiRNARecords(BusConstants.SchemaRNA + "." + BusConstants.predictedTarget);
                        if (geneDeleted)
                        {
                            if (geneRecords.Count() > 0 && geneRecords != null)
                            {
                                dtGene.Columns.Add("miRFamily", typeof(string));
                                dtGene.Columns.Add("geneID", typeof(string));
                                dtGene.Columns.Add("geneSymbol", typeof(string));
                                dtGene.Columns.Add("transcriptID", typeof(string));
                                dtGene.Columns.Add("speciesID", typeof(string));
                                dtGene.Columns.Add("utrStart", typeof(Int32));
                                dtGene.Columns.Add("utrEnd", typeof(Int32));
                                dtGene.Columns.Add("msaStart", typeof(Int32));
                                dtGene.Columns.Add("msaEnd", typeof(Int32));
                                dtGene.Columns.Add("seedMatch", typeof(string));
                                dtGene.Columns.Add("pct", typeof(string));

                                int ittIndex = 0;
                                foreach (string item in geneRecords.Skip(1))
                                {
                                    string[] itemSplit = item.Split('\t');

                                    if (itemSplit.Count() == dtGene.Columns.Count)
                                        dtGene.Rows.Add(itemSplit);

                                    if (ittIndex == 500000)
                                    {
                                        ittIndex = 0;

                                        bool importSuccessful = BusMicroRNA.ImportPredictedTargetInfo(dtGene, true);
                                        dtGene.Clear();
                                    }
                                    ittIndex++;
                                }
                                bool importSuccessful2 = BusMicroRNA.ImportPredictedTargetInfo(dtGene, true);
                            }
                        }
                        return dtGene;
                    }
                    else
                        return new DataTable();
                default:
                    return new DataTable();
            }
        }
        /// <summary>
        /// The following method is designed in order to find and assign
        /// the dbVar to each MiRNA record in the database.
        /// </summary>
        private static void AssignVarToMiRNA()
        {
            List<KeyValuePair<string, bool>> MiRNAVarImportStatus = new List<KeyValuePair<string, bool>>();
            DataTable allMiRNA = BusMicroRNA.GetAllMiRNARecords();

            if (allMiRNA.Rows.Count > 1)
            {
                IEnumerable<IGrouping<string, DataRow>> xsomeGroupped =
                    allMiRNA.AsEnumerable().GroupBy(a => a.Field<string>("xsome_num"));

                if (xsomeGroupped.Count() > 1)
                {
                    bool deletionSucceeded = BusMicroRNA.DeleteMiRNARecords(BusConstants.miRNAVarTable);

                    if (deletionSucceeded)
                    {
                        foreach (IGrouping<string, DataRow> item in xsomeGroupped)
                        {
                            DataTable allVarDtByXsome = BusVar.GetVarByChromosome(item.Key);

                            DataTable MiRNAVarAssnTemp = new DataTable();
                            MiRNAVarAssnTemp.Columns.Add("human_micro_rna_id");
                            MiRNAVarAssnTemp.Columns.Add("var_id");
                            MiRNAVarAssnTemp.Columns.Add("chromosome_num");
                            MiRNAVarAssnTemp.Columns.Add("chromosome_range_from_num");
                            MiRNAVarAssnTemp.Columns.Add("chromosome_range_to_num");

                            if (allVarDtByXsome.Rows.Count > 1)
                            {
                                EnumerableRowCollection<DataRow> MiRNAVar = null;
                                foreach (DataRow rec in item)
                                {
                                    MiRNAVar = allVarDtByXsome.AsEnumerable().
                                            Where(a => a.Field<string>("xsome_num") == rec["xsome_num"].ToString() &&
                                                   ((a.Field<Int64>("range_from_num") <= rec["range_from_num"].ToString().ToInt64()
                                                        && a.Field<Int64>("range_to_num") >= rec["range_to_num"].ToString().ToInt64()) ||

                                                    (a.Field<Int64>("range_from_num") >= rec["range_from_num"].ToString().ToInt64()
                                                        && a.Field<Int64>("range_to_num") <= rec["range_to_num"].ToString().ToInt64()) ||

                                                    ((a.Field<Int64>("range_from_num") > rec["range_from_num"].ToString().ToInt64())
                                                        && (a.Field<Int64>("range_from_num") < rec["range_to_num"].ToString().ToInt64())
                                                        && (a.Field<Int64>("range_to_num") >= rec["range_to_num"].ToString().ToInt64())) ||

                                                    ((a.Field<Int64>("range_from_num") <= rec["range_from_num"].ToString().ToInt64())
                                                        && (a.Field<Int64>("range_to_num") < rec["range_to_num"].ToString().ToInt64())
                                                        && (a.Field<Int64>("range_to_num") > rec["range_from_num"].ToString().ToInt64()))));


                                    foreach (DataRow dr in MiRNAVar)
                                        MiRNAVarAssnTemp.Rows.Add(rec["human_micro_rna_id"], dr["var_id"], dr["xsome_num"], dr["range_from_num"], dr["range_to_num"]);
                                }
                                bool successfulImport = BusMicroRNA.ImportMiRNAVar(MiRNAVarAssnTemp);
                                MiRNAVarImportStatus.Add(new KeyValuePair<string, bool>(item.Key, successfulImport));
                            }
                        }
                        IEnumerable<KeyValuePair<string, bool>> failedImport = MiRNAVarImportStatus.AsEnumerable().Where(a => !a.Value);

                        if (failedImport.Count() > 0)
                        {
                            //TODO: Inform the user that the import was not 100% complete!
                        }
                    }
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to parse the 
        /// Variant data from the file downloaded from the ftp server.
        /// </summary>
        /// <param name="UpdateSuccessfulList">List(KeyValuePair(string, bool)) UpdateSuccessfulList</param>
        private static void ParseAndImportdbVarData(List<KeyValuePair<string, bool>> UpdateSuccessfulList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("var_id");
            dt.Columns.Add("var_symbol");
            dt.Columns.Add("var_type");
            dt.Columns.Add("xsome");
            dt.Columns.Add("range_from");
            dt.Columns.Add("range_to");

            if (Directory.Exists(HostingEnvironment.MapPath(BusConstants.varTempDirPath)))
            {
                string[] dbVarFiles = Directory.GetFiles(HostingEnvironment.MapPath(BusConstants.varTempDirPath), BusConstants.VarFile.dbVarFile);

                if (dbVarFiles.Count() > 0)
                {
                    string[] Records = System.IO.File.ReadAllLines(dbVarFiles[0]);

                    if (Records.Count() > 0)
                    {
                        bool deleteAllVarFromTable = BusVar.DeleteAllVarFromTable();

                        List<string> TempRec = new List<string>(Records);
                        TempRec.RemoveRange(0, 10);

                        Records = TempRec.ToArray();

                        int count = 0;
                        foreach (string item in Records)
                        {
                            string[] splitRecord = item.Split('\t');
                            if (splitRecord.Count() > 0)
                            {
                                string var_id = string.Empty;
                                string xsome = splitRecord[0];
                                string var_symbol = splitRecord[1];
                                string var_type = splitRecord[2];
                                string range_from = splitRecord[3];
                                string range_to = splitRecord[4];

                                string[] varInfo = splitRecord[8].Split(';');
                                if (varInfo.Count() > 0)
                                    var_id = varInfo[0].Replace("ID=", string.Empty);
                                dt.Rows.Add(var_id, var_symbol, var_type, xsome, range_from, range_to);
                            }
                            if (count == Math.Floor((Records.Count() / 2).ToString().ToDecimal()))
                            {

                                if (deleteAllVarFromTable)
                                {
                                    bool successfulInsert =
                                         BusVar.ImportVar(dt, BusConstants.dbVarTbl, false, true);
                                    UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.dbVarTbl, successfulInsert));
                                }
                                count = 0;
                                dt.Clear();
                            }
                            else if (count == Math.Ceiling((Records.Count() / 2).ToString().ToDecimal()))
                            {
                                bool successfulInsert =
                                          BusVar.ImportVar(dt, BusConstants.dbVarTbl, false, true);
                                UpdateSuccessfulList.Add(new KeyValuePair<string, bool>(BusConstants.dbVarTbl, successfulInsert));
                                count = 0;
                                dt.Clear();
                            }
                            count++;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// The following method fetch the data from the miRNA.xls(x) file and return
        /// the data in a datatable.
        /// </summary>
        /// <returns>Return data as a datatable.</returns>
        private static DataTable GetmiRNADataFromExcel()
        {
            DataTable readDataFromExcel = null;
            List<string> fields = new List<string>() { "Accession", "ID", "Status", "Sequence", "Mature1_Acc", "Mature1_ID",
                                                                "Mature1_Seq", "Mature2_Acc", "Mature2_ID", "Mature2_Seq"};

            if (Directory.Exists(HostingEnvironment.MapPath(BusConstants.snpTempDirPath)))
            {
                var excelFiles = Directory.GetFiles(HostingEnvironment.MapPath(BusConstants.miRNATempDirPath), "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx"));

                if (excelFiles.Count() > 0)
                {
                    string whereClause = "WHERE ID LIKE 'hsa%'";
                    readDataFromExcel = ExcelReader.ReadDataFromExcelWithWhereClause(excelFiles.ToArray()[0], BusConstants.miRNASheetName, fields, whereClause);
                }
            }
            return readDataFromExcel;
        }
        /// <summary>
        /// The following method is designed in order to parse the
        /// miRNA records into a DataTable.
        /// </summary>
        /// <param name="miRNARecords">string[] miRNARecords</param>
        /// <returns>DataTable of MiRNA</returns>
        private static DataTable ParseMiRNA(string[] miRNARecords, string tableName)
        {
            DataTable dt = new DataTable();
            DataTable dtMiRNARec;
            dt.TableName = tableName;

            switch (tableName)
            {
                case BusConstants.miRNAFiles.mirna:
                    dt.Columns.Add("auto_mirna", typeof(int));
                    dt.Columns.Add("mirna_acc", typeof(string));
                    dt.Columns.Add("mirna_id", typeof(string));
                    dt.Columns.Add("mirna_l_id", typeof(string));
                    dt.Columns.Add("description", typeof(string));
                    dt.Columns.Add("sequence", typeof(string));
                    dt.Columns.Add("comment", typeof(string));
                    dt.Columns.Add("auto_species", typeof(int));

                    if (miRNARecords != null && miRNARecords.Count() > 0)
                        foreach (string item in miRNARecords)
                        {
                            string[] itemSplit = item.Split('\t');

                            if (itemSplit.Count() == dt.Columns.Count)
                                if (itemSplit[2].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                                    dt.Rows.Add(itemSplit);
                        }
                    return dt;

                case BusConstants.miRNAFiles.mirna_chromosome_build:
                    dt.Columns.Add("auto_mirna", typeof(int));
                    dt.Columns.Add("xsome", typeof(string));
                    dt.Columns.Add("contig_start", typeof(Int64));
                    dt.Columns.Add("contig_end", typeof(Int64));
                    dt.Columns.Add("strand", typeof(string));

                    //Get the MiRNA Records from the database as a list
                    //and compare each xsome record (auto_mirna) to the list of MiRNA Records
                    //if auto_mirna matches add the xsome records to the table to be added to the db later.
                    dtMiRNARec = BusMicroRNA.GetAllMiRNAByAutoMiRNAorMiRNAAcc(true, false);
                    if (dtMiRNARec != null && dtMiRNARec.Rows.Count > 1)
                    {
                        if (miRNARecords != null && miRNARecords.Count() > 0)
                            foreach (string item in miRNARecords)
                            {
                                string[] itemSplit = item.Split('\t');
                                if (itemSplit.Count() == dt.Columns.Count)
                                {
                                    DataRow newVariable =
                                         dtMiRNARec.AsEnumerable().Where(a => a.Field<int>("auto_mirna").ToString() == itemSplit[0]).FirstOrDefault();
                                    if (newVariable != null)
                                        dt.Rows.Add(itemSplit);
                                }
                            }
                    }
                    return dt;
                case BusConstants.miRNAFiles.mirna_mature:
                    dt.Columns.Add("auto_mirna", typeof(int));
                    dt.Columns.Add("mature_name", typeof(string));
                    dt.Columns.Add("mature_acc", typeof(string));
                    dt.Columns.Add("matrue_from", typeof(string));
                    dt.Columns.Add("matrue_to", typeof(string));
                    dt.Columns.Add("evidence", typeof(string));
                    dt.Columns.Add("experiment", typeof(string));
                    dt.Columns.Add("similarity", typeof(string));

                    foreach (string item in miRNARecords)
                    {
                        string[] itemSplit = item.Split('\t');

                        if (itemSplit.Count() == dt.Columns.Count)
                            if (itemSplit[1].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                                dt.Rows.Add(itemSplit);
                    }
                    return dt;
                #region Files Excluded
                //case BusConstants.miRNAFiles.dead_mirna:
                //    dt.Columns.Add("mirna_acc", typeof(string));
                //    dt.Columns.Add("mirna_id", typeof(string));
                //    dt.Columns.Add("previous_id", typeof(string));
                //    dt.Columns.Add("forward_to", typeof(string));
                //    dt.Columns.Add("comment", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.experiment:
                //    dt.Columns.Add("auto_experiment", typeof(int));
                //    dt.Columns.Add("experiment_acc", typeof(string));
                //    dt.Columns.Add("organism", typeof(string));
                //    dt.Columns.Add("tissu_ontology_id", typeof(string));
                //    dt.Columns.Add("technology", typeof(string));
                //    dt.Columns.Add("comment", typeof(string));
                //    dt.Columns.Add("auto_lit", typeof(int));
                //    dt.Columns.Add("link", typeof(string));
                //    dt.Columns.Add("mir_read_counts", typeof(Int32));
                //    dt.Columns.Add("all_read_counts", typeof(Int32));
                //    break;
                //case BusConstants.miRNAFiles.experiment_pre_read:
                //    dt.Columns.Add("auto_experiment", typeof(int));
                //    dt.Columns.Add("auto_read", typeof(int));
                //    dt.Columns.Add("count", typeof(float));
                //    break;
                //case BusConstants.miRNAFiles.literature_references:
                //    dt.Columns.Add("auto_lit", typeof(int));
                //    dt.Columns.Add("medline", typeof(int));
                //    dt.Columns.Add("title", typeof(string));
                //    dt.Columns.Add("author", typeof(string));
                //    dt.Columns.Add("journal", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mature_pre_read:
                //    dt.Columns.Add("auto_read", typeof(int));
                //    dt.Columns.Add("auto_mature", typeof(int));
                //    break;
                //case BusConstants.miRNAFiles.mature_read_count:
                //    dt.Columns.Add("auto_mature", typeof(int));
                //    dt.Columns.Add("mature_acc", typeof(string));
                //    dt.Columns.Add("read_count", typeof(float));
                //    dt.Columns.Add("experiment_count", typeof(Int64));
                //    break;
                //case BusConstants.miRNAFiles.mature_read_count_by_experiment:
                //    dt.Columns.Add("auto_mature", typeof(int));
                //    dt.Columns.Add("mature_acc", typeof(string));
                //    dt.Columns.Add("read_count", typeof(float));
                //    dt.Columns.Add("auto_experiment", typeof(int));
                //    break;
                //case BusConstants.miRNAFiles.mirna_2_prefam:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("auto_prefam", typeof(int));
                //    break;
                //case BusConstants.miRNAFiles.mirna_context:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("transcript_id", typeof(string));
                //    dt.Columns.Add("overlap_sense", typeof(string));
                //    dt.Columns.Add("overlap_type", typeof(string));
                //    dt.Columns.Add("number", typeof(int));
                //    dt.Columns.Add("transcript_source", typeof(string));
                //    dt.Columns.Add("transcript_name", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mirna_database_links:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("db_id", typeof(string));
                //    dt.Columns.Add("comment", typeof(string));
                //    dt.Columns.Add("db_link", typeof(string));
                //    dt.Columns.Add("db_secondary", typeof(string));
                //    dt.Columns.Add("other_params", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mirna_literature_references:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("auto_lit", typeof(int));
                //    dt.Columns.Add("comment", typeof(string));
                //    dt.Columns.Add("order_added", typeof(int));
                //    break;
                //case BusConstants.miRNAFiles.mirna_mature:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("mature_name", typeof(string));
                //    dt.Columns.Add("mature_acc", typeof(string));
                //    dt.Columns.Add("matrue_from", typeof(string));
                //    dt.Columns.Add("matrue_to", typeof(string));
                //    dt.Columns.Add("evidence", typeof(string));
                //    dt.Columns.Add("experiment", typeof(string));
                //    dt.Columns.Add("similarity", typeof(string));

                //    foreach (string item in miRNARecords)
                //    {
                //        string[] itemSplit = item.Split('\t');

                //        if (itemSplit.Count() == dt.Columns.Count)
                //            if (itemSplit[1].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                //                dt.Rows.Add(itemSplit);
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_pre_mature:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("auto_mature", typeof(int));
                //    break;
                //case BusConstants.miRNAFiles.mirna_pre_read:
                //    dt.Columns.Add("auto_read", typeof(int));
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("position_start", typeof(int));
                //    dt.Columns.Add("cost_5p", typeof(int));
                //    dt.Columns.Add("cost_3p", typeof(int));
                //    dt.Columns.Add("sense", typeof(int));

                //    dtMiRNARec = BusMicroRNA.GetAllMiRNAByAutoMiRNAorMiRNAAcc(true, false);
                //    if (dtMiRNARec != null && dtMiRNARec.Rows.Count > 1)
                //    {
                //        if (miRNARecords != null && miRNARecords.Count() > 0)
                //            foreach (string item in miRNARecords)
                //            {
                //                string[] itemSplit = item.Split('\t');
                //                if (itemSplit.Count() == dt.Columns.Count)
                //                {
                //                    DataRow newVariable =
                //                         dtMiRNARec.AsEnumerable().Where(a => a.Field<int>("auto_mirna").ToString() == itemSplit[1]).FirstOrDefault();
                //                    if (newVariable != null)
                //                        dt.Rows.Add(itemSplit);
                //                }
                //            }
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_prefam:
                //    dt.Columns.Add("auto_prefam", typeof(int));
                //    dt.Columns.Add("prefam_acc", typeof(string));
                //    dt.Columns.Add("prefam_id", typeof(string));
                //    dt.Columns.Add("description", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mirna_read:
                //    dt.Columns.Add("auto_read", typeof(int));
                //    dt.Columns.Add("read_acc", typeof(string));
                //    dt.Columns.Add("sequence", typeof(string));
                //    dt.Columns.Add("organism", typeof(string));

                //    foreach (string item in miRNARecords)
                //    {
                //        string[] itemSplit = item.Split('\t');

                //        if (itemSplit.Count() == dt.Columns.Count)
                //            if (itemSplit[3].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                //                dt.Rows.Add(itemSplit);
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_read_count:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("mirna_acc", typeof(string));
                //    dt.Columns.Add("read_count", typeof(float));
                //    dt.Columns.Add("experiment_count", typeof(Int64));

                //    //Get the MiRNA Records from the database as a list
                //    //and compare each xsome record (auto_mirna) to the list of MiRNA Records
                //    //if auto_mirna matches add the xsome records to the table to be added to the db later.
                //    dtMiRNARec = BusMicroRNA.GetAllMiRNAByAutoMiRNAorMiRNAAcc(true, false);
                //    if (dtMiRNARec != null && dtMiRNARec.Rows.Count > 1)
                //    {
                //        if (miRNARecords != null && miRNARecords.Count() > 0)
                //            foreach (string item in miRNARecords)
                //            {
                //                string[] itemSplit = item.Split('\t');
                //                if (itemSplit.Count() == dt.Columns.Count)
                //                {
                //                    DataRow newVariable =
                //                         dtMiRNARec.AsEnumerable().Where(a => a.Field<int>("auto_mirna").ToString() == itemSplit[0]).FirstOrDefault();
                //                    if (newVariable != null)
                //                        dt.Rows.Add(itemSplit);
                //                }
                //            }
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_read_count_by_experiment:
                //    dt.Columns.Add("mirna_acc", typeof(string));
                //    dt.Columns.Add("read_acc", typeof(string));
                //    dt.Columns.Add("sequence", typeof(string));
                //    dt.Columns.Add("experiment_acc", typeof(string));
                //    dt.Columns.Add("count", typeof(float));
                //    dt.Columns.Add("cost_5p", typeof(int));
                //    dt.Columns.Add("cost_3p", typeof(int));
                //    dt.Columns.Add("position_start", typeof(int));

                //    //Get the MiRNA Records from the database as a list
                //    // and compare each MiRNA Read Count By Experiment record (auto_mirna) to the list of MiRNA Records
                //    //if auto_mirna matches add the xsome records to the table to be added to the db later.
                //    dtMiRNARec = BusMicroRNA.GetAllMiRNAByAutoMiRNAorMiRNAAcc(false, true);
                //    if (dtMiRNARec != null && dtMiRNARec.Rows.Count > 1)
                //    {
                //        if (miRNARecords != null && miRNARecords.Count() > 0)

                //            foreach (string item in miRNARecords)
                //            {
                //                string[] itemSplit = item.Split('\t');
                //                if (itemSplit.Count() == dt.Columns.Count)
                //                {
                //                    DataRow newVariable = dtMiRNARec.AsEnumerable().
                //                      Where(a => a.Field<string>("mirna_acc").ToString() == itemSplit[0]).FirstOrDefault();
                //                    if (newVariable != null)
                //                        dt.Rows.Add(itemSplit);
                //                }
                //            }
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_read_experiment_count:
                //    dt.Columns.Add("auto_mirna", typeof(int));
                //    dt.Columns.Add("mirna_id", typeof(string));
                //    dt.Columns.Add("mirna_acc", typeof(string));
                //    dt.Columns.Add("auto_experiment", typeof(int));
                //    dt.Columns.Add("experiment_acc", typeof(string));
                //    dt.Columns.Add("read_count", typeof(float));

                //    foreach (string item in miRNARecords)
                //    {
                //        string[] itemSplit = item.Split('\t');

                //        if (itemSplit.Count() == dt.Columns.Count)
                //            if (itemSplit[1].ToString().StartsWith(BusConstants.HomoSapiensAbr))
                //                dt.Rows.Add(itemSplit);
                //    }
                //    return dt;
                //case BusConstants.miRNAFiles.mirna_species:
                //    dt.Columns.Add("auto_id", typeof(Int64));
                //    dt.Columns.Add("organism", typeof(string));
                //    dt.Columns.Add("division", typeof(string));
                //    dt.Columns.Add("name", typeof(string));
                //    dt.Columns.Add("taxonomy", typeof(string));
                //    dt.Columns.Add("genome_assembly", typeof(string));
                //    dt.Columns.Add("ensembl_db", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mirna_target_links:
                //    dt.Columns.Add("auto_mature", typeof(int));
                //    dt.Columns.Add("auto_db", typeof(int));
                //    dt.Columns.Add("display_name", typeof(string));
                //    dt.Columns.Add("field1", typeof(string));
                //    dt.Columns.Add("field2", typeof(string));
                //    break;
                //case BusConstants.miRNAFiles.mirna_target_url:
                //    dt.Columns.Add("auto_db", typeof(int));
                //    dt.Columns.Add("display_name", typeof(string));
                //    dt.Columns.Add("url", typeof(string));
                //    break; 
                #endregion
                default:
                    break;
            }
            return dt;
        }

        /// <summary>
        /// The following method is designed in order to check for the specific
        /// directories. If the directories do not exists, create them.
        /// </summary>
        private static void DirectoriesValidation()
        {
            string TemDocumentPath = HostingEnvironment.MapPath(BusConstants.TempDocumentsDirPath);
            string TempMiRNAPath = HostingEnvironment.MapPath(BusConstants.miRNATempDirPath);
            string TempSNPPath = HostingEnvironment.MapPath(BusConstants.snpTempDirPath);
            string TempVarPath = HostingEnvironment.MapPath(BusConstants.varTempDirPath);
            string TempGeneDirPath = HostingEnvironment.MapPath(BusConstants.geneTempDirPath);

            if (!string.IsNullOrEmpty(TemDocumentPath) &&
                    !string.IsNullOrEmpty(TempMiRNAPath) &&
                        !string.IsNullOrEmpty(TempSNPPath) &&
                            !string.IsNullOrEmpty(TempVarPath) &&
                                !string.IsNullOrEmpty(TempGeneDirPath))
            {
                if (!Directory.Exists(TemDocumentPath)) Directory.CreateDirectory(TemDocumentPath);
                if (!Directory.Exists(TempMiRNAPath)) Directory.CreateDirectory(TempMiRNAPath);
                if (!Directory.Exists(TempSNPPath)) Directory.CreateDirectory(TempSNPPath);
                if (!Directory.Exists(TempVarPath)) Directory.CreateDirectory(TempVarPath);
                if (!Directory.Exists(TempGeneDirPath)) Directory.CreateDirectory(TempGeneDirPath);
            }
        }
        /// <summary>
        /// The following method is designed in order to download the
        /// miRNA files from the ftp://mirbase.org/pub/mirbase/CURRENT/database_files/
        /// ftp server.
        /// </summary>
        private static void DownloadMiRNAFilesFromFTP()
        {
            string TempMiRNAPath = HostingEnvironment.MapPath(BusConstants.miRNATempDirPath);

            if (Directory.Exists(TempMiRNAPath))
            {
                //Deleting all the files from the directory before downloading the new files from the ftp.
                Array.ForEach(Directory.GetFiles(TempMiRNAPath), delegate(string path) { File.Delete(path); });

                #region Download .gz files
                Uri mirBaseFTP = new Uri(BusConstants.MIRNAFTP);

                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(mirBaseFTP + "*.gz");
                req.Credentials = new NetworkCredential(string.Empty, string.Empty);

                try
                {
                    List<string> dirList = new List<string>();
                    req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);

                    string[] ftpDirList;

                    if (reader != null)
                    {
                        ftpDirList = reader.ReadToEnd().Split('\r', '\n');

                        if (ftpDirList.Count() > 0)
                        {
                            ftpDirList = ftpDirList.Where(val => !string.IsNullOrEmpty(val)).ToArray();
                            if (ftpDirList.Count() > 0)
                            {
                                foreach (string item in ftpDirList)
                                {
                                    string[] tempDirList = item.Split(' ');
                                    if (tempDirList[tempDirList.Length - 1].Equals(BusConstants.miRNAFiles.mirna + ".gz") ||
                                            tempDirList[tempDirList.Length - 1].Equals(BusConstants.miRNAFiles.mirna_mature + ".gz") ||
                                                tempDirList[tempDirList.Length - 1].Equals(BusConstants.miRNAFiles.mirna_chromosome_build + ".gz"))
                                        dirList.Add(tempDirList[tempDirList.Length - 1]);
                                }
                            }

                            if (dirList != null && dirList.Count > 0)
                            {
                                foreach (string item in dirList)
                                {
                                    FileInfo fi = new FileInfo(HostingEnvironment.MapPath(BusConstants.miRNATempDirPath) + @"\" + item);

                                    FtpWebRequest reqTemp = (FtpWebRequest)FtpWebRequest.Create(mirBaseFTP + item);
                                    reqTemp.Credentials = new NetworkCredential(string.Empty, string.Empty);

                                    reqTemp.Method = WebRequestMethods.Ftp.DownloadFile;
                                    reqTemp.UseBinary = true;     //cautious but not necessary for this
                                    reqTemp.KeepAlive = true;     //no effect
                                    reqTemp.UsePassive = true;    //no effect

                                    FtpWebResponse respTemp = (FtpWebResponse)reqTemp.GetResponse();
                                    Stream fis = respTemp.GetResponseStream();
                                    FileStream fos = fi.Create();

                                    int bufferSize = 2048;
                                    byte[] buffer = new byte[bufferSize];
                                    int readCount = fis.Read(buffer, 0, bufferSize);
                                    while (readCount > 0)
                                    {
                                        fos.Write(buffer, 0, readCount);
                                        readCount = fis.Read(buffer, 0, bufferSize);
                                    }

                                    fos.Close();
                                    fis.Close();
                                    respTemp.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region Download .xls files
                Uri mirBaseFTPCurrent = new Uri(BusConstants.MIRNAFTPCurrent);

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(mirBaseFTPCurrent + "miRNA.*");
                request.Credentials = new NetworkCredential(string.Empty, string.Empty);

                try
                {
                    List<string> dirList = new List<string>();
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);

                    string[] ftpDirList;

                    if (reader != null)
                    {
                        ftpDirList = reader.ReadToEnd().Split('\r', '\n');

                        if (ftpDirList.Count() > 0)
                        {
                            ftpDirList = ftpDirList.Where(val => !string.IsNullOrEmpty(val)).ToArray();
                            if (ftpDirList.Count() > 0)
                            {
                                foreach (string item in ftpDirList)
                                {
                                    string[] tempDirList = item.Split(' ');
                                    if (tempDirList[tempDirList.Length - 1].Equals(BusConstants.miRNAFiles.miRNAXls) ||
                                            tempDirList[tempDirList.Length - 1].Equals(BusConstants.miRNAFiles.miRNAXlsx))
                                        dirList.Add(tempDirList[tempDirList.Length - 1]);
                                }
                            }

                            if (dirList != null && dirList.Count > 0)
                            {
                                foreach (string item in dirList)
                                {
                                    FileInfo fi = new FileInfo(HostingEnvironment.MapPath(BusConstants.miRNATempDirPath) + @"\" + item);

                                    FtpWebRequest reqTemp = (FtpWebRequest)FtpWebRequest.Create(mirBaseFTPCurrent + item);
                                    reqTemp.Credentials = new NetworkCredential(string.Empty, string.Empty);

                                    reqTemp.Method = WebRequestMethods.Ftp.DownloadFile;
                                    reqTemp.UseBinary = true;     //cautious but not necessary for this
                                    reqTemp.KeepAlive = true;     //no effect
                                    reqTemp.UsePassive = true;    //no effect

                                    FtpWebResponse respTemp = (FtpWebResponse)reqTemp.GetResponse();
                                    Stream fis = respTemp.GetResponseStream();
                                    FileStream fos = fi.Create();

                                    int bufferSize = 2048;
                                    byte[] buffer = new byte[bufferSize];
                                    int readCount = fis.Read(buffer, 0, bufferSize);
                                    while (readCount > 0)
                                    {
                                        fos.Write(buffer, 0, readCount);
                                        readCount = fis.Read(buffer, 0, bufferSize);
                                    }

                                    fos.Close();
                                    fis.Close();
                                    respTemp.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }
        }
        /// <summary>
        /// The following method is designed in order to download the file
        /// from the dbSNP FTP server.
        /// </summary>
        private void DownloadSNPFileFromFTP()
        {
            string TempSNPPath = Server.MapPath(BusConstants.snpTempDirPath);

            if (Directory.Exists(TempSNPPath))
            {
                //Deleting all the files from the directory before downloading the new files from the ftp.
                Array.ForEach(Directory.GetFiles(TempSNPPath), delegate(string path) { File.Delete(path); });

                #region Download .gz files
                Uri snpFTPURI = new Uri(BusConstants.SNPFTP);
                //Uri snpFTP = new Uri(BusConstants.MIRNAFTP);

                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(snpFTPURI + "*.bcp.gz");
                req.Credentials = new NetworkCredential(string.Empty, string.Empty);

                try
                {
                    List<string> dirList = new List<string>();
                    req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);

                    string[] ftpDirList;

                    if (reader != null)
                    {
                        ftpDirList = reader.ReadToEnd().Split('\r', '\n');

                        if (ftpDirList.Count() > 0)
                        {
                            ftpDirList = ftpDirList.Where(val => !string.IsNullOrEmpty(val)).ToArray();
                            if (ftpDirList.Count() > 0)
                            {
                                foreach (string item in ftpDirList)
                                {
                                    string[] tempDirList = item.Split(' ');
                                    if (tempDirList[tempDirList.Length - 1].Contains(BusConstants.SNPFile.SNPContigLoc + ".gz"))
                                        dirList.Add(tempDirList[tempDirList.Length - 1]);
                                }
                            }

                            if (dirList != null && dirList.Count > 0)
                            {
                                foreach (string item in dirList)
                                {
                                    FileInfo fi = new FileInfo(Server.MapPath(BusConstants.snpTempDirPath) + @"\" + item);

                                    FtpWebRequest reqTemp = (FtpWebRequest)FtpWebRequest.Create(snpFTPURI + item);
                                    reqTemp.Credentials = new NetworkCredential(string.Empty, string.Empty);

                                    reqTemp.Method = WebRequestMethods.Ftp.DownloadFile;
                                    reqTemp.UseBinary = true;     //cautious but not necessary for this
                                    reqTemp.KeepAlive = true;     //no effect
                                    reqTemp.UsePassive = true;    //no effect

                                    FtpWebResponse respTemp = (FtpWebResponse)reqTemp.GetResponse();
                                    Stream fis = respTemp.GetResponseStream();
                                    FileStream fos = fi.Create();

                                    int bufferSize = 2048;
                                    byte[] buffer = new byte[bufferSize];
                                    int readCount = fis.Read(buffer, 0, bufferSize);
                                    while (readCount > 0)
                                    {
                                        fos.Write(buffer, 0, readCount);
                                        readCount = fis.Read(buffer, 0, bufferSize);
                                    }

                                    fos.Close();
                                    fis.Close();
                                    respTemp.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO: Global Error Page.
                    throw ex;
                }
                #endregion
            }
        }
        /// <summary>
        /// The following method is designed in order to download the file
        /// from the dbVar FTP server.
        /// </summary>
        private static void DownloadVarFilesFromFTP()
        {
            string TempVarPath = HostingEnvironment.MapPath(BusConstants.varTempDirPath);

            if (Directory.Exists(TempVarPath))
            {
                //Deleting all the files from the directory before downloading the new files from the ftp.
                Array.ForEach(Directory.GetFiles(TempVarPath), delegate(string path) { File.Delete(path); });

                #region Download .gz files
                Uri varFTPURI = new Uri(BusConstants.VarFTP);

                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(varFTPURI + BusConstants.VarFile.dbVarFile + ".gz");
                req.Credentials = new NetworkCredential(string.Empty, string.Empty);

                try
                {
                    List<string> dirList = new List<string>();
                    req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);

                    string[] ftpDirList;

                    if (reader != null)
                    {
                        ftpDirList = reader.ReadToEnd().Split('\r', '\n');

                        if (ftpDirList.Count() > 0)
                        {
                            ftpDirList = ftpDirList.Where(val => !string.IsNullOrEmpty(val)).ToArray();
                            if (ftpDirList.Count() > 0)
                            {
                                foreach (string item in ftpDirList)
                                {
                                    string[] tempDirList = item.Split(' ');
                                    if (tempDirList[tempDirList.Length - 1].Contains(BusConstants.VarFile.dbVarFile + ".gz"))
                                        dirList.Add(tempDirList[tempDirList.Length - 1]);
                                }
                            }

                            if (dirList != null && dirList.Count > 0)
                            {
                                foreach (string item in dirList)
                                {
                                    FileInfo fi = new FileInfo(HostingEnvironment.MapPath(BusConstants.varTempDirPath) + @"\" + item);

                                    FtpWebRequest reqTemp = (FtpWebRequest)FtpWebRequest.Create(varFTPURI + item);
                                    reqTemp.Credentials = new NetworkCredential(string.Empty, string.Empty);

                                    reqTemp.Method = WebRequestMethods.Ftp.DownloadFile;
                                    reqTemp.UseBinary = true;     //cautious but not necessary for this
                                    reqTemp.KeepAlive = true;     //no effect
                                    reqTemp.UsePassive = true;    //no effect

                                    FtpWebResponse respTemp = (FtpWebResponse)reqTemp.GetResponse();
                                    Stream fis = respTemp.GetResponseStream();
                                    FileStream fos = fi.Create();

                                    int bufferSize = 2048;
                                    byte[] buffer = new byte[bufferSize];
                                    int readCount = fis.Read(buffer, 0, bufferSize);
                                    while (readCount > 0)
                                    {
                                        fos.Write(buffer, 0, readCount);
                                        readCount = fis.Read(buffer, 0, bufferSize);
                                    }

                                    fos.Close();
                                    fis.Close();
                                    respTemp.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO: Global Error Page.
                    throw ex;
                }
                #endregion
            }
        }
        /// <summary>
        /// The following method is designed in order to decompress the
        /// files downloaded from the ftp server.
        /// </summary>
        /// <returns>List of KeyValuePair of string and Bool indicating whether the decompression of each file was successful.</returns>
        private static List<KeyValuePair<string, bool>> DecompressMiRNAFiles(string fileIdentifier, string compressionExt)
        {
            string serverMapPath = HostingEnvironment.MapPath(BusConstants.TempDocumentsDirPath);
            string dirPath = string.Empty;
            string extension = string.Empty;

            switch (fileIdentifier.ToUpper())
            {
                case BusConstants.FTPSource.MiRNA:
                    dirPath = HostingEnvironment.MapPath(BusConstants.miRNATempDirPath);
                    extension = ".txt";
                    break;
                case BusConstants.FTPSource.SNP:
                    dirPath = HostingEnvironment.MapPath(BusConstants.snpTempDirPath);
                    extension = ".bcp";
                    break;
                case BusConstants.FTPSource.VAR:
                    dirPath = HostingEnvironment.MapPath(BusConstants.varTempDirPath);
                    extension = ".gvf";
                    break;
                case BusConstants.FTPSource.Gene:
                    dirPath = HostingEnvironment.MapPath(BusConstants.geneTempDirPath);
                    extension = ".txt";
                    break;
                default:
                    break;
            }

            List<KeyValuePair<string, bool>> DecompressedList = new List<KeyValuePair<string, bool>>();


            if (Directory.Exists(serverMapPath))
            {
                string[] files = Directory.GetFiles(dirPath, "*." + compressionExt);

                if (files.Count() > 0)
                {
                    Array.ForEach(Directory.GetFiles(dirPath, extension), delegate(string path) { File.Delete(path); });
                    foreach (string item in files)
                    {
                        try
                        {
                            using (Stream fileOutput = File.Create(item.Replace(compressionExt, string.Empty)))
                            using (Stream fileInput = File.OpenRead(item))
                            using (Stream zipStream = new GZipStream(fileInput, CompressionMode.Decompress))
                            {
                                byte[] decompressedBuffer = new byte[1024];
                                int nRead;
                                while ((nRead = zipStream.Read(decompressedBuffer, 0, decompressedBuffer.Length)) > 0)
                                {
                                    fileOutput.Write(decompressedBuffer, 0, nRead);
                                }
                                DecompressedList.Add(new KeyValuePair<string, bool>(item.Replace(dirPath, string.Empty), true));
                            }
                        }
                        catch { DecompressedList.Add(new KeyValuePair<string, bool>(item.Replace(dirPath, string.Empty), false)); }
                    }
                }
            }
            return DecompressedList;
        }
        #endregion
    }
}