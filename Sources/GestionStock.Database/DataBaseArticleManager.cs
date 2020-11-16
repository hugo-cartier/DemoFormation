using GestionStock.Database.Interface;
using GestionStock.Database.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Database
{
    public class DataBaseArticleManager : IDataBaseArticleManager
    {
        List<DataBaseArticle> ListeArticles = new List<DataBaseArticle>()
        {
            new DataBaseArticle("4700", "Oeufs", 1.5f, 180, false),
            new DataBaseArticle("4700", "Oeufs test", 5f, 1000, false),
            new DataBaseArticle("9000", "Lait de riz", 1.2f, 780, false),
            new DataBaseArticle("0096", "Pizza", 3f, 500, false),
            new DataBaseArticle("0097", "Pizza 4 fromages", 3.1f, 500, false),
            new DataBaseArticle("0098", "Pizza bacon", 3.3f, 500, false),
            new DataBaseArticle("0099", "Pizza 3 fromages", 3.1f, 500, false),
            new DataBaseArticle("0100", "Pizza chorizo", 3.2f, 500, false),
            new DataBaseArticle("0666", "Pizza Ananas", 4.0f, 500, false),
            new DataBaseArticle("0101", "Pizza saumon", 4.2f, 500, false),
            new DataBaseArticle("0103", "Yaourt vanille", 1.2f, 500, false),
            new DataBaseArticle("0105", "Yaourt chocolat", 1.3f, 500, false),
            new DataBaseArticle("0106", "Yaourt pistache", 1.3f, 500, false),
        };

        public DataBaseArticleManager()
        {
        }

        public List<DataBaseArticle> Load()
        {
            // Connection à la BDD
            SetDataBaseTest();
            return ListeArticles;
        }

        public void AjouterArticle(string reference, string designation, float prixVente, float qteStock, bool sommeil)
        {
            if (reference == null)
                throw new Exception("Reference nulle impossible.");
            if (designation == null || designation.Length < 1)
                throw new Exception("Designation vide impossible.");
            if (prixVente < 0)
                throw new Exception("Prix de vente inférieur à 0 impossible.");
            if (qteStock < 0)
                throw new Exception("Quantité stock inférieur à 0 impossible.");
            ListeArticles.Add(new DataBaseArticle(reference, designation, prixVente, qteStock, sommeil));
        }

        public void AfficherArticles()
        {
            foreach (var LigneArticle in ListeArticles)
            {
                Console.WriteLine(LigneArticle.ToString());
            }
        }

        public DataBaseArticle RechercheParRef(string reference)
        {
            var temp = ListeArticles.Where(r => r.Reference == reference);
            var result = temp.FirstOrDefault();
            return result;
        }

        public void ViderArticle()
        {
            ListeArticles.Clear();
        }

        public void SupprimerParRef(string reference)
        {
            var temp = ListeArticles.Where(r => r.Reference == reference);
            var temp2 = temp.ToList();
            foreach (var item in temp2)
            {
                ListeArticles.Remove(item);
            }
            //MonStock.RemoveAll(r => r.Reference == reference);
        }

        public void ModifierParRef(string reference, string designation = null, float? prixVente = null, float? qteStock = null)
        {
            if (prixVente < 0)
                throw new Exception("Prix de vente inférieur à 0 impossible.");
            if (qteStock < 0)
                throw new Exception("Quantité stock inférieur à 0 impossible.");
            //Recherche de ton/tes objet(s)
            var temp = ListeArticles.Where(r => r.Reference == reference);
            //Execution de la recherche et récupération des résultats
            var temp2 = temp.ToList();
            //Pour chaque résultat, j'effectue la modification de ceux-ci
            foreach (var item in temp2)
            {
                item.Designation = designation ?? item.Designation;
                item.PrixVente = prixVente ?? item.PrixVente;
                item.QteStock = qteStock ?? item.QteStock;
            }
        }

        public List<DataBaseArticle> RechercheParIntervallePrix(float min, float max)
        {
            var temp = ListeArticles.Where(p => p.PrixVente >= min && p.PrixVente <= max);
            var result = temp.ToList();
            return result;
        }

        public void SetDataBaseTest()
        {
            //ListeArticles.Add(new DataBaseArticle("4700", "Oeufs", 1.5f, 180));
            //ListeArticles.Add(new DataBaseArticle("4700", "Oeufs test", 5f, 1000));
            //ListeArticles.Add(new DataBaseArticle("9000", "Lait de riz", 1.2f, 780));
            //ListeArticles.Add(new DataBaseArticle("0096", "Pizza", 3f, 500));
        }
    }
}
