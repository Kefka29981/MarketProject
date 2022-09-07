using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoginSystem
{
    //fields
    //path to CSV
    private static string path = "Assets/Resources/logins.csv";

    public static List<LoginData> login_data;

    //methods
    //initialize login data
    public static void Initialize()
    {
        login_data = new List<LoginData>();
        //read CSV and parse into login data
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines)
        {
            string[] split = line.Split(',');
            login_data.Add(new LoginData(split[0], split[1]));
        }
    }

    //Find login in login/password array
    public static bool CompareLoginPasswordPair(string login, string password)
    {
        foreach (LoginData data in login_data)
        {
            if (data.login == login && data.password == password)
            {
                return true;
            }
        }
        return false;
    }

    //create new account in login/password array
    public static void CreateAccount(string login, string password)
    {
        //check if login already exists
        foreach (LoginData data in login_data)
        {
            if (data.login == login)
            {
                Debug.Log("Login already exists");
                return;
            }
        }

        //check if login and password are valid
        if (login.Length < 3 || password.Length < 3)
        {
            Debug.Log("Login or password is too short");
            return;
        }

        //add new login/password pair to array
        login_data.Add(new LoginData(login, password));
        //write CSV
        string[] lines = new string[login_data.Count];
        for (int i = 0; i < login_data.Count; i++)
        {
            lines[i] = login_data[i].login + "," + login_data[i].password;
        }
        System.IO.File.WriteAllLines(path, lines);

        //create account folder
        string account_path = "Assets/Resources/accounts/" + login;
        System.IO.Directory.CreateDirectory(account_path);

        //create active session file
        string active_session_path = account_path + "/active_session.txt";
        System.IO.File.WriteAllText(active_session_path, "LOGGED_OUT");
    }

    //validate login and return new LoginData object
    public static ValidationThrow ValidateLogin(string login)
    {
        //error message string
        string error_message = "";
        bool valid = true;

        //if login is too short
        if (login.Length < 3)
        {
            //add new error message line
            error_message += "Login is too short\n";
            valid = false;
        }
        //check if login already exists
        foreach (LoginData data in login_data)
        {
            if (data.login == login)
            {
                //add new error message line
                error_message += "Login already exists\n";
                valid = false;
            }
        }

        //if symbols not from alphabet
        if (!System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
        {
            //add new error message line
            error_message += "Login contains invalid symbols\n";
            valid = false;
        }

        //login is valid
        return new ValidationThrow(valid, error_message);
    }

    //validate password and return new ValidationThrow object
    public static ValidationThrow ValidatePassword(string password)
    {
        //error message string
        string error_message = "";
        bool valid = true;

        //if password is too short
        if (password.Length < 3)
        {
            //add new error message line
            error_message += "Password is too short\n";
            valid = false;
        }

        //if symbols not from alphabet
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
        {
            //add new error message line
            error_message += "Password contains invalid symbols\n";
            valid = false;
        }
        
        /*/if someone already uses this password
        foreach (LoginData data in login_data)
        {
            if (data.password == password)
            {
                return new ValidationThrow(false, "Password already used by " + data.login);
            }
        }*/

        //password is valid
        return new ValidationThrow(valid, error_message);
    }

    
    

}

public class LoginData
{
    //fields
    public string login;
    public string password;

    //constructor
    public LoginData(string login, string password)
    {
        this.login = login;
        this.password = password;
    }
}

public class ValidationThrow
{
    //bool
    public bool isValid;

    //string with error message
    public string errorMessage;

    //constructor
    public ValidationThrow(bool isValid, string errorMessage)
    {
        this.isValid = isValid;
        this.errorMessage = errorMessage;
    }
}
