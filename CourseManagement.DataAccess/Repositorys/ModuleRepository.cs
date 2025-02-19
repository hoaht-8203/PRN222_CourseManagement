using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CourseManagement.Model.DTOs.ModuleDTO;

namespace CourseManagement.DataAccess.Repositorys {
    public class ModuleRepository : IModuleRepository {
        private readonly CourseManagementDb _context;

        public ModuleRepository(CourseManagementDb context)
        {
            _context = context;
        }

        public async Task CreateModule(ModuleDTO.AddModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                throw new ArgumentException("Invalid Course ID format");
            }

            var courseExists = await _context.Courses
               .AnyAsync(c => c.Id == courseGuid);

            if (!courseExists) {
                throw new ArgumentException($"Course with id {req.CourseId} not found");
            }

            var maxModuleOrder = await _context.Modules
                .Where(m => m.CourseId == courseGuid && m.Status == ModuleStatus.Active)
                .MaxAsync(m => (int?)m.Order) ?? 0;

            Module newModule = new Module() {
                Title = req.Title,
                CourseId = courseGuid,
                Order = maxModuleOrder + 1,
                Status = ModuleStatus.Active
            };

            _context.Modules.Add(newModule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModule(ModuleDTO.UpdateModuleRequest req) {
            if (!int.TryParse(req.ModuleId, out int moduleId)) {
                throw new ArgumentException("Invalid Module ID format");
            }

            var module = await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == moduleId && m.Status == ModuleStatus.Active)
                ?? throw new ArgumentException($"Module with id {req.ModuleId} not found or not active");

            if (module.Course.Status == CourseStatus.UnAvailable) {
                throw new ArgumentException("Cannot update module in an unavailable course");
            }

            // Update title
            module.Title = req.Title;

            // Handle order change if needed
            if (req.NewOrder != module.Order) {
                var modulesList = await _context.Modules
                    .Where(m => m.CourseId == module.CourseId && m.Status == ModuleStatus.Active)
                    .OrderBy(m => m.Order)
                    .ToListAsync();

                var currentOrder = module.Order ?? modulesList.Count;
                var newOrder = Math.Max(1, Math.Min(req.NewOrder, modulesList.Count));

                if (newOrder < currentOrder) {
                    foreach (var mod in modulesList) {
                        if (mod.Id == module.Id) {
                            mod.Order = newOrder;
                        } else if (mod.Order >= newOrder && mod.Order < currentOrder) {
                            mod.Order++;
                        }
                    }
                } else if (newOrder > currentOrder) {
                    foreach (var mod in modulesList) {
                        if (mod.Id == module.Id) {
                            mod.Order = newOrder;
                        } else if (mod.Order <= newOrder && mod.Order > currentOrder) {
                            mod.Order--;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<SearchModuleResponse>> SearchModule(ModuleDTO.SearchModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                throw new ArgumentException("Invalid Course ID format");
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseGuid && c.Status != CourseStatus.UnAvailable)
                ?? throw new ArgumentException($"Course with id {courseGuid} not found");

            var modules = await _context.Modules
                .Where(m => m.CourseId == courseGuid && m.Status == ModuleStatus.Active)
                .Select(m => new SearchModuleResponse {
                    Id = m.Id,
                    Title = m.Title,
                    Order = m.Order ?? 0,
                    Status = (int) m.Status,
                    CourseId = m.CourseId.ToString()
                })
                .OrderBy(m => m.Order)
                .ToListAsync();

            return modules;
        }

        public async Task<DetailModuleResponse> DetailModule(ModuleDTO.DetailModuleRequest req) {
            var moduleDetail = await _context.Modules.FindAsync(req.ModuleId);

            if (moduleDetail == null) {
                throw new ArgumentException($"Module with id {req.ModuleId} not found or removed");
            }

            DetailModuleResponse res = new() {
                Id = moduleDetail.Id,
                Title = moduleDetail.Title,
                Order = moduleDetail.Order ?? 0,
                Status = (int) moduleDetail.Status,
                CourseId = moduleDetail.CourseId.ToString()
            };

            return res;
        }

        public async Task RemoveModule(ModuleDTO.RemoveModuleRequest req) {
            var module = await _context.Modules
                .Include(m => m.Course)
                .Include(m => m.Lessons)
                .FirstOrDefaultAsync(m => m.Id == req.ModuleId && m.Status == ModuleStatus.Active)
                ?? throw new ArgumentException($"Module with id {req.ModuleId} not found or already removed");

            if (module.Course.Status == CourseStatus.UnAvailable) {
                throw new ArgumentException("Cannot remove module from an unavailable course");
            }

            // Check if module has any active lessons
            if (module.Lessons != null && module.Lessons.Any()) {
                throw new ArgumentException("Cannot remove module that contains lessons. Please remove all lessons first.");
            }

            // Get current order
            var currentOrder = module.Order;

            // Mark module as deleted
            module.Status = ModuleStatus.Delete;
            module.Order = null;

            // Reorder remaining modules
            if (currentOrder.HasValue) {
                var modulesToReorder = await _context.Modules
                    .Where(m => m.CourseId == module.CourseId
                            && m.Status == ModuleStatus.Active
                            && m.Order > currentOrder)
                    .ToListAsync();

                foreach (var mod in modulesToReorder) {
                    mod.Order--;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
