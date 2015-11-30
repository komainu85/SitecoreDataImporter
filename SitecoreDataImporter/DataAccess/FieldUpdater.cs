using System;
using System.Collections.Generic;
using MikeRobbins.SitecoreDataImporter.Contracts;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Services.Core.ComponentModel;

namespace MikeRobbins.SitecoreDataImporter.DataAccess
{
    public class FieldUpdater : IFieldUpdater
    {
        public void AddFieldsToItem<T>(Item item, T sourceObject)
        {
            try
            {
                item.Editing.BeginEdit();

                var fields = sourceObject.ToDictionary();

                foreach (var key in fields.Keys)
                {
                    object value = null;
                    fields.TryGetValue(key, out value);

                    var field = item.Fields[key];

                    if (field != null && key!="Id")
                    {
                        UpdateFieldValue(field, value);
                    }
                }

                item.Editing.AcceptChanges();

            }
            catch (Exception ex)
            {
                item.Editing.CancelEdit();
                Sitecore.Diagnostics.Log.Error(ex.Message, this);
            }

        }

        public void AddFieldsDictionaryToItem(Item item, Dictionary<string, string> fields)
        {
            try
            {
                item.Editing.BeginEdit();

                foreach (var key in fields.Keys)
                {
                    string value = null;
                    fields.TryGetValue(key, out value);

                    var field = item.Fields[key];

                    if (field != null && key != "Id")
                    {
                        UpdateFieldValue(field, value);
                    }
                }

                item.Editing.AcceptChanges();

            }
            catch (Exception ex)
            {
                item.Editing.CancelEdit();
                Sitecore.Diagnostics.Log.Error(ex.Message, this);
            } 
        }

        private static void UpdateFieldValue(Field field, object value)
        {
            if (field.Type == "Date")
            {
                field.Value = DateUtil.ToIsoDate(((DateTime) value));
            }
            else
            {
                field.Value = value.ToString();
            }
        }
    }
}