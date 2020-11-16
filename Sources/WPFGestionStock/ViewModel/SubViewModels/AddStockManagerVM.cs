using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFGestionStock.Model;

namespace WPFGestionStock.ViewModel.SubViewModels
{
    public class AddStockManagerVM : ViewModelBase
    {
        #region IsAjouterArticle property
        private bool _IsAjouterArticle = false;

        public bool IsAjouterArticle
        {
            get => _IsAjouterArticle;
            set
            {
                if (_IsAjouterArticle == value)
                    return;

                _IsAjouterArticle = value;
                RaisePropertyChanged("IsAjouterArticle");
            }
        }
        #endregion IsAjouterArticle property

        public ArticleVM ArticleAjoutVM { get; set; }

        public RelayCommand AfficherMasquerArticleStockCommand { get; set; }
        public RelayCommand ValiderArticleStockCommand { get; set; }

        private MainViewModel _Parent;

        public AddStockManagerVM(MainViewModel parent)
        {
            _Parent = parent;
            ArticleAjoutVM = new ArticleVM();
            AfficherMasquerArticleStockCommand = new RelayCommand(AfficherMasquerArticleStock);
            ValiderArticleStockCommand = new RelayCommand(ValiderArticleStock);
        }

        public void AfficherMasquerArticleStock()
        {
            IsAjouterArticle = !IsAjouterArticle;
        }

        public void ValiderArticleStock()
        {
            _Parent.BusinessManager.AddArticle(ArticleAjoutVM.Reference, ArticleAjoutVM.Designation, ArticleAjoutVM.PrixVente, ArticleAjoutVM.QteStock, ArticleAjoutVM.EstVisible);
            _Parent.ChargerStock();
            IsAjouterArticle = !IsAjouterArticle;
        }
    }
}
