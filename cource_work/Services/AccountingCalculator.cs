using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Services
{
    public class AccountingCalculator
    {
        private String _connection;

        public AccountingCalculator(String connection)
        {
            _connection = connection;
        }

        public void calculate(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("fillLastAccountingReport", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@acc_id", System.Data.SqlDbType.Int).Value = id;
                    command.ExecuteScalar();
                }

            }
        }
    }
}
