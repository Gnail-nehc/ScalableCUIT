using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class ProjectInfo
    {
        public static string Id
        {
            get { return "Id"; }
        }
        public static string Name
        {
            get { return "ProjectName"; }
        }
        public static string FuncInDB
        {
            get { return "Project"; }
        }

        public static string PrintAll()
        {
            try
            {
                string info = "";
                string sql = "select * from " + FuncInDB;
                DataTable dt = SqlHelper.GetDataTableBySql(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr[Id].ToString();
                    string name = dr[Name].ToString();
                    info += id + "." + name + " ";
                }
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FindPrjNameById(string strid)
        {
            try
            {
                string sql = "select top 1 * from " + FuncInDB + " where " + Id + "=" + strid;
                DataTable dt = SqlHelper.GetDataTableBySql(sql);
                int id = Convert.ToInt32(strid);
                return dt.Rows[0][id].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
