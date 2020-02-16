using Acclaim.Api.Contracts.V1.Requests;
using Acclaim.Api.Domain.MongoDomains;
using Acclaim.Api.Services;
using Acclaim.Api.V1.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Acclaim.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoAcclaimController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProvideAcclaimService _mongoAcclaimService;

        public MongoAcclaimController(IHostingEnvironment HostingEnvironment,
                                        IProvideAcclaimService mongoAcclaimService)
        {

            _hostingEnvironment = HostingEnvironment;
            _mongoAcclaimService = mongoAcclaimService;
        }

        #region Get
        [HttpGet(ApiRoutes.Acclaim.GetAllAcclaims)]
        public async Task<IActionResult> GetAllAcclaims()
        {
            var acclaims = await _mongoAcclaimService.GetAllAcclaims();
            if (acclaims != null)
            {
                return Ok(new { incomingAcclaims = acclaims });
            }
            return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
        }

        [HttpGet(ApiRoutes.Acclaim.GetAcclaimById)]
        public async Task<IActionResult> GetAcclaimById(string Id)
        {
            var acclaim = await _mongoAcclaimService.GetAcclaimById(Id);
            if (acclaim != null)
            {
                return Ok(new {  incomingAcclaim = acclaim });
            }
            return NotFound(new { status = 0, message = "Not Found" });
        }

        [HttpGet(ApiRoutes.Acclaim.Search)]
        public async Task<IActionResult> Search(string acclaimName)
        {
            var acclaim = await _mongoAcclaimService.GetAcclaimByName(acclaimName);
            if (acclaim != null)
            {
                return Ok(new { incomingAcclaims = acclaim });
            }
            return NotFound(new { status = 0, message = "Not Found" });
        }

        [HttpGet(ApiRoutes.Acclaim.GetUsersInAcclaim)]
        public async Task<IActionResult> GetUsersInAcclaim(string acclaimId)
        {
            if (acclaimId != null)
            {
                var users = await _mongoAcclaimService.GetUsersInAcclaim(acclaimId);
                if (users.Count > 0)
                {
                    return Ok(new { usersInAcclaim = users });
                }
                return NoContent();

            }
            return BadRequest(new { status = 0, message = "Invalid request" });
        }
        
        
        [HttpGet(ApiRoutes.Acclaim.GetRelatiedAcclaimsInAcclaim)]
        public async Task<IActionResult> GetRelatiedAcclaimsInAcclaim(string acclaimId)
        {
            if (acclaimId != null)
            {
                var relatedAcclaims = await _mongoAcclaimService.GetRelatiedAcclaimsInAcclaim(acclaimId);
                if (relatedAcclaims.Count > 0)
                {
                    return Ok(new { usersInAcclaim = relatedAcclaims });
                }
                return NoContent();

            }
            return BadRequest(new { status = 0, message = "Invalid request" });
        }

        [HttpGet(ApiRoutes.Acclaim.GetUserById)]
        public async Task<IActionResult> GetUserById([FromRoute] string acclaimId, [FromRoute]string userId)
        {
            var user = await _mongoAcclaimService.GetUserById(acclaimId, userId);
            if (user != null)
            {
                return Ok(new {   incomingUser = user });
            }
            return NotFound(new { status = 0, message = "Not Found" });
        }

        //there is logic need to modifie
        [HttpGet(ApiRoutes.Acclaim.GetProvidedAcclaim)]
        public async Task<IActionResult> GetProvidedAcclaim([FromRoute] string acclaimId)
        {
            var providedAcclaim = await _mongoAcclaimService.GetProvidedAcclaim(acclaimId);
            if (providedAcclaim != null)
            {
                return Ok(new { incomingProvidedAcclaim = providedAcclaim });
            }
            return NotFound(new { status = 0, message = "Not Found" });
        }
        #endregion

        #region Post
        [HttpPost(ApiRoutes.Acclaim.CreateAcclaim)]
        public async Task<IActionResult> Create([FromForm] CreateAcclaimViewModel model)
        {

            var result = await _mongoAcclaimService.CreateAcclaim(model);
            if (result)
            {
                return Ok(new { status = 1, message = "Created successfully" });
            }
            return BadRequest(new { status = 0, message = "Not Created" });
        }

        #endregion

        #region Put


        [HttpPut(ApiRoutes.Acclaim.AssignAProviderToAcclaim)]
        public async Task<IActionResult> AssignAProviderToAcclaim([FromRoute]string acclaimId, [FromForm] AProviderViewModel aProviderViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AssignAProviderToAcclaim(acclaimId, aProviderViewModel);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to Assign AProvider" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }



        [HttpPut(ApiRoutes.Acclaim.AssignIssuerToAcclaim)]
        public async Task<IActionResult> AssignIssuerToAcclaim([FromRoute]string acclaimId, [FromForm] IssuerViewModel issuerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AssignIssuerToAcclaim(acclaimId, issuerViewModel);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to Assign issuer" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }
        
        [HttpPut(ApiRoutes.Acclaim.AssignRelatiedAcclaimToAcclaim)]
        public async Task<IActionResult> AssignRelatiedAcclaimToAcclaim([FromRoute]string acclaimId, [FromForm] RelatedAcclaimsViewModel relatedAcclaimsViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AssignRelatiedAcclaimToAcclaim(acclaimId, relatedAcclaimsViewModel);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to Assign Relatied Acclaim" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }

        [HttpPut(ApiRoutes.Acclaim.AssignAProviderTypeToAcclaim)]
        public async Task<IActionResult> AssignAProviderTypeToAcclaim([FromRoute]string acclaimId, [FromForm] AcclaimProvider acclaimProvider)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AssignAProviderTypeToAcclaim(acclaimId, acclaimProvider);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to Assign AProvider Type" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }

        [HttpPut(ApiRoutes.Acclaim.AssignAcclaimTypeToAcclaim)]
        public async Task<IActionResult> AssignAcclaimTypeToAcclaim([FromRoute]string acclaimId, [FromForm] AcclaimType acclaimType)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AssignAcclaimTypeToAcclaim(acclaimId, acclaimType);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to Assign Acclaim Type" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }


        [HttpPut(ApiRoutes.Acclaim.AddRuleToAcclaim)]
        public async Task<IActionResult> AddRuleToAcclaim([FromRoute]string acclaimId, [FromForm]  AcclaimRuleViewModel acclaimRule)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AddRuleToAcclaim(acclaimId, acclaimRule);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to add rule" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }

        [HttpPut(ApiRoutes.Acclaim.AddUserToAcclaim)]
        public async Task<IActionResult> AddUserToAcclaim([FromRoute]string acclaimId, [FromForm]  AcclaimUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.AddUserToAcclaim(acclaimId, user);
                if (result)
                {
                    return Ok(new { status = 1, message = "Success to add user" });
                }
                return BadRequest(new { status = 1, message = "Something wrong happen" });
            }
            return BadRequest(new { status = 0, message = "Bad request" });
        }
        [HttpPut(ApiRoutes.Acclaim.DesignLogo)]
        public async Task<IActionResult> DesignLogo(string acclaimId, IFormFile file)
        {
            var logoLocation = "Logos";
            if (ModelState.IsValid)
            {
                var filePath = ProcessUploadedFile(file, logoLocation);
                var result = await _mongoAcclaimService.DesignLogo(acclaimId, filePath);
                if (result)
                {
                    return Ok(new { status = 1, message = "success" });
                }
            }
            return BadRequest(new { status = 0, message = "Bad request" });

        }

        [HttpPut(ApiRoutes.Acclaim.UpdateAcclaim)]
        public async Task<IActionResult> UpdateAcclaim([FromRoute]string acclaimId, [FromForm] CreateAcclaimViewModel model)
        {
            var updatedAcclaim = new MongoAcclaim
            {
                Name = model.Name
            };
            var result = await _mongoAcclaimService.UpdateAcclaim(acclaimId, updatedAcclaim);
            if (result)
            {
                return Ok(new { status = 1, message = "Updated successfully" });
            }
            return BadRequest(new { status = 0, message = "Not Updated" });
        }

        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.Acclaim.RemoveRuleFromAcclaim)]
        public async Task<IActionResult> RemoveRuleFromAcclaim([FromRoute]string acclaimId, [FromForm]string ruleId)
        {
            if (ModelState.IsValid)
            {
                var result = await _mongoAcclaimService.RemoveRuleFromAcclaim(acclaimId, ruleId);
                if (result)
                {
                    return Ok(new { status = 1, message = "Removed successfully" });
                }
                return BadRequest(new { status = 0, message = "Not removed" });

            }
            return BadRequest(new { status = 0, message = "Not Valid" });
        }

        [HttpDelete(ApiRoutes.Acclaim.RemoveAcclaimById)]
        public async Task<IActionResult> RemoveAcclaimById(string acclaimId)
        {
            var result = await _mongoAcclaimService.RemoveAcclaim(acclaimId);
            try
            {
                if (result)
                {
                    return Ok(new { status = 1, message = "Acclaim was deleted successfully" });
                }
                return NotFound(new { status = 0, message = "Not Found this item to delete" });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        #endregion

        #region upload
        private string ProcessUploadedFile(IFormFile model, string location)
        {
            string filePath = null;
            if (model != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, $"{location}");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }
            return filePath;
        }
        #endregion
    }
}
