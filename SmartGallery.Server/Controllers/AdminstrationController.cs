using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Shared.ViewModels;

namespace SmartGallery.Server.Controllers
{
	public class AdminstrationController:ControllerBase
	{
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminstrationController(RoleManager<IdentityRole> roleManager)
		{
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return Ok(model);
            
            }
        
            return BadRequest();
        }

        [HttpGet]
        public ActionResult<IQueryable<IdentityRole>> GetRoles()
        {
            IQueryable<IdentityRole> roles = _roleManager.Roles;

            if (roles is null)
                return NotFound();
            return Ok(roles);
        }
	}
}

