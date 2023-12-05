using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Data.Metadata.Edm;

namespace Eu.Europa.Ec.Olaf.GmsrDAL
{
    public partial class GmsrEntities
    {
        #region Enumerations

        public enum AuditAction
        {
            I, U, D
        }

        #endregion

        #region Fields

        List<Audit> auditList = new List<Audit>();

        #endregion

        #region Properties

        public string UserLogin { get; set; }

        #endregion

        #region Event Handlers

        void GmsrEntities_SavingChanges(object sender, EventArgs e)
        {
            IEnumerable<ObjectStateEntry> changes = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified);

            foreach (ObjectStateEntry stateEntryEntity in changes)
            {
                if (!stateEntryEntity.IsRelationship && stateEntryEntity.Entity != null && !(stateEntryEntity.Entity is Audit) && !(stateEntryEntity.Entity is Session)) // Is a normal entry, not a relationship. Is not an Audit and is not a Session.
                {                    
                    Audit audit = this.AuditTrailFactory(stateEntryEntity, UserLogin);
                    auditList.Add(audit);
                }
            }

            if (auditList.Count > 0)
            {
                foreach (var audit in auditList) //add all audits 
                {
                    this.AddToAuditSet(audit);
                }
            }
        }

        #endregion

        #region Private Methods

        partial void OnContextCreated()
        {
            this.SavingChanges += new EventHandler(GmsrEntities_SavingChanges);
        }

        private Audit AuditTrailFactory(ObjectStateEntry entry, string UserLogin)
        {
            Audit audit = new Audit();

            audit.Id = Guid.NewGuid().ToString();
            audit.RevisionStamp = DateTime.Now;
            audit.TableName = entry.EntitySet.Name;
            audit.UserLogin = UserLogin;

            if (entry.State == EntityState.Added) //entry is Added 
            {
                audit.NewData = GetEntryValueInString(entry, false);
                audit.Action = AuditAction.I.ToString();
            }
            else if (entry.State == EntityState.Deleted) //entry in deleted
            {
                audit.OldData = GetEntryValueInString(entry, true);
                audit.Action = AuditAction.D.ToString();
            }
            else //entry is modified
            {
                audit.OldData = GetEntryValueInString(entry, true);
                audit.NewData = GetEntryValueInString(entry, false);
                audit.Action = AuditAction.U.ToString();
                
                //passing collection of mismatched Columns as string
                IEnumerable<string> modifiedProperties = entry.GetModifiedProperties();
                string changedColumns = "<Columns>";
                foreach (string column in modifiedProperties)
                {
                    changedColumns += "<Column>" + column + "</Column>";
                }

                audit.ChangedColumns = changedColumns + "</Columns>";
            }

            return audit;
        }

        private string GetEntryValueInString(ObjectStateEntry entry, bool isOrginal)
        {
            string result = "<NameValuePairs>";
            if (isOrginal)
            {                
                for (int i = 0; i < entry.OriginalValues.FieldCount; i++)
                {
                    result += "<" + entry.OriginalValues.GetName(i) + ">";
                    result += entry.OriginalValues.GetValue(i);
                    result += "</" + entry.OriginalValues.GetName(i) + ">";
                }                                
            }
            else
            {
                for (int i = 0; i < entry.CurrentValues.FieldCount; i++)
                {
                    result += "<" + entry.CurrentValues.GetName(i) + ">";
                    result += entry.CurrentValues.GetValue(i);
                    result += "</" + entry.CurrentValues.GetName(i) + ">";
                } 
            }
            result += "</NameValuePairs>";
            
            return result;
        }

        #endregion
    }
}
