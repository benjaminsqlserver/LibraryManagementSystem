using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using LibraryManagementSystem.Client.ClientConstants;

namespace LibraryManagementSystem.Client.Pages
{
    public partial class AddLibraryEmployee
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public MyLibraryDBService MyLibraryDBService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            libraryEmployee = new LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee();
        }
        protected bool errorVisible;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee libraryEmployee;

        protected async Task FormSubmit()
        {
            try
            {
                //check if the email has been used to register a previous user
                IEnumerable<Server.Models.ApplicationUser> users = await Security.GetUsers();
                if(users.Any())
                {
                    Server.Models.ApplicationUser existingUser = users.FirstOrDefault(p => p.Email == libraryEmployee.EmailAddress);
                    if(existingUser!=null)
                    {
                        //email has been utilized for registration previously
                        NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = MyAppConstants.DUPLICATE_EMAIL, Detail = MyAppConstants.DUPLICATE_EMAIL_DETAIL });
                    }
                    else
                    {
                        //get application roles
                        var roles = await Security.GetRoles();
                        if(roles.Any())
                        {
                          
                            var employeeRole = roles.FirstOrDefault(r => r.Name == MyAppConstants.LIBRARY_STAFF_ROLE);
                            if(employeeRole!=null)
                            {
                                var newUser = new Server.Models.ApplicationUser ();
                                newUser.Email = libraryEmployee.EmailAddress;
                                newUser.Name = newUser.Email;
                                newUser.Password = libraryEmployee.Password;
                                newUser.ConfirmPassword = libraryEmployee.ConfirmPassword;
                                
                                newUser.Roles = new List<Server.Models.ApplicationRole>();
                                newUser.Roles.Add(employeeRole);
                                //we do not want to store user's password in unsecure location
                                libraryEmployee.Password = "Not Displayed";
                                libraryEmployee.ConfirmPassword = "Not Displayed";

                                var result = await MyLibraryDBService.CreateLibraryEmployee(libraryEmployee);
                                await Security.CreateUser(newUser);
                                DialogService.Close(libraryEmployee);
                            }
                        }
                        
                    }
                }    
                
            }
            catch (Exception ex)
            {
                //errorVisible = true;
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary="Error", Detail = ex.Message});

            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }
    }
}