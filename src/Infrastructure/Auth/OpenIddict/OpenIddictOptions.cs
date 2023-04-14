﻿using FSH.Microservices.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace FSH.Microservices.Infrastructure.Auth.OpenIddict;

public class OpenIddictOptions : IOptionsRoot
{
    [Required(AllowEmptyStrings = false)]
    public string? ClientId { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    public string? ClientSecret { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    public string? IssuerUrl { get; set; } = string.Empty;
}
