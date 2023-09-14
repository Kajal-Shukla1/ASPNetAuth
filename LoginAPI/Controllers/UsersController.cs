using LoginAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Getallusers")]
        public List<Users> Getallusers()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());
            List<Users> response = new List<Users>();
            DAL dAL = new DAL();
            response = dAL.GetAllusers(sqlConnection);
            return response;
        }

        [HttpGet]
        [Route("GetUserbyId/{id}")]
        public Users GetUserbyId(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());
            Users response = new Users();
            DAL dAL = new DAL();
            response= dAL.GetuserById(sqlConnection, id);
            return response;
        }

        [HttpPost]
        [Route("AddUser")]
        public Response AddUser(Users user)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddUser(sqlConnection,user);

            return response;
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]

        public Users UpdateUser(Users user,int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());

            Users response = new Users();
            DAL dAL = new DAL();
            response = dAL.UpdateUser(sqlConnection, user, id);

            return response;
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public Response DeleteUser(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.DeleteUser(id,sqlConnection);

            return response;
        }



        [HttpGet]
        [Route("CheckLogin/{username}/{password}")]
        public bool CheckLogin(string username, string password)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection").ToString());
            bool response;
            DAL dAL = new DAL();
            response = dAL.CheckLogin(sqlConnection, username, password);
            return response;
        }

    }

}
