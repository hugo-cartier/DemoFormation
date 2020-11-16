using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database.Interface;
using GestionStock.Database.Modele;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Test
{
    [TestClass]
    public class BusinessArticleManagerTestClass
    {
        [TestMethod]
        public void RechercheParRefTest()
        {
            //Arrange
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.RechercheParRef("4700")).Returns(() => new DataBaseArticle("4700", "", 1, 1, true));
            var aTester = new BusinessStockManager(mock.Object);

            //Act
            var trouve = aTester.RechercheParRef("4700");

            //Assert
            Assert.IsNotNull(trouve);

        }

        [TestMethod]
        public void RechercheParRefNulleTest()
        {
            //Arrange
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.RechercheParRef("4800")).Returns(() => null);
            var aTester = new BusinessStockManager(mock.Object);

            //Act
            var trouve = aTester.RechercheParRef("4800");

            //Assert
            Assert.IsNull(trouve);

        }

        [TestMethod]
        public void AjoutTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.AjouterArticle("4800", "Test", 123, 1234, true));
            mock.Setup(x => x.RechercheParRef("4800")).Returns(() => new DataBaseArticle("4800", "Test", 123, 1234, true));
            var aTester = new BusinessStockManager(mock.Object);


            aTester.AddArticle("4800", "Test", 123, 1234, true);
            var trouve = aTester.RechercheParRef("4800");


            Assert.IsNotNull(trouve);
            Assert.AreEqual(trouve.Designation, "Test");
            Assert.AreEqual(trouve.PrixVente, 123);
            Assert.AreEqual(trouve.QteStock, 1234);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutRefNulleTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            var aTester = new BusinessStockManager(mock.Object);
            aTester.AddArticle(null, "Test", 123, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutDesignationNulleTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            var aTester = new BusinessStockManager(mock.Object);
            aTester.AddArticle("4700", null, 123, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutPrixNegatifTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            var aTester = new BusinessStockManager(mock.Object);
            aTester.AddArticle("4700", "TEST", -2, 25);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutStockNegatifTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            var aTester = new BusinessStockManager(mock.Object);
            aTester.AddArticle("4700", "TEST", 10, -5);
        }

        [TestMethod]
        public void NettoyerArticleTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.RechercheParRef("4700")).Returns(() => null);
            var aTester = new BusinessStockManager(mock.Object);

            aTester.ViderArticle();
            var trouve = aTester.RechercheParRef("4700");

            mock.Verify(x => x.ViderArticle(), Times.Once());

            Assert.IsNull(trouve);
        }

        [TestMethod]
        public void SupprimerArticleTest()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.SupprimerParRef("4700"));
            var aTester = new BusinessStockManager(mock.Object);

            aTester.SupprimerParRef("4700");
            mock.Verify(x => x.SupprimerParRef("4700"), Times.Once());
        }

        [TestMethod]
        public void ModifierParRef()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.ModifierParRef("4700", "TEST MODIF", 45, 50));
            var aTester = new BusinessStockManager(mock.Object);

            aTester.ModifierParRef("4700", "TEST MODIF", 45, 50);
            mock.Verify(x => x.ModifierParRef("4700", "TEST MODIF", 45, 50), Times.Once());
        }

        [TestMethod]
        public void RechercheParIntervallePrix()
        {
            var mock = new Mock<IDataBaseArticleManager>();
            mock.Setup(x => x.RechercheParIntervallePrix(25, 50)).Returns(() => new List<DataBaseArticle>());
            var aTester = new BusinessStockManager(mock.Object);

            var maListe = aTester.RechercheParIntervallePrix(25, 50);

            Assert.AreEqual(0, maListe.Count);
            mock.Verify(x => x.RechercheParIntervallePrix(25, 50), Times.Once());
        }
    }
}
