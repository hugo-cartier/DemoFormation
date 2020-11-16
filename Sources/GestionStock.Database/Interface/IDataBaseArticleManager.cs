using GestionStock.Database.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Database.Interface
{
    public interface IDataBaseArticleManager
    {
        List<DataBaseArticle> Load();

        void AjouterArticle(string reference, string designation, float prixVente, float qteStock, bool sommeil);

        void AfficherArticles();

        DataBaseArticle RechercheParRef(string reference);

        void ViderArticle();

        void SupprimerParRef(string reference);

        void ModifierParRef(string reference, string designation = null, float? prixVente = null, float? qteStock = null);

        List<DataBaseArticle> RechercheParIntervallePrix(float min, float max);
    }
}
