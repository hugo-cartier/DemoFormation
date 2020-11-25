using GestionStock;
using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }

            BusinessStockManager MonStock = new BusinessStockManager(new DataBaseArticleManager());
            MonStock.AddArticle("4700", "Oeufs", 1.5f, 180, true);
            MonStock.AddArticle("4700", "Oeufs test", 5f, 1000, true);
            MonStock.AddArticle("9000", "Lait de riz", 1.2f, 780, true);
            MonStock.AddArticle("0096", "Pizza", 3f, 500, true);
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
