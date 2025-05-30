using System;
using System.Collections.Generic;
using AddressablesAddress;
using Zenject;

namespace Feature.UI
{
    public class UIModuleInstaller : Installer<UIModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIService>().To<UIService>().AsSingle()
                .WithArguments(new Dictionary<Type, UIConfig> {
                    { typeof(WinUI), new UIConfig(Address.UI.WinUI, 0) },
                    { typeof(LoseUI), new UIConfig(Address.UI.LoseUI, 1) }
                });
        }
    }
}
