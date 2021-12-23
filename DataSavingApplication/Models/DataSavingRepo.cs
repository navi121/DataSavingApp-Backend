using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Models
{
    public class DataSavingRepo
    {
        private string connectionString;
        public DataSavingRepo()
        {
            connectionString = @"Server=localhost;Database=SavingDataApp;User ID=sa;password=World@#2021;";
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void AddData(List<DataSavingModel> requestData)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO SavingDataApp(Name,PhoneNumber) VALUES(@Name,@PhoneNumber)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, requestData);
            }
        }

        public IEnumerable<DataSavingModel> GetAllData()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"Select * FROM SavingDataApp";
                dbConnection.Open();

                return dbConnection.Query<DataSavingModel>(sQuery);
            }
        }
    }
}
