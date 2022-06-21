using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone = Request.Form["phone"];
            employeeInfo.address = Request.Form["address"];

            if(employeeInfo.name.Length==0 || employeeInfo.email.Length==0 || 
                employeeInfo.phone.Length==0 || employeeInfo.address.Length==0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new employee into the database
            try
            {
                String connectionString = "Data Source=.\\sqlserver1;Initial Catalog=employees;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO employeeInfo" + 
                        "(name, email, phone, address) VALUES" +
                        "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", employeeInfo.name);
                        command.Parameters.AddWithValue("@email", employeeInfo.email);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            employeeInfo.name = "";
            employeeInfo.email = "";
            employeeInfo.phone = "";
            employeeInfo.address = "";
            successMessage = "New Employee Added Correctly";

            Response.Redirect("/Employees/Index");
        }
    }
}
