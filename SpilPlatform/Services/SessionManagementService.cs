using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpilPlatform.Mvvm.Models;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SpilPlatform.Services
{
    public class SessionManagementService : INotifyPropertyChanged
    {
        public bool IsUserLoggedIn { get; private set; }

        private User loggedInUser;
        public User LoggedInUser
        {
            get => loggedInUser;
            set
            {
                if (loggedInUser != value)
                {
                    loggedInUser = value;
                    OnPropertyChanged(nameof(LoggedInUser));
                    OnPropertyChanged(nameof(IsUserLoggedIn));
                }
            }
        }

        public void LogIn(User user)
        {
            LoggedInUser = user;
            IsUserLoggedIn = user != null;
        }

        public void LogOut()
        {
            LoggedInUser = null;
            IsUserLoggedIn = false;
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }

        public bool IsLoggedInUserAdmin()
        {
            if(IsUserLoggedIn && LoggedInUser?.IsAdmin == true)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}