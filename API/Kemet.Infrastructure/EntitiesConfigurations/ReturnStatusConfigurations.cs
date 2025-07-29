using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class ReturnStatusConfigurations : IEntityTypeConfiguration<ReturnStatus>
{
    public void Configure(EntityTypeBuilder<ReturnStatus> builder)
    {
        builder.HasData(GetReturnStatuses());
    }

    private List<ReturnStatus> GetReturnStatuses()
    {
        return new()
        {
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.WithDeliveryCompany,
                Name = "عند شركة الشحن",
                Description = "المندوب استلم المنتجات المرتجعة.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.InTransit,
                Name = "في الطريق",
                Description = "شركة الشحن في طريقها لإرجاع المنتجات.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.Received,
                Name = "تم الاستلام",
                Description = "تم استلام المنتجات فعليًا في مكان العمل.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.UnderInspection,
                Name = "قيد الفحص",
                Description = "يتم الآن فحص حالة المنتج المرتجع.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.Restocked,
                Name = "تمت إعادة التخزين",
                Description = "تم إرجاع المنتج إلى المخزون.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.Disposed,
                Name = "تم الإتلاف",
                Description = "تم إتلاف المنتج المرتجع.",
            },
            new ReturnStatus
            {
                ReturnStatusId = (int)enReturnStatus.Lost,
                Name = "فُقد",
                Description = "تم فقدان المنتج المرتجع.",
            },
        };
    }
}
