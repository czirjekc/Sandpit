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
    public class BasePage : System.Web.UI.Page
    {
        #region Properties

        public AuditBL SelectedAudit
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_AUDIT], null))
                {
                    Session[Constants.SELECTED_AUDIT] = new AuditBL();
                }
                return (AuditBL)Session[Constants.SELECTED_AUDIT];
            }
            set { Session[Constants.SELECTED_AUDIT] = value; }
        }

        public GroupBL SelectedGroup
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_GROUP], null))
                {
                    Session[Constants.SELECTED_GROUP] = new GroupBL();
                }
                return (GroupBL)Session[Constants.SELECTED_GROUP];
            }
            set { Session[Constants.SELECTED_GROUP] = value; }
        }

        public UserBL SelectedUser
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_USER], null))
                {
                    Session[Constants.SELECTED_USER] = new UserBL();
                }
                return (UserBL)Session[Constants.SELECTED_USER];
            }
            set { Session[Constants.SELECTED_USER] = value; }
        }

        public HardwareItemBL SelectedHardwareItem
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_HARDWARE_ITEM], null))
                {
                    Session[Constants.SELECTED_HARDWARE_ITEM] = new HardwareItemBL();
                }
                return (HardwareItemBL)Session[Constants.SELECTED_HARDWARE_ITEM];
            }
            set { Session[Constants.SELECTED_HARDWARE_ITEM] = value; }
        }

        public OlafUserBL SelectedOlafUser
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_OLAF_USER], null))
                {
                    Session[Constants.SELECTED_OLAF_USER] = new OlafUserBL();
                }
                return (OlafUserBL)Session[Constants.SELECTED_OLAF_USER];
            }
            set { Session[Constants.SELECTED_OLAF_USER] = value; }
        }

        public SoftwareProductBL SelectedSoftwareProduct
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_SOFTWARE_PRODUCT], null))
                {
                    Session[Constants.SELECTED_SOFTWARE_PRODUCT] = new SoftwareProductBL();
                }
                return (SoftwareProductBL)Session[Constants.SELECTED_SOFTWARE_PRODUCT];
            }
            set { Session[Constants.SELECTED_SOFTWARE_PRODUCT] = value; }
        }
        
        public SoftwareLicenseBL SelectedSoftwareLicense
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_SOFTWARE_LICENSE], null))
                {
                    Session[Constants.SELECTED_SOFTWARE_LICENSE] = new SoftwareLicenseBL();
                }
                return (SoftwareLicenseBL)Session[Constants.SELECTED_SOFTWARE_LICENSE];
            }
            set { Session[Constants.SELECTED_SOFTWARE_LICENSE] = value; }
        }

        public SoftwareLicenseAssignmentBL SelectedSoftwareLicenseAssignment
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_SOFTWARE_LICENSE_ASSIGNMENT], null))
                {
                    Session[Constants.SELECTED_SOFTWARE_LICENSE_ASSIGNMENT] = new SoftwareLicenseAssignmentBL();
                }
                return (SoftwareLicenseAssignmentBL)Session[Constants.SELECTED_SOFTWARE_LICENSE_ASSIGNMENT];
            }
            set { Session[Constants.SELECTED_SOFTWARE_LICENSE_ASSIGNMENT] = value; }
        }

        public SoftwareOrderItemBL SelectedSoftwareOrderItem
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_SOFTWARE_ORDER_ITEM], null))
                {
                    Session[Constants.SELECTED_SOFTWARE_ORDER_ITEM] = new SoftwareOrderItemBL();
                }
                return (SoftwareOrderItemBL)Session[Constants.SELECTED_SOFTWARE_ORDER_ITEM];
            }
            set { Session[Constants.SELECTED_SOFTWARE_ORDER_ITEM] = value; }
        }            

        public OrderFormBL SelectedOrderForm
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_ORDER_FORM], null))
                {
                    Session[Constants.SELECTED_ORDER_FORM] = new OrderFormBL();
                }
                return (OrderFormBL)Session[Constants.SELECTED_ORDER_FORM];
            }
            set { Session[Constants.SELECTED_ORDER_FORM] = value; }
        }
        
        public OrderFormDocumentBL SelectedOrderFormDocument
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_ORDER_FORM_DOCUMENT], null))
                {
                    Session[Constants.SELECTED_ORDER_FORM_DOCUMENT] = new OrderFormDocumentBL();
                }
                return (OrderFormDocumentBL)Session[Constants.SELECTED_ORDER_FORM_DOCUMENT];
            }
            set { Session[Constants.SELECTED_ORDER_FORM_DOCUMENT] = value; }
        }

        public MediaItemBL SelectedMediaItem
        {
            get
            {
                if (Equals(Session[Constants.SELECTED_MEDIA_ITEM], null))
                {
                    Session[Constants.SELECTED_MEDIA_ITEM] = new MediaItemBL();
                }
                return (MediaItemBL)Session[Constants.SELECTED_MEDIA_ITEM];
            }
            set { Session[Constants.SELECTED_MEDIA_ITEM] = value; }
        }
        


        public Stack<PageBL> PreviousPageStack
        {
            get
            {
                if (Equals(Session[Constants.PREVIOUS_PAGE_STACK], null))
                {
                    Session[Constants.PREVIOUS_PAGE_STACK] = new Stack<PageBL>();
                }
                return (Stack<PageBL>)Session[Constants.PREVIOUS_PAGE_STACK];
            }
            set { Session[Constants.PREVIOUS_PAGE_STACK] = value; }
        }

        public PageBL FromPage
        {
            get
            {
                if (Equals(Session[Constants.FROM_PAGE], null))
                {
                    return null;
                }
                else
                {
                    return (PageBL)Session[Constants.FROM_PAGE];
                }
            }
            set { Session[Constants.FROM_PAGE] = value; }
        }
              
        #endregion

        #region Constructors

        public BasePage()
        {
        }

        #endregion

        #region Public Methods

        public PageBL GetCurrentPage()
        {
            return new PageBL("~" + Request.RawUrl, ((MasterPage)Master).SitePath);
        }

        #endregion
    }
}