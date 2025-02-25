using System.Drawing;

using Snake.Controllers;

namespace Snake.GameStates
{
    public class MainMenuState : IGameState
    {
        #region Attributes and Properties

        public string Name
        {
            get { return GameStatesManager.MAIN_MENU_STATE; }
        }

        public Rectangle CreditsRectangle { get; private set; }
        public Rectangle StartGameRectangle { get; private set; }

        #endregion

        #region Private Fields

        private MainMenuController _controller;
        private bool _startGameSelected;
        private bool _creditsSelected;

        #endregion

        #region Private Methods

        private void RenderTitle(Graphics graphics)
        {
            var titleBrush = new SolidBrush(Color.Yellow);
            var fontBig = new Font(FontFamily.GenericSansSerif, 35);

            var halfWidth = GameWindow.GetGameWindow().Width / 2f;
            var halfHeight = GameWindow.GetGameWindow().Height / 2f;

            var title = "Snake by Scholl";
            var titleSize = graphics.MeasureString(title, fontBig);

            var titlePosition = new PointF(
                halfWidth - (titleSize.Width / 2f),
                halfHeight - (titleSize.Height / 2f) - titleSize.Height
                );            

            graphics.DrawString(title, fontBig, titleBrush, titlePosition);
        }

        private void RenderOptions(Graphics graphics)
        {
            var brush = new SolidBrush(Color.Blue);
            var highlightBrush = new SolidBrush(Color.Yellow);

            var fontSmall = new Font(FontFamily.GenericSansSerif, 15);

            var startBrush = _startGameSelected ? highlightBrush : brush;
            var creditsBrush = _creditsSelected ? highlightBrush : brush;

            var startGameString = "Start Game";
            if (StartGameRectangle.IsEmpty)
            {
                var sizeF = graphics.MeasureString(startGameString, fontSmall);
                var position = new Point(500, 400);
                StartGameRectangle = new Rectangle(position, sizeF.ToSize());
            }

            var creditsString = "Credits";
            if (CreditsRectangle.IsEmpty)
            {
                var sizeF = graphics.MeasureString(creditsString, fontSmall);
                var position = new Point(500, 430);
                CreditsRectangle = new Rectangle(position, sizeF.ToSize());
            }

            graphics.DrawString(startGameString, fontSmall, startBrush, StartGameRectangle.Location);
            graphics.DrawString(creditsString, fontSmall, creditsBrush, CreditsRectangle.Location);
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            _controller.InitializeController(this);
        }

        public void FinalizeState()
        {
            _controller = null;
            StartGameRectangle = Rectangle.Empty;
            CreditsRectangle = Rectangle.Empty;
        }

        public void InitializeState()
        {
            _controller = new MainMenuController();
            StartGameRectangle = Rectangle.Empty;
            CreditsRectangle = Rectangle.Empty;
        }

        public void LeaveState()
        {
            _controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            RenderTitle(graphics);
            RenderOptions(graphics);
        }

        public void Update(float deltaTime)
        {
        }


        public void Deselected()
        {
            _startGameSelected = false;
            _creditsSelected = false;
        }

        public void SelectStartGame()
        {
            _creditsSelected = false;
            _startGameSelected = true;
        }

        public void SelectCreditsGame()
        {
            _creditsSelected = true;
            _startGameSelected = false;
        }

        #endregion
    }
}
