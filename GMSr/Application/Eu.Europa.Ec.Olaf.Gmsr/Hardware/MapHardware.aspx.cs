using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Eu.Europa.Ec.Olaf.GmsrBLL;
using System.Web.UI.HtmlControls;
using System.Transactions;

namespace Eu.Europa.Ec.Olaf.Gmsr.Hardware
{
    public partial class MapHardware : BasePage
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click); // to handle the Action buttons

            if (!IsPostBack)
            {
                #region enable relevant Action buttons

                ((MasterPage)Master).EnableAction(Action.Save);
                ((MasterPage)Master).EnableAction(Action.Clear);

                #endregion

                SetFormValues();
            }
        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {
                // get the id out of the selected text value
                string str = txtOrder.Text;
                int startIndex = str.IndexOf("| (") + 3;
                int length = str.IndexOf(')', startIndex < 3 ? 0 : startIndex) - startIndex;
                int orderId;

                if (txtOrder.Text != "" && startIndex >= 0 && length > 0 && int.TryParse(str.Substring(startIndex, length), out orderId))
                {
                    ArrayList validationErrors = new ArrayList();

                    using (TransactionScope transaction = new TransactionScope())
                    {
                        // remove the previously linked items
                        List<HardwareItemOrderFormBL> previousItemBL_List = HardwareItemOrderFormBL.GetByOrderForm(orderId);
                        previousItemBL_List.ForEach(item =>
                        {
                            item.Delete(ref validationErrors, ((MasterPage)Master).UserLogin);
                        });

                        // save the currently linked items
                        List<HardwareItemOrderFormBL> itemList = GetFormValues();
                        itemList.ForEach(item =>
                        {
                            item.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                        });

                        transaction.Complete();
                    }

                    ((MasterPage)Master).ShowValidationMessages(validationErrors);

                    if (validationErrors.Count == 0)
                    {
                        SetFormValues();
                    }
                }

            }
            else if (((MasterPage)Master).CurrentAction == Action.Clear)
            {
                ((MasterPage)Master).ClearErrorMessages();

                SetFormValues();
            }
        }

        protected void lbUnlinked_PreRender(object sender, EventArgs e)
        {
            Utilities.AttachBackColors(lbUnlinked);
        }

        protected void lbLinked_PreRender(object sender, EventArgs e)
        {
            Utilities.AttachBackColors(lbLinked);
        }

        protected void ibtnLink_Click(object sender, ImageClickEventArgs e)
        {
            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem item in lbUnlinked.Items)
            {
                if (item.Selected)
                {
                    lbLinked.Items.Add(new ListItem(item.Text, item.Value));
                    itemsToRemove.Add(item);
                }
            }

            itemsToRemove.ForEach(delegate(ListItem itemToRemove)
            {
                lbUnlinked.Items.Remove(itemToRemove);
            });

            ((MasterPage)Master).ClearErrorMessages();
        }

        protected void ibtnUnlink_Click(object sender, EventArgs e)
        {
            List<ListItem> itemsToRemove = new List<ListItem>();

            foreach (ListItem item in lbLinked.Items)
            {
                if (item.Selected)
                {
                    lbUnlinked.Items.Add(new ListItem(item.Text, item.Value));
                    itemsToRemove.Add(item);
                }
            }
            itemsToRemove.ForEach(delegate(ListItem itemToRemove)
            {
                lbLinked.Items.Remove(itemToRemove);
            });

            ((MasterPage)Master).ClearErrorMessages();
        }

        protected void txtOrder_TextChanged(object sender, EventArgs e)
        {
            // get the id out of the selected text value
            string str = txtOrder.Text;
            int startIndex = str.IndexOf("| (") + 3;
            int length = str.IndexOf(')', startIndex < 3 ? 0 : startIndex) - startIndex;
            int orderId;

            if (txtOrder.Text != "" && startIndex >= 0 && length > 0 && int.TryParse(str.Substring(startIndex, length), out orderId))
            {
                ibtnLink.Enabled = true;
                ibtnLink.ImageUrl = "~/Images/ArrowUp.png";
                ibtnUnlink.Enabled = true;
                ibtnUnlink.ImageUrl = "~/Images/ArrowDown.png";

                LoadLbLinked(orderId);
            }
            else
            {
                ibtnLink.Enabled = false;
                ibtnLink.ImageUrl = "~/Images/ArrowUpGray.png";
                ibtnUnlink.Enabled = false;
                ibtnUnlink.ImageUrl = "~/Images/ArrowDownGray.png";

                LoadLbLinked(0);
            }

            ((MasterPage)Master).ClearErrorMessages();
        }

        protected void txtUnlinked_TextChanged(object sender, EventArgs e)
        {
            lbUnlinked.ClearSelection();

            if (txtUnlinked.Text != "")
            {
                foreach (ListItem item in lbUnlinked.Items)
                {
                    if (item.Text == txtUnlinked.Text)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            MasterPage masterPage = Master as MasterPage;
            masterPage.ClearErrorMessages();
        }

        #endregion

        #region Public Methods

        [System.Web.Services.WebMethod]
        public static string[] GetOrderCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<OrderFormBL> itemList = OrderFormBL.GetByPrefix(prefixText);

            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        [System.Web.Services.WebMethod]
        public static string[] GetUnlinkedCompletionList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            List<string> completionList = new List<string>(count);

            List<HardwareItemBL> itemList = HardwareItemBL.GetNotLinkedWithOrderByPrefix(prefixText);
            itemList.ForEach(item =>
            {
                completionList.Add(item.Concatenation);
            });

            completionList.Sort();

            return completionList.ToArray();
        }

        #endregion

        #region Private Methods

        private void SetFormValues()
        {
            txtOrder.Text = "";

            LoadLbUnlinked();
            txtUnlinked.Text = "";

            LoadLbLinked(0);

            ibtnLink.Enabled = false;
            ibtnLink.ImageUrl = "~/Images/ArrowUpGray.png";
            ibtnUnlink.Enabled = false;
            ibtnUnlink.ImageUrl = "~/Images/ArrowDownGray.png";
        }

        private List<HardwareItemOrderFormBL> GetFormValues()
        {
            List<HardwareItemOrderFormBL> itemList = new List<HardwareItemOrderFormBL>();

            // get the id out of the selected text value
            string str = txtOrder.Text;
            int startIndex = str.IndexOf("| (") + 3;
            int length = str.IndexOf(')', startIndex < 3 ? 0 : startIndex) - startIndex;
            int orderId;

            if (txtOrder.Text != "" && startIndex >= 0 && length > 0 && int.TryParse(str.Substring(startIndex, length), out orderId))
            {
                HardwareItemOrderFormBL item;
                foreach (ListItem listItem in lbLinked.Items)
                {
                    item = new HardwareItemOrderFormBL();

                    item.OrderFormId = orderId;
                    item.HardwareItemId = int.Parse(listItem.Value);

                    itemList.Add(item);
                }
            }

            return itemList;
        }

        private void LoadLbLinked(int orderId)
        {
            lbLinked.DataSource = HardwareItemBL.GetByOrderForm(orderId);

            lbLinked.DataTextField = "Concatenation";
            lbLinked.DataValueField = "Id";

            lbLinked.DataBind();
        }

        private void LoadLbUnlinked()
        {
            lbUnlinked.DataSource = HardwareItemBL.GetNotLinkedWithOrder();

            lbUnlinked.DataTextField = "Concatenation";
            lbUnlinked.DataValueField = "Id";

            lbUnlinked.DataBind();
        }

        #endregion
    }
}