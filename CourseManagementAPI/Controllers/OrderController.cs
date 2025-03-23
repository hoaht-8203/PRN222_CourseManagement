using CourseManagement.Business.Services.IService;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
        [Authorize(Roles = Role.Role_User_Admin)]
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
    }

    public class OrderStatusUpdateModel
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }      
     
    }
}
