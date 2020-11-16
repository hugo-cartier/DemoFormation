using GestionStock;
using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessStockManager MonStock = new BusinessStockManager(new DatabaseArticleManager());
            MonStock.AddArticle("4700", "Oeufs", (decimal)1.5, 180, true);
            MonStock.AddArticle("4700", "Oeufs test", (decimal)5, 1000, true);
            MonStock.AddArticle("9000", "Lait de riz", (decimal)1.2, 780, true);
            MonStock.AddArticle("0096", "Pizza", 3, 500, true);
            MonStock.AfficherArticles();
            var MaRef = MonStock.RechercheParRefApproximative("4545");
            //MonStock.SupprimerParRef("9000");
            MonStock.ModifierParRef("0096", "Pizza 3 fromages");
            var MaModif = MonStock.RechercheParRefApproximative("0096");
            Console.WriteLine(MaModif);
            MonStock.ModifierParRef("0096", null, 4);
            Console.WriteLine(MaModif);
            MonStock.ModifierParRef("0096", null, null, 1975);
            Console.WriteLine(MaModif);

            List<Article>  MonStockFiltre = MonStock.RechercheParIntervallePrix(1, 2);
            //MonStockFiltre.AfficherArticles();

            Console.ReadLine();
        }
    }
}
