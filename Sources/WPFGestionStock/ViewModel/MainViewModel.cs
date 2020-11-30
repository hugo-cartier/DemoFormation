using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database;
using GestionStock.Database.Interface;
using Ninject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using WPFGestionStock.Model;
using WPFGestionStock.ViewModel.SubViewModels;

namespace WPFGestionStock.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        public BusinessStockManager BusinessManager { get; set; }

        public AddStockManagerVM AddStockManager { get; set; }


        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();
        #endregion

        #region Commands
        public RelayCommand ChargerStockCommand { get; set; }
        public RelayCommand<string> RechercherParRefCommand { get; set; }
       
        #endregion

        #region ctor
        public MainViewModel()
        {
            BusinessManager = App.Kernal.Get<BusinessStockManager>();
            AddStockManager = new AddStockManagerVM(this);
            ChargerStockCommand = new RelayCommand(ChargerStock);
            RechercherParRefCommand = new RelayCommand<string>(RechercherParRef);
        }
        #endregion

        #region Command Methods
        public void ChargerStock()
        {
            Articles.Clear();
            BusinessManager.Load().ForEach(c => Articles.Add(c));
        }

        public void RechercherParRef(string reference)
        {
            Articles.Clear();
            BusinessManager.RechercheParRefApproximative(reference).ForEach(c => Articles.Add(c));
        }


        #endregion





    }
}