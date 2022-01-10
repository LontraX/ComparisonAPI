using AutoMapper;
using Comparly.Core.Security;
using Comparly.Data.Dtos;
using Comparly.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comparly.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWT_TokenGenerator _tokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AppUserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IJWT_TokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
            _mapper = mapper;
            
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody]AddAppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("model", "PLease fill out the form correctly");
                return BadRequest(ModelState);
            }

            // check if user already exists
            AppUser checkUser = await _userManager.FindByEmailAsync(model.Email);
            if (checkUser != null)
            {
                ModelState.AddModelError("model", "User already exists");
                return BadRequest(ModelState);
            }

            // add user
            AppUser incomingUser = _mapper.Map<AppUser>(model);
            incomingUser.UserName = model.Email;
            IdentityResult result = await _userManager.CreateAsync(incomingUser, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    return StatusCode(500, ModelState);
                }
            }

            // check if role (Guest) exists
            if (!await _roleManager.RoleExistsAsync("Assistant"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Assistant"));
            }

            // add user to role
            IdentityResult roleResult = await _userManager.AddToRoleAsync(incomingUser, "Assistant");
            if (!roleResult.Succeeded)
            {
                foreach (var err in roleResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return StatusCode(500, ModelState);
            }


            return CreatedAtAction("RegisterUser", incomingUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {

            // check if the login form was correctly filled
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Model", "Please fill out the form correctly");
                return BadRequest(ModelState);
            }

            // check if any user has the inputted email
            AppUser checkUser = await _userManager.FindByEmailAsync(model.Email);
            if (checkUser == null)
            {
                ModelState.AddModelError("Email", "Invalid Credential");
                return NotFound(ModelState);
            }

            // sign out any currently logged in user
            await _signInManager.SignOutAsync();

            // login the user after checking if password is correct
            var checkPassword = await _signInManager.PasswordSignInAsync(checkUser, model.Password, false, false);
            if (!checkPassword.Succeeded)
            {
                ModelState.AddModelError("Password", "Invalid Credential");
                return BadRequest(ModelState);
            }

            var getToken = await _tokenGenerator.GenerateToken(checkUser);

            var token = new LogInResponseDto
            {
                Token = getToken,
            };
            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}
