using GestionStock.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Business.Modele
{
    public class Article
    {
        public string Reference { get; private set; }
        public string Designation { get; private set; }
        public decimal PrixVente { get; private set; }
        public decimal QteStock { get; private set; }
        public bool IsVisible { get; private set; }

        public Article(DatabaseArticle databaseArticle)
        {
            Reference = databaseArticle.Reference;
            Designation = databaseArticle.Designation;
            PrixVente = databaseArticle.PrixVente;
            QteStock = databaseArticle.QteStock;
            IsVisible = !databaseArticle.Sommeil;
        }

        public override string ToString()
        {
            return "Référence : " + Reference + " - Désignation : " + Designation + " - Prix vente : " + PrixVente + " - Quantité stock : " + QteStock + " - Visible ? " + IsVisible;
        }
    }
}
