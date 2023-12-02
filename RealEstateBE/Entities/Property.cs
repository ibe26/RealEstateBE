﻿using RealEstateBE.Entities.DTOs.Property;
using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities
{
    public class Property
    {
        [Required, Key]
        public int PropertyID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string PropertyName { get; set; } = string.Empty;
        [Required]
        public DateTime DateListed { get; set; }
        [Required]
        public int PropertyTypeID { get; set; }
        [Required]
        public int PropertyListingTypeID { get; set; }
        [Required]
        public virtual PropertyType PropertyType { get; set; }
        [Required]
        public virtual PropertyListingType PropertyListingType { get; set; }
        [Required]
        public int PropertyPrice { get; set; }
        [Required]
        public short BedroomCount { get; set; }
        [Required]
        public short BathroomCount { get; set; }
        [Required]
        public bool Balcony { get; set; }
        [Required]
        public int GrossArea { get; set; }
        [Required]
        public int NetArea { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string District { get; set; } = string.Empty;
        [Required]
        public string Quarter { get; set; } = string.Empty;
        [Required]
        public int Dues { get; set; }
        [Required]
        public short BuildedYear { get; set; }
        [Required]
        public string HeatSystem { get; set; } = string.Empty;
        public short? Floor { get; set; }
        public short? TotalFloor { get; set; }
        public string? Description { get; set; } = string.Empty;

    }
}
