using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace LibraryManagementSystem.Client.Pages
{
    public partial class AddLibraryClient
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
            libraryClient = new LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient();
        }
        protected bool errorVisible;
        protected LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient libraryClient;

        protected async Task FormSubmit()
        {
            try
            {
                //we will call get user by email method

                var user = await Security.GetUserByEmail(libraryClient.EmailAddress);

                if(user!=null)
                {
                    NotificationService.Notify(NotificationSeverity.Error, ClientConstants.MyAppConstants.DUPLICATE_EMAIL, ClientConstants.MyAppConstants.DUPLICATE_EMAIL_DETAIL,ClientConstants.MyAppConstants.NOTIFICATION_DEFAULT);
                    return;
                }
                else
                {
                    var userRole = await Security.GetRoleByRoleName(ClientConstants.MyAppConstants.USER_ROLE);

                    if (userRole != null)
                    {
                        var newUser = new Server.Models.ApplicationUser();
                        newUser.Email = libraryClient.EmailAddress;
                        newUser.Name = newUser.Email;
                        newUser.Password = libraryClient.Password;
                        newUser.ConfirmPassword = libraryClient.ConfirmPassword;

                        newUser.Roles = new List<Server.Models.ApplicationRole>();
                        newUser.Roles.Add(userRole);
                        //we do not want to store user's password in unsecure location
                        libraryClient.Password = "Not Displayed";
                        libraryClient.ConfirmPassword = "Not Displayed";

                        var result = await MyLibraryDBService.CreateLibraryClient(libraryClient);
                        await Security.CreateUser(newUser);
                        DialogService.Close(libraryClient);
                    }
                    
                   
                }



            }
            catch (Exception ex)
            {
                errorVisible = true;
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