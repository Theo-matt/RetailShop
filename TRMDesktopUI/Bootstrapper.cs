﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using TRMDesktopUI.Helpers;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {

        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAPIHelper, APIHelper>();

            foreach (var assembly in SelectAssemblies())
            {
                assembly.GetTypes()
                        .Where(type => type.IsClass)
                        .Where(type => type.Name.EndsWith("ViewModel"))
                        .ToList()
                        .ForEach(viewModelType => _container.RegisterPerRequest(
                            viewModelType, viewModelType.ToString(), viewModelType));
            }
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync(typeof(ShellViewModel));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
