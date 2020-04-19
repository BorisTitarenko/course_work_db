using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Models
{
    public class WorkPreparatorMiddleware
    {
        private readonly RequestDelegate _next;
        private String _connection;

        public WorkPreparatorMiddleware(RequestDelegate next, String connection)
        {
            this._next = next;
            _connection = connection;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                using (SqlCommand command = new SqlCommand("createJourneysForToday", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    command.ExecuteScalar();
                }
            }
            await _next.Invoke(context);
        }
    }
}