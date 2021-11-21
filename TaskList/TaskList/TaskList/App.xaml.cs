using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.IO;
using TaskList.Data;
using TaskList.ViewModels;
using TaskList.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskList
{
    public partial class App : PrismApplication
    {
        static DatabaseQuery database;

        public static DatabaseQuery Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseQuery(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataBaseToDo.db3"));
                }

                return database;
            }
        }

        public App(IPlatformInitializer init = null) : base(init) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/Index");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Index, IndexViewModel>();
        }
    }
}
