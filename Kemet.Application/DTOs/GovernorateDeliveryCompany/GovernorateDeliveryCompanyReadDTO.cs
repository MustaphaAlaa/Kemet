namespace Entities.Models.DTOs;

    public class GovernorateDeliveryCompanyReadDTO
    {
        public int GovernorateDeliveryCompanyId { get; set; }
        
        public int DeliveryCompanyId { get; set; }
        
        public int GovernorateId { get; set; }
     
        /// <summary>
        /// DeliveryCost is nullable because when it automatically created it won't have value
        /// </summary>
        public decimal? DeliveryCost { get; set; }
        /// <summary>
        /// IsActive is nullable because when it automatically created it won't have value
        /// </summary>
        public bool? IsActive { get; set; }
        
    }


    public class GovernorateDeliveryCompanyDTO : GovernorateDeliveryCompanyReadDTO
    { 
        public string Name { get; set; } 
    }