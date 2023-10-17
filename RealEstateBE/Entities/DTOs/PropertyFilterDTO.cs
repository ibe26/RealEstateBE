﻿using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyFilterDTO
    {
        public string PropertyName { get; set; } = String.Empty;
        public int? PropertyType { get; set; }
        public int? PropertyListingType { get; set; }
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
    }
}
