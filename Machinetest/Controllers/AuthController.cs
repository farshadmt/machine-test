using Application;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Machinetest.Controllers
{
    [Produces("application/json")] 
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthController(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;
        }

        [HttpPost]
        public async Task<Response<User>> RegisterUser(User user)
        {

            var apiResponse = new Response<User>();

            try
            {
                var data = await _authRepository.RegisterUserAsync(user);
                apiResponse.success = true;
                apiResponse.statusCode = 200;
                apiResponse.data = data;
            }
            catch (SqlException ex)
            {
                apiResponse.success = false;
                apiResponse.statusCode = 400;
                apiResponse.message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.success = false;
                apiResponse.statusCode = 500;
                apiResponse.message = ex.Message;
            }

            return apiResponse;
        }

        [HttpPost]
        public async Task<Response<User>> Login(Login login)
        {

            var apiResponse = new Response<User>();

            try
            {
                var data = await _authRepository.LoginAsync(login);
                apiResponse.success = true;
                apiResponse.statusCode = 200;
                apiResponse.message = "Login Succesful";
                apiResponse.data = data;
            }
            catch (SqlException ex)
            {
                apiResponse.success = false;
                apiResponse.statusCode = 400;
                apiResponse.message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.success = false;
                apiResponse.statusCode = 500;
                apiResponse.message = ex.Message;
            }

            return apiResponse;
        }
        
        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return Unauthorized();
        }

    }
}
