using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlserver1;Initial Catalog=employees;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM employeeInfo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.email = reader.GetString(2);
                                employeeInfo.phone = reader.GetString(3);
                                employeeInfo.address = reader.GetString(4);
                                employeeInfo.created_at = reader.GetDateTime(5).ToString();

                                listEmployees.Add(employeeInfo);

                            }
                        }
                    }
                }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.ToString());
            }
        }
    }


    public class EmployeeInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
