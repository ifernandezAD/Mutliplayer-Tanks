using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AuthenticationWrapper 
{
    public static AuthState AuthState { get; private set; } = AuthState.NotAuthenticated;

    //public static async Task<AuthState> DoAuth(int tries = 5);
}

public enum AuthState
{
    NotAuthenticated,
    Authenticating,
    Authenticated,
    Error,
    TimeOut
}