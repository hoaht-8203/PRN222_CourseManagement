using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Http;


namespace CourseManagement.Business.Services.IService
{
    public interface IVnPayService
	{
		string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
		VnPaymentResponseModel PaymentExecute(IQueryCollection collections);


	}
}
