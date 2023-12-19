using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Tests;

namespace Test;

public class Test : PageTest
{
    public class RegistrationTests
    {
        private readonly HttpClient _httpClient;

        public RegistrationTests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Helper.ApiBaseUrl);
        }

        [Test]
        public async Task RegisterUserTest()
        {
            var registrationData = new Register
            {
                userEmail = "testuser@example.com",
                password = "TestPassword123",
                profilePhoto = "profile.jpg",
                username = "testuser",
                firstname = "John",
                lastname = "Doe",
                education = "Bachelor's Degree",
                birthDate = new DateTime(1990, 1, 1),
               
            };

            var jsonContent = JsonConvert.SerializeObject(registrationData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            

            var response = await _httpClient.PostAsync("/api/account/register", httpContent);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

    }
    
    public class LoginTests
    {
        private readonly HttpClient _httpClient;
        private string? _token = "";

        public LoginTests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Helper.ApiBaseUrl);
        }

        [Test]
        public async Task LoginUserTest()
        {
            var loginData = new Login
            {
                email = "testuser@example.com",
                password = "TestPassword123"
            };

            var jsonContent = JsonConvert.SerializeObject(loginData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/account/login", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(jsonResponse)["token"]?.ToString();
            _token = token;
            Console.WriteLine(token);
        }
        [Test]
        public async Task UpdateAndReloginUserTest()
        {
            // Ensure you have previously stored the token in the Helper class using LoginUserTest
            if (string.IsNullOrEmpty(_token))
            {
                Assert.Fail("Token not available. Run LoginUserTest first.");
            }

            // Create an UpdateUserCommandModel with the new information
            var updateUserModel = new UpdateUser
            {
                UserEmail = "updatedemail@example.com",
                Username = "updatedusername",
                Firstname = "UpdatedFirstName",
                Lastname = "UpdatedLastName",
                Education = "UpdatedEducation",
                BirthDate = new DateTime(1995, 1, 1),
                ProfilePhoto = "updatedprofile.jpg"
            };

            var jsonContent = JsonConvert.SerializeObject(updateUserModel);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var updateResponse = await _httpClient.PutAsync("/api/account/update/user", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
    
            var loginData = new Login
            {
                email = "updatedemail@example.com",
                password = "TestPassword123" 
            };

            jsonContent = JsonConvert.SerializeObject(loginData);
            httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var loginResponse = await _httpClient.PostAsync("/api/account/login", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            
            var jsonResponse = await loginResponse.Content.ReadAsStringAsync();
            var newToken = JObject.Parse(jsonResponse)["token"]?.ToString();
            _token = "Bearer " + newToken;
        }

    }
}
