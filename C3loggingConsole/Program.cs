using C3logging.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3loggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var fd = GetFlogDetail("starting application", null);
            C3logger.WriteDiagnostic(fd);

            var tracker = new PerfTracker("FloggerConsole_Execution", "", fd.UserName,
                fd.Location, fd.Product, fd.Layer);

            try
            {
                var ex = new Exception("Something bad has happened!");
                ex.Data.Add("input param", "nothing to see here");
                throw ex;
            }
            catch (Exception ex)
            {
                fd = GetFlogDetail("", ex);
                C3logger.WriteError(fd);
            }
            //var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //using (var db = new SqlConnection(connStr))
            //{
            //    db.Open();
            //    try
            //    {
            //        RAW ADO.NET
            //        var rawAdoSp = new SqlCommand("CreateNewCustomer", db)
            //        {
            //            CommandType = System.Data.CommandType.StoredProcedure
            //        };
            //        rawAdoSp.Parameters.Add(new SqlParameter("@Name", "waytoolongforitsowngood"));
            //        rawAdoSp.Parameters.Add(new SqlParameter("@TotalPurchases", 12000));
            //        rawAdoSp.Parameters.Add(new SqlParameter("@TotalReturns", 100.50M));
            //        rawAdoSp.ExecuteNonQuery();
            //        var sp = new Sproc(db, "CreateNewCustomer");
            //        sp.SetParam("@Name", "waytoolongforitsowngood");
            //        sp.SetParam("@TotalPurchases", 12000);
            //        sp.SetParam("@TotalReturns", 100.50M);
            //        sp.ExecNonQuery();

            //    }
            //    catch (Exception ex)
            //    {
            //        var efd = GetFlogDetail("", ex);
            //        Flogger.WriteError(efd);
            //    }

            //    try
            //    {
            //        // Dapper
            //        //db.Execute("CreateNewCustomer", new
            //        //{
            //        //    Name = "dappernametoolongtowork",
            //        //    TotalPurchases = 12000,
            //        //    TotalReturns = 100.50M
            //        //}, commandType: System.Data.CommandType.StoredProcedure);                    
            //        // Wrapped Dapper 
            //        db.DapperProcNonQuery("CreateNewCustomer", new
            //        {
            //            Name = "dappernametoolongtowork",
            //            TotalPurchases = 12000,
            //            TotalReturns = 100.50M
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        var efd = GetFlogDetail("", ex);
            //        Flogger.WriteError(efd);
            //    }
            //}
            //var ctx = new CustomerDbContext();
            //try
            //{
            //    // Entity Framework                
            //    var name = new SqlParameter("@Name", "waytoolongforitsowngood");
            //    var totalPurchases = new SqlParameter("@TotalPurchases", 12000);
            //    var totalReturns = new SqlParameter("@TotalReturns", 100.50M);
            //    ctx.Database.ExecuteSqlCommand("EXEC dbo.CreateNewCustomer @Name, @TotalPurchases, @TotalReturns",
            //        name, totalPurchases, totalReturns);
            //}
            //catch (Exception ex)
            //{
            //    var efd = GetFlogDetail("", ex);
            //    Flogger.WriteError(efd);
            //}

            //var customers = ctx.Customers.ToList();
            //fd = GetFlogDetail($"{customers.Count} customers in the database", null);
            //Flogger.WriteDiagnostic(fd);


            fd = GetFlogDetail("used flogging console", null);
            C3logger.WriteUsage(fd);

            fd = GetFlogDetail("stopping app", null);
            C3logger.WriteDiagnostic(fd);

            tracker.Stop();
        }

        private static C3logDetail GetFlogDetail(string message, Exception ex)
        {
            return new C3logDetail
            {
                Product = "Flogger",
                Location = "FloggerConsole",    // this application
                Layer = "Job",                  // unattended executable invoked somehow
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Timestamp = DateTime.Now,
                Exception = ex
            };
        }
    }
}
