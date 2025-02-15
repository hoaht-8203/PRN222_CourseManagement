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

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase {
        private readonly CourseManagementDb _db;

        public ModuleController(CourseManagementDb db) {
            _db = db;
        }

        /*
         - Khi set[new item] len dau tien thi:
            1.set prev[new item] = Null, next[new item] = Id[old first item]
            2.set prev[old first item] = Id[new item]
         - Khi set item vao giua thi:
            1.set prev[new item] = id[target prev item], next[new item] = id[target next item]
            2.set next[target prev item] = id[new item], prev[target next item] = id[new item]
         - Khi set item vao cuoi thi
            1.set next[new item] = Null, set prev[new item] = id[old last item]
        */
        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                return BadRequest(new { Message = "Invalid Course ID format" });
            }

            if (req.PrevModuleId != null) {
                var prevModuleId = req.PrevModuleId;
                var foundedModule = _db.Modules.Find(prevModuleId);
                if (foundedModule == null) {
                    return BadRequest(new { Message = $"Module with id {req.PrevModuleId} not existed or removed" });
                }
            }

            if (req.NextModuleId != null) {
                var nextModuleId = req.NextModuleId;
                var foundedModule = _db.Modules.Find(nextModuleId);
                if (foundedModule == null) {
                    return BadRequest(new { Message = $"Module with id {req.NextModuleId} not existed or removed" });
                }
            }

            var findCourse = _db.Courses.Find(courseGuid);
            if (findCourse == null) {
                return BadRequest(new { Message = $"Course with id {req.CourseId} not existed or removed" });
            }

            // Tạo module mới
            Module newModule = new Module() {
                Title = req.Title,
                CourseId = findCourse.Id,
                PreModuleId = req.PrevModuleId,
                NextModuleId = req.NextModuleId,
            };

            // Thêm module mới vào database trước để có Id
            _db.Modules.Add(newModule);
            _db.SaveChanges();

            // Trường hợp 1: Thêm vào đầu (PrevModuleId = null, NextModuleId != null)
            if (req.PrevModuleId == null && req.NextModuleId != null) {
                var oldFirstModule = _db.Modules.Find(req.NextModuleId);
                if (oldFirstModule != null) {
                    oldFirstModule.PreModuleId = newModule.Id;
                    _db.Update(oldFirstModule);
                }
            }
            // Trường hợp 2: Thêm vào giữa (PrevModuleId != null và NextModuleId != null)
            else if (req.PrevModuleId != null && req.NextModuleId != null) {
                var prevModule = _db.Modules.Find(req.PrevModuleId);
                var nextModule = _db.Modules.Find(req.NextModuleId);

                if (prevModule != null) {
                    prevModule.NextModuleId = newModule.Id;
                    _db.Update(prevModule);
                }

                if (nextModule != null) {
                    nextModule.PreModuleId = newModule.Id;
                    _db.Update(nextModule);
                }
            }
            // Trường hợp 3: Thêm vào cuối (PrevModuleId != null, NextModuleId = null)
            else if (req.PrevModuleId != null && req.NextModuleId == null) {
                var lastModule = _db.Modules.Find(req.PrevModuleId);
                if (lastModule != null) {
                    lastModule.NextModuleId = newModule.Id;
                    _db.Update(lastModule);
                }
            }
            // Trường hợp 4: Module độc lập (PrevModuleId = null, NextModuleId = null)
            // Không cần làm gì thêm vì đã set cả prev và next là null khi tạo

            _db.SaveChanges();

            return Ok(new { Message = "Add new module success" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                return BadRequest(new { Message = "Invalid Course ID format" });
            }

            var findCourse = _db.Courses.Find(courseGuid);

            if (findCourse == null) {
                return BadRequest(new { Message = $"Course with id {req.CourseId} not existed or removed" });
            }

            var modules = _db.Modules
                .Where(m => m.CourseId == findCourse.Id)
                .Select(m => new {
                    m.Id,
                    m.Title,
                    m.PreModuleId,
                    m.NextModuleId,
                    m.CourseId,
                    CourseName = m.Course.Title
                })
                .ToList();

            // Sắp xếp modules theo thứ tự yêu cầu
            var sortedModules = new List<object>();

            // 1. Tìm module đầu tiên (PreModuleId = null và có NextModuleId)
            var firstModule = modules.FirstOrDefault(m => m.PreModuleId == null && m.NextModuleId != null);
            if (firstModule != null) {
                sortedModules.Add(firstModule);
                var currentModule = firstModule;

                // 2. Thêm các module ở giữa theo thứ tự liên kết
                while (currentModule.NextModuleId != null) {
                    var nextModule = modules.FirstOrDefault(m => m.Id == currentModule.NextModuleId);
                    if (nextModule == null) break;

                    sortedModules.Add(nextModule);
                    currentModule = nextModule;
                }
            }

            // 3. Thêm các module có NextModuleId = null và có PreModuleId (các module cuối chuỗi)
            var endModules = modules
                .Where(m => m.NextModuleId == null && m.PreModuleId != null && !sortedModules.Contains(m))
                .ToList();
            sortedModules.AddRange(endModules);

            // 4. Thêm các module độc lập (cả PreModuleId và NextModuleId đều null)
            var independentModules = modules
                .Where(m => m.PreModuleId == null && m.NextModuleId == null)
                .ToList();
            sortedModules.AddRange(independentModules);

            // Thêm các module còn lại nếu có (để đảm bảo không bỏ sót module nào)
            var remainingModules = modules
                .Where(m => !sortedModules.Contains(m))
                .ToList();
            sortedModules.AddRange(remainingModules);

            return Ok(sortedModules);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public IActionResult Remove([FromBody] RemoveModuleRequest req) {
            var foundedModule = _db.Modules.Find(req.ModuleId);

            if (foundedModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }

            if (foundedModule.PreModuleId == null && foundedModule.NextModuleId != null) {
                var nextModule = _db.Modules.Find(foundedModule.NextModuleId);
                if (nextModule == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                nextModule.PreModuleId = null;
                _db.Update(nextModule);
            }

            if (foundedModule.PreModuleId != null && foundedModule.NextModuleId == null) {
                var prevModule = _db.Modules.Find(foundedModule.PreModuleId);
                if (prevModule == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                prevModule.NextModuleId = null;
                _db.Update(prevModule);
            }

            if (foundedModule.PreModuleId != null && foundedModule.NextModuleId != null) {
                var prevModule = _db.Modules.Find(foundedModule.PreModuleId);
                var nextModule = _db.Modules.Find(foundedModule.NextModuleId);
                if (prevModule == null || nextModule == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                prevModule.NextModuleId = nextModule.Id;
                nextModule.PreModuleId = prevModule.Id;
                _db.Update(prevModule);
                _db.Update(nextModule);
            }

            _db.SaveChanges();

            return Ok(new { Message = "Remove module successfully" });
        }


        /*
         - Khi set[new item] len dau tien thi:
            1.set prev[new item] = Null, next[new item] = Id[old first item]
            2.set prev[old first item] = Id[new item]
         - Khi set item vao giua thi:
            1.set prev[new item] = id[target prev item], next[new item] = id[target next item]
            2.set next[target prev item] = id[new item], prev[target next item] = id[new item]
         - Khi set item vao cuoi thi
            1.set next[new item] = Null, set prev[new item] = id[old last item]
        */
        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public IActionResult Update([FromBody] UpdateModuleRequest req) {
            var findModule = _db.Modules.Find(req.ModuleId);
            if (findModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }

            if (req.PrevModuleId != null) {
                var prevModule = _db.Modules.Find(req.PrevModuleId);
                if (prevModule == null) {
                    return BadRequest(new { Message = $"Module with id {req.PrevModuleId} not existed or removed" });
                }
            }

            if (req.NextModuleId != null) {
                var nextModule = _db.Modules.Find(req.NextModuleId);
                if (nextModule == null) {
                    return BadRequest(new { Message = $"Module with id {req.NextModuleId} not existed or removed" });
                }
            }

            // Cập nhật các liên kết cũ trước khi thay đổi
            // Xóa liên kết từ module trước đó (nếu có)
            if (findModule.PreModuleId != null) {
                var oldPrevModule = _db.Modules.Find(findModule.PreModuleId);
                if (oldPrevModule != null) {
                    oldPrevModule.NextModuleId = null;
                    _db.Update(oldPrevModule);
                }
            }

            // Xóa liên kết từ module tiếp theo (nếu có)
            if (findModule.NextModuleId != null) {
                var oldNextModule = _db.Modules.Find(findModule.NextModuleId);
                if (oldNextModule != null) {
                    oldNextModule.PreModuleId = null;
                    _db.Update(oldNextModule);
                }
            }

            // Cập nhật thông tin module hiện tại
            findModule.Title = req.Title;
            findModule.PreModuleId = req.PrevModuleId;
            findModule.NextModuleId = req.NextModuleId;
            _db.Update(findModule);

            // Cập nhật các liên kết mới
            // Trường hợp 1: Chuyển lên đầu (PrevModuleId = null, NextModuleId != null)
            if (req.PrevModuleId == null && req.NextModuleId != null) {
                var newNextModule = _db.Modules.Find(req.NextModuleId);
                if (newNextModule != null) {
                    newNextModule.PreModuleId = findModule.Id;
                    _db.Update(newNextModule);
                }
            }
            // Trường hợp 2: Chuyển vào giữa (PrevModuleId != null và NextModuleId != null)
            else if (req.PrevModuleId != null && req.NextModuleId != null) {
                var newPrevModule = _db.Modules.Find(req.PrevModuleId);
                var newNextModule = _db.Modules.Find(req.NextModuleId);

                if (newPrevModule != null) {
                    newPrevModule.NextModuleId = findModule.Id;
                    _db.Update(newPrevModule);
                }

                if (newNextModule != null) {
                    newNextModule.PreModuleId = findModule.Id;
                    _db.Update(newNextModule);
                }
            }
            // Trường hợp 3: Chuyển xuống cuối (PrevModuleId != null, NextModuleId = null)
            else if (req.PrevModuleId != null && req.NextModuleId == null) {
                var newPrevModule = _db.Modules.Find(req.PrevModuleId);
                if (newPrevModule != null) {
                    newPrevModule.NextModuleId = findModule.Id;
                    _db.Update(newPrevModule);
                }
            }
            // Trường hợp 4: Module độc lập (PrevModuleId = null, NextModuleId = null)
            // Không cần làm gì thêm vì đã set cả prev và next là null ở trên

            _db.SaveChanges();

            return Ok(new { Message = "Update module successfully" });
        }
    }
}
