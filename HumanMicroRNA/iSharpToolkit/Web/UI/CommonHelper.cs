using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;

namespace iSharpToolkit.Web.UI
{
    public class CommonHelper
    {
        #region Write Response
        /// <summary>
        /// Write CSV to response
        /// </summary>
        /// <param name="filePath">Image File Path</param>
        /// <param name="targetFileName">Filename that the user will see</param>
        public static void WriteResponseImage(string filePath, string targetFileName)
        {
            if (String.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return;

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            response.ContentType = "image/" + new FileInfo(filePath).Extension.Trim('.');
            response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", targetFileName));
            response.Write(File.ReadAllBytes(filePath));
            response.End();
        }

        /// <summary>
        /// Write CSV to response
        /// </summary>
        /// <param name="csv">CSV text</param>
        /// <param name="Filename">Filename that the user will see</param>
        public static void WriteResponseCSV(ref string csv, string Filename)
        {
            if (String.IsNullOrEmpty(csv))
                return;

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", Filename));
            response.Write(csv);
            response.End();
        }

        /// <summary>
        /// Write XML to response
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="Filename">Filename</param>
        public static void WriteResponseXML(string xml, string Filename)
        {
            if (String.IsNullOrEmpty(xml))
                return;

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlDeclaration xmlDeclaration = document.FirstChild as XmlDeclaration;
            if (xmlDeclaration != null)
                xmlDeclaration.Encoding = "utf-8";
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "utf-8";
            response.ContentType = "text/xml";
            response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Filename));
            response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
            response.End();
        }

        /// <summary>
        /// Write XLS file to response
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="targetFileName">Target file name</param>
        public static void WriteResponseXLS(string filePath, string targetFileName)
        {
            if (String.IsNullOrEmpty(filePath))
                return;

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "utf-8";
            response.ContentType = "text/xls";
            response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
            response.BinaryWrite(File.ReadAllBytes(filePath));
            response.End();
        }

        /// <summary>
        /// Write PDF file to response
        /// </summary>
        /// <param name="filePath">File napathme</param>
        /// <param name="targetFileName">Target file name</param>
        public static void WriteResponsePDF(string filePath, string targetFileName)
        {
            if (String.IsNullOrEmpty(filePath))
                return;

            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "utf-8";
            response.ContentType = "text/pdf";
            response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
            response.BinaryWrite(File.ReadAllBytes(filePath));
            response.End();
        }
        #endregion
    }
}
