using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Security.Cryptography;
using static CourseManagement.Model.DTOs.ModuleDTO;
using CourseManagement.DataAccess.Repositorys;
using CourseManagement.DataAccess.Repositorys.IRepositorys;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase {
        private readonly ModuleRepository moduleRepository;

        public ModuleController(ModuleRepository moduleRepository) {
            this.moduleRepository = moduleRepository;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddModuleRequest req) {
            try {
                await moduleRepository.CreateModule(req);
                return Ok(new {
                    Message = "Module created successfully"
                });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while creating the module" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchModuleRequest req) {
            try {
                var res = await moduleRepository.SearchModule(req);
                return Ok(res);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while searching the module" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("detail")]
        public async Task<IActionResult> Detail([FromQuery] DetailModuleRequest req) {
            try {
                var res = await moduleRepository.DetailModule(req);
                return Ok(res);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while get detail the module" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateModuleRequest req) {
            try {
                await moduleRepository.UpdateModule(req);
                return Ok(new { Message = "Update module successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while updating the module" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveModuleRequest req) {
            try {
                await moduleRepository.RemoveModule(req);
                return Ok(new { Message = "Remove module successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while removing the module" });
            }
        }
    }
}
