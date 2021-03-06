﻿using System;
using AnorocMobileApp.Interfaces;
using AnorocMobileApp.Models;
using AnorocMobileApp.Services;
using AnorocMobileApp.Views.Navigation;
using Microsoft.Identity.Client;
using Plugin.SecureStorage;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AnorocMobileApp.Views.Forms
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>

    //[Preserve(AllMembers = true)]

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginWithSocialIconPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWithSocialIconPage" /> class.
        /// </summary>

        NavigationPage HomePage;
        private string title = "";
        private string body = "";

        public LoginWithSocialIconPage()
        {
            InitializeComponent();
           

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var signedIn = CrossSecureStorage.Current.GetValue("RememberMe");
            //var signedIn = Application.Current.Properties["RememberMe"];
            if (signedIn != null)
            {
                if (signedIn.ToString().Equals("true"))
                {
                    var name = CrossSecureStorage.Current.GetValue("Name");
                    var surname = CrossSecureStorage.Current.GetValue("Surname");
                    var email = CrossSecureStorage.Current.GetValue("Email");
                    IUserManagementService ims = App.IoCContainer.GetInstance<IUserManagementService>();
                    //Application.Current.Properties["TOKEN"] = userContext.AccessToken;

                    ims.UserLoggedIn(name, surname, email);
                    Application.Current.MainPage = new NavigationPage(new BottomNavigationPage());
                }
            }
            else
            {
                Application.Current.MainPage = new LoginWithSocialIconPage();
            }
        }

        /// <summary>
        /// Function sets Main Page to Navigation Page
        /// </summary>
        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            OnSignIn(sender,e);
        }

        private void PasswordResetButton_Clicked(object sender, EventArgs e)
        {
            OnPasswordReset();
        }

        async void OnSignIn(object sender, EventArgs e)
        {


            try
            {

                var userContext = await B2CAuthenticationService.Instance.SignInAsync();
                UpdateSignInState(userContext);
                
                if(userContext.IsLoggedOn)
                {
               
                    /*Console.WriteLine("Access Token: " + userContext.AccessToken);
                    Application.Current.Properties["TOKEN"] = userContext.AccessToken;*/
                    IUserManagementService ims =  App.IoCContainer.GetInstance<IUserManagementService>();
                    //Application.Current.Properties["TOKEN"] = userContext.AccessToken;

                    ims.UserLoggedIn(userContext.GivenName, userContext.FamilyName, userContext.EmailAddress);

                    Application.Current.MainPage = new NavigationPage(new BottomNavigationPage());

                }
                else
                {

                    Console.WriteLine("Error: Access Token is not available");
                    Application.Current.MainPage = new LoginWithSocialIconPage();

                }

            }
            catch (Exception ex)
            {
                // Checking the exception message 
                // should ONLY be done for B2C
                // reset and not any other error.
                if (ex.Message.Contains("AADB2C90118"))
                    OnPasswordReset();
                // Alert if any exception excluding user canceling sign-in dialog
                else if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }


        void UpdateSignInState(UserContext userContext)
        {
            var isSignedIn = userContext.IsLoggedOn;

            CrossSecureStorage.Current.SetValue("SignedIn", isSignedIn.ToString());
            CrossSecureStorage.Current.SetValue("RememberMe", "true");
            CrossSecureStorage.Current.SetValue("SignedInFirstTime", "true");
            CrossSecureStorage.Current.SetValue("APIKEY", userContext.AccessToken);
            CrossSecureStorage.Current.SetValue("Name", userContext.GivenName);
            CrossSecureStorage.Current.SetValue("Surname", userContext.FamilyName);
            CrossSecureStorage.Current.SetValue("Email", userContext.EmailAddress);

            //btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Sign in";

        }

        async void OnPasswordReset()
        {
            try
            {
                var userContext = await B2CAuthenticationService.Instance.ResetPasswordAsync();
                UpdateSignInState(userContext);
            }
            catch (Exception ex)
            {
                // Alert if any exception excluding user canceling sign-in dialog
                if (((ex as MsalException)?.ErrorCode != "authentication_canceled"))
                    await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

    }
}