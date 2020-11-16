using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Database.Interface
{
    public interface IDatabaseArticleManager
    {
        List<DatabaseArticle> Load();

        void AjouterArticle(string reference, string designation, decimal prixVente, decimal qteStock, bool sommeil);

        void AfficherArticles();

        DatabaseArticle RechercheParRefExacte(string reference);

        List<DatabaseArticle> RechercheParRefApproximative(string reference);

        void ViderArticle();

        void SupprimerParRef(string reference);

        void ModifierParRef(string reference, string designation = null, decimal? prixVente = null, decimal? qteStock = null);

        List<DatabaseArticle> RechercheParIntervallePrix(decimal min, decimal max);
    }
}
