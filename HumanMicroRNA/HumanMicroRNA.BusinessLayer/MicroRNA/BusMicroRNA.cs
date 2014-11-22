using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumanMicroRNA.DataLayer.MicroRNA;
using System.Data;
using HumanMicroRNA.BusinessLayer.Base;

namespace HumanMicroRNA.BusinessLayer.MicroRNA
{
    public class BusMicroRNA
    {
        private DbMicroRNA _microRNA;

        /// <constructor>
        /// Constructor BusMicroRNA
        /// </constructor>
        public BusMicroRNA()
        {
            _microRNA = new DbMicroRNA();
        }

        /// <summary>
        /// The following method is designed in order to import the data from the mirna file
        /// downloaded from the ftp server to the mirna table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable miRNADt</param>
        /// <param name="tableName">string tableName</param>
        /// <param name="batchSize">bool batchSize</param>
        /// <param name="columnMapping">bool columnMapping</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportMiRNA(DataTable mirRNADt, string tableName, bool batchSize, bool columnMapping)
        {
            return (DbMicroRNA.ImportMiRNA(mirRNADt, tableName, batchSize, columnMapping));
        }
        /// <summary>
        /// The following method is designed in order to return the MiRNA records from
        /// the rna.mirna table from the database based on the parameters passed to it.
        /// </summary>
        /// <param name="autoMiRNA">bool autoMiRNA</param>
        /// <param name="miRNAAcc">bool miRNAAcc</param>
        /// <returns>DataTable containing either the MiRNA auto_mirna, mirna_acc or both fields.</returns>
        public static DataTable GetAllMiRNAByAutoMiRNAorMiRNAAcc(bool autoMiRNA, bool miRNAAcc)
        {
            return (DbMicroRNA.GetAllMiRNAByAutoMiRNAorMiRNAAcc(autoMiRNA, miRNAAcc));
        }
        /// <summary>
        /// The following method is designed in order to detele
        /// the records from the designated tables.
        /// </summary>
        /// <param name="tableName">string tableName</param>
        /// <returns>Return a boolean value indicating true for successful insert; otherwise false.</returns>
        public static bool DeleteMiRNARecords(string tableName)
        {
            return (DbMicroRNA.DeleteMiRNARecords(tableName) > -1);
        }
        /// <summary>
        /// The following method is designed in order to return the MiRNA
        /// value into a datatable.
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAllMiRNARecords()
        {
            return DbMicroRNA.GetAllMiRNARecords();
        }
        /// <summary>
        /// The following method is designed in order to bulk import
        /// the MiRNA-Var into the hmrna_var_assn table.
        /// </summary>
        /// <param name="miRNAVarDt">DataTable miRNAVarDt</param>
        /// <returns>Boolean value indicating if the import was successful.</returns>
        public static bool ImportMiRNAVar(DataTable miRNAVarDt)
        {
            return DbMicroRNA.ImportMiRNAVar(miRNAVarDt);
        }
        /// <summary>
        /// The following method is designed in order to get the
        /// MiRNA records from the database based on the search
        /// criterias.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <param name="accession">string accession</param>
        /// <param name="chromosome">string chromosome</param>
        /// <param name="variant">string variant</param>
        /// <returns>DataTable of MiRNA based on the search criteria</returns>
        public static DataTable GetMiRNABySearchCriteria(string miRNAID, string accession, string chromosome, string variant)
        {
            return DbMicroRNA.GetMiRNABySearchCriteria(miRNAID, accession, chromosome, variant);
        }
        /// <summary>
        /// The following method is designed in order
        /// to query the database for MiRNA Accession based 
        /// on the string passed to it.
        /// </summary>
        /// <param name="Accession">List of MiRNA Accession</param>
        public static List<string> GetMiRNAAccession(string Accession)
        {
            return DbMicroRNA.GetMiRNAAccession(Accession);
        }
        /// <summary>
        /// The following method is designed in order to return
        /// the record of a unique miRNA based on the id passed to
        /// it.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>DataTable of miRNA record.</returns>
        public static DataTable GetUniqueMiRNAByMiRNAID(string miRNAID)
        {
            return DbMicroRNA.GetUniqueMiRNAByMiRNAID(miRNAID);
        }
        /// <summary>
        /// The following method is designed in order to
        /// return a list of MiRNA IDs.
        /// </summary>
        /// <param name="varID">string miRNAID</param>
        /// <returns>List of MiRNA ID</returns>
        public static List<string> GetMiRNAID(string miRNAID)
        {
            return DbMicroRNA.GetMiRNAID(miRNAID);
        }
        /// <summary>
        /// The following method is designed in order to
        /// return a list of Var IDs.
        /// </summary>
        /// <param name="varID">string varID</param>
        /// <returns>List of Var ID</returns>
        public static List<string> GeVarID(string varID)
        {
            return DbMicroRNA.GeVarID(varID);
        }
        /// <summary>
        /// The following method is designed in order to import gene
        /// information into the mir_family_info table.
        /// </summary>
        /// <param name="geneDt">DataTable geneDt</param>
        /// <param name="p">bool batchImport</param>
        /// <returns>Return a boolean value indication the status of the import.</returns>
        public static bool ImportGeneFamilyInfo(DataTable geneDt, bool batchImport)
        {
            return DbMicroRNA.ImportGeneFamilyInfo(geneDt, batchImport);
        }
        /// <summary>
        /// The following method is designed in order to return all of the
        /// gene target information.
        /// </summary>
        /// <returns>DataTable holding all of the Gene Target Information.</returns>
        public static DataTable GetAllmiRFamilyInfo()
        {
            return DbMicroRNA.GetAllmiRFamilyInfo();
        }
        /// <summary>
        /// The following method is designed in order to import gene
        /// information into the mir_family_info table.
        /// </summary>
        /// <param name="dtGene">DataTable geneDt</param>
        /// <param name="batchImport">bool batchImport</param>
        /// <returns>Return a boolean value indication the status of the import.</returns>
        public static bool ImportPredictedTargetInfo(DataTable dtGene, bool batchImport)
        {
            return DbMicroRNA.ImportPredictedTargetInfo(dtGene, batchImport);
        }
        /// <summary>
        /// Get all of the Gene info associated with a specific mirna id.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>Returns the gene information about a specific mirna id.</returns>
        public static DataTable GetAllGeneInfoByMiRNAID(string miRNAID)
        {
            DataTable dt = DbMicroRNA.GetAllGeneInfoByMiRNAID(miRNAID);

            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("gene_id_non_link", typeof(string));
                foreach (DataRow item in dt.Rows)
                {
                    item["gene_id_non_link"] = item["gene_id"];
                    item["gene_id"] = BusConstants.GetGeneLink(item["gene_id"].ToString());
                }
            }

            return dt;
        }
    }
}
