using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eu.Europa.Ec.Olaf.Gmsr
{
    public class Constants
    {
        #region Fields

        #region Session Constants
        
        public const string USER_NAME = "userName";
        public const string USER_LOGIN = "userLogin";
        public const string MENU_ROOT = "menuRoot";
        public const string SITE_PATH = "sitePath";
        public const string ADDITIONAL_PAGE_LIST = "additionalPageList";

        public const string PREVIOUS_PAGE_STACK = "previousPageStack";
        public const string FROM_PAGE = "FromPage";

        public const string SELECTED_AUDIT = "selectedAudit";
        public const string SELECTED_GROUP = "selectedGroup";
        public const string SELECTED_USER = "selectedUser";
        public const string SELECTED_HARDWARE_ITEM = "selectedHardwareItem";
        public const string SELECTED_OLAF_USER = "selectedOlafUser";
        public const string SELECTED_SOFTWARE_PRODUCT = "SelectedSoftwareProduct";
        public const string SELECTED_SOFTWARE_LICENSE = "selectedSoftwareLicense";
        public const string SELECTED_SOFTWARE_LICENSE_ASSIGNMENT = "selectedSoftwareLicenseAssignment";
        public const string SELECTED_SOFTWARE_ORDER_ITEM = "selectedSoftwareOrderItem";        
        public const string SELECTED_ORDER_FORM = "selectedOrderForm";
        public const string SELECTED_ORDER_FORM_DOCUMENT = "selectedOrderFormDocument";
        public const string SELECTED_MEDIA_ITEM = "selectedMediaItem";

        public const string HARDWARE_SEARCH_VALUES = "hardwareSearchValues";
        public const string AUDIT_SEARCH_VALUES = "auditSearchValues";
        public const string SESSION_SEARCH_VALUES = "sessionSearchValues";
        public const string WEB_EVENT_SEARCH_VALUES = "webEventSearchValues";

        public const string GROUPS_KEYWORD = "groupsKeyword";
        public const string USERS_KEYWORD = "usersKeyword";
        public const string DOCUMENTS_KEYWORD = "documentsKeyword";
        public const string MEDIA_ITEMS_KEYWORD = "mediaItemsKeyword";
        public const string ORDER_FORMS_KEYWORD = "orderFormsKeyword";
        public const string ORDER_ITEMS_KEYWORD = "orderItemsKeyword";
        public const string PRODUCTS_KEYWORD = "productsKeyword";

        #endregion

        public const string GMSR = "GMSr";        
        public const string OLD_GMS = "OldGMS";
        public const string ABAC = "ABAC";

        #endregion

        #region Constructors

        public Constants()
        {
            //Read values from web.config or perform typical initialization functionality
        }

        #endregion
    }
}