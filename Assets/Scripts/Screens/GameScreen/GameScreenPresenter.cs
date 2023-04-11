using Unity.VisualScripting;

namespace Screens.GameScreen
{
    public class GameScreenPresenter : IInitializable
    {
        private readonly GameScreenView _view;

        public GameScreenPresenter(GameScreenView view)
        {
            _view = view;
        }

        public void Initialize()
        {
        }
    }
}