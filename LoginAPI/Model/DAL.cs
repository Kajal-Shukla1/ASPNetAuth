using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LoginAPI.Model
{
    public class DAL
    {

        public List<Users> GetAllusers(SqlConnection connection)
        {
            //Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT *FROM Users2",connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Users> list = new List<Users>();
            if(dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     
                    Users users = new Users();
                    users.UserID = Convert.ToInt32(dt.Rows[i]["UserId"]);
                    users.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    users.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    users.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    users.PhoneNumber = Convert.ToInt64(dt.Rows[i]["PhoneNumber"]);
                    users.DOB = Convert.ToDateTime(dt.Rows[i]["DOB"]);
                    users.Gender = Convert.ToString(dt.Rows[i]["Gender"]);
                    users.Address = Convert.ToString(dt.Rows[i]["Address"]);
                    users.Pin = Convert.ToInt32(dt.Rows[i]["Pin"]);
                    users.ProfilePhotoPath = Convert.ToString(dt.Rows[i]["ProfilePhotoPath"]);
                    list.Add(users);
                }
            }
            //if (list.Count > 0)
            //{
            //    response.StatusCode = 200;
            //    response.StatusMessage = "Data Found";
            //    response.listUser = list;
            //}
            //else
            //{
            //    response.StatusCode = 100;
            //    response.StatusMessage = "No Data Found";
            //    response.listUser = null;
            //}
            return list;
        }

        public Users GetuserById(SqlConnection connection, int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users2 WHERE UserId = @id", connection);
            da.SelectCommand.Parameters.AddWithValue("@id", id); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            
                    Users users = new Users();
                    users.UserID = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    users.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    users.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                    users.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    users.PhoneNumber = Convert.ToInt64(dt.Rows[0]["PhoneNumber"]);
                    users.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                    users.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    users.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    users.Pin = Convert.ToInt32(dt.Rows[0]["Pin"]);
                    users.ProfilePhotoPath = Convert.ToString(dt.Rows[0]["ProfilePhotoPath"]);
                   
            

            return users;
        }

        public Response AddUser(SqlConnection connection, Users users)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Users2 (FirstName, LastName, Email, Password, DOB, Gender, PhoneNumber, Address, Pin, ProfilePhotoPath) VALUES (@FirstName, @LastName, @Email, @Password, @DOB, @Gender, @PhoneNumber, @Address, @Pin, @ProfilePhotoPath)", connection);
            connection.Open();
          
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@DOB", users.DOB);
            cmd.Parameters.AddWithValue("@Gender", users.Gender);
            cmd.Parameters.AddWithValue("@PhoneNumber", users.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", users.Address);
            cmd.Parameters.AddWithValue("@Pin", users.Pin);
            cmd.Parameters.AddWithValue("@ProfilePhotoPath", users.ProfilePhotoPath);

            int i= cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode= 200;
                response.StatusMessage = "User Added";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No User Added";
            }
            return response;
        }

        public Users UpdateUser(SqlConnection connection, Users users,int Id)
        {
            
            Users response = new Users();
            SqlCommand cmd = new SqlCommand("UPDATE Users2 SET FirstName=@FirstName, LastName=@LastName, Password=@Password, Gender=@Gender, PhoneNumber=@PhoneNumber, Address=@Address, Pin=@Pin, ProfilePhotoPath= @ProfilePhotoPath  WHERE UserId= @Id", connection);
            connection.Open();

            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Gender", users.Gender);
            cmd.Parameters.AddWithValue("@PhoneNumber", users.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", users.Address);
            cmd.Parameters.AddWithValue("@Pin", users.Pin);
            cmd.Parameters.AddWithValue("@ProfilePhotoPath", users.ProfilePhotoPath);
            cmd.Parameters.AddWithValue("@Id", Id);

            int i = cmd.ExecuteNonQuery();
            connection.Close();

            //if (i > 0)
            //{
            //    response.StatusCode = 200;
            //    response.StatusMessage = "User Updates";
            //}
            //else
            //{
            //    response.StatusCode = 100;
            //    response.StatusMessage = "No User Updated";
            //}
            return response;
        }

        public Response DeleteUser(int Id,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("DELETE FROM Users2 WHERE UserId= @Id", connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@Id", Id);

            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No User Deleted";
            }
            return response;
        }



        public bool CheckLogin(SqlConnection connection, string email, string password)
        {
            bool check;

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users2 WHERE Email = @username AND Password = @password", connection);
            cmd.Parameters.AddWithValue("@username", email);
            cmd.Parameters.AddWithValue("@password", password);

            connection.Open();
            int count = (int)cmd.ExecuteScalar();
            connection.Close();

            check = (count > 0);

            return check;
        }


    }
}
