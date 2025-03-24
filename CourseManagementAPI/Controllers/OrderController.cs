using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CourseManagementDb _db;

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager, CourseManagementDb db)
        {
            _orderService = orderService;
            _userManager = userManager;
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng!");
            }
            return Ok(order);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderVm orderVm)
        {
            if (orderVm == null)
            {
                return BadRequest("Dữ liệu đơn hàng không hợp lệ!");
            }

            string emailFromClaims = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(emailFromClaims);

            var order = new Order
            {
                UserId = user.Id,
                Status = OrderStatus.Pending,
                TotalAmount = orderVm.TotalAmount,
                PurchasedPlan = orderVm.PurchasedPlan,
                VipExpirationDate = orderVm.VipExpirationDate,
                VipPrice = orderVm.VipPrice
            };

            await _orderService.AddAsync(order);
            return Ok(order);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest("Thông tin đơn hàng không hợp lệ!");
            }

            var existingOrder = await _orderService.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound("Không tìm thấy đơn hàng!");
            }

            await _orderService.UpdateAsync(order);
            return Ok(order);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng!");
            }

            await _orderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request data");
            }

            var order = await _orderService.GetByIdAsync(model.OrderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            order.Status = model.Status;
          

            await _orderService.UpdateAsync(order);
            return Ok(new { message = "Order status updated successfully" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchOrderRequest request) {
            try {
                var query = _db.Orders
                    .Include(o => o.User)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.UserEmail)) {
                    query = query.Where(o => o.User.Email.Contains(request.UserEmail));
                }

                if (request.Status.HasValue) {
                    query = query.Where(o => o.Status == request.Status.Value);
                }

                if (request.FromDate.HasValue) {
                    query = query.Where(o => o.OrderDate >= request.FromDate.Value);
                }

                if (request.ToDate.HasValue) {
                    query = query.Where(o => o.OrderDate <= request.ToDate.Value);
                }

                var orders = await query
                    .Select(o => new SearchOrderResponse {
                        Id = o.Id,
                        UserEmail = o.User.Email,
                        UserName = o.User.UserName,
                        Status = o.Status,
                        StatusName = o.Status.ToString(),
                        TotalAmount = o.TotalAmount,
                        OrderDate = o.OrderDate,
                        PurchasedPlan = o.PurchasedPlan,
                        PlanName = o.PurchasedPlan.ToString(),
                        VipExpirationDate = o.VipExpirationDate
                    })
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                return Ok(orders);
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusRequest request) {
            try {
                var order = await _db.Orders.FindAsync(request.OrderId);
                if (order == null) {
                    return NotFound(new { Error = "Order not found" });
                }

                order.Status = request.NewStatus;
                await _db.SaveChangesAsync();

                return Ok(new { Message = "Order status updated successfully" });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex.Message}" });
            }
        }
    }

    public class OrderStatusUpdateModel
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }      
     
    }
}
