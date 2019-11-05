
using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    using Lands.Models;

    
    public class LandViewModel
    {
        public Land Land
        {
            get;
            set;
        }

        #region Contructors
        public LandViewModel(Land land)
        {
            this.Land = land;
        }
        #endregion

    }
}
