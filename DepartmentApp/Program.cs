using DepartmentApp.Controller;
using Terminal.Gui;

Application.Run<LoginWindow>();

System.Console.WriteLine($"Username: {((LoginWindow)Application.Top).usernameText.Text}");

// Before the application exits, reset Terminal.Gui for clean shutdown
Application.Shutdown();