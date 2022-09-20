using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace MyCoworkers.Pages.Coworkers
{
    public class EditModel : PageModel
    {
        public CoworkerInfo coworkerInfo = new CoworkerInfo();
        public String errorMessage = "";
        public String successMessage = "";

        //OnGet() Method - Allows you to see the data of the current coworker
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Server=tcp:mycoworkers.database.windows.net,1433;Initial Catalog=MyCoworkers;Persist Security Info=False;User ID=masoncomia;Password=091823Mtcf-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                coworkerInfo.id = "" + reader.GetInt32(0);
                                coworkerInfo.name = reader.GetString(1);
                                coworkerInfo.email = reader.GetString(2);
                                coworkerInfo.phone = reader.GetString(3);
                                coworkerInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        //OnPost() Method - Allows you to update the data of the current coworker
        public void OnPost()
        {
            coworkerInfo.id = Request.Form["id"];
            coworkerInfo.name = Request.Form["name"];
            coworkerInfo.email = Request.Form["email"];
            coworkerInfo.phone = Request.Form["phone"];
            coworkerInfo.address = Request.Form["address"];

            if (coworkerInfo.id.Length == 0 || coworkerInfo.name.Length == 0 || 
                coworkerInfo.email.Length == 0 || coworkerInfo.phone.Length == 0 || 
                coworkerInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Server=tcp:mycoworkers.database.windows.net,1433;Initial Catalog=MyCoworkers;Persist Security Info=False;User ID=masoncomia;Password=091823Mtcf-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE id =@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", coworkerInfo.name);
                        command.Parameters.AddWithValue("@email", coworkerInfo.email);
                        command.Parameters.AddWithValue("@phone", coworkerInfo.phone);
                        command.Parameters.AddWithValue("@address", coworkerInfo.address);
                        command.Parameters.AddWithValue("@id", coworkerInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Coworkers/Index");
        }
    }
}
