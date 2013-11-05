﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class SqlHelper
    {
        public static readonly string Src = ConfigurationManager.AppSettings["SqlConnectionString"];

        public static DataTable GetDataTableBySql(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(SqlHelper.Src))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static DataTable GetDataTableByStoreProcedure(string spName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(SqlHelper.Src))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static DataTable GetDataTableByStoreProcedure(string spName, string PuList, string quarter)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(SqlHelper.Src))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PuList", PuList);
                    cmd.Parameters.AddWithValue("@quarter", quarter);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static int GetAffectedRowBySql(string sql)
        {
            int affectedRow;
            using (SqlConnection conn = new SqlConnection(SqlHelper.Src))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    affectedRow = cmd.ExecuteNonQuery();
                }
            }
            return affectedRow;
        }

        public static bool IsExsitingBySql(string sql)
        {
            try
            {
                bool isExisting;
                using (SqlConnection conn = new SqlConnection(SqlHelper.Src))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.Read())
                                isExisting = true;
                            else
                                isExisting = false;
                        }
                    }
                }
                return isExisting;
            }
            catch { return false; }
        }

    }
}
