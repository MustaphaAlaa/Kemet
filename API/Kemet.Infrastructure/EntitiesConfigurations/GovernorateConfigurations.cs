using System.Diagnostics.Metrics;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class GovernorateConfigurations : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.HasData(GetGovernorates()); 
    }

    private List<Governorate> GetGovernorates()
    {
        byte id = 1;
        return new List<Governorate>
        {
            new Governorate
            {
                GovernorateId = id++,
                Name = "القاهرة",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الجيزة",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الإسكندرية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الدقهلية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "البحر الأحمر",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "البحيرة",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الفيوم",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الغربية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الشرقية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الإسماعيلية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "المنوفية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "المنيا",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "القليوبية",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الوادي الجديد",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "السويس",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "أسوان",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "أسيوط",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "بني سويف",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "بورسعيد",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "دمياط",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "كفر الشيخ",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "مطروح",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "الأقصر",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "قنا",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "سوهاج",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "جنوب سيناء",
                IsAvailableToDeliver = true,
            },
            new Governorate
            {
                GovernorateId = id++,
                Name = "شمال سيناء",
                IsAvailableToDeliver = true,
            },
        };
    }
}
