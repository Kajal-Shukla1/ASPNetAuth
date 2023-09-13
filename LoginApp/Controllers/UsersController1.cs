using LoginApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace LoginApp.Controllers
{
    public class UsersController1 : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7037/api/");

        private readonly HttpClient _client;

        public UsersController1()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserModel> userslist = new List<UserModel>();
            HttpResponseMessage response= _client.GetAsync(baseAddress + "users/Getallusers").Result;

            if(response.IsSuccessStatusCode)
            {
                string data= response.Content.ReadAsStringAsync().Result;
                userslist = JsonConvert.DeserializeObject<List<UserModel>>(data);
            }
            return View(userslist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "users/AddUser", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "User Added";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                UserModel user = new UserModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Users/GetUserbyId/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserModel>(content);
                    return View(user);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"]= ex.Message;
                return View(); ;
            }
            
        }

        [HttpPost]
        public IActionResult Edit(UserModel model, int id)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Users/UpdateUser/" + id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
               TempData["errorMessage"] = ex.Message;
               return View(); 
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                UserModel user = new UserModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Users/GetUserbyId/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserModel>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {

                return View();
            }
            
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "Users/DeleteUser/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    RedirectToAction("Index");
                }
            }
            catch (Exception )
            {

                return View();
            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            bool res;
            HttpResponseMessage checkLoginResponse = _client.GetAsync(baseAddress + "users/CheckLogin/" + username + "/" + password).Result;
            if (checkLoginResponse.IsSuccessStatusCode)
            {
                res = Convert.ToBoolean(await checkLoginResponse.Content.ReadAsStringAsync());
                if (res==true)
                {
                    TempData["Username"] = username;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["errorMessage"] = "Invalid username or password.";
                }
            }
            else
            {
                TempData["errorMessage"] = "Invalid username or password.";
                
            }
            return View();
        }


        
        public IActionResult Dashboard()
        {
            string username = TempData["Username"] as string;

            if (username != null)
            {
                ViewData["username"] = username;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}
