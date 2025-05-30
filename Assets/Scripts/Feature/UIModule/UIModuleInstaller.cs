using System;
using System.Collections.Generic;
using ShootingCar.AddressablesAddress;
using Zenject;

namespace ShootingCar.Feature.UIModule
{
    public class UIModuleInstaller : Installer<UIModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIService>().To<UIService>().AsSingle()
                .WithArguments(new Dictionary<Type, UIConfig> {
                    { typeof(WinUI.WinUI), new UIConfig(Address.UI.WinUI, 0) },
                    { typeof(LoseUI.LoseUI), new UIConfig(Address.UI.LoseUI, 1) }
                });
        }
    }
}
