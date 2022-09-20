using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MyCoworkers.Pages.Coworkers
{
    public class IndexModel : PageModel
    {
        public List<CoworkerInfo> listCoworkers = new List<CoworkerInfo>();
        
        public void OnGet()
        {
            try
            {
                //LOCAL
                //String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myCoworkers;Integrated Security=True";

                String connectionString = "Server=tcp:mycoworkers.database.windows.net,1433;Initial Catalog=MyCoworkers;Persist Security Info=False;User ID=masoncomia;Password=091823Mtcf-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CoworkerInfo coworkerInfo = new CoworkerInfo();
                                coworkerInfo.id = "" + reader.GetInt32(0);
                                coworkerInfo.name = reader.GetString(1);
                                coworkerInfo.email = reader.GetString(2);
                                coworkerInfo.phone = reader.GetString(3);
                                coworkerInfo.address = reader.GetString(4);
                                coworkerInfo.created_at = reader.GetDateTime(5).ToString();

                                listCoworkers.Add(coworkerInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class CoworkerInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
