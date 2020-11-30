using GestionStock.Business.Modele;
using GestionStock.Database;
using GestionStock.Database.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Business
{
    public class BusinessStockManager
    {
        [Inject]
        public IDatabaseArticleManager DbManager { get; set; }

        public List<Article> Load()
        {
            return DbManager.Load().Select(a => new Article(a)).ToList();
        }

        // null, null, -50, -2
        public void AddArticle(string reference, string designation, decimal prixVente, decimal qteStock, bool isVisible)
        {
            if (RechercheParRefExacte(reference) == null)
            {
                if (reference == null)
                    throw new Exception("Reference nulle impossible.");
                if (designation == null || designation.Length < 1)
                    throw new Exception("Designation vide impossible.");
                if (prixVente < 0)
                    throw new Exception("Prix de vente inférieur à 0 impossible.");
                if (qteStock < 0)
                    throw new Exception("Quantité stock inférieur à 0 impossible.");

                DbManager.AjouterArticle(reference, designation, prixVente, qteStock, !isVisible);
            }
        }

        public void AfficherArticles()
        {
            DbManager.AfficherArticles();
        }

        public Article RechercheParRefExacte(string reference)
        {
            DatabaseArticle dbTempArticle = DbManager.RechercheParRefExacte(reference);
            if (dbTempArticle == null)
                return null;
            else
                return new Article(dbTempArticle);
        }

        public List<Article> RechercheParRefApproximative(string reference)
        {
            List<DatabaseArticle> dbTempListArticle = DbManager.RechercheParRefApproximative(reference);
            return dbTempListArticle.Select(c => new Article(c)).ToList();
        }

        public void ViderArticle()
        {
            DbManager.ViderArticle();
        }

        public void SupprimerParRef(string reference)
        {
            DbManager.SupprimerParRef(reference);
        }

        public void ModifierParRef(string reference, string designation = null, decimal? prixVente = null, decimal? qteStock = null)
        {
            DbManager.ModifierParRef(reference, designation, prixVente, qteStock);
        }

        public List<Article> RechercheParIntervallePrix(decimal min, decimal max)
        {
            return DbManager
                .RechercheParIntervallePrix(min, max)
                .Select(a => new Article(a))
                .ToList();
        }
    }
}
