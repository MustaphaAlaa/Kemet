using Entities.Models.DTOs;

namespace Entities.Models.DTOs;

public class ProductWithVariantsCreateDTO
{
    //Product
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

    //For Product Variant
    public bool AllColorsHasSameSizes { get; set; }




    //If not
    public List<int> ColorsIds { get; set; }
    public List<int>? SizesIds { get; set; }

    //if AllColorsHasSameSizes is false
    public Dictionary<int, List<int>>? ColorsWithItSizes { get; set; }
}

public class ProductWithItSpecificationCreateDTOcccc
{
    //Product
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

    //For Product Variant
    public bool AllColorsHasSameSizes { get; set; }

    public bool IsStockQuantityUnified { get; set; }
    public int UnifiedStock { get; set; }

    //If not
    public List<int> ColorsIds { get; set; }
    public List<int> SizesIds { get; set; }

    //Key is the color id and value is a record of   size ids and second value is the stock quantity
    public Dictionary<int, List<SizeStockPair>> ColorsWithItSizesAndStock { get; set; }

    public IEnumerable<ProductQuantityPriceCreateDTO> ProductQuantityPriceCreateDTOs { get; set; }

    //UnitPrice
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; } = true;
}
