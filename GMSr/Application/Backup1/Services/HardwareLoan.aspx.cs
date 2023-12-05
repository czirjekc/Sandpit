using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using System.Collections;
using DocumentFormat.OpenXml;

namespace Eu.Europa.Ec.Olaf.Gmsr.Services
{
    public partial class HardwareLoan : System.Web.UI.Page
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExport);
            LinkButton menuItem;
            HardwareLoanCategoryBL.GetAll().ForEach(x =>
            {
                if (!IsPostBack)
                {
                    ddlCategory.Items.Add(new ListItem(x.Name, x.Id.ToString()));
                }
                menuItem = new LinkButton() { Text = x.Name, CssClass = "ContextMenuItem", CausesValidation = false, ID = "Category_" + x.Id.ToString() };
                menuItem.Click += CategoryMenuItem_Click;
                pnlCategory.Controls.Add(menuItem);
                ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(menuItem);
            });
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons
            if (!IsPostBack)
            {
                HardwareLoanStatusBL.GetAll().ForEach(x =>
                {
                    ddlStatus.Items.Add(new ListItem(x.Name, x.Id.ToString()));
                });
                ((MasterPage)Master).EnableAction(Action.Search);
                tbSearchItem.Attributes.Add("onKeyPress", "doClick('" + "ibtnActionSearch" + "',event)");
            }
            else
            {
            }
            gvItems1.RowCommand += gvItems1_RowCommand; // to handle the row selection of gvItems1
            //gvItems2.RowDeleting += gvItems2_RowDeleting;
            ddlCategory.Enabled = false;
            ddlStatus.Enabled = false;
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Search)
            {
                List<HardwareLoanItemBL> tempList = new List<HardwareLoanItemBL>();
                if (calTimeFrameEnd.SelectedDate.ToBinary() > 0 && calTimeFrameStart.SelectedDate.ToBinary() > 0)
                {
                    tempList.AddRange(HardwareLoanItemBL.GetByInventoryNumber(tbSearchItem.Text, calTimeFrameStart.SelectedDate, calTimeFrameEnd.SelectedDate));
                    tempList.AddRange(HardwareLoanItemBL.GetByTicketCode(tbSearchItem.Text, calTimeFrameStart.SelectedDate, calTimeFrameEnd.SelectedDate));
                    gvItems1.DataSource = tempList.Distinct<HardwareLoanItemBL>().ToList();
                }
                else
                {
                    tempList.AddRange(HardwareLoanItemBL.GetByInventoryNumber(tbSearchItem.Text));
                    tempList.AddRange(HardwareLoanItemBL.GetByTicketCode(tbSearchItem.Text));
                    gvItems1.DataSource = tempList.Distinct<HardwareLoanItemBL>().ToList();
                }
                gvItems1.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems1);

                lblItems1Count.Text = gvItems1.Rows.Count.ToString();
                lblCategorySelection.Text = "You've searched for: " + tbSearchItem.Text;
                tbSearchItem.Text = "Or search by inventory number or ticket code";
                upResultBox.Update();
                upSelectedCategory.Update();
            }
        }

        #region Timeframe calendars

        protected void calTimeFrameStart_SelectionChanged(object sender, EventArgs e)
        {
            if (calTimeFrameEnd.SelectedDate.ToBinary() == 0)
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Yellow;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "Please select an end date.";
                imgbtnClearTimeFrame.Visible = false;
            }
            else if (calTimeFrameStart.SelectedDate.ToBinary() > 0 && calTimeFrameEnd.SelectedDate.ToBinary() > 0 && calTimeFrameEnd.SelectedDate < calTimeFrameStart.SelectedDate)
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Red;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "The end date must be bigger than the start date.";
                imgbtnClearTimeFrame.Visible = false;
            }
            else
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Green;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "Time frame set to: " + calTimeFrameStart.SelectedDate.ToShortDateString() + " -- " + calTimeFrameEnd.SelectedDate.ToShortDateString() + ".   ";
                imgbtnClearTimeFrame.Visible = true;
            }


            up1.Update();
        }

        protected void calTimeFrameStart_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            up1.Update();
        }

        protected void calTimeFrameEnd_SelectionChanged(object sender, EventArgs e)
        {
            if (calTimeFrameStart.SelectedDate.ToBinary() == 0)
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Yellow;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "Please select a start date.";
                imgbtnClearTimeFrame.Visible = false;
            }
            else if (calTimeFrameStart.SelectedDate.ToBinary() > 0 && calTimeFrameEnd.SelectedDate.ToBinary() > 0 && calTimeFrameEnd.SelectedDate < calTimeFrameStart.SelectedDate)
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Red;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "The end date must be bigger than the start date.";
                imgbtnClearTimeFrame.Visible = false;
            }
            else
            {
                lblTimeFrame.ForeColor = System.Drawing.Color.Green;
                lblTimeFrame.Font.Bold = true;
                lblTimeFrame.Text = "Time frame set to: " + calTimeFrameStart.SelectedDate.ToShortDateString() + " -- " + calTimeFrameEnd.SelectedDate.ToShortDateString() + ".   ";
                imgbtnClearTimeFrame.Visible = true;
            }
            up1.Update();
        }

        protected void calTimeFrameEnd_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            up1.Update();
        }

        #endregion

        protected void imgbtnClearTimeFrame_Click(object sender, ImageClickEventArgs e)
        {
            calTimeFrameEnd.SelectedDate = DateTime.FromBinary(0);
            calTimeFrameStart.SelectedDate = DateTime.FromBinary(0);
            lblTimeFrame.Text = "No time frame set.";
            lblTimeFrame.ForeColor = System.Drawing.Color.White;
            imgbtnClearTimeFrame.Visible = false;
            up1.Update();
        }

        protected void CategoryMenuItem_Click(object sender, EventArgs e)
        {
            lblCategorySelection.Text = "You have selected: " + ((LinkButton)sender).Text;
            lblCategorySelection.Visible = true;
            lblCategoryId.Text = ((LinkButton)sender).ID.Replace("Category_", "");

            upSelectedCategory.Update();


            int Id;
            if (int.TryParse(lblCategoryId.Text, out Id))
            {
                if (calTimeFrameEnd.SelectedDate.ToBinary() > 0 && calTimeFrameStart.SelectedDate.ToBinary() > 0)
                {
                    gvItems1.DataSource = HardwareLoanItemBL.GetByCategoryId(Id, calTimeFrameStart.SelectedDate, calTimeFrameEnd.SelectedDate);
                    gvItems1.DataBind();
                }
                else
                {
                    gvItems1.DataSource = HardwareLoanItemBL.GetByCategoryId(Id);
                    gvItems1.DataBind();
                }




                lblItems1Count.Text = gvItems1.Rows.Count.ToString();
                ((MasterPage)Master).SetGridviewProperties(gvItems1);

                upResultBox.Update();

            }

        }

        private void gvItems1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = gvItems1.Rows[int.Parse(e.CommandArgument.ToString())];
                int id = int.Parse(row.Cells[4].Text);

                HardwareLoanItemBL currentItem = new HardwareLoanItemBL();
                currentItem = HardwareLoanItemBL.GetByItemId(id).FirstOrDefault();

                lblSummaryIdValue.Text = currentItem.Id.ToString();
                lblSummaryDescriptionValue.Text = currentItem.Description;
                lblSummaryInventoryNbrValue.Text = currentItem.InventoryNumber;
                ddlCategory.Enabled = true;
                ddlCategory.SelectedValue = currentItem.CategoryId.ToString();
                ddlStatus.Enabled = true;
                ddlStatus.SelectedValue = currentItem.StatusId.ToString();

                gvItems2.DataSource = currentItem.Assignements;
                gvItems2.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                lblItems2Count.Text = gvItems2.Rows.Count.ToString();

                upAssignmentBox.Update();
                upInnerItemSummary.Update();

            }
        }

        #region Summary

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            HardwareLoanItemBL saveMe = new HardwareLoanItemBL();
            saveMe = HardwareLoanItemBL.GetByItemId(int.Parse(lblSummaryIdValue.Text)).FirstOrDefault();
            saveMe.CategoryId = int.Parse(ddlCategory.SelectedValue);
            ArrayList validationErrors = new ArrayList();
            saveMe.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
            ddlCategory.Enabled = true;
            ddlStatus.Enabled = true;
            if (validationErrors.Count > 0)
            {
                ddlCategory.SelectedValue = HardwareLoanItemBL.GetByItemId(int.Parse(lblSummaryIdValue.Text)).FirstOrDefault().CategoryId.ToString();
            }
            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            upItemSummary.Update();

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            HardwareLoanItemBL saveMe = new HardwareLoanItemBL();
            saveMe = HardwareLoanItemBL.GetByItemId(int.Parse(lblSummaryIdValue.Text)).FirstOrDefault();
            saveMe.StatusId = int.Parse(ddlStatus.SelectedValue);
            ArrayList validationErrors = new ArrayList();
            saveMe.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
            ddlCategory.Enabled = true;
            ddlStatus.Enabled = true;
            if (validationErrors.Count > 0)
            {
                ddlCategory.SelectedValue = HardwareLoanItemBL.GetByItemId(int.Parse(lblSummaryIdValue.Text)).FirstOrDefault().CategoryId.ToString();
            }
            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            upItemSummary.Update();
        }

        #endregion

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Utilities.MakeSelectable(gvItems1, Page);

            base.Render(writer);
        }

        protected void gvItems2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HardwareLoanAssignmentBL item = new HardwareLoanAssignmentBL();
            item = HardwareLoanAssignmentBL.GetById((int)e.Keys[0]);
            ArrayList validationErrors = new ArrayList();
            item.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);

            ((MasterPage)Master).ShowValidationMessages(validationErrors);
            List<HardwareLoanAssignmentBL> itemList = new List<HardwareLoanAssignmentBL>();

            itemList = HardwareLoanAssignmentBL.GetAllByItemId(int.Parse(lblSummaryIdValue.Text));
            gvItems2.DataSource = itemList;
            gvItems2.DataBind();
            ((MasterPage)Master).SetGridviewProperties(gvItems2);
            lblItems2Count.Text = gvItems2.Rows.Count.ToString();
            upAssignmentBox.Update();
        }

        protected void imgbtnAddAssignment_Click(object sender, ImageClickEventArgs e)
        {
            lblAddAssignment.Text = "";
            if (calAssignmentEnd.SelectedDate.ToBinary() == 0)
            {
                lblAddAssignment.ForeColor = System.Drawing.Color.Red;
                lblAddAssignment.Text += "Select an end date.<br>";
            }
            if (calAssignmentStart.SelectedDate.ToBinary() == 0)
            {
                lblAddAssignment.ForeColor = System.Drawing.Color.Red;
                lblAddAssignment.Text += "Select a start date.<br>";
            }
            if (tbTicketCode.Text.Trim(' ') == "")
            {
                lblAddAssignment.ForeColor = System.Drawing.Color.Red;
                lblAddAssignment.Text += "Enter a ticket code.<br>";
            }
            if ((calAssignmentEnd.SelectedDate < calAssignmentStart.SelectedDate) && calAssignmentEnd.SelectedDate.ToBinary() > 0)
            {
                lblAddAssignment.ForeColor = System.Drawing.Color.Red;
                lblAddAssignment.Text += "The end date must be greater than or equal to the start date.<br>";
            }


            if ((calAssignmentEnd.SelectedDate >= calAssignmentStart.SelectedDate) && (calAssignmentStart.SelectedDate.ToBinary() > 0) && (tbTicketCode.Text.Trim(' ') != ""))
            {
                ArrayList validationErrors = new ArrayList();
                HardwareLoanAssignmentBL saveMe = new HardwareLoanAssignmentBL();
                saveMe.DateEnd = calAssignmentEnd.SelectedDate;
                saveMe.DateStart = calAssignmentStart.SelectedDate;
                saveMe.TicketCode = tbTicketCode.Text;
                saveMe.ItemId = int.Parse(lblSummaryIdValue.Text);

                saveMe.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                lblAddAssignment.ForeColor = System.Drawing.Color.Green;
                lblAddAssignment.Text += "Assignment saved!<br>";

                List<HardwareLoanAssignmentBL> itemList = new List<HardwareLoanAssignmentBL>();

                itemList = HardwareLoanAssignmentBL.GetAllByItemId(int.Parse(lblSummaryIdValue.Text));
                gvItems2.DataSource = itemList;
                gvItems2.DataBind();
                ((MasterPage)Master).SetGridviewProperties(gvItems2);
                lblItems2Count.Text = gvItems2.Rows.Count.ToString();
                upAssignmentBox.Update();

            }
            upAddAssignment.Update();
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            ExportAssignment();
        }

        #region Assignment calendars

        protected void calAssignmentStart_SelectionChanged(object sender, EventArgs e)
        {
            upAddAssignment.Update();
        }

        protected void calAssignmentStart_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            upAddAssignment.Update();
        }

        protected void calAssignmentEnd_SelectionChanged(object sender, EventArgs e)
        {
            upAddAssignment.Update();
        }

        protected void calAssignmentEnd_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            upAddAssignment.Update();
        }

        #endregion
        
        #endregion

        #region Private Methods

        private bool DatesOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (end1.Ticks >= start2.Ticks) && (end2.Ticks >= start1.Ticks);
        }

        private List<DateTime> GetDays()
        {
            List<DateTime> itemList = new List<DateTime>();

            DateTime shortDate = new DateTime();
            shortDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime firstDate = new DateTime();
            firstDate = shortDate.AddDays(-shortDate.Day + 1);
            DateTime lastDay = new DateTime();
            lastDay = firstDate.AddMonths(2).AddDays(-1);
            TimeSpan ts = lastDay - firstDate;

            for (int i = 0; i <= ts.Days; i++)
            {
                itemList.Add(firstDate.AddDays(i));
            }


            return itemList;

        }

        private void ExportAssignment()
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

            //Green fill
            DocumentFormat.OpenXml.Spreadsheet.Fill fill3 = new DocumentFormat.OpenXml.Spreadsheet.Fill();
            DocumentFormat.OpenXml.Spreadsheet.PatternFill patternFill3 = new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid };
            DocumentFormat.OpenXml.Spreadsheet.BackgroundColor backgroundColor1 = new DocumentFormat.OpenXml.Spreadsheet.BackgroundColor() { };
            //BackgroundColor is always ignored in favor of ForegroundColor
            DocumentFormat.OpenXml.Spreadsheet.ForegroundColor foregroundColor1 = new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Indexed = (UInt32Value)57U };


            patternFill3.AppendChild(foregroundColor1);
            patternFill3.AppendChild(backgroundColor1);
            //Foreground must be appended before background
            //Foreground and background must be childs of patternfill and not attributes

            fill3.AppendChild(patternFill3);

            //Red fill
            DocumentFormat.OpenXml.Spreadsheet.Fill fill4 = new DocumentFormat.OpenXml.Spreadsheet.Fill();
            DocumentFormat.OpenXml.Spreadsheet.PatternFill patternFill4 = new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid };
            DocumentFormat.OpenXml.Spreadsheet.BackgroundColor backgroundColor2 = new DocumentFormat.OpenXml.Spreadsheet.BackgroundColor() { };
            //BackgroundColor is always ignored in favor of ForegroundColor
            DocumentFormat.OpenXml.Spreadsheet.ForegroundColor foregroundColor2 = new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor() { Indexed = (UInt32Value)10U };


            patternFill4.AppendChild(foregroundColor2);
            patternFill4.AppendChild(backgroundColor2);
            //Foreground must be appended before background
            //Foreground and background must be childs of patternfill and not attributes

            fill4.AppendChild(patternFill4);





            fills.AppendChild(fill1);
            fills.AppendChild(fill2);
            fills.AppendChild(fill3);
            fills.AppendChild(fill4);
            //Order matters! First and second childs are fixed, only childs starting from the third are user-defined!

            fills.Count = (UInt32Value)4U;
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

            leftborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 64U };
            leftborder2.Append(borderColor);

            rightborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 64U };
            rightborder2.Append(borderColor);

            topborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 64U };
            topborder2.Append(borderColor);

            bottomborder2.Style = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            borderColor = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = 64U };
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

            #region Default format (0U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormats cellFormats = new DocumentFormat.OpenXml.Spreadsheet.CellFormats();
            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat1 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat1.FormatId = 0U;
            cellFormat1.NumberFormatId = 0U;
            cellFormat1.FontId = 0U;
            cellFormat1.BorderId = 1U;
            cellFormat1.FillId = 0U;

            cellFormats.Append(cellFormat1);
            //The first appended DocumentFormat.OpenXml.Spreadsheet.CellFormat is the default cell format. It applies to all cells not having a specified cell format
            cellFormats.Count = 1U;
            //Seems to be useless

            #endregion

            #region Header format (1U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat2 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat2.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom | 14 = d/mm/yyyy custom
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

            #region Date (d/mm/yyyy) format (2U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat3 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat3.NumberFormatId = 14U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom | 14 = d/mm/yyyy custom
            cellFormat3.FontId = 0U;
            cellFormat3.BorderId = 1U;
            cellFormat3.FillId = 0U;
            cellFormat3.ApplyFill = false;
            cellFormat3.ApplyFont = false;
            cellFormat3.ApplyBorder = true;
            cellFormat3.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat3);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 3U;
            //Seems to be useless

            #endregion

            #region Bold format (3U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat4 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat4.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat4.FontId = 2U;
            cellFormat4.BorderId = 1U;
            cellFormat4.FillId = 0U;
            cellFormat4.ApplyFill = false;
            cellFormat4.ApplyFont = true;
            cellFormat4.ApplyBorder = true;
            cellFormat4.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat4);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 4U;
            //Seems to be useless

            #endregion

            #region Red BG format (4U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat5 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat5.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat5.FontId = 2U;
            cellFormat5.BorderId = 1U;
            cellFormat5.FillId = 3U;
            cellFormat5.ApplyFill = true;
            cellFormat5.ApplyFont = true;
            cellFormat5.ApplyBorder = true;
            cellFormat5.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat5);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 5U;
            //Seems to be useless

            #endregion

            #region Green BG format (5U)

            DocumentFormat.OpenXml.Spreadsheet.CellFormat cellFormat6 = new DocumentFormat.OpenXml.Spreadsheet.CellFormat();
            cellFormat6.NumberFormatId = 0U;
            //Does not seem to require DocumentFormat.OpenXml.Spreadsheet.NumberingFormat elements for the basic formats. 17 = mmm-yy custom
            cellFormat6.FontId = 2U;
            cellFormat6.BorderId = 1U;
            cellFormat6.FillId = 2U;
            cellFormat6.ApplyFill = true;
            cellFormat6.ApplyFont = true;
            cellFormat6.ApplyBorder = true;
            cellFormat6.ApplyNumberFormat = true;
            //ApplyX does not seem to have any effect

            cellFormats.Append(cellFormat6);
            //The format is applied to only one cell starting from the second appended DocumentFormat.OpenXml.Spreadsheet.CellFormat
            cellFormats.Count = 6U;
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

            #region Data

            #region Active hardware

            DocumentFormat.OpenXml.Packaging.WorksheetPart worksheetPart;
            DocumentFormat.OpenXml.Spreadsheet.Sheet sheet;
            DocumentFormat.OpenXml.Spreadsheet.Worksheet worksheet;
            DocumentFormat.OpenXml.Spreadsheet.SheetData sheetData;
            DocumentFormat.OpenXml.Spreadsheet.Row row;
            DocumentFormat.OpenXml.Spreadsheet.Cell cell;

            HardwareLoanCategoryBL.GetAll().ForEach(category =>
            {
                worksheetPart = spreadSheetDocument.WorkbookPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WorksheetPart>();
                sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet();
                sheet.Name = category.Name;
                sheet.SheetId = (uint)spreadSheetDocument.WorkbookPart.Workbook.Sheets.Count() + 1;
                sheet.Id = spreadSheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
                spreadSheetDocument.WorkbookPart.Workbook.Sheets.AppendChild(sheet);
                worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();
                worksheetPart.Worksheet = worksheet;
                sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                worksheet.AppendChild(sheetData);



                //Add the titles
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Model");
                cell.StyleIndex = 3U;
                row.AppendChild(cell);

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Inventory number");
                cell.StyleIndex = 3U;
                row.AppendChild(cell);

                //Add the date headers

                GetDays().ForEach(day =>
                {
                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Date;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(day.ToOADate().ToString());
                    cell.StyleIndex = 2U;
                    row.AppendChild(cell);
                });

                sheetData.AppendChild(row);

                //Add the hardware
                HardwareLoanItemBL.GetByCategoryId(category.Id).Where(item => item.Status == "Available" && item.IsPermaLoan == false).ToList().ForEach(item =>
                {
                    row = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.Description);
                    cell.StyleIndex = 0U;
                    row.AppendChild(cell);

                    cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(item.InventoryNumber);
                    cell.StyleIndex = 0U;
                    row.AppendChild(cell);

                    string ticketcode;
                    GetDays().ForEach(day =>
                    {
                        if (item.Assignements.Any(x => DatesOverlap(x.DateStart, x.DateEnd, day, day)))
                        {
                            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            ticketcode = "";
                            item.Assignements.Where(x => DatesOverlap(x.DateStart, x.DateEnd, day, day)).ToList().ForEach(x =>
                            {
                                ticketcode += x.TicketCode + " ";
                            });
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(ticketcode);
                            cell.StyleIndex = 4U;
                            row.AppendChild(cell);
                        }
                        else
                        {
                            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cell.StyleIndex = 5U;
                            row.AppendChild(cell);
                        }
                    });



                    sheetData.AppendChild(row);
                });


            });

            #endregion

            #region Unavailable hardware

            worksheetPart = spreadSheetDocument.WorkbookPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WorksheetPart>();
            sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet();
            sheet.Name = "Unavailable";
            sheet.SheetId = (uint)spreadSheetDocument.WorkbookPart.Workbook.Sheets.Count() + 1;
            sheet.Id = spreadSheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
            spreadSheetDocument.WorkbookPart.Workbook.Sheets.AppendChild(sheet);
            worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();
            worksheetPart.Worksheet = worksheet;
            sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
            worksheet.AppendChild(sheetData);

            row = new DocumentFormat.OpenXml.Spreadsheet.Row();

            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Model");
            cell.StyleIndex = 3U;
            row.AppendChild(cell);

            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Inventory number");
            cell.StyleIndex = 3U;
            row.AppendChild(cell);

            sheetData.AppendChild(row);

            HardwareLoanItemBL.GetByStatus("Unavailable").ForEach(x =>
            {
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.Description);
                cell.StyleIndex = 0U;
                row.AppendChild(cell);

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.InventoryNumber);
                cell.StyleIndex = 0U;
                row.AppendChild(cell);

                sheetData.AppendChild(row);
            });

            #endregion

            #region Permaloan hardware

            worksheetPart = spreadSheetDocument.WorkbookPart.AddNewPart<DocumentFormat.OpenXml.Packaging.WorksheetPart>();
            sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet();
            sheet.Name = "Permaloan";
            sheet.SheetId = (uint)spreadSheetDocument.WorkbookPart.Workbook.Sheets.Count() + 1;
            sheet.Id = spreadSheetDocument.WorkbookPart.GetIdOfPart(worksheetPart);
            spreadSheetDocument.WorkbookPart.Workbook.Sheets.AppendChild(sheet);
            worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();
            worksheetPart.Worksheet = worksheet;
            sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
            worksheet.AppendChild(sheetData);

            row = new DocumentFormat.OpenXml.Spreadsheet.Row();

            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Model");
            cell.StyleIndex = 3U;
            row.AppendChild(cell);

            cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("Inventory number");
            cell.StyleIndex = 3U;
            row.AppendChild(cell);

            sheetData.AppendChild(row);

            HardwareLoanItemBL.GetByisPermaloan(true).ForEach(x =>
            {
                row = new DocumentFormat.OpenXml.Spreadsheet.Row();

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.Description);
                cell.StyleIndex = 0U;
                row.AppendChild(cell);

                cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(x.InventoryNumber);
                cell.StyleIndex = 0U;
                row.AppendChild(cell);

                sheetData.AppendChild(row);
            });

            #endregion


            #endregion

            #region Finally

            spreadSheetDocument.Close();
            spreadSheetDocument.Dispose();

            if (memoryStream != null)
            {
                Byte[] byteArray = memoryStream.ToArray();
                memoryStream.Flush();
                memoryStream.Close();
                Response.BufferOutput = true;
                // Clear all content output from the buffer stream
                Response.Clear();
                //to fix the “file not found” error when opening excel file
                //See http://www.aspose.com/Community/forums/ShowThread.aspx?PostID=61444
                Response.ClearHeaders();
                // Add a HTTP header to the output stream that specifies the default filename
                // for the browser’s download dialog
                string timeStamp = Convert.ToString(DateTime.Now.ToString("MMddyyyy_HHmmss"));
                Response.AddHeader("Content-Disposition", "attachment; filename=GLoan_" + timeStamp + ".xlsx");
                // Set the HTTP MIME type of the output stream
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Write the data
                Response.BinaryWrite(byteArray);
                Response.End();
            }

            memoryStream.Close();
            memoryStream.Dispose();
            GC.Collect();

            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            #endregion
        }

        #endregion
    }
}