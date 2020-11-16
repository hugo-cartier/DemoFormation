using GestionStock.Business.Modele;
using GestionStock.Database;
using GestionStock.Database.Interface;
using GestionStock.Database.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Business
{
    public class BusinessStockManager
    {
        public IDataBaseArticleManager DbManager;

        public BusinessStockManager(IDataBaseArticleManager dbManager)
        {
            DbManager = dbManager;
        }

        public List<Article> Load()
        {
            return DbManager.Load().Select(a => new Article(a)).ToList();
        }

        // null, null, -50, -2
        public void AddArticle(string reference, string designation, float prixVente, float qteStock)
        {
            if (RechercheParRef(reference) == null)
            {
                if (reference == null)
                    throw new Exception("Reference nulle impossible.");
                if (designation == null || designation.Length < 1)
                    throw new Exception("Designation vide impossible.");
                if (prixVente < 0)
                    throw new Exception("Prix de vente inférieur à 0 impossible.");
                if (qteStock < 0)
                    throw new Exception("Quantité stock inférieur à 0 impossible.");

                DbManager.AjouterArticle(reference, designation, prixVente, qteStock);
            }
        }

        public void AfficherArticles()
        {
            DbManager.AfficherArticles();
        }

        public Article RechercheParRef(string reference)
        {
            DataBaseArticle dbTempArticle = DbManager.RechercheParRef(reference);
            if (dbTempArticle == null)
                return null;
            else
                return new Article(dbTempArticle);
        }

        public void ViderArticle()
        {
            DbManager.ViderArticle();
        }

        public void SupprimerParRef(string reference)
        {
            DbManager.SupprimerParRef(reference);
        }

        public void ModifierParRef(string reference, string designation = null, float? prixVente = null, float? qteStock = null)
        {
            DbManager.ModifierParRef(reference, designation, prixVente, qteStock);
        }

        public List<Article> RechercheParIntervallePrix(float min, float max)
        {
            return DbManager
                .RechercheParIntervallePrix(min, max)
                .Select(a => new Article(a))
                .ToList();
        }
    }
}
