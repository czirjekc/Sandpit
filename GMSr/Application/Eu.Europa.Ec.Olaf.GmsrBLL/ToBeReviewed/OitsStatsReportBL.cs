using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eu.Europa.Ec.Olaf.GmsrDAL.Custom;
using System.Web;
using DocumentFormat.OpenXml;

namespace Eu.Europa.Ec.Olaf.GmsrBLL
{
    public class OitsStatsReportBL
    {
        #region Public Methods

        public static void OpenXMLExport(DateTime startDate, DateTime endDate, HttpResponse response)
        {
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            DocumentFormat.OpenXml.Packaging.SpreadsheetDocument spreadSheetDocument = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

            #region Spreadsheet common base elements

            DocumentFormat.OpenXml.Packaging.WorkbookPart workbookPart = spreadSheetDocument.AddWorkbookPart();
            DocumentFormat.OpenXml.Spreadsheet.Workbook workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
            workbookPart.Workbook = workbook;
            DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
            workbook.Sheets = sheets;

            #endregion

            #region Spreadsheet styling

            #region Base elements

            DocumentFormat.OpenXml.Packaging.WorkbookStylesPart workBookStylesPart = workbookPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WorkbookStylesPart>();
            DocumentFormat.OpenXml.Spreadsheet.Stylesheet styleSheet = new DocumentFormat.OpenXml.Spreadsheet.Stylesheet();
            workBookStylesPart.Stylesheet = styleSheet;

            #endregion

            #region Fonts

            DocumentFormat.OpenXml.Spreadsheet.Fonts fonts = new DocumentFormat.OpenXml.Spreadsheet.Fonts();
            DocumentFormat.OpenXml.Spreadsheet.Font font1 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font1.FontSize = new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            font1.Color = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };
            font1.FontName = new DocumentFormat.OpenXml.Spreadsheet.FontName() { Val = "Calibri" };
            font1.FontFamilyNumbering = new DocumentFormat.OpenXml.Spreadsheet.FontFamilyNumbering() { Val = 2 };
            fonts.Count = 1U;

            fonts.AppendChild(font1);
            //First appended font is the default font

            DocumentFormat.OpenXml.Spreadsheet.Font font2 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font2.FontSize = new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            font2.Color = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };
            font2.FontName = new DocumentFormat.OpenXml.Spreadsheet.FontName() { Val = "Arial" };
            font2.FontFamilyNumbering = new DocumentFormat.OpenXml.Spreadsheet.FontFamilyNumbering() { Val = 2 };                    
            fonts.Count = 2U;

            fonts.AppendChild(font2);

            DocumentFormat.OpenXml.Spreadsheet.Font font3 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font3.FontSize = new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            font3.Color = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };
            font3.FontName = new DocumentFormat.OpenXml.Spreadsheet.FontName() { Val = "Arial Black" };
            font3.FontFamilyNumbering = new DocumentFormat.OpenXml.Spreadsheet.FontFamilyNumbering() { Val = 2 };
            font3.Bold = new DocumentFormat.OpenXml.Spreadsheet.Bold() { Val = true };
            fonts.Count = 3U;

            fonts.AppendChild(font3);

            #endregion

            #region Fills
            DocumentFormat.OpenXml.Spreadsheet.Fills fills = new DocumentFormat.OpenXml.Spreadsheet.Fills();
            DocumentFormat.OpenXml.Spreadsheet.Fill fill1 = new DocumentFormat.OpenXml.Spreadsheet.Fill();
            DocumentFormat.OpenXml.Spreadsheet.PatternFill patternFill1 = new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = DocumentFormat.OpenXml.Spreadsheet.PatternValues.None };
            //PatternType is ignored for the first appended child of the Fills element. It will always be equal to "DocumentFormat.OpenXml.Spreadsheet.PatternValues.None"
            fill1.AppendChild(patternFill1);

            DocumentFormat.OpenXml.Spreadsheet.Fill fill2 = new DocumentFormat.OpenXml.Spreadsheet.Fill();
            DocumentFormat.OpenXml.Spreadsheet.PatternFill patternFill2 = new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = DocumentFormat.OpenXml.Spreadsheet.PatternValues.Gray125 };
            //PatternType is ignored for the second appended child of the Fills element. It will always be equal to "DocumentFormat.OpenXml.Spreadsheet.PatternValues.Gray125"

            fill2.AppendChild(patternFill2);

            DocumentFormat.OpenXml.Spreadsheet.Fill fill3 = new DocumentFormat.OpenXml.Spreadsheet.Fill();
            DocumentFormat.OpenXml.Spreadsheet.PatternFill patternFill3 = new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid };
            DocumentFormat.OpenXml.Spreadsheet.BackgroundColor backgroundColor1 = new DocumentFormat.OpenXml.Spreadsheet.BackgroundColor() { };
            //BackgroundColor is always ignored in favor of ForegroundColor
            DocumentFormat.OpenXml.Spreadsheet.ForegroundColor foregroundColor1 = new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Indexed = (UInt32Value)38U };


            patternFill3.AppendChild(foregroundColor1);
            patternFill3.AppendChild(backgroundColor1);
            //Foreground must be appended before background
            //Foreground and background must be childs of patternfill and not attributes

            fill3.AppendChild(patternFill3);

            fills.AppendChild(fill1);
            fills.AppendChild(fill2);
            fills.AppendChild(fill3);
            //Order matters! First and second childs are fixed, only childs starting from the third are user-defined!

            fills.Count = (UInt32Value)3U;
            //Seems to be useless

            #endregion

            #region Borders



            DocumentFormat.OpenXml.Spreadsheet.Borders borders = new DocumentFormat.OpenXml.Spreadsheet.Borders();
            DocumentFormat.OpenXml.Spreadsheet.Border border1 = new DocumentFormat.OpenXml.Spreadsheet.Border();
            DocumentFormat.OpenXml.Spreadsheet.LeftBorder leftborder1 = new DocumentFormat.OpenXml.Spreadsheet.LeftBorder();
            DocumentFormat.OpenXml.Spreadsheet.RightBorder rightborder1 = new DocumentFormat.OpenXml.Spreadsheet.RightBorder();
            DocumentFormat.OpenXml.Spreadsheet.TopBorder topborder1 = new DocumentFormat.OpenXml.Spreadsheet.TopBorder();
            DocumentFormat.OpenXml.Spreadsheet.BottomBorder bottomborder1 = new DocumentFormat.OpenXml.Spreadsheet.BottomBorder();
            DocumentFormat.OpenXml.Spreadsheet.DiagonalBorder diagonalborder1 = new DocumentFormat.OpenXml.Spreadsheet.DiagonalBorder();

            //!!THE ELEMENTS ORDER MATTERS!!

            border1.AppendChild(leftborder1);
            border1.AppendChild(rightborder1);
            border1.AppendChild(topborder1);
            border1.AppendChild(bottomborder1);
            border1.AppendChild(diagonalborder1);
            borders.Count = 1U;
            borders.Append(border1);
            //First border is the default border

            DocumentFormat.OpenXml.Spreadsheet.Border border2 = new DocumentFormat.OpenXml.Spreadsheet.Border();
            DocumentFormat.OpenXml.Spreadsheet.LeftBorder leftborder2 = new DocumentFormat.OpenXml.Spreadsheet.LeftBorder();
            DocumentFormat.OpenXml.Spreadsheet.RightBorder rightborder2 = new DocumentFormat.OpenXml.Spreadsheet.RightBorder();
            DocumentFormat.OpenXml.Spreadsheet.TopBorder topborder2 = new DocumentFormat.OpenXml.Spreadsheet.TopBorder();
            DocumentFormat.OpenXml.Spreadsheet.BottomBorder bottomborder2 = new DocumentFormat.OpenXml.Spreadsheet.BottomBorder();
            DocumentFormat.OpenXml.Spreadsheet.DiagonalBorder diagonalborder2 = new DocumentFormat.OpenXml.Spreadsheet.DiagonalBorder();
            DocumentFormat.OpenXml.Spreadsheet.Color borderColor;

            leftborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thick;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 41U };
            leftborder2.Append(borderColor);

            rightborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thick;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 41U };
            rightborder2.Append(borderColor);

            topborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thick;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 41U };
            topborder2.Append(borderColor);

            bottomborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thick;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 41U };
            bottomborder2.Append(borderColor);

            //!!THE ELEMENTS ORDER MATTERS!!

            border2.AppendChild(leftborder2);
            border2.AppendChild(rightborder2);
            border2.AppendChild(topborder2);
            border2.AppendChild(bottomborder2);
            border2.AppendChild(diagonalborder2);
            borders.Count = 2U;
            borders.Append(border2);

            #endregion

            #region Number formats

            //Not mandatory

            #endregion

            #region Cell style formats

            DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats cellstyleformats = new DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats();
            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellstyleformat = new DocumentFormat.OpenXml.Spreadsheet.CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U };
            cellstyleformats.AppendChild(cellstyleformat);
            cellstyleformats.Count = 1;

            //Mandatory (but useless?)

            #endregion

            #region Cell formats

            #region Default format

            DocumentFormat.OpenXml.Spreadsheet.CellFormats cellFormats = new DocumentFormat.OpenXml.Spreadsheet.CellFormats();
            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat1 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat1.FormatId = 0U;
            cellFormat1.NumberFormatId = 0U;
            cellFormat1.FontId = 0U;
            cellFormat1.BorderId = 0U;
            cellFormat1.FillId = 0U;

            cellFormats.Append(cellFormat1);
            //The first appended DocumentFormat.OpenXml.Spreadsheet.CellFormat is the default cell format. It applies to all cells not having a specified cell format
            cellFormats.Count = 1U;
            //Seems to be useless

            #endregion

            #region Header format

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat2 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat2.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat2.FontId = 1U;
            cellFormat2.BorderId = 1U;
            cellFormat2.FillId = 2U;
            cellFormat2.ApplyFill = true;
            cellFormat2.ApplyFont = true;
            cellFormat2.ApplyBorder = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat2);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 2U;
            //Seems to be useless

            #endregion

            #region Date (mmm-yy) format

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat3 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat3.NumberFormatId = 17U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat3.FontId = 0U;
            cellFormat3.BorderId = 0U;
            cellFormat3.FillId = 0U;
            cellFormat3.ApplyFill = false;
            cellFormat3.ApplyFont = false;
            cellFormat3.ApplyBorder = false;
            cellFormat3.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat3);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 3U;
            //Seems to be useless

            #endregion

            #region Bold format

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat4 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat4.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat4.FontId = 2U;
            cellFormat4.BorderId = 0U;
            cellFormat4.FillId = 0U;
            cellFormat4.ApplyFill = false;
            cellFormat4.ApplyFont = true;
            cellFormat4.ApplyBorder = false;
            cellFormat4.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat4);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 4U;
            //Seems to be useless

            #endregion

            #endregion

            #region Cell styles

            DocumentFormat.OpenXml.Spreadsheet.CellStyles cellstyles = new DocumentFormat.OpenXml.Spreadsheet.CellStyles();
            DocumentFormat.OpenXml.Spreadsheet.CellStyle cellstyle = new DocumentFormat.OpenXml.Spreadsheet.CellStyle();
            cellstyle.Name = "Normal";
            cellstyle.FormatId = 0U;
            cellstyle.BuiltinId = 0U;
            cellstyles.Count = 1U;
            cellstyles.AppendChild(cellstyle);

            //Mandatory but useless

            #endregion

            #region Differential formats

            //Not mandatory

            #endregion

            #region Table styles

            //Not mandatory

            #endregion

            //!!THE ELEMENTS ORDER MATTERS!!
            styleSheet.Append(fonts);
            styleSheet.Append(fills);
            styleSheet.Append(borders);
            styleSheet.Append(cellstyleformats);
            styleSheet.Append(cellFormats);
            styleSheet.Append(cellstyles);

            #endregion

            #region Add sheets and data

            List<string> titleList = new List<string>();
            List<List<string>> headerListList = new List<List<string>>();
            List<List<object>> itemListList = new List<List<object>>();

            titleList.Add("NET1 incidents natures from " + startDate.ToString() + " to " + endDate.ToString());
            headerListList.Add(new List<string> { "Nature name", "Total" });
            itemListList.Add(GetNet1IncidentNaturesFromUntil(startDate, endDate).ToList<object>());
            spreadSheetDocument = AddSheetAndData(spreadSheetDocument, "NET1 Incidents natures (1)", itemListList, headerListList, titleList);
            titleList.Clear();
            headerListList.Clear();
            itemListList.Clear();

            titleList.Add("CBIS incidents natures from " + startDate.ToString() + " to " + endDate.ToString());
            headerListList.Add(new List<string> { "Nature name", "Total" });
            itemListList.Add(GetCbisIncidentNaturesFromUntil(startDate, endDate).ToList<object>());
            spreadSheetDocument = AddSheetAndData(spreadSheetDocument, "CBIS Incidents natures (1)", itemListList, headerListList, titleList);
            titleList.Clear();
            headerListList.Clear();
            itemListList.Clear();

            titleList.Add("NET1 Incidents breakdown by month from " + startDate.ToString() + " to " + endDate.ToString());
            titleList.Add("CBIS Incidents breakdown by month from " + startDate.ToString() + " to " + endDate.ToString());
            headerListList.Add(new List<string> { "Month", "Total" });
            headerListList.Add(new List<string> { "Month", "Total" });
            itemListList.Add(GetNet1IncidentItemsByMonthFromUntil(startDate, endDate).ToList<object>());
            itemListList.Add(GetCbisIncidentItemsByMonthFromUntil(startDate, endDate).ToList<object>());
            spreadSheetDocument = AddSheetAndData(spreadSheetDocument, "Incidents by month (2)", itemListList, headerListList, titleList);
            titleList.Clear();
            headerListList.Clear();
            itemListList.Clear();

            titleList.Add("NET1 Incidents breakdown by month from " + startDate.ToString() + " to " + endDate.ToString());
            titleList.Add("CBIS Incidents breakdown by month from " + startDate.ToString() + " to " + endDate.ToString());
            headerListList.Add(new List<string> { "Month", "Total" });
            headerListList.Add(new List<string> { "Month", "Total" });
            itemListList.Add(GetNet1ItemsByMonthFromUntil(startDate, endDate).ToList<object>());
            itemListList.Add(GetCbisItemsByMonthFromUntil(startDate, endDate).ToList<object>());
            spreadSheetDocument = AddSheetAndData(spreadSheetDocument, "Items by month (2)", itemListList, headerListList, titleList);
            titleList.Clear();
            headerListList.Clear();
            itemListList.Clear();

            #endregion

            #region Finally

            spreadSheetDocument.Close();
            spreadSheetDocument.Dispose();

            if (memoryStream != null)
            {
                Byte[] byteArray = memoryStream.ToArray();
                memoryStream.Flush();
                memoryStream.Close();
                response.BufferOutput = true;
                // Clear all content output from the buffer stream
                response.Clear();
                //to fix the “file not found” error when opening excel file
                //See http://www.aspose.com/Community/forums/ShowThread.aspx?PostID=61444
                response.ClearHeaders();
                // Add a HTTP header to the output stream that specifies the default filename
                // for the browser’s download dialog
                string timeStamp = Convert.ToString(DateTime.Now.ToString("MMddyyyy_HHmmss"));
                response.AddHeader("Content-Disposition", "attachment; filename=OITS_Stats_" + timeStamp + ".xlsx");
                // Set the HTTP MIME type of the output stream
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Write the data
                response.BinaryWrite(byteArray);
                response.End();
            }

            memoryStream.Close();
            memoryStream.Dispose();
            GC.Collect();

            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            #endregion


        }

        #endregion

        #region Private Methods

        private static List<NameAndTotalBL> GetNet1IncidentNaturesFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalBL> itemBL_list = new List<NameAndTotalBL>();
            NameAndTotalBL itemBL;

            GetSmcGeneric.GetNet1IncidentNaturesFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new NameAndTotalBL();
                itemBL.MapDataFromNameAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<NameAndTotalBL> GetCbisIncidentNaturesFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalBL> itemBL_list = new List<NameAndTotalBL>();
            NameAndTotalBL itemBL;

            GetSmcGeneric.GetCbisIncidentNaturesFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new NameAndTotalBL();
                itemBL.MapDataFromNameAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<NameAndTotalAndDateBL> GetNet1IncidentNaturesByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalAndDateBL> itemBL_list = new List<NameAndTotalAndDateBL>();
            NameAndTotalAndDateBL itemBL;

            GetSmcGeneric.GetNet1IncidentNaturesByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new NameAndTotalAndDateBL();
                itemBL.MapDataFromNameAndTotalAndDateDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<NameAndTotalAndDateBL> GetCbisIncidentNaturesByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<NameAndTotalAndDateBL> itemBL_list = new List<NameAndTotalAndDateBL>();
            NameAndTotalAndDateBL itemBL;

            GetSmcGeneric.GetCbisIncidentNaturesByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new NameAndTotalAndDateBL();
                itemBL.MapDataFromNameAndTotalAndDateDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<DateAndTotalBL> GetNet1IncidentItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalBL> itemBL_list = new List<DateAndTotalBL>();
            DateAndTotalBL itemBL;

            GetSmcGeneric.GetNet1IncidentItemsByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new DateAndTotalBL();
                itemBL.MapDataFromDateAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<DateAndTotalBL> GetCbisIncidentItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalBL> itemBL_list = new List<DateAndTotalBL>();
            DateAndTotalBL itemBL;

            GetSmcGeneric.GetCbisIncidentItemsByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new DateAndTotalBL();
                itemBL.MapDataFromDateAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<DateAndTotalBL> GetNet1ItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalBL> itemBL_list = new List<DateAndTotalBL>();
            DateAndTotalBL itemBL;

            GetSmcGeneric.GetNet1ItemsByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new DateAndTotalBL();
                itemBL.MapDataFromDateAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static List<DateAndTotalBL> GetCbisItemsByMonthFromUntil(DateTime startDate, DateTime endDate)
        {
            List<DateAndTotalBL> itemBL_list = new List<DateAndTotalBL>();
            DateAndTotalBL itemBL;

            GetSmcGeneric.GetCbisItemsByMonthFromUntil(startDate, endDate).ForEach(x =>
            {
                itemBL = new DateAndTotalBL();
                itemBL.MapDataFromDateAndTotalDL(x);
                itemBL_list.Add(itemBL);
            });

            return itemBL_list;
        }

        private static DocumentFormat.OpenXml.Packaging.SpreadsheetDocument AddSheetAndData(DocumentFormat.OpenXml.Packaging.SpreadsheetDocument spreadsheetDocument, string sheetName, List<List<object>> itemListList, List<List<string>> headerListList, List<string> titleList)
        {

            //Add the new sheet
            DocumentFormat.OpenXml.Packaging.WorksheetPart worksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WorksheetPart>();
            DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet();
            sheet.Name = sheetName;
            sheet.SheetId = (uint)spreadsheetDocument.WorkbookPart.Workbook.Sheets.Count() + 1;
            sheet.Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
            spreadsheetDocument.WorkbookPart.Workbook.Sheets.AppendChild(sheet);
            DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();
            worksheetPart.Worksheet = worksheet;
            DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
            worksheet.AppendChild(sheetData);

            DocumentFormat.OpenXml.Spreadsheet.Row row;
            DocumentFormat.OpenXml.Spreadsheet.Cell cell;

            Int32 currentIndex = 0;
            itemListList.ForEach(itemList =>
            {
                //Add the title
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                if ((titleList.Count > 0) && (titleList.Count - 1 >= currentIndex))
                {
                    //Gets here if a corresponding title exists
                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(titleList[currentIndex]);
                    cell.StyleIndex = 3U;
                    row.AppendChild(cell);                                      
                }
                else
                {
                    //Gets here if no corresponding list of headers exists
                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("No title");
                    cell.StyleIndex = 3U;
                    row.AppendChild(cell);                   
                }
                sheetData.AppendChild(row);

                //Add the headers
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                if ((headerListList.Count > 0) && (headerListList.Count - 1 >= currentIndex))
                {
                    //Gets here if a corresponding list of headers exists
                    headerListList[currentIndex].ForEach(header =>
                    {
                        cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(header);
                        cell.StyleIndex = 1U;
                        row.AppendChild(cell);
                    });

                }
                else
                {
                    //Gets here if no corresponding list of headers exists
                    itemList.FirstOrDefault().GetType().GetProperties().ToList().ForEach(y =>
                    {
                        cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(y.Name);
                        cell.StyleIndex = 1U;
                        row.AppendChild(cell);
                    }
                 );
                }
                sheetData.AppendChild(row);
                currentIndex++;
                //Add the data
                itemList.ForEach(x =>
                {
                    row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    x.GetType().GetProperties().ToList().ForEach(y =>
                    {
                        cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.GetType().GetProperty(y.Name).GetValue(x, null).ToString());
                        if (x.GetType().GetProperty(y.Name).GetValue(x, null).GetType() == typeof(DateTime))
                        {
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(((DateTime)x.GetType().GetProperty(y.Name).GetValue(x, null)).ToOADate().ToString());
                            cell.StyleIndex = 2U;
                        }
                        else if (x.GetType().GetProperty(y.Name).GetValue(x, null).GetType() == typeof(Double) || x.GetType().GetProperty(y.Name).GetValue(x, null).GetType() == typeof(Int32) || x.GetType().GetProperty(y.Name).GetValue(x, null).GetType() == typeof(Decimal))
                        {
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.GetType().GetProperty(y.Name).GetValue(x, null).ToString());
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                        }
                        else
                        {
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.GetType().GetProperty(y.Name).GetValue(x, null).ToString());
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        }
                        row.AppendChild(cell);
                    });
                    sheetData.AppendChild(row);
                });
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                sheetData.AppendChild(row);
            });

            return spreadsheetDocument;
        }

        #endregion

    }
}
