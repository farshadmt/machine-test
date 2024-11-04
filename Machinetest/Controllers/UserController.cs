using Application;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Machinetest.Controllers
{
    //[Authorize] 
    [ApiController]
    [Route("[controller]/[Action]")]
    public class UserController : ControllerBase
    { 

        private readonly ILogger<UserController> _logger;

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, IUserRepository userRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<Response<UserList>> GetUserList()
        {

            var apiResponse = new Response<UserList>();

            try
            {
                var data = await _userRepository.GetUserListAsync();
                apiResponse.success = true;
                apiResponse.statusCode = 200;
                apiResponse.data = new UserList
                {
                    totalRecord = data.Count(),
                    employeeList = data
                };
            }
            catch (SqlException ex)
            {
                apiResponse.success = false;
                apiResponse.statusCode = ex.ErrorCode;
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

        [HttpGet("{id}")]
        public async Task<Response<User>> GetUserByCode(long id)
        {

            var apiResponse = new Response<User>();

            try
            {
                var data = await _userRepository.GetUserByCodeAsync(id);
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
    }
}
