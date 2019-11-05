using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    
    class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public LandsViewModels Lands
        {
            get;
            set;
        }

        public LandViewModel Land {
            get;
            set;
        }

        #endregion

        #region Constructores

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            else {
                return instance;
            }
        }

        #endregion
    }
}