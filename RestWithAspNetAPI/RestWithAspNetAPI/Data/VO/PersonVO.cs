﻿using RestWithAspNetAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetAPI.Data.VO;

public class PersonVo
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
}
