﻿using System.ComponentModel.DataAnnotations;

namespace PortfolioEye.Domain.Entities;

public class Potfolio
{
    public Guid Id { get; set; }
    
    [StringLength(450)]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    [StringLength(200)]
    [Required]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(3)]
    [Required]
    public string Currency { get; set; }
}