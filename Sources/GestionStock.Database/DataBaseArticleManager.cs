using GestionStock.Database.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Database
{
    public class DatabaseArticleManager : IDatabaseArticleManager
    {
        public DatabaseArticleManager()
        {
        }

        public List<DatabaseArticle> Load()
        {
            using(var context = new DBProdEntities())
            {
                return context.DatabaseArticles.ToList();
            }
        }

        public void AjouterArticle(string reference, string designation, decimal prixVente, decimal qteStock, bool sommeil)
        {
            if (reference == null)
                throw new Exception("Reference nulle impossible.");
            if (designation == null || designation.Length < 1)
                throw new Exception("Designation vide impossible.");
            if (prixVente < 0)
                throw new Exception("Prix de vente inférieur à 0 impossible.");
            if (qteStock < 0)
                throw new Exception("Quantité stock inférieur à 0 impossible.");

            using (var context = new DBProdEntities())
            {
                context.DatabaseArticles.Add(new DatabaseArticle()
                {
                    Reference = reference,
                    Designation = designation,
                    PrixVente = prixVente,
                    QteStock = qteStock,
                    Sommeil = sommeil,
                });

                context.SaveChanges();
            }
        }

        public void AfficherArticles()
        {
            using (var context = new DBProdEntities())
            {
                foreach (var LigneArticle in context.DatabaseArticles)
                {
                    Console.WriteLine(LigneArticle.ToString());
                }
            }
        }

        public DatabaseArticle RechercheParRefExacte(string reference)
        {
            using (var context = new DBProdEntities())
            {
                var temp = context.DatabaseArticles.Where(r => r.Reference == reference);
                var result = temp.FirstOrDefault();
                return result;
            }
        }

        public List<DatabaseArticle> RechercheParRefApproximative(string reference)
        {
            using (var context = new DBProdEntities())
            {
                return context.DatabaseArticles.Where(r => r.Reference.Contains(reference)).ToList();
            }
        }

        public void ViderArticle()
        {
            using (var context = new DBProdEntities())
            {
                context.DatabaseArticles.RemoveRange(context.DatabaseArticles);
            }
        }

        public void SupprimerParRef(string reference)
        {
            using (var context = new DBProdEntities())
            {
                var temp = context.DatabaseArticles.Where(r => r.Reference == reference);
                context.DatabaseArticles.RemoveRange(temp);
            }
        }

        public void ModifierParRef(string reference, string designation = null, decimal? prixVente = null, decimal? qteStock = null)
        {
            if (prixVente < 0)
                throw new Exception("Prix de vente inférieur à 0 impossible.");
            if (qteStock < 0)
                throw new Exception("Quantité stock inférieur à 0 impossible.");

            using (var context = new DBProdEntities())
            {
                var temp = context.DatabaseArticles.Where(r => r.Reference == reference);
                var result = temp.FirstOrDefault();
                if(result != null)
                {
                    result.Designation = designation ?? result.Designation;
                    result.PrixVente = prixVente ?? result.PrixVente;
                    result.QteStock = qteStock ?? result.QteStock;
                }
                context.SaveChanges();
            }
        }

        public List<DatabaseArticle> RechercheParIntervallePrix(decimal min, decimal max)
        {
            using (var context = new DBProdEntities())
            {
                var temp = context.DatabaseArticles.Where(p => p.PrixVente >= min && p.PrixVente <= max);
                return temp.ToList();
            }
        }

        public void SetDataBaseTest()
        {
            //ListeArticles.Add(new DatabaseArticle("4700", "Oeufs", 1.5f, 180));
            //ListeArticles.Add(new DatabaseArticle("4700", "Oeufs test", 5f, 1000));
            //ListeArticles.Add(new DatabaseArticle("9000", "Lait de riz", 1.2f, 780));
            //ListeArticles.Add(new DatabaseArticle("0096", "Pizza", 3f, 500));
        }
    }
}
