using Snake.GameStates;
using System.Windows.Forms;

namespace Snake.Controllers
{
    public class CreditsController
    {
        #region Public Methods

        public void FinalizeController()
        {
            GameWindow.GetGameWindow().MouseClick -= MouseClick;
        }

        public void InitializeController()
        {
            GameWindow.GetGameWindow().MouseClick += MouseClick;
        }

        #endregion

        #region SignedEvents

        private void MouseClick(object sender, MouseEventArgs e)
        {
            GameWindow.GetGameWindow().GameStatesManager.ChangeToState(GameStatesManager.END_GAME_STATE);
        }

        #endregion
    }
}
