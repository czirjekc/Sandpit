using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Eu.Europa.Ec.Olaf.Gmsr
{
    public class Utilities
    {
        #region Public Methods

        /// <summary>
        /// Binds the dropdown list item texts to the associated tooltips.
        /// This is useful for displaying item texts that are longer than the width of the dropdown.
        /// </summary>
        /// <param name="ddl">The dropdown to bind the tooltips to.</param>
        public static void BindToolTip(ListControl listControl)
        {
            foreach (ListItem item in listControl.Items)
            {
                item.Attributes.Add("title", item.Text);
            }
        }

        public static void AttachBackColors(ListControl listControl)
        {
            AttachBackColors(listControl, "#DFF7FD", "#CBD7ED");
        }

        public static void AttachBackColorsGray(ListControl listControl)
        {
            AttachBackColors(listControl, "#FFFFFF", "#DDDDDD");
        }

        public static void MakeSelectable(GridView gv, Page page)
        {
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Drawing.Color previousColor = row.BackColor;

                    row.Attributes["onmouseover"] =
                           "this.style.cursor='pointer';this.style.backgroundColor='#BBBBBB';";

                    row.Attributes["onmouseout"] =
                           "this.style.cursor='pointer';this.style.backgroundColor='" + System.Drawing.ColorTranslator.ToHtml(previousColor) + "';";
                    
                    row.Attributes["onclick"] +=
                           "javascript:ChangeRowStyle('" + gv.ClientID.Substring(gv.ClientID.Length - 1) + "',this);";
                    
                    // Set the last parameter to True 
                    // to register for event validation. 
                    row.Attributes["onclick"] +=
                            page.ClientScript.GetPostBackClientHyperlink(gv, "Select$" + row.DataItemIndex, true);                    
                }
            }
        }

        public static int GetGridViewIndex(GridView gv, string fieldName)
        {
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                DataControlField field = gv.Columns[i];

                BoundField bfield = field as BoundField;

                //Assuming accessing happens at data level, e.g with data field's name
                if (bfield != null && bfield.DataField == fieldName)
                    return i;
            }
            return -1;
        }

        public static BoundField GetGridViewField(GridView gv, string fieldName)
        {
            int index = GetGridViewIndex(gv, fieldName);
            return (index == -1) ? null : gv.Columns[index] as BoundField;
        }

        public static string GetGridViewRowText(GridViewRow row, string fieldName)
        {
            GridView gv = row.NamingContainer as GridView;
            if (gv != null)
            {
                int index = GetGridViewIndex(gv, fieldName);
                if (index != -1)
                    return row.Cells[index].Text;
            }
            return "";
        }  

        public static void ExportToExcel(GridView gv, HttpResponse response)
        {
            response.ClearContent();

            response.AddHeader("content-disposition", "attachment; filename=" + gv.ID + ".xls");
            response.Charset = "";

            // If you want the option to open the Excel file without saving than comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //Response.ContentType = "application/ms-excel";
            response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            gv.RenderControl(htmlWriter);

            response.Write(stringWriter.ToString());
            response.End();
        }

        public static void DownloadFile(string filename, byte[] file, string fileSize, HttpResponse response)
        {
            using (MemoryStream ms = new MemoryStream(file))
            {
                long dataLengthToRead = ms.Length;
                int blockSize = dataLengthToRead >= 5000 ? 5000 : (int)dataLengthToRead;
                byte[] buffer = new byte[dataLengthToRead];

                response.Clear();

                // Clear the content of the response
                response.ClearContent();
                response.ClearHeaders();

                // Buffer response so that page is sent
                // after processing is complete.
                response.BufferOutput = true;

                // Add the file name and attachment,
                // which will force the open/cance/save dialog to show, to the header
                response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

                // bypass the Open/Save/Cancel dialog
                //Response.AddHeader("Content-Disposition", "inline; filename=" + doc.FileName);

                // Add the file size into the response header
                response.AddHeader("Content-Length", fileSize);

                // Set the ContentType
                response.ContentType = "application/octet-stream";

                // Write the document into the response
                while (dataLengthToRead > 0 && response.IsClientConnected)
                {
                    Int32 lengthRead = ms.Read(buffer, 0, blockSize);
                    response.OutputStream.Write(buffer, 0, lengthRead);
                    //response.Flush(); // do not flush since BufferOutput = true
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }

                response.Flush();
                response.Close();
            }

            // End the response
            response.End();
        }

        /// <summary>
        /// Returns the id between brackets at the end of a concatenation.
        /// Returns -1 if it can't find the id in the concatenation.
        /// </summary>
        /// <param name="concatenation">The concatenation.</param>
        /// <returns>The id.</returns>
        public static int GetIdFromConcatenation(string concatenation)
        {
            int id;

            int startIndex = concatenation.IndexOf("| (") + 3;
            int length = concatenation.IndexOf(')', startIndex < 3 ? 0 : startIndex) - startIndex;

            if (startIndex >= 0 && length > 0)
            {
                id = int.Parse(concatenation.Substring(startIndex, length));
            }
            else
            {
                id = -1;
            }

            return id;
        }

        #endregion

        #region Private Methods

        public static void AttachBackColors(ListControl listControl, string itemColor, string alternatingColor)
        {
            string color = itemColor;

            foreach (ListItem li in listControl.Items)
            {
                color = (color == itemColor) ? alternatingColor : itemColor;
                li.Attributes["Style"] = " background-color: " + color;
            }
        }

        #endregion
    }
}