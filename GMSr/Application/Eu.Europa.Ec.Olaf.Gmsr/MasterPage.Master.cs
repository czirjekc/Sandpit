using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Eu.Europa.Ec.Olaf.GmsrBLL;

namespace Eu.Europa.Ec.Olaf.Gmsr
{
    #region Enumerations

    public enum Action
    {
        Back,
        Forward,
        Save,        
        Search,
        Clear,
        Help
    }

    #endregion

    #region Delegates

    public delegate void ActionClickHandler(object sender, EventArgs e);

    #endregion

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Properties

        public string UserLogin
        {
            get
            {
                if (Equals(Session[Constants.USER_LOGIN], null))
                {
                    Session[Constants.USER_LOGIN] = "";
                }
                return (string)Session[Constants.USER_LOGIN];
            }
            set { Session[Constants.USER_LOGIN] = value; }
        }

        public string UserName
        {
            get
            {
                if (Equals(Session[Constants.USER_NAME], null))
                {
                    Session[Constants.USER_NAME] = "";
                }
                return (string)Session[Constants.USER_NAME];
            }
            set { Session[Constants.USER_NAME] = value; }
        }

        public MenuItem MenuRoot
        {
            get
            {
                if (Equals(Session[Constants.MENU_ROOT], null))
                {
                    Session[Constants.MENU_ROOT] = new MenuItem();
                }
                return (MenuItem)Session[Constants.MENU_ROOT];
            }
            set { Session[Constants.MENU_ROOT] = value; }
        }

        public string SitePath
        {
            get
            {
                if (Equals(Session[Constants.SITE_PATH], null))
                {
                    Session[Constants.SITE_PATH] = "";
                }
                return (string)Session[Constants.SITE_PATH];
            }
            set { Session[Constants.SITE_PATH] = value; }
        }
        
        public List<PageBL> AdditionalPageList
        {
            get
            {
                if (Equals(Session[Constants.ADDITIONAL_PAGE_LIST], null))
                {
                    Session[Constants.ADDITIONAL_PAGE_LIST] = new List<PageBL>();
                }
                return (List<PageBL>)Session[Constants.ADDITIONAL_PAGE_LIST];
            }
            set { Session[Constants.ADDITIONAL_PAGE_LIST] = value; }
        }

        public Action CurrentAction { get; set; }

        #endregion

        #region Events

        public event ActionClickHandler ActionEvent;

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserLogin == "") // Session Start
                {
                    #region Authentication

                    #region Determine the user login name

                    //keep only the login part after the last '\'
                    string login = Context.User.Identity.Name;
                    int index = login.IndexOf('\\');
                    while (index >= 0)
                    {
                        login = login.Remove(0, index + 1);
                        index = login.IndexOf('\\');
                    }
                    UserLogin = login;

                    #endregion

                    UserBL user = null;

                    #region Only for development (place this part in comment before deployment)

                    //bool inDevelopment = true;
                    //if (inDevelopment)
                    //{
                    //    user = new UserBL(-1, "developer", "Frederik", "Developer","", false);
                    //    //user = new UserBL(38, "vanhafk", "Frederik", "Vanhauwaert");
                    //}

                    #endregion

                    if (user == null)
                    {
                        user = new UserBL();
                        if (!user.SelectByUser(login)) //if the login does not exist for GMSr
                        {
                            Response.Redirect("~/AccessDenied.htm");
                        }
                    }

                    UserName = user.FullName;

                    SessionBL currentSession = new SessionBL(Session.SessionID, login, user.FullName, DateTime.Now);
                    Application[Session.SessionID] = currentSession; // To keep track of who has open sessions (works only for in-process session state!)
                    currentSession.Save();

                    #endregion

                    #region Build Menu based on authentication

                    List<MenuElementBL> menuElementList = MenuElementBL.GetFullByUser(user.Id);

                    MenuItem root = new MenuItem("Start", " ", null, "~/Default.aspx");

                    menuElementList.ForEach(delegate(MenuElementBL element)
                    {
                        //check the level of the menu element
                        if (element.Location % 1000000 == 0) // level 1
                        {
                            root.ChildItems.Add(new MenuItem(element.Name, " ► " + element.Name, null, element.Url));
                        }
                        else if (element.Location % 10000 == 0) // level 2
                        {
                            int lastIndex1 = root.ChildItems.Count - 1;
                            root.ChildItems[lastIndex1].ChildItems.Add(new MenuItem(element.Name, " ► " + element.Name, null, element.Url));
                        }
                        else if (element.Location % 100 == 0) // level 3
                        {
                            int lastIndex1 = root.ChildItems.Count - 1;
                            int lastIndex2 = root.ChildItems[lastIndex1].ChildItems.Count - 1;
                            root.ChildItems[lastIndex1].ChildItems[lastIndex2].ChildItems.Add(new MenuItem(element.Name, " ► " + element.Name, null, element.Url));
                        }
                        else // level 4
                        {
                            int lastIndex1 = root.ChildItems.Count - 1;
                            int lastIndex2 = root.ChildItems[lastIndex1].ChildItems.Count - 1;
                            int lastIndex3 = root.ChildItems[lastIndex1].ChildItems[lastIndex2].ChildItems.Count - 1;
                            root.ChildItems[lastIndex1].ChildItems[lastIndex2].ChildItems[lastIndex3].ChildItems.Add(new MenuItem(element.Name, " ► " + element.Name, null, element.Url));
                        }
                    });

                    #region Make the menu item containers unselectable and remove the empty menu item containers

                    int maxU = root.ChildItems.Count - 1;
                    for (int u = maxU; u >= 0; u--) // level 1
                    {
                        int maxV = root.ChildItems[u].ChildItems.Count - 1;
                        for (int v = maxV; v >= 0; v--) // level 2
                        {
                            int maxW = root.ChildItems[u].ChildItems[v].ChildItems.Count - 1;
                            for (int w = maxW; w >= 0; w--) // level 3
                            {
                                if (root.ChildItems[u].ChildItems[v].ChildItems[w].NavigateUrl == "")
                                {
                                    if (root.ChildItems[u].ChildItems[v].ChildItems[w].ChildItems.Count == 0)
                                        root.ChildItems[u].ChildItems[v].ChildItems.RemoveAt(w);
                                    else
                                        root.ChildItems[u].ChildItems[v].ChildItems[w].Selectable = false;
                                }
                            }
                            if (root.ChildItems[u].ChildItems[v].NavigateUrl == "")
                            {
                                if (root.ChildItems[u].ChildItems[v].ChildItems.Count == 0)
                                    root.ChildItems[u].ChildItems.RemoveAt(v);
                                else
                                    root.ChildItems[u].ChildItems[v].Selectable = false;
                            }
                        }
                        if (root.ChildItems[u].NavigateUrl == "")
                        {
                            if (root.ChildItems[u].ChildItems.Count == 0)
                                root.ChildItems.RemoveAt(u);
                            else
                                root.ChildItems[u].Selectable = false;
                        }
                    }

                    #endregion

                    MenuRoot = root;

                    #endregion
                }

                lblUser.Text = UserName;

                mMaster.Items.Add(MenuRoot);

                #region Authorization

                #region Determine relative url

                //keep only the login part after the third '/'
                string url = Request.Url.ToString();

                int i = url.IndexOf('/');
                int j = 0;
                while (i >= 0 && j < 3)
                {
                    url = url.Remove(0, i + 1);
                    i = url.IndexOf('/');
                    j++;
                }
                url = "~/" + url;

                #endregion

                string sitePath = DetermineSitePath(url);
                
                if (sitePath == "")
                {
                    Response.Redirect("~/AccessDenied.htm");
                }
                else
                {
                    SitePath = sitePath;
                    lblPath.Text = sitePath;
                }

                #endregion
            }                 
        }

        protected void ibtnActionBack_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Back;
            ActionEvent(this, e);
        }

        protected void ibtnActionForward_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Forward;
            ActionEvent(this, e);
        }

        protected void ibtnActionSave_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Save;
            ActionEvent(this, e);
        }      

        protected void ibtnActionSearch_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Search;            
            ActionEvent(this, e);            
        }

        protected void ibtnActionClear_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Clear;
            ActionEvent(this, e);
        }

        protected void ibtnActionHelp_Click(object sender, ImageClickEventArgs e)
        {
            CurrentAction = Action.Help;
            ActionEvent(this, e);
        }

        #endregion

        #region Public Methods

        public void ShowValidationMessages(ArrayList validationErrors)
        {
            if (validationErrors.Count > 0)
            {
                string errorMessages = "<br />";
                foreach (string message in validationErrors)
                {
                    errorMessages += message + "<br />";
                }

                SetErrorMessages(errorMessages);
            }
            else
            {
                ClearErrorMessages();
            }
        }

        public void SetErrorMessages(string errorMessages)
        {
            lblErrorMessages.Text = errorMessages;            
            lblErrorMessages.Visible = true;
        }

        public void ClearErrorMessages()
        {            
            lblErrorMessages.Visible = false;
        }

        public void SetGridviewProperties(GridView gv)
        {
            if (gv.HeaderRow != null)
            {
                gv.HeaderRow.Attributes["style"] = "display:none";
                gv.UseAccessibleHeader = true;
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        
        public void EnableAction(Action action)
        {
            if (action == Action.Back)
            {
                ibtnActionBack.ImageUrl = "~/Images/ActionBack.png";
                ibtnActionBack.Enabled = true;
            }
            else if (action == Action.Forward)
            {
                ibtnActionForward.ImageUrl = "~/Images/ActionForward.png";
                ibtnActionForward.Enabled = true;
            }
            else if (action == Action.Save)
            {
                ibtnActionSave.ImageUrl = "~/Images/ActionSave.png";
                ibtnActionSave.Enabled = true;
            }
            else if (action == Action.Search)
            {
                ibtnActionSearch.ImageUrl = "~/Images/ActionSearch.png";
                ibtnActionSearch.Enabled = true;
            }
            else if (action == Action.Clear)
            {
                ibtnActionClear.ImageUrl = "~/Images/ActionClear.png";
                ibtnActionClear.Enabled = true;
            }
            else if (action == Action.Help)
            {
                ibtnActionHelp.ImageUrl = "~/Images/ActionHelp.png";
                ibtnActionHelp.Enabled = true;
            }
        }

        public void DisableAction(Action action)
        {
            if (action == Action.Back)
            {
                ibtnActionBack.ImageUrl = "~/Images/ActionBackGray.png";
                ibtnActionBack.Enabled = false;
            }
            else if (action == Action.Forward)
            {
                ibtnActionForward.ImageUrl = "~/Images/ActionForwardGray.png";
                ibtnActionForward.Enabled = false;
            }
            else if (action == Action.Save)
            {
                ibtnActionSave.ImageUrl = "~/Images/ActionSaveGray.png";
                ibtnActionSave.Enabled = false;
            } 
            else if (action == Action.Search)
            {
                ibtnActionSearch.ImageUrl = "~/Images/ActionSearchGray.png";
                ibtnActionSearch.Enabled = false;
            }
            else if (action == Action.Clear)
            {
                ibtnActionClear.ImageUrl = "~/Images/ActionClearGray.png";
                ibtnActionClear.Enabled = false;
            }
            else if (action == Action.Help)
            {
                ibtnActionHelp.ImageUrl = "~/Images/ActionHelpGray.png";
                ibtnActionHelp.Enabled = false;
            }
        }

        public string DetermineSitePath(string url)
        {
            string sitePath = "";
            if (mMaster.Items[0].NavigateUrl.Equals(url))
            {
                sitePath = mMaster.Items[0].ValuePath;
            }

            foreach (MenuItem item1 in mMaster.Items[0].ChildItems) // level 1
            {
                foreach (MenuItem item2 in item1.ChildItems) // level 2
                {
                    foreach (MenuItem item3 in item2.ChildItems) // level 3
                    {
                        foreach (MenuItem item4 in item3.ChildItems) // level 4
                        {
                            if (item4.NavigateUrl.Equals(url))
                            {
                                sitePath = item4.ValuePath;
                                break;
                            }
                        }
                        if (item3.NavigateUrl.Equals(url))
                        {
                            sitePath = item3.ValuePath;
                            break;
                        }
                    }
                    if (item2.NavigateUrl.Equals(url))
                    {
                        sitePath = item2.ValuePath;
                        break;
                    }
                }
                if (item1.NavigateUrl.Equals(url))
                {
                    sitePath = item1.ValuePath;
                    break;
                }

                // check whether url exists in additional page list
                if (sitePath == "")
                {
                    Eu.Europa.Ec.Olaf.GmsrBLL.PageBL result = AdditionalPageList.Find(item => item.Url == url);
                    if (result != null)
                    {
                        sitePath = result.SitePath;
                    }
                }

                // check whether this is the 'Oops' page because an unexpected error occurred
                if (sitePath == "" && url.Contains("Oops"))
                {
                    sitePath = "Oops!";
                }
            }
            return sitePath;
        }

        public bool IsAccessible(string url)
        {
            return DetermineSitePath(url) != "";                            
        }

        #endregion
    }
}
