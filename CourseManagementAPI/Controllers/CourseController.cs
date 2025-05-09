﻿using AutoMapper;
using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase {
        private readonly CourseManagementDb _db;
        private readonly IMapper _mapper;
        private readonly CourseRepository _courseRepository;

        public CourseController(CourseManagementDb db, IMapper mapper, CourseRepository courseRepository) {
            _db = db;
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddCourseRequest req) {
            try {
                await _courseRepository.CreateCourse(req);
                return Ok(new { Message = "Add new course success" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while creating the course" });
            }
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCourseRequest req) {
            try {
                var result = await _courseRepository.Search(req);
                return Ok(result);
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred while searching courses: {ex}" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveCourseRequest req) {
            try {
                await _courseRepository.RemoveCourse(req);
                return Ok(new { Message = "Course removed successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while removing the course" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("detail")]
        public async Task<IActionResult> Detail([FromQuery] DetailCourseRequest req) {
            try {
                var response = await _courseRepository.Detail(req);
                return Ok(response);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while getting course details" });
            }
        }

        [Authorize]
        [HttpGet("learn")]
        public async Task<IActionResult> Learn([FromQuery] LearnCourseRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                var response = await _courseRepository.Learn(req, userEmail.Value);
                return Ok(response);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while getting learning details" });
            }
        }

        [HttpGet("preview")]
        public async Task<IActionResult> Preview([FromQuery] DetailCourseRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }

                var response = await _courseRepository.Preview(req, userEmail.Value);
                return Ok(response);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred while getting course preview: {ex}" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCourseRequest req) {
            try {
                await _courseRepository.UpdateCourse(req);
                return Ok(new { Message = "Update course successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while updating course details" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest req) {
            try {
                await _courseRepository.UpdateStatus(req);
                return Ok(new { Message = "Update course status successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while updating course status" });
            }
        }

        [Authorize]
        [HttpPost("enroll-course")]
        public async Task<IActionResult> EnrollCourse([FromBody] JoinCourseRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                await _courseRepository.EnrollCourse(new() {
                    CourseId = req.CourseId,
                    UserEmail = userEmail.Value
                });
                return Ok(new { Message = "Enroll course successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while enroll course" });
            }
        }
    }
}
