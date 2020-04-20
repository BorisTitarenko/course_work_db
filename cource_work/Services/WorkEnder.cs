using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Services
{
    public class WorkEnder
    {
        private String _connection;

        public WorkEnder(String connection)
        {
            _connection = connection;
        }

        public async void collectTransactions()
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("collectTransactions", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.ExecuteScalar();
                }

            }
        }
    }
}
