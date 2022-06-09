using Services.Abstracts;
using Services.Implementations;
using Zenject;

namespace DependencyInjectionInstallers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInfoService>().To<InfoServiceV1>().AsSingle();
        }
    }
}