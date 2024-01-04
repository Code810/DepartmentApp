﻿using DepartmentApp.Business.Interface;
using DepartmentApp.Business.Servicess;
using DepartmentApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Terminal.Gui;

namespace DepartmentApp.Controller
{
    public class MenuWindow : Window
    {
        public MenuWindow()
        {
        
            var top = Application.Top;

            var win = new Window("Admin Menu")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };
            
            var menu = new MenuBar(new[]
            {
            new MenuBarItem("_File", new[]
            {
                new MenuItem("_Quit", "Quit", () => Application.RequestStop())
            }),
            new MenuBarItem("Employe", new[]
            {
                new MenuItem("1", "Get All Employee",GetAllEmployee),
                new MenuItem("2 ","Creat Employe", CreatEmployee),
                new MenuItem("3", "Delet Employee by Id", DeleteEmployee),
                new MenuItem("4", "Get Employee by Id", GetEmployeById),
                new MenuItem("5", "Get all Employee by name", GetAllEmployeByName),

                // Add more methods as needed
            })
        });

            var statusBar = new StatusBar(new StatusItem[]
            {
            new StatusItem(Key.CtrlMask | Key.Q, "Quit", () => Application.RequestStop())
            });

            top.Add(menu, win, statusBar);

            Application.Run();
        }
        static void GetAllEmployee()
        {
            var employeservice = new EmployeeService();
            var text = "";
            foreach (var item in employeservice.GetAll())
            {
                text += $"Id:{item.Id} Name:{item.Name} Surname:{item.Surname} Email:{item.Email}  Age:{item.Age} Department Name:{item.department.Name} \n";
            }
            MessageBox.Query("Employes",text , "next");
        }

        static void CreatEmployee()
        {
            AccountService servic = new();
            EmployeeService employeeService = new ();
            var top = Application.Top;

            // Create the main window
            var mainWindow = new Window("Fill out the form ")
            {
                X = 0,
                Y = 1, // Leave space for the menu bar
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            // Create labels, text fields, and buttons
            var nameLabel = new Label("Name:")
            {
                X = 3,
                Y = 2
            };

            var nameTextField = new TextField("")
            {
                X = Pos.Right(nameLabel) + 12,
                Y = 2,
                Width = 40
            };
            var SurnameLabel = new Label("Surname:")
            {
                X = 3,
                Y = 4
            };

            var SurnameTextField = new TextField("")
            {
                X = Pos.Right(SurnameLabel) + 9,
                Y = 4,
                Width = 40
            };

            var emailLabel = new Label("Email:")
            {
                X = 3,
                Y = 6
            };

            var emailTextField = new TextField("")
            {
                X = Pos.Right(emailLabel) + 11,
                Y = 6,
                Width = 40
            };
            var passwordLabel = new Label("Password:")
            {
                X = 3,
                Y = 8
            };

            var passwordTextField = new TextField("")
            {
                X = Pos.Right(passwordLabel) + 8,
                Y = 8,
                Width = 40
            };

            var ageLabel = new Label("Age:")
            {
                X = 3,
                Y = 10
            };

            var ageTextField = new TextField("")
            {
                X = Pos.Right(ageLabel) + 13,
                Y = 10,
                Width = 40
            };

            var adressLabel = new Label("Adress:")
            {
                X = 3,
                Y = 12
            };

            var adressTextField = new TextField("")
            {
                X = Pos.Right(adressLabel) + 10,
                Y = 12,
                Width = 40
            };

            var departmentNameLabel = new Label("Department name:")
            {
                X = 3,
                Y = 14
            };

            var departmentNameTextField = new TextField("")
            {
                X = Pos.Right(departmentNameLabel) + 1,
                Y = 14,
                Width = 40
            };

            var Creat = new Button("Creat Employee")
            {
                X = 3,
                Y = 16,

            };
            Creat.Clicked += () =>
            {
                bool result = int.TryParse(ageTextField.Text.ToString(), out int intAge);
                if (nameTextField.Text == NStack.ustring.Empty || SurnameTextField.Text == NStack.ustring.Empty || emailTextField.Text == NStack.ustring.Empty || passwordTextField.Text == NStack.ustring.Empty
                || ageTextField.Text == NStack.ustring.Empty || adressTextField.Text == NStack.ustring.Empty || departmentNameTextField.Text == NStack.ustring.Empty)
                {
                    MessageBox.ErrorQuery("Wrong", "fill in all the cells", "Ok");
                }
               
                else if (!servic.PaswordandEmailChecker((string)passwordTextField.Text, (string)emailTextField.Text))
                {
                    MessageBox.ErrorQuery("Wrong", "Password or Email coorect", "Ok");
                }
               
               else if (result)
                {
                    Employee employe = new()
                    {
                        Name = nameTextField.Text.ToString(),
                        Surname = SurnameTextField.Text.ToString(),
                        Email = emailTextField.Text.ToString(),
                        Password = passwordTextField.Text.ToString(),
                        Age=intAge,
                        Adress=adressTextField.Text.ToString(),
                    };
                    var existemploye=employeeService.Creat(employe,departmentNameTextField.Text.ToString());
                    if (existemploye != null)
                    {
                        MessageBox.Query("Registration Info",$"Name: {nameTextField.Text} Email: {emailTextField.Text}\n successfully created","OK");
                    }
                    else
                    {
                        MessageBox.ErrorQuery("Eror", "Something went wrong", "Ok");
                    }
                 }
                else
                {
                    MessageBox.ErrorQuery("Wrong", "Age should be a number only", "Ok");
                }



               
            
               
            };
            // Add labels, text fields, and buttons to the main window
            mainWindow.Add(nameLabel, nameTextField,SurnameLabel,SurnameTextField, emailLabel, emailTextField, 
                passwordLabel,passwordTextField,ageLabel,ageTextField,adressLabel,adressTextField,departmentNameLabel,departmentNameTextField, Creat);

            top.Add(mainWindow);
        }

        static void DeleteEmployee()
        {
            EmployeeService employeeService = new();
            var top = Application.Top;

            var mainWindow = new Window("Enter Id")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            // Create labels, text fields, and buttons
            var idLabel = new Label("Id:")
            {
                X = 3,
                Y = 2
            };
            var idTextField = new TextField("")
            {
                X = Pos.Right(idLabel) + 1,
                Y = 2,
                Width = 40
            };

            var delete = new Button("Delete Employee")
            {
                X = 3,
                Y = 4,

            };
            delete.Clicked += () =>
            {
                bool result = int.TryParse(idTextField.Text.ToString(),out int intId);
                if (result)
                {
                    if (employeeService.Delete(intId) is not null)
                    {
                        MessageBox.Query("successful", "Employee deleted", "OK");
                    }
                    else
                    {
                        MessageBox.ErrorQuery("Wrong", "Something went wrong", "Ok");
                    }
                }
                else
                {
                    MessageBox.ErrorQuery("Eror", "Id must be a number", "Ok");
                }
            };

            mainWindow.Add(idLabel,idTextField,delete);

            top.Add(mainWindow);

        }

        static void GetEmployeById()
        {
            var employeservice = new EmployeeService();

            var top = Application.Top;

            var mainWindow = new Window("Enter Id")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            // Create labels, text fields, and buttons
            var idLabel = new Label("Id:")
            {
                X = 3,
                Y = 2
            };
            var idTextField = new TextField("")
            {
                X = Pos.Right(idLabel) + 1,
                Y = 2,
                Width = 40
            };

            var Get = new Button("Get Employee")
            {
                X = 3,
                Y = 4,

            };
            Get.Clicked += () =>
            {
                bool result = int.TryParse(idTextField.Text.ToString(), out int intId);
                if (result)
                {
                    var employe = employeservice.Get(intId);
                    if (employe is not null)
                    {
                        MessageBox.Query("Employee", $"iD:{employe.Id} \n Name:{employe.Name} \n Surname:{employe.Surname} \n Age:{employe.Age}\n" +
                            $"Email:{employe.Email}\n Password:{employe.Password} \n Adress:{employe.Adress} \n Department name:{employe.department.Name}", "OK");
                    }
                    else
                    {
                        MessageBox.ErrorQuery("Wrong", "Something went wrong", "Ok");
                    }
                }
                else
                {
                    MessageBox.ErrorQuery("Eror", "Id must be a number", "Ok");
                }
            };

            mainWindow.Add(idLabel, idTextField, Get);

            top.Add(mainWindow);
        }

        static void GetAllEmployeByName()
        {
            var employeservice = new EmployeeService();

            var top = Application.Top;

            var mainWindow = new Window("Enter name")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            // Create labels, text fields, and buttons
            var nameLabel = new Label("Name:")
            {
                X = 3,
                Y = 2
            };
            var nameTextField = new TextField("")
            {
                X = Pos.Right(nameLabel) + 1,
                Y = 2,
                Width = 40
            };

            var Get = new Button("Get Employee")
            {
                X = 3,
                Y = 4,

            };
            Get.Clicked += () =>
            {
                var employes = employeservice.GetAll(nameTextField.Text.ToString());
                if (employes is not null)
                {
                    foreach (var employe in employes)
                    {
                        MessageBox.Query("Employee", $"iD:{employe.Id} \n Name:{employe.Name} \n Surname:{employe.Surname} \n Age:{employe.Age}\n" +
                        $"Email:{employe.Email}\n Password:{employe.Password} \n Adress:{employe.Adress} \n Department name:{employe.department.Name}", "OK");
                    }
                }
                else
                {
                    MessageBox.ErrorQuery("Wrong", "Something went wrong", "Ok");
                }
            };
        

            mainWindow.Add(nameLabel, nameTextField, Get);

            top.Add(mainWindow);
        }
    }
}
