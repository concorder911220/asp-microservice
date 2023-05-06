﻿using Microsoft.AspNetCore.Authorization;

namespace FSH.Framework.Infrastructure.Auth.OpenId;
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        Console.WriteLine(requirement.Issuer);
        Console.WriteLine(requirement.Scope);
        foreach (var item in context.User.Claims)
        {
            Console.WriteLine($"{item.Type} : {item.Issuer}");
        }
        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Split the scopes string into an array
        string[] scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)!.Value.Split(' ');
        Console.WriteLine("All User Scopes : {scopes}", scopes);
        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}