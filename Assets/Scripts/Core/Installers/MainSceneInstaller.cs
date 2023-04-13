using Objects.Platforms;
using Objects.Score;
using Screens;
using Screens.GamePausedPopup;
using Screens.GameScreen;
using Utils;
using Zenject;

namespace Core.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
            BindViews();
            BindScreens();
        }
        
        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameController>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlatformController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnimationsController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScreenNavigationSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<global::Core.GameTime.GameTime>().AsSingle().NonLazy();
        }
        
        private void BindViews()
        {
            Container.BindInterfacesAndSelfTo<PlatformSpawnerView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BallController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
        
        private void BindScreens()
        {
            Container.BindViewAndPresenter<GameScreenView, GameScreenPresenter>();
            Container.BindViewAndPresenter<GamePausedPopupView, GamePausedPopupPresenter>();
        }
    }
}