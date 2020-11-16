using GestionStock.Business.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGestionStock.Model
{
    public class ArticleVM
    {
        public string Reference { get; set; }
        public string Designation { get; set; }
        public decimal PrixVente { get; set; }
        public decimal QteStock { get; set; }
        public bool EstVisible { get; set; }

        //public ArticleVM(Article databaseArticle)
        //{
        //    Reference = databaseArticle.Reference;
        //    Designation = databaseArticle.Designation;
        //    PrixVente = databaseArticle.PrixVente;
        //    QteStock = databaseArticle.QteStock;
        //}

        public override string ToString()
        {
            return "Référence : " + Reference + " - Désignation : " + Designation + " - Prix vente : " + PrixVente + " - Quantité stock : " + QteStock;
        }
    }
}
