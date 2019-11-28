using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;


    class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Propiedades
        public string Email {
            get {
                return this.email;
            }
            set {
                if (this.email != value)
                {
                    this.email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Email)));
                }
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Password)));
                }
            }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsRunning)));
                }

            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }

            }
        }

        public bool IsRemembered { get; set; }
        
        #endregion

     

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = "xquenda14@yahoo.com.mx";
            this.Password = "1234";
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }



        private async void Login()
        {
            IsEnabled = false;
            IsRunning = true;
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tu debe de capturar un email", "Accept");

                IsEnabled = true;
                IsRunning = false;

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tu debe de capturar el password", "Accept");
                IsEnabled = true;
                IsRunning = false;

                return;
            }

            if (this.Email != "xquenda14@yahoo.com.mx" || this.Password != "1234")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El email ó password son incorrectos", "Accept");

                IsEnabled = true;
                IsRunning = false;

                this.Password = string.Empty;
                return;
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Exitoso", "El acceso es exitoso", "Accept");

                IsEnabled = true;
                IsRunning = false;

                this.Email = string.Empty;
                this.Password = string.Empty;

                MainViewModel.GetInstance().Lands = new LandsViewModels();

                await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
                return;
            }

        }



        #endregion




    }
}
