﻿using GestionStock.Business;
using GestionStock.Database;
using GestionStock.Database.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGestionStock
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IKernel Kernal = new StandardKernel();
        public App()
        {
            Kernal.Bind<BusinessStockManager>().To<BusinessStockManager>();
            Kernal.Bind<IDatabaseArticleManager>().To<DatabaseArticleManager>();
        }
    }
}
