using DepartmentApp.Controller;
using Terminal.Gui;

Application.Run<LoginWindow>();

System.Console.WriteLine($"Username: {((LoginWindow)Application.Top).usernameText.Text}");

Application.Shutdown();