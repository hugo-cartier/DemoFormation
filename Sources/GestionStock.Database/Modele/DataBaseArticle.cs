using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Database.Modele
{
    public class DataBaseArticle
    {
        public string Reference { get; set; }
        public string Designation { get; set; }
        public float PrixVente { get; set; }
        public float QteStock { get; set; }

        public DataBaseArticle(string reference, string designation, float prixVente, float qteStock)
        {
            Reference = reference;
            Designation = designation;
            PrixVente = prixVente;
            QteStock = qteStock;
        }
    }
}
