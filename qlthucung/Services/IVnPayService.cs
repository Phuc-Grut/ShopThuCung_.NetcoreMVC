using Microsoft.AspNetCore.Http;
using qlthucung.Models;

namespace qlthucung.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}