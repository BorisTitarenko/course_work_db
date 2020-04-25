using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Models
{
    public class WorkPreparator
    {
        private String _connection;

        public WorkPreparator(String connection)
        {
            _connection = connection;
        }

        public void createJourneysAndCashAmount()
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("createJourneysForToday", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.ExecuteScalar();
                }
                using (SqlCommand command = new SqlCommand("createDayCashAmountOnStartUp", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.ExecuteScalar();
                }

            }
        }
    }
}