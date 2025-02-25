using Snake.GameStates;
using System;
using System.Windows.Forms;

namespace Snake.Controllers
{
    public class InGameController
    {
        #region Private Fields

        private InGameState _inGameState;

        #endregion

        #region Public Methods

        public void FinalizeController()
        {
            GameWindow.GetGameWindow().KeyDown -= KeyDown;
        }

        public void InitializeController(InGameState gameState)
        {
            _inGameState = gameState;
            GameWindow.GetGameWindow().KeyDown += KeyDown;
            GameWindow.GetGameWindow().KeyUp += KeyUp;
        }

        #endregion

        #region Signed Events

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GameWindow.GetGameWindow().TogglePause();
            
            if (e.KeyCode == Keys.Down) { }
            if (e.KeyCode == Keys.Left) { }
            if (e.KeyCode == Keys.Right) { }
            if (e.KeyCode == Keys.Up) { }
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GameWindow.GetGameWindow().TogglePause();

            if (e.KeyCode == Keys.Down) { }
            if (e.KeyCode == Keys.Left) { }
            if (e.KeyCode == Keys.Right) { }
            if (e.KeyCode == Keys.Up) { }
        }

        #endregion
    }
}
