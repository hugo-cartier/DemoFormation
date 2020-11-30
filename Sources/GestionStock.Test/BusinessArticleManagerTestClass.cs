using GestionStock.Business;
using GestionStock.Business.Modele;
using GestionStock.Database;
using GestionStock.Database.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
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
        IKernel _Kernal;
        [TestInitialize]
        public void BeforeTest()
        {
            _Kernal = new StandardKernel();
            _Kernal.Bind<BusinessStockManager>().To<BusinessStockManager>();
        }

        [TestMethod]
        public void RechercheParRefApproximativeTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                var mock = new Mock<IDatabaseArticleManager>();
                mock.Setup(x => x.RechercheParRefExacte("4700")).Returns(() => new DatabaseArticle() { Reference = "4700", Designation = "", PrixVente = 1, QteStock = 1, Sommeil = true });
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            //Act
            var trouve = aTester.RechercheParRefExacte("4700");

            //Assert
            Assert.IsNotNull(trouve);

        }

        [TestMethod]
        public void RechercheParRefApproximativeNulleTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                var mock = new Mock<IDatabaseArticleManager>();
                mock.Setup(x => x.RechercheParRefApproximative("4800")).Returns(() => new List<DatabaseArticle>()); 
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            //Act
            var trouve = aTester.RechercheParRefApproximative("4800");

            //Assert
            Assert.AreEqual(0, trouve.Count);

        }

        [TestMethod]
        public void AjoutTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                var mock = new Mock<IDatabaseArticleManager>();
                mock.Setup(x => x.AjouterArticle("4800", "Test", 123, 1234, true));
                mock.Setup(x => x.RechercheParRefExacte("4800")).Returns(() => new DatabaseArticle() { Reference = "4800", Designation = "Test", PrixVente = 123, QteStock = 1234, Sommeil = true });
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            aTester.AddArticle("4800", "Test", 123, 1234, true);
            var trouve = aTester.RechercheParRefExacte("4800");


            Assert.IsNotNull(trouve);
            Assert.AreEqual(trouve.Designation, "Test");
            Assert.AreEqual(trouve.PrixVente, 123);
            Assert.AreEqual(trouve.QteStock, 1234);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutRefNulleTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context => new Mock<IDatabaseArticleManager>().Object);
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();
            aTester.AddArticle(null, "Test", 123, 0, true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutDesignationNulleTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context => new Mock<IDatabaseArticleManager>().Object);
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();
            aTester.AddArticle("4700", null, 123, 0, true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutPrixNegatifTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context => new Mock<IDatabaseArticleManager>().Object);
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();
            aTester.AddArticle("4700", "TEST", -2, 25, true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AjoutStockNegatifTest()
        {
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context => new Mock<IDatabaseArticleManager>().Object);
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();
            aTester.AddArticle("4700", "TEST", 10, -5, true);
        }

        [TestMethod]
        public void NettoyerArticleTest()
        {
            var mock = new Mock<IDatabaseArticleManager>();
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                mock.Setup(x => x.RechercheParRefApproximative("4700")).Returns(() => new List<DatabaseArticle>());
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            aTester.ViderArticle();
            var trouve = aTester.RechercheParRefApproximative("4700");

            mock.Verify(x => x.ViderArticle(), Times.Once());

            Assert.AreEqual(0, trouve.Count);
        }

        [TestMethod]
        public void SupprimerArticleTest()
        {
            var mock = new Mock<IDatabaseArticleManager>();
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                mock.Setup(x => x.SupprimerParRef("4700"));
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            aTester.SupprimerParRef("4700");
            mock.Verify(x => x.SupprimerParRef("4700"), Times.Once());
        }

        [TestMethod]
        public void ModifierParRef()
        {
            var mock = new Mock<IDatabaseArticleManager>();
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                mock.Setup(x => x.ModifierParRef("4700", "TEST MODIF", 45, 50));
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            aTester.ModifierParRef("4700", "TEST MODIF", 45, 50);
            mock.Verify(x => x.ModifierParRef("4700", "TEST MODIF", 45, 50), Times.Once());
        }

        [TestMethod]
        public void RechercheParIntervallePrix()
        {
            var mock = new Mock<IDatabaseArticleManager>();
            _Kernal.Bind<IDatabaseArticleManager>().ToMethod(context =>
            {
                mock.Setup(x => x.RechercheParIntervallePrix(25, 50)).Returns(() => new List<DatabaseArticle>());
                return mock.Object;
            });
            //Arrange
            var aTester = _Kernal.Get<BusinessStockManager>();

            var maListe = aTester.RechercheParIntervallePrix(25, 50);

            Assert.AreEqual(0, maListe.Count);
            mock.Verify(x => x.RechercheParIntervallePrix(25, 50), Times.Once());
        }
    }
}
