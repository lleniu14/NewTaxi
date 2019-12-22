using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models.Users;
using WebApi.Models;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            return Ok(await _tokenService.CreateToken(model));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                await _userService.CreateAsync(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
           
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                // update user 
                await _userService.UpdateAsync(user, model.Password);
                return NoContent();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
