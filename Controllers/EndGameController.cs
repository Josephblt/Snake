using Snake.GameStates;
using System.Windows.Forms;

namespace Snake.Controllers
{
    public class EndGameController
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
            GameWindow.GetGameWindow().GameStatesManager.ChangeToState(GameStatesManager.MAIN_MENU_STATE);
        }

        #endregion
    }
}
