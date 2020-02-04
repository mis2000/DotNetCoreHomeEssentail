using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using MySqlBasicCore.Models;
using System;
using System.Data;

namespace MySqlBasicCore.Utility
{
    public class DbfunctionUtility
    {
        private MySqlConnection connection;
        private string connectionString = "";
        private readonly IOptions<Appsettings> _appSettings;

        public DbfunctionUtility(IOptions<Appsettings> appSettings)
        {
            connection = new MySqlConnection();
            _appSettings = appSettings;
            connection.ConnectionString = appSettings.Value.ConnectionString_HEANDB;
        }

        public DataSet GetDataset(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand();
                da.SelectCommand = cmd;
                cmd.Connection = connection;
                cmd.CommandText = query;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }

            return ds;
        }
    }
}