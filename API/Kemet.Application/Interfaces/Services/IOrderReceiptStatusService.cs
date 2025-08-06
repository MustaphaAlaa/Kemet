using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderReceiptStatusService
    : IServiceAsync<
        OrderReceiptStatus,
        int,
        OrderReceiptStatus,
        OrderReceiptStatus,
        OrderReceiptStatus,
        OrderReceiptStatusReadDTO
    >
{
    Task<OrderReceiptStatusReadDTO> GetById(int key);
}
