using GuanajuatoAdminUsuarios.Models;
using System.Text;
using System;

namespace GuanajuatoAdminUsuarios.Repository
{
    public class SqlClientInstructions<T> where T : EntityDomain
    {
        private string GetDelete()
        {
            Type type = typeof(T);
            StringBuilder sbQry = new StringBuilder();
            System.Reflection.PropertyInfo[] propInfo = type.GetProperties();

            sbQry.AppendFormat("Delete From {0} Where {1}={2}",
              type.Name.Replace("Entity", string.Empty), propInfo[0].Name, "{0}");

            return sbQry.ToString();
        }

        private string GetUpdate()
        {
            int ctr = 0;
            Type type = typeof(T);
            StringBuilder sbQry = new StringBuilder();
            System.Reflection.PropertyInfo[] propInfo = type.GetProperties();
            foreach (System.Reflection.PropertyInfo pi in propInfo)
            {
                if (sbQry.ToString() == string.Empty)
                    sbQry.AppendFormat("Update {0} Set {1}={2}",
                             type.Name, pi.Name, "[" + ctr + "]");
                else
                    sbQry.AppendFormat(", {0}={1}", pi.Name, "[" + ctr + "]");

                ctr++;
            }

            if (sbQry.ToString() != string.Empty)
                sbQry.AppendFormat(" Where {0}={1} ", propInfo[0].Name, "[" + ctr + "]");

            sbQry.Replace("[", "{").Replace("]", "}");

            return sbQry.ToString();
        }

        private string GetInsert()
        {
            StringBuilder sb = new StringBuilder();
            int ctr = 0;

            Type type = typeof(T);
            StringBuilder sbQry = new StringBuilder();
            System.Reflection.PropertyInfo[] propInfo = type.GetProperties();
            foreach (System.Reflection.PropertyInfo pi in propInfo)
            {
                if (sbQry.ToString() == string.Empty)
                    sbQry.AppendFormat("INSERT INTO {0} ({1}",
                       type.Name.Replace("Entity", string.Empty), pi.Name);
                else
                {
                    sbQry.AppendFormat(", {0}", pi.Name);
                    sb.Append(",");
                }
                sb.Append("{" + ctr++ + "}");
            }

            if (sbQry.ToString() != string.Empty)
                sbQry.AppendFormat(") VALUES({0})", sb.ToString());

            return sbQry.ToString();
        }

        private string GetSelect()
        {
            Type type = typeof(T);
            StringBuilder sbQry = new StringBuilder();
            System.Reflection.PropertyInfo[] propInfo = type.GetProperties();
            foreach (System.Reflection.PropertyInfo pi in propInfo)
            {
                if (sbQry.ToString() == string.Empty)
                    sbQry.AppendFormat("Select {0}", pi.Name);
                else
                    sbQry.AppendFormat(", {0}", pi.Name);
            }

            if (sbQry.ToString() != string.Empty)
                sbQry.AppendFormat(" From {0} ", type.Name.Replace("Entity", string.Empty));

            return sbQry.ToString();
        }
    }
}
