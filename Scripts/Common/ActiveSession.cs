using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ActiveSession
{
    //fields
    private static string _username;

    private static SessionState _sessionState;

    private static SceneState _sceneState;

    //methods
    //log in
    public static void LogIn(string username)
    {
        _username = username;

        _sessionState = SessionState.LOGGED_IN;

        Debug.Log("Logged in as " + _username);

        SceneManager.LoadScene("UserMenu");
    }

    //log out
    public static void LogOut()
    {
        _username = "";

        _sessionState = SessionState.LOGGED_OUT;

        Debug.Log("Logged out");

        SceneManager.LoadScene("LoginScreen");
    }

    //get username
    public static string GetUsername()
    {
        return _username;
    }
}

public enum SessionState
{
    LOGGED_OUT,
    LOGGED_IN
}

public enum SceneState
{
    LOADING,
    LOADED,
}

public enum SceneType
{
    LOGIN,
    MAIN_MENU,
    MARKET,
    RACK
}
