using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HumanMicroRNA.DataLayer.Var;

namespace HumanMicroRNA.BusinessLayer.Var
{
    /// <summary>
    /// The following class is designed in order to handle all
    /// of the business layer operations.
    /// </summary>
    public class BusVar
    {
        private DbVar _var;

        /// <constructor>
        /// Constructor BusVar
        /// </constructor>
        public BusVar()
        {
            _var  = new DbVar();
        }
        /// <summary>
        /// The following method is designed in order to delete all of
        /// the dbVar records from the table db_var in the database.
        /// </summary>
        /// <returns>Return true if the delete was successful; otherwise false.</returns>
        public static bool DeleteAllVarFromTable()
        {
            return (DbVar.DeleteAllVarFromTable() > -1);
        }
        /// <summary>
        /// The following method is designed in order to import the data from the var file
        /// downloaded from the ftp server to the db_var table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable varDt</param>
        /// <param name="tableName">string tableName</param>
        /// <param name="batchSize">bool batchSize</param>
        /// <param name="columnMapping">bool columnMapping</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportVar(DataTable varDt, string tableName, bool batchSize, bool columnMapping)
        {
            return (DbVar.ImportVar(varDt, tableName, batchSize, columnMapping));
        }
        /// <summary>
        /// The following method is designed in order to
        /// return all of the var records.
        /// </summary>
        /// <returns>Return all of the Var records in a datatable.</returns>
        public static DataTable GetAllVar()
        {
            return (DbVar.GetAllVar());
        }
        /// <summary>
        /// The following method is designed in order to
        /// return all of the var records by chromosome.
        /// </summary>
        /// <param name="chromosome">string chromosome</param>
        /// <returns>Return all of the Var records in a datatable.</returns>
        public static DataTable GetVarByChromosome(string chromosome)
        {
            return (DbVar.GetVarByChromosome(chromosome));
        }
        /// <summary>
        /// The following method is designed in order to return
        /// all of the Variants associated with a unique MiRNA based
        /// on the Human MicroRNA ID passed to it.
        /// </summary>
        /// <param name="hMiRNAID">int hMiRNAID</param>
        /// <returns>DataTable of all the Variants records.</returns>
        public static DataTable GetAllVarsByMiRNAID(int hMiRNAID)
        {
            return DbVar.GetAllVarsByMiRNAID(hMiRNAID);
        }
    }
}
