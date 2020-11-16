using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database;
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
       
        #endregion

        #region ctor
        public MainViewModel()
        {
            BusinessManager = new BusinessStockManager(new DataBaseArticleManager());
            AddStockManager = new AddStockManagerVM(this);
            ChargerStockCommand = new RelayCommand(ChargerStock);
        }
        #endregion

        #region Command Methods
        public void ChargerStock()
        {
            Articles.Clear();
            BusinessManager.Load().ForEach(c => Articles.Add(c));
        }


        #endregion





    }
}