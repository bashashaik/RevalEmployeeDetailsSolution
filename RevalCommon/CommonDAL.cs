using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Revalsys.Common
{
    public static class CommonDAL
    {
        #region ConvertToList

        public static List<ListDTO> ConvertToList<ListDTO>(List<DataRow> rows)
        {
            List<ListDTO> lst = null;
            try
            {
                if (rows != null)
                {
                    lst = new List<ListDTO>();
                    foreach (DataRow row in rows)
                    {
                        ListDTO item = CreateItem<ListDTO>(row);
                        lst.Add(item);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ListDTO CreateItem<ListDTO>(DataRow row)
        {
            ListDTO obj = default(ListDTO);
            try
            {
                if (row != null)
                {
                    obj = Activator.CreateInstance<ListDTO>();
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        FieldInfo prop = obj.GetType().GetField(column.ColumnName);
                        if (prop != null)
                        {
                            //PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                            try
                            {
                                object value = row[column.ColumnName];

                                if (value.ToString().Trim() != string.Empty)
                                {
                                    prop.SetValue(obj, value);
                                }
                            }
                            catch (Exception ex)
                            {
                                // WriteException(ex);
                                throw ex;
                            }
                        }
                        else
                        {
                            PropertyInfo objprop = obj.GetType().GetProperty(column.ColumnName);
                            if (objprop != null)
                            {
                                try
                                {
                                    object value = row[column.ColumnName];

                                    if (value.ToString().Trim() != string.Empty)
                                    {
                                        objprop.SetValue(obj, value, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // WriteException(ex);
                                    throw ex;
                                }
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
