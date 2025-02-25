using System.Drawing;

using Snake.Controllers;
using Snake.GameObjects;

namespace Snake.GameStates
{
    public class InGameState : IGameState
    {
        #region Constructor

        public InGameState()
        {
        }

        #endregion

        #region Attributes and Properties

        private InGameController _controller;
        private InGameController Controller
        {
            get
            {
                if (_controller == null)
                    _controller = new InGameController();
                return _controller;
            }
        }
        
        private Field _field;
        public Field Field
        {
            get { return _field; }
        }

        private FoodManager _foodManager;
        public FoodManager FoodManager
        {
            get { return _foodManager; }
        }

        private bool _waitingForStartInput;
        public bool WaitingForStartInput
        {
            get { return _waitingForStartInput; }
            set { _waitingForStartInput = value; }
        }

        private int _lifes;
        public int Lifes
        {
            get { return _lifes; }
            set 
            { 
                _lifes = value;
                LifeLost();
            }
        }

        public string Name
        {
            get { return GameStatesManager.IN_GAME_STATE; }
        }

        private bool _paused;
        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }

        private float _score;
        public float Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private SnakeEntity _snake;
        public SnakeEntity Snake
        {
            get { return _snake; }
        }

        #endregion

        #region Private Fields

        public float _blinkTimer;

        #endregion

        #region Private Methods

        private void CreateField()
        {
            float x = GameWindow.GetGameWindow().ClientRectangle.X + 10f;
            float y = GameWindow.GetGameWindow().ClientRectangle.X + 50f;
            float width = GameWindow.GetGameWindow().ClientSize.Width - 20f;
            float height = GameWindow.GetGameWindow().ClientSize.Height - 60f;

            _field = new Field(new PointF(x, y), new SizeF(width, height));
        }

        private void CreateSnake()
        {
            _waitingForStartInput = true;

            float x = Field.Location.X + (Field.Size.Width / 2f);
            float y = Field.Location.Y + (Field.Size.Height / 2f);

            _snake = new SnakeEntity(new PointF(x, y), 10f, Field);
        }

        private void CreateFoodFactory()
        {
            _foodManager = new FoodManager(Field);
        }

        private void DrawLifes(Graphics graphics)
        {
            graphics.DrawString("Lifes: " + Lifes, new Font(SystemFonts.DefaultFont.Name, 30, FontStyle.Bold), new SolidBrush(Color.White), new PointF(-4f, 0f));
        }

        private void DrawPaused(Graphics graphics)
        {
            if (!Paused) return;

            if (_blinkTimer > 100f)
                _blinkTimer = 0;
            if (_blinkTimer > 50f)
                return;
            graphics.DrawString("Paused", new Font(SystemFonts.DefaultFont.Name, 20, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(340f, 300));
        }

        private void DrawScore(Graphics graphics)
        {
            Font font = new Font(SystemFonts.DefaultFont.Name, 30, FontStyle.Bold);
            string scoreString = "Score: " + Score;
            
            SizeF stringSize = graphics.MeasureString(scoreString, font);
            float x = GameWindow.GetGameWindow().ClientSize.Width - stringSize.Width + 4f;
            graphics.DrawString("Score: " + Score, font, new SolidBrush(Color.White), new PointF(x, 0f));
        }

        private void DrawWaitingForStartInput(Graphics graphics)
        {
            if (!WaitingForStartInput) return;

            if (_blinkTimer > 100f)
                _blinkTimer = 0;
            if (_blinkTimer > 50f)
                return;
            graphics.DrawString("Press arrow key to start...", new Font(SystemFonts.DefaultFont.Name, 20, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(240f, 300));
        }

        private void LifeLost()
        {
            if (Lifes < 0)
                GameWindow.GetGameWindow().GameStatesManager.ChangeToState(GameStatesManager.END_GAME_STATE);
            else
                CreateSnake();
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            _lifes = 3;

            CreateField();
            CreateSnake();
            CreateFoodFactory();
            Controller.InitializeController(this);
        }

        public void FinalizeState()
        {
        }

        public void InitializeState()
        {
        }

        public void LeaveState()
        {
            Controller.FinalizeController();
        }

        public void Render(Graphics graphics)
        {
            Field.Render(graphics);
            Snake.Render(graphics);
            FoodManager.Render(graphics);
            DrawLifes(graphics);
            DrawScore(graphics);
            DrawWaitingForStartInput(graphics);
            DrawPaused(graphics);
        }

        public void Update(float deltaTime)
        {
            _blinkTimer += deltaTime;

            if (WaitingForStartInput) return;
            if (Paused) return;

            Snake.Update(deltaTime);
            FoodManager.Update(deltaTime);
            FoodManager.CheckForCollision(Snake);
        }

        #endregion
    }
}
