using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr.Services
{
    public partial class FerretCreate : System.Web.UI.Page
    {
        #region Properties 

        public List<FerretCheckBL> SelectedFerretChecks
        {
            get
            {
                if (Equals(Session["SelectedFerretChecks"], null))
                {
                    Session["SelectedFerretChecks"] = new List<FerretCheckBL>();
                }
                return (List<FerretCheckBL>)Session["SelectedFerretChecks"];
            }
            set { Session["SelectedFerretChecks"] = value; }
        }

        #endregion

        #region Event Handlers




        protected void Page_Load(object sender, EventArgs e)
        {
            ((MasterPage)Master).ActionEvent += new ActionClickHandler(Action_Click);

            if (!Page.IsPostBack)
            {
                lbGroups.DataSource = FerretExtComputerGroupBL.GetAll();
                lbGroups.DataTextField = "Name";
                lbGroups.DataValueField = "Id";
                lbGroups.DataBind();

                ((MasterPage)Master).EnableAction(Action.Save);
                ((MasterPage)Master).EnableAction(Action.Clear);
            }
            else
            {
                ((MasterPage)Master).ClearErrorMessages();
                ArrayList validationErrors = new ArrayList();
                ((MasterPage)Master).ShowValidationMessages(validationErrors);
            }



        }

        protected void Action_Click(object sender, EventArgs e)
        {
            if (((MasterPage)Master).CurrentAction == Action.Save)
            {

                ArrayList validationErrors = new ArrayList();
                
                //Update FerretComputers
                List<FerretExtComputerBL> SelectedExtComputers = new List<FerretExtComputerBL>();

                foreach (ListItem item in lbToSave.Items)
                {
                    SelectedExtComputers.AddRange(FerretExtComputerBL.GetByGroupId(int.Parse(item.Value)));
                }

                SelectedExtComputers = SelectedExtComputers.Distinct().ToList();

                List<FerretComputerBL> FerretComputerList = new List<FerretComputerBL>();

                FerretComputerList = FerretComputerBL.GetAll();
                FerretComputerBL FerretComputer;

                SelectedExtComputers.Where(x => !FerretComputerList.Select(y => y.Name).Contains(x.Name)).ToList().ForEach(x =>
                {
                    FerretComputer = new FerretComputerBL() { Name = x.Name };
                    FerretComputer.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                });

                //Save script
                FerretScriptBL newScript = new FerretScriptBL();
                newScript.Description = tbScriptName.Text;
                newScript.IsEnabled = cbScriptEnabled.Checked;

                newScript.Save(ref validationErrors, ((MasterPage)Master).UserLogin);

                //Retrieve FerretComputers & Save checks
                FerretComputerList = FerretComputerBL.GetAll();

                FerretComputerList.Where(x => SelectedExtComputers.Select(y => y.Name).Contains(x.Name)).ToList().ForEach(x => {
                    SelectedFerretChecks.ForEach(y => 
                    {
                        y.FerretComputerId = x.Id;
                        y.FerretScriptId = newScript.Id;
                        y.CreationDate = DateTime.Now;
                        y.Save(ref validationErrors, ((MasterPage)Master).UserLogin);
                    });
                });
               
                
               
                


            }
        }

        protected void imgbtnLeft_Click(object sender, ImageClickEventArgs e)
        {
            List<ListItem> toRemove = new List<ListItem>();

            lbToSave.ClearSelection();

            foreach (ListItem item in lbGroups.Items)
            {
                if (item.Selected)
                {
                    lbToSave.Items.Add(item);
                    toRemove.Add(item);

                }
            }
            toRemove.ForEach(x =>
            {
                lbGroups.Items.Remove(x);
            });

            toRemove.Clear();

            upListBoxes.Update();



        }

        protected void imgbtnRight_Click(object sender, ImageClickEventArgs e)
        {
            List<ListItem> toRemove = new List<ListItem>();

            lbGroups.ClearSelection();

            foreach (ListItem item in lbToSave.Items)
            {
                if (item.Selected)
                {
                    lbGroups.Items.Add(item);
                    toRemove.Add(item);

                }
            }
            toRemove.ForEach(x =>
            {
                lbToSave.Items.Remove(x);
            });


            toRemove.Clear();

            upListBoxes.Update();

        }

        #endregion

        #region Private Methods

        private void UpdateFerretComputers()
        {
            
            
            
            
        }

        #endregion

        protected void imgbtnAddCheck_Click(object sender, ImageClickEventArgs e)
        {
            SelectedFerretChecks.Add(new FerretCheckBL() { KeyName = tbKeyName.Text, Path = tbPath.Text, Type = ddlType.SelectedValue, Value = tbValue.Text });
            gvItems1.DataSource = SelectedFerretChecks;
            gvItems1.DataBind();
            upChecks.Update();            
        }

        protected void gvItems1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SelectedFerretChecks.RemoveAt(e.RowIndex);
            gvItems1.DataSource = SelectedFerretChecks;
            gvItems1.DataBind();
            upChecks.Update();

         
        }
    }
}