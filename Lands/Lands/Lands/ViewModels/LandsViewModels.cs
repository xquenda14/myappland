using System;
using System.Text;

namespace Lands.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Lands.Services;
    using Models;
    using Xamarin.Forms;

    public class LandsViewModels : BaseViewModel
    {

        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes

        //private List<Land> landsList { get; set; }
        private ObservableCollection<LandItemViewModel> lands;

        #endregion

        #region Properties
        
        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }


        private bool isRefreshing;

        public bool IsRefreshing {

            get {
                return this.isRefreshing;
            }

            set {
                SetValue(ref this.isRefreshing, value);
            }
        }

        private string filter;

        public string Filter
        {

            get{ return this.filter; }
            set{
                SetValue(ref this.filter, value);
                Search();
            }
        }

        #endregion
        
        #region Constructor
        public LandsViewModels()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }

        #endregion

        #region Metodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu", 
                "/rest", 
                "/v2/all");

            
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
            
            this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            this.IsRefreshing = false;

        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {

            return MainViewModel.GetInstance().LandsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations
            });

        }

        private void Search()
        {

            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            }
            else {
                    this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower()))
                        );
            }
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand {
            get
            {
                return new RelayCommand(LoadLands);
            }
                }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        
        #endregion
    }
}
