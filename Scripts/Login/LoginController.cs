using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    //fields
    //text input login
    public TMP_InputField login_input;

    //text input password
    public TMP_InputField password_input;

    //text header
    public TextMeshProUGUI header;

    //text error
    public TextMeshProUGUI error;

    // Start is called before the first frame update
    void Start()
    {
        //initialize login data
        LoginSystem.Initialize();
    }

    //login button
    public void Login()
    {
        //get login and password
        string login = login_input.text;
        string password = password_input.text;

        //compare login and password
        if (LoginSystem.CompareLoginPasswordPair(login, password))
        {
            //login successful
            header.text = "Login successful";
            error.text = "";
            //set session state
            ActiveSession.LogIn(login);
        }
        else
        {
            //login failed
            header.text = "Login failed";
            error.text = "Wrong login or password";
        }

        
    }

    //create account button
    public void CreateAccount()
    {
        //get login and password
        string login = login_input.text;
        string password = password_input.text;

        //validate login and password
        ValidationThrow new_login = LoginSystem.ValidateLogin(login);
        ValidationThrow new_password = LoginSystem.ValidatePassword(password);

        //if both login and password are valid
        if (new_login.isValid && new_password.isValid)
        {
            //create account
            LoginSystem.CreateAccount(login, password);
            //login successful
            header.text = "Account created";
            error.text = "";
        }
        else
        {
            //login failed
            header.text = "Account creation failed";
            error.text = new_login.errorMessage + "\n" + "\n" + new_password.errorMessage;
        }
    }


}
