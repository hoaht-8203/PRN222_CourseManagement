using CourseManagement.Business.Services.IService;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagement.Business.Services;
using System.Security.Claims;
using CourseManagement.DataAccess.Data;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IVnPayService _vnPayService;
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly CourseManagementDb _db;

    public PaymentController(
        IVnPayService vnPayService,
        IOrderService orderService,
        IUserService userService,
        CourseManagementDb db)
    {
        _vnPayService = vnPayService;
        _orderService = orderService;
        _userService = userService;
        _db = db;
    }

    [HttpPost("create")]
    public IActionResult CreatePayment( VnPaymentRequestModel model)
    {
        var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, model);
        return Ok(paymentUrl); 
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback()
    {
        try
        {
            var vnpParams = Request.Query.ToDictionary(k => k.Key, v => v.Value.ToString());

            if (vnpParams.Count == 0)
            {
                return BadRequest(new { message = "No parameters received" });
            }

            string responseCode = vnpParams.GetValueOrDefault("vnp_ResponseCode");
            string orderInfo = vnpParams.GetValueOrDefault("vnp_OrderInfo");
            string transactionNo = vnpParams.GetValueOrDefault("vnp_TransactionNo");
            string amountStr = vnpParams.GetValueOrDefault("vnp_Amount");
            string orderIdStr = vnpParams.GetValueOrDefault("vnp_TxnRef");

            if (string.IsNullOrEmpty(orderIdStr) || !int.TryParse(orderIdStr, out int orderId))
            {
                return BadRequest(new { message = "Invalid order ID" });
            }

            if (string.IsNullOrEmpty(amountStr) || !decimal.TryParse(amountStr, out decimal amount))
            {
                return BadRequest(new { message = "Invalid amount" });
            }

            amount /= 100; // VNPay trả về số tiền nhân 100, cần chia lại

            if (responseCode == "00") // Thành công
            {
                var order = await _orderService.GetByIdAsync(orderId);
                if (order != null) {
                    order.Status = OrderStatus.Completed;
                     _db.Orders.Update(order);
                }
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail != null) {
                    var user = await _userService.GetUserByEmail(userEmail.Value);
                    if (user != null) {
                        user.VipStatus = VipStatus.Premium;
                        user.VipPrice = amount;
                        user.VipExpirationDate = order.VipExpirationDate;
                        await _userService.UpdateUser(userEmail.Value, user);
                    }

                    return Redirect($"https://localhost:7195/subscription-success?orderId={orderId}");
                } else {
                    return BadRequest($"Internal server error");
                }
            }
            else // Thanh toán thất bại
            {
                var order = await _orderService.GetByIdAsync(orderId);
                if (order != null)
                {
                    order.Status = OrderStatus.Cancelled;
                    _db.Orders.Update(order);
                }

                return Redirect($"https://localhost:7195/subscription-error?orderId={orderId}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}
