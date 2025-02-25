using System.Drawing;

using Snake.Controllers;

namespace Snake.GameStates
{
    public class EndGameState : IGameState
    {
        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.END_GAME_STATE; }
        }

        #endregion

        #region Private Fields
        
        private int _instructionsBlink;
        private EndGameController _controller;

        #endregion

        #region Private Methods

        private void RenderEndGame(Graphics graphics)
        {
            var fontBig = new Font(FontFamily.GenericSansSerif, 35);
            var fontMedium = new Font(FontFamily.GenericSansSerif, 20);

            var blueBrush = new SolidBrush(Color.Blue);
            var yellowBrush = new SolidBrush(Color.Yellow);

            var string1 = "Congratulations!";
            var string2 = "Your score was.";
            var string3 = "Thanks for playing.";

            var size1 = graphics.MeasureString(string1, fontMedium);
            var size2 = graphics.MeasureString(string2, fontBig);
            var size3 = graphics.MeasureString(string3, fontMedium);

            var halfWidth = GameWindow.GetGameWindow().Width / 2f;
            var halfHeight = GameWindow.GetGameWindow().Height / 2f;

            var position1 = new PointF(
                halfWidth - (size1.Width / 2f),
                halfHeight - (size2.Height / 2f) - 50f - size1.Height
                );
            var position2 = new PointF(
                halfWidth - (size2.Width / 2f),
                halfHeight - (size2.Height / 2f)
                );
            var position3 = new PointF(
                halfWidth - (size3.Width / 2f),
                halfHeight + (size2.Height / 2f) + 50f
                );

            graphics.DrawString(string1, fontMedium, blueBrush, position1);
            graphics.DrawString(string2, fontBig, yellowBrush, position2);
            graphics.DrawString(string3, fontMedium, yellowBrush, position3);
        }

        private void RenderInstructions(Graphics graphics)
        {
            var fontSmall = new Font(FontFamily.GenericSansSerif, 15);
            var halfWidth = GameWindow.GetGameWindow().Width / 2f;

            if (_instructionsBlink < 250)
            {
                var redBrush = new SolidBrush(Color.Red);
                var instruction = "Click to go to menu...";
                var instructionSize = graphics.MeasureString(instruction, fontSmall);
                var position = new PointF(halfWidth - (instructionSize.Width / 2f),
                GameWindow.GetGameWindow().Height - 50f - (instructionSize.Height)
                );

                graphics.DrawString(instruction, fontSmall, redBrush, position);
            }
            else if (_instructionsBlink >= 500)
                _instructionsBlink = 0;

            _instructionsBlink++;
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            _controller.InitializeController();
        }

        public void FinalizeState()
        {
            _controller = null;
        }

        public void InitializeState()
        {
            _controller = new EndGameController();
        }

        public void LeaveState()
        {
            _controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            RenderEndGame(graphics);
            RenderInstructions(graphics);
        }

        public void Update(float deltaTime)
        {
        }

        #endregion
    }
}
