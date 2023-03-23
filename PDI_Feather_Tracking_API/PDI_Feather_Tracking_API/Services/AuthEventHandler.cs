﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
/// <summary>
/// Singleton class handler of events related to JWT authentication
/// </summary>
public class AuthEventsHandler : JwtBearerEvents
{
    private const string BearerPrefix = "Bearer ";

    private AuthEventsHandler() => OnMessageReceived = MessageReceivedHandler;

    /// <summary>
    /// Gets single available instance of <see cref="AuthEventsHandler"/>
    /// </summary>
    public static AuthEventsHandler Instance { get; } = new AuthEventsHandler();

    private Task MessageReceivedHandler(MessageReceivedContext context)
    {
        if (context.Request.Headers.TryGetValue("AccessToken", out StringValues headerValue))
        {
            string token = headerValue;
            if (!string.IsNullOrEmpty(token) && token.StartsWith(BearerPrefix))
            {
                token = token.Substring(BearerPrefix.Length);
            }

            context.Token = token;
        }

        return Task.CompletedTask;
    }
}