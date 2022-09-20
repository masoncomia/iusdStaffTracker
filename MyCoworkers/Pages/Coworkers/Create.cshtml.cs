using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace MyCoworkers.Pages.Coworkers
{
    public class CreateModel : PageModel
    {
        public CoworkerInfo coworkerInfo = new CoworkerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            coworkerInfo.name = Request.Form["name"];
            coworkerInfo.email = Request.Form["email"];
            coworkerInfo.phone = Request.Form["phone"];
            coworkerInfo.address = Request.Form["address"];

            if (coworkerInfo.name.Length == 0 || coworkerInfo.email.Length == 0 ||
                coworkerInfo.phone.Length == 0 || coworkerInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new coworker into the database

            try
            {
                String connectionString = "Server=tcp:mycoworkers.database.windows.net,1433;Initial Catalog=MyCoworkers;Persist Security Info=False;User ID=masoncomia;Password=091823Mtcf-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", coworkerInfo.name);
                        command.Parameters.AddWithValue("@email", coworkerInfo.email);
                        command.Parameters.AddWithValue("@phone", coworkerInfo.phone);
                        command.Parameters.AddWithValue("@address", coworkerInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            coworkerInfo.name = ""; coworkerInfo.email = ""; coworkerInfo.phone = ""; coworkerInfo.address = "";
            successMessage = "New Coworker Added Correctly";

            Response.Redirect("/Coworkers/Index");

        }
    }
}
