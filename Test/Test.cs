using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests;

namespace Test
{
    [TestFixture, Order(1)]
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

    [TestFixture, Order(2)]
    public class LoginUpdateUser
    {
        private readonly HttpClient _httpClient;
        private string? _token = "";

        public LoginUpdateUser()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Helper.ApiBaseUrl);
        }

        [Test, Order(1)]
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
        }

        [Test, Order(2)]
        public async Task UpdateAndReloginUserTest()
        {
            if (string.IsNullOrEmpty(_token))
            {
                Assert.Fail("Token not available. Run LoginUserTest first.");
            }

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
    [TestFixture,Order(3)]
        public class LoginUpdatePasswordTests
        {
            private readonly HttpClient _httpClient;
            private string? _token = "";

            public LoginUpdatePasswordTests()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(Helper.ApiBaseUrl);
            }

            [Test, Order(1)]
            public async Task LoginUserTest()
            {
                var loginData = new Login
                {
                    email = "updatedemail@example.com",
                    password = "TestPassword123"
                };

                var jsonContent = JsonConvert.SerializeObject(loginData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/account/login", httpContent);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(jsonResponse)["token"]?.ToString();
                _token = token;
            }

            [Test, Order(2)]
            public async Task ChangePasswordAndReLoginTest()
            {
                if (string.IsNullOrEmpty(_token))
                {
                    Assert.Fail("Token not available. Run LoginUserTest first.");
                }

                var changePasswordModel = new ChangePassword
                {
                    UserEmail = "updatedemail@example.com",
                    OldPassword = "TestPassword123", 
                    NewPassword = "NewPassword123"    
                };

                var jsonContent = JsonConvert.SerializeObject(changePasswordModel);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var changePasswordResponse = await _httpClient.PutAsync("/api/account/edit/password", httpContent);

                Assert.AreEqual(HttpStatusCode.OK, changePasswordResponse.StatusCode);

                var loginData = new Login
                {
                    email = "updatedemail@example.com",
                    password = "NewPassword123"
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
        
        [TestFixture, Order(4)]
        public class DeleteUserTests
        {
            private readonly HttpClient _httpClient;
            private string? _token = "";

            public DeleteUserTests()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(Helper.ApiBaseUrl);
            }

            [Test, Order(1)]
            public async Task LoginUserTest()
            {
                var loginData = new Login
                {
                    email = "updatedemail@example.com",
                    password = "NewPassword123"
                };

                var jsonContent = JsonConvert.SerializeObject(loginData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/account/login", httpContent);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(jsonResponse)["token"]?.ToString();
                _token = token;
            }

            [Test, Order(2)]
            public async Task DeleteUserTest()
            {
                if (string.IsNullOrEmpty(_token))
                {
                    Assert.Fail("Token not available. Run LoginUserTest first.");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var deleteUserResponse = await _httpClient.DeleteAsync("/api/account/delete");
                Assert.AreEqual(HttpStatusCode.OK, deleteUserResponse.StatusCode);
            }
        }
       
}
