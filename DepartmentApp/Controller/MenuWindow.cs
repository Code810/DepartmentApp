using DepartmentApp.Business.Interface;
using DepartmentApp.Business.Servicess;
using DepartmentApp.Domain.Models;
using DepartmentApp.Domain.Models.Helpers;
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
            var employe = UserSession.Employee;
            if (employe.Rol == Roles.Admin)
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
                new MenuItem("1", "Quit", () => Application.RequestStop())
            }),
            new MenuBarItem("Employe", new[]
            {
                new MenuItem("1", "Get All Employee",GetAllEmployee),
                new MenuItem("2 ","Creat Employe", CreatEmployee),
                new MenuItem("3", "Delete Employee by Id", DeleteEmployee),
                new MenuItem("4", "Get Employee by Id", GetEmployeById),
                new MenuItem("5", "Get all Employee by name", GetAllEmployeByName),
                new MenuItem("6", "Get all Employee by age", GetAllEmployeByAge),
                new MenuItem("7", "Update Employee", UpdateEmployee)

                // Add more methods as needed
            }),
            new MenuBarItem("Department", new[]
            {
                new MenuItem("1", "Get All Department",GetAllDepartment),
                new MenuItem("2 ","Creat Department", CreatDepartment),
                new MenuItem("3", "Delete Department ", DeleteDepartment),
                new MenuItem("4", "Get Department by Id", GetDepartmentById),
                new MenuItem("5", "Get Department by name", GetDepartmentByName),
                new MenuItem("6", "Get all Deparments by capacity", GetAllDepartmentByCapacity),
                new MenuItem("7", "Update Department", UpdateDepartment),
                new MenuItem("8", " Department capacity status", DepartmentCapacityStatus)

                // Add more methods as needed
            })

          });

                var statusBar = new StatusBar(new StatusItem[]
                {
            new StatusItem(Key.CtrlMask | Key.Q, "Quit", () => Application.RequestStop())
                });

                top.Add(menu, win, statusBar);

                Application.Run();

                #region EmployeMenuMethods
                static void GetAllEmployee()
                {
                    var employeservice = new EmployeeService();
                    var text = "";
                    foreach (var item in employeservice.GetAll())
                    {
                        text += $"Id:{item.Id} Name:{item.Name} Surname:{item.Surname} Email:{item.Email}  Age:{item.Age} Department Name:{item.department.Name} \n";
                    }
                    MessageBox.Query("Employes", text, "next");
                }

                static void CreatEmployee()
                {
                    AccountService servic = new();
                    EmployeeService employeeService = new();
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
                    var checkbox = new CheckBox("Admin")
                    {
                        X = 3,
                        Y = 16,
                    };
                    var Creat = new Button("Creat Employee")
                    {
                        X = 3,
                        Y = 18,

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
                                Age = intAge,
                                Adress = adressTextField.Text.ToString(),
                            };
                            if (checkbox.Checked)
                            {
                                employe.Rol = Roles.Admin;
                            }
                            var existemploye = employeeService.Creat(employe, departmentNameTextField.Text.ToString());
                            if (existemploye != null)
                            {
                                MessageBox.Query("Registration Info", $"Name: {nameTextField.Text} Email: {emailTextField.Text}\n successfully created", "OK");
                                
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
                    mainWindow.Add(nameLabel, nameTextField, SurnameLabel, SurnameTextField, emailLabel, emailTextField,
                        passwordLabel, passwordTextField, ageLabel, ageTextField, adressLabel, adressTextField, departmentNameLabel, departmentNameTextField, checkbox, Creat);

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
                        bool result = int.TryParse(idTextField.Text.ToString(), out int intId);
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

                    mainWindow.Add(idLabel, idTextField, delete);

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
                            MessageBox.ErrorQuery("Wrong", "Empty List", "Ok");
                        }
                    };


                    mainWindow.Add(nameLabel, nameTextField, Get);

                    top.Add(mainWindow);
                }
                static void GetAllEmployeByAge()
                {
                    var employeservice = new EmployeeService();

                    var top = Application.Top;

                    var mainWindow = new Window("Enter age")
                    {
                        X = 0,
                        Y = 1,
                        Width = Dim.Fill(),
                        Height = Dim.Fill()
                    };

                    // Create labels, text fields, and buttons
                    var AgeLabel = new Label("Age:")
                    {
                        X = 3,
                        Y = 2
                    };
                    var AgeTextField = new TextField("")
                    {
                        X = Pos.Right(AgeLabel) + 1,
                        Y = 2,
                        Width = 40
                    };

                    var Get = new Button("Get Employees")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        bool result = int.TryParse(AgeTextField.Text.ToString(), out int intAge);
                        if (result)
                        {
                            var employes = employeservice.GetAll(intAge);
                            if (employes is not null)
                            {
                                foreach (var employe in employes)
                                {
                                    MessageBox.Query("Employee", $"iD:{employe.Id} \n Name:{employe.Name} \n Surname:{employe.Surname} \n Age:{employe.Age}\n" +
                                    $"Email:{employe.Email}\n Password:{employe.Password} \n Adress:{employe.Adress} \n Department name:{employe.department.Name}", "Next");
                                }
                            }
                            else
                            {
                                MessageBox.ErrorQuery("Wrong", "Empty List", "Ok");
                            }
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Eror", "Age must be a number", "Ok");
                        }

                    };


                    mainWindow.Add(AgeLabel, AgeTextField, Get);

                    top.Add(mainWindow);
                }

                static void UpdateEmployee()
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

                                AccountService servic = new();
                                EmployeeService employeeService = new();
                                var top = Application.Top;

                                var mainWindow = new Window("Fill out the Update form ")
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

                                var nameTextField = new TextField($"{employe.Name}")
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

                                var SurnameTextField = new TextField($"{employe.Surname}")
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

                                var emailTextField = new TextField($"{employe.Email}")
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

                                var passwordTextField = new TextField($"{employe.Password}")
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

                                var ageTextField = new TextField($"{employe.Age}")
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

                                var adressTextField = new TextField($"{employe.Adress}")
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

                                var departmentNameTextField = new TextField($"{employe.department.Name}")
                                {
                                    X = Pos.Right(departmentNameLabel) + 1,
                                    Y = 14,
                                    Width = 40
                                };

                                var checkboxAdmin = new CheckBox("Admin")
                                {
                                    X = 3,
                                    Y = 16,
                                };
                                var checkboxUser = new CheckBox("User")
                                {
                                    X = Pos.Right(checkboxAdmin) + 2,
                                    Y = 16,
                                };

                                var Update = new Button("Update Employee")
                                {
                                    X = 3,
                                    Y = 18,

                                };
                                Update.Clicked += () =>
                                {
                                    bool result = int.TryParse(ageTextField.Text.ToString(), out int intAge);
                                    if (nameTextField.Text == NStack.ustring.Empty || SurnameTextField.Text == NStack.ustring.Empty || emailTextField.Text == NStack.ustring.Empty || passwordTextField.Text == NStack.ustring.Empty
                                    || ageTextField.Text == NStack.ustring.Empty || adressTextField.Text == NStack.ustring.Empty || departmentNameTextField.Text == NStack.ustring.Empty)
                                    {
                                        MessageBox.ErrorQuery("Wrong", "fill in all the cells", "Ok");
                                    }

                                    else if (!servic.PaswordandEmailChecker((string)passwordTextField.Text, (string)emailTextField.Text))
                                    {
                                        MessageBox.ErrorQuery("Wrong", "Password or Email correct", "Ok");
                                    }

                                    else if (result)
                                    {
                                        Employee employe = new()
                                        {
                                            Name = nameTextField.Text.ToString(),
                                            Surname = SurnameTextField.Text.ToString(),
                                            Email = emailTextField.Text.ToString(),
                                            Password = passwordTextField.Text.ToString(),
                                            Age = intAge,
                                            Adress = adressTextField.Text.ToString(),
                                        };
                                        if (checkboxAdmin.Checked && checkboxUser.Checked)
                                        {
                                            MessageBox.ErrorQuery("Eror", "Only one of the roles can be selected ", "Ok");
                                        }
                                        if (checkboxAdmin.Checked)
                                        {
                                            employe.Rol = Roles.Admin;
                                        }
                                        if (checkboxUser.Checked)
                                        {
                                            employe.Rol= Roles.User;
                                        }
                                           
                                        var existemploye = employeeService.Update(intId, employe, departmentNameTextField.Text.ToString());
                                        if (existemploye != null)
                                        {
                                            MessageBox.Query("Updated Info", $"Employe {nameTextField.Text} \n successfully updated", "OK");
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
                                mainWindow.Add(nameLabel, nameTextField, SurnameLabel, SurnameTextField, emailLabel, emailTextField,
                                    passwordLabel, passwordTextField, ageLabel, ageTextField, adressLabel, adressTextField, 
                                    departmentNameLabel, departmentNameTextField,checkboxAdmin,checkboxUser, Update);

                                top.Add(mainWindow);

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
                #endregion

                #region EmployeMenuMethods
                static void GetAllDepartment()
                {
                    var departmentservice = new DepartmentService();
                    var text = "";
                    foreach (var item in departmentservice.GetAll())
                    {
                        text += $"Id:{item.Id} Department name:{item.Name} Capacity:{item.Capacity} \n";
                    }
                    MessageBox.Query("Departments", text, "next");
                }

                static void CreatDepartment()
                {
                 DepartmentService departmentService = new();
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
                    var departmentNameLabel = new Label("Department Name:")
                    {
                        X = 3,
                        Y = 2
                    };

                    var departmentNameTextField = new TextField("")
                    {
                        X = Pos.Right(departmentNameLabel) + 2,
                        Y = 2,
                        Width = 40
                    };
                    var DepartmentCapacityLabel = new Label("Capacity:")
                    {
                        X = 3,
                        Y = 4
                    };

                    var DepartmentCapacityTextField = new TextField("")
                    {
                        X = Pos.Right(DepartmentCapacityLabel) + 9,
                        Y = 4,
                        Width = 40
                    };

                    var Creat = new Button("Creat Department")
                    {
                        X = 3,
                        Y = 16,

                    };
                    Creat.Clicked += () =>
                    {
                        bool result = int.TryParse(DepartmentCapacityTextField.Text.ToString(), out int intCapacity);
                        if (departmentNameTextField.Text == NStack.ustring.Empty || DepartmentCapacityTextField.Text == NStack.ustring.Empty)
                        {
                            MessageBox.ErrorQuery("Wrong", "fill in all the cells", "Ok");
                        }

                        else if (result)
                        {
                            Department department = new()
                            {
                                Name = departmentNameTextField.Text.ToString(),
                                Capacity = intCapacity,
                            };
                            var existdepartment = departmentService.Creat(department);
                            if (existdepartment != null)
                            {
                                MessageBox.Query("Registration Info", $"Name: {departmentNameTextField.Text} \n successfully created", "OK");
                            }
                            else
                            {
                                MessageBox.ErrorQuery("Eror", "Something went wrong", "Ok");
                            }
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Wrong", "Capacity should be a number only", "Ok");
                        }

                    };
                    // Add labels, text fields, and buttons to the main window
                    mainWindow.Add(departmentNameLabel, departmentNameTextField,DepartmentCapacityLabel,DepartmentCapacityTextField, Creat);

                    top.Add(mainWindow);
                }

                static void DeleteDepartment()
                {
                    DepartmentService departmentService = new();
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

                    var delete = new Button("Delete Department")
                    {
                        X = 3,
                        Y = 4,

                    };
                    delete.Clicked += () =>
                    {
                        bool result = int.TryParse(idTextField.Text.ToString(), out int intId);
                        if (result)
                        {
                            if (departmentService.Delete(intId) is not null)
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

                    mainWindow.Add(idLabel, idTextField, delete);

                    top.Add(mainWindow);

                }

                static void GetDepartmentById()
                {
                    var departmentService = new DepartmentService();

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

                    var Get = new Button("Get Department")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        bool result = int.TryParse(idTextField.Text.ToString(), out int intId);
                        if (result)
                        {
                            var department = departmentService.Get(intId);
                            if (department is not null)
                            {
                                MessageBox.Query("Employee", $"iD:{department.Id} \n Name:{department.Name} Capacity:{department.Capacity}" , "OK");
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

                static void GetDepartmentByName()
                {
                    var departmentService = new DepartmentService();

                    var top = Application.Top;

                    var mainWindow = new Window("Enter department name")
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

                    var Get = new Button("Get Department")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        var department = departmentService.Get(nameTextField.Text.ToString());
                        if (department is not null)
                        {
                            MessageBox.Query("Employee", $"iD:{department.Id} \n Name:{department.Name} Capacity:{department.Capacity}", "OK");
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Wrong", "Something went wrong", "Ok");
                        }
                    };


                    mainWindow.Add(nameLabel, nameTextField, Get);

                    top.Add(mainWindow);
                }

                static void GetAllDepartmentByCapacity()
                {
                    var departmentService = new DepartmentService();

                    var top = Application.Top;

                    var mainWindow = new Window("Enter capacity")
                    {
                        X = 0,
                        Y = 1,
                        Width = Dim.Fill(),
                        Height = Dim.Fill()
                    };

                    // Create labels, text fields, and buttons
                    var capacityLabel = new Label("Capacity:")
                    {
                        X = 3,
                        Y = 2
                    };
                    var capacityTextField = new TextField("")
                    {
                        X = Pos.Right(capacityLabel) + 1,
                        Y = 2,
                        Width = 40
                    };

                    var Get = new Button("Get Department")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        bool result = int.TryParse(capacityTextField.Text.ToString(), out int intCapacity);
                        if (result)
                        {
                            var departments = departmentService.GetAll(intCapacity);
                            if (departments is not null)
                            {
                                foreach (var department in departments)
                                {
                                    MessageBox.Query("Employee", $"iD:{department.Id} \n Department Name:{department.Name} \n Department Capacity {department.Capacity}", "Next");
                                }
                            }
                            else
                            {
                                MessageBox.ErrorQuery("Wrong", "Empty List", "Ok");
                            }
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Eror", "Capacity must be a number", "Ok");
                        }

                    };


                    mainWindow.Add(capacityLabel, capacityTextField, Get);

                    top.Add(mainWindow);
                }

                static void UpdateDepartment()
                {

                    var departmentService = new DepartmentService();

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

                    var Get = new Button("Get Department")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        bool result = int.TryParse(idTextField.Text.ToString(), out int intId);
                        if (result)
                        {
                            var department = departmentService.Get(intId);
                            if (department is not null)
                            {
                                MessageBox.Query("Employee", $"iD:{department.Id} \n Department Name:{department.Name} \n Department Capacity {department.Capacity} ", "Update");

                                AccountService servic = new();
                                 var top = Application.Top;

                                var mainWindow = new Window("Fill out the Update form ")
                                {
                                    X = 0,
                                    Y = 1, // Leave space for the menu bar
                                    Width = Dim.Fill(),
                                    Height = Dim.Fill()
                                };

                                // Create labels, text fields, and buttons
                                var nameLabel = new Label("Department Name:")
                                {
                                    X = 3,
                                    Y = 2
                                };

                                var nameTextField = new TextField($"{department.Name}")
                                {
                                    X = Pos.Right(nameLabel) + 1,
                                    Y = 2,
                                    Width = 40
                                };
                               
                                var departmentCapacityLabel = new Label("Capacity:")
                                {
                                    X = 3,
                                    Y = 4
                                };

                                var departmentCapacityTextField = new TextField($"{department.Capacity}")
                                {
                                    X = Pos.Right(departmentCapacityLabel) + 8,
                                    Y = 4,
                                    Width = 40
                                };

                                var Update = new Button("Update Department")
                                {
                                    X = 3,
                                    Y = 8,

                                };
                                Update.Clicked += () =>
                                {
                                    bool result = int.TryParse(departmentCapacityTextField.Text.ToString(), out int intCapacity);
                                    if (nameTextField.Text == NStack.ustring.Empty || departmentCapacityTextField.Text == NStack.ustring.Empty)
                                    {
                                        MessageBox.ErrorQuery("Wrong", "fill in all the cells", "Ok");
                                    }

                                    else if (result)
                                    {
                                        Department department = new()
                                        {
                                            Name = nameTextField.Text.ToString(),
                                            Capacity = intCapacity
                                        };
                                        var existemploye = departmentService.Update(intId, department);
                                        if (existemploye != null)
                                        {
                                            MessageBox.Query("Updated Info", $" {nameTextField.Text} Department \n successfully updated", "OK");
                                        }
                                        else
                                        {
                                            MessageBox.ErrorQuery("Eror", "Something went wrong", "Ok");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.ErrorQuery("Wrong", "Capacity should be a number only", "Ok");
                                    }

                                };
                                // Add labels, text fields, and buttons to the main window
                                mainWindow.Add(nameLabel, nameTextField, departmentCapacityLabel, departmentCapacityTextField, Update);

                                top.Add(mainWindow);

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
                static void DepartmentCapacityStatus()
                {
                    var departmentService = new DepartmentService();

                    var top = Application.Top;

                    var mainWindow = new Window("Enter department name")
                    {
                        X = 0,
                        Y = 1,
                        Width = Dim.Fill(),
                        Height = Dim.Fill()
                    };
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

                    var Get = new Button("Get Department Status")
                    {
                        X = 3,
                        Y = 4,

                    };
                    Get.Clicked += () =>
                    {
                        var departmentStatus = departmentService.GetDepartmentCapacityStatus(nameTextField.Text.ToString());
                        if (departmentStatus is not null)
                        {
                            MessageBox.Query("Employee", departmentStatus, "OK");
                        }
                        else
                        {
                            MessageBox.ErrorQuery("Wrong", "Something went wrong", "Ok");
                        }
                    };


                    mainWindow.Add(nameLabel, nameTextField, Get);

                    top.Add(mainWindow);
                }
                #endregion



            }
            if (employe.Rol==Roles.User)
            {
                
                EmployeeService employeeService = new();

                Title = $"Information about {employe.Name} {employe.Surname}";

                var informationLabel = new Label("If you wont to look your information please click button (LOOK):")
                {
                   Y=4,
                   X=Pos.Center(),
                };

                var btnLogin = new Button()
                {
                    Text = "LOOK",
                    Y = Pos.Bottom(informationLabel) + 1,
                    X = Pos.Center(),
                    IsDefault = true,
                };

                btnLogin.Clicked += () =>
                {
                    MessageBox.Query("Employee", $"iD:{employe.Id} \n Name:{employe.Name} \n Surname:{employe.Surname} \n Age:{employe.Age}\n" +
                                     $"Email:{employe.Email}\n Password:{employe.Password} \n Adress:{employe.Adress} \n Department name:{employe.department.Name}", "OK");
                };

                // Add the views to the Window
                Add(informationLabel, btnLogin);
            }
        }
    }
}

