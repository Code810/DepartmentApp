using DepartmentApp.Business.Servicess;
using DepartmentApp.Domain.Models;
using DepartmentApp.Domain.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Terminal.Gui;

namespace DepartmentApp.Controller
{
    public class LoginWindow : Window
    {
        private AccountService _service { get; set; }= new AccountService();
        public TextField usernameText;

        public LoginWindow()
        {
          
            Title = "(Ctrl+Q to quit)";

            // Create input components and labels
            var usernameLabel = new Label()
            {
                Text = "Username:",
                Y=3,
                X=2
            };

            var usernameText = new TextField("")
            {
                // Position text field adjacent to the label
                X = Pos.Right(usernameLabel) + 1,
                Y=3,
                // Fill remaining horizontal space
                Width = Dim.Fill(88),
            };

            var passwordLabel = new Label()
            {
                Text = "Password:",
                X = Pos.Left(usernameLabel),
                Y = Pos.Bottom(usernameLabel) + 1
            };

            var passwordText = new TextField("")
            {
                Secret = true,
                // align with the text box above
                X = Pos.Left(usernameText),
                Y = Pos.Top(passwordLabel),
                Width = Dim.Fill(88),
            };

            // Create login button
            var btnLogin = new Button()
            {
                Text = "Login",
                Y = 4,
                // center the login button horizontally
                X = 50,
                IsDefault = true,
            };

            // When login button is clicked display a message popup
            btnLogin.Clicked += () => {
                Employee emp = _service.Login((string)passwordText.Text, (string)usernameText.Text);
                UserSession.Employee =emp;
                if (emp is null)
                {
                    MessageBox.ErrorQuery("Logging In", "Incorrect username or password", "Ok");
                    
                }
                else
                {
                        MessageBox.Query("Logging In", "User Login Successful", "Ok");
                        Application.Run<MenuWindow>();
                }
            };

            // Add the views to the Window
            Add(usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
        }
    }
}
