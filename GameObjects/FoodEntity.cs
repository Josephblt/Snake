using System.Drawing;

using Snake.GameStates;

namespace Snake.GameObjects
{
    public class FoodEntity : AbstractEntity
    {
        #region Constructor

        public FoodEntity(PointF location, float radius, float lifeSpam)
            : base(location, radius, Color.Red)
        {
            _lifeSpam = lifeSpam;
        }

        #endregion

        #region Private Fields

        private float _accumulatedTime;
        public float _lifeSpam;

        #endregion

        #region Overriden Methods

        public override void Render(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color), RealLocation.X, RealLocation.Y, Radius * 2f, Radius * 2f);
        }

        public override void SolveCollision(AbstractEntity collider)
        {
            //InGameState inGameState = GameWindow.GetGameWindow().GameStatesManager[GameStatesManager.IN_GAME_STATE] as InGameState;
            //inGameState.FoodManager.DestroyFood(this);
            //inGameState.Score += 25f;
        }

        public override void Update(float deltaTime)
        {
            _accumulatedTime += deltaTime;
            if (_accumulatedTime >= _lifeSpam)
            {
                //InGameState inGameState = GameWindow.GetGameWindow().GameStatesManager[GameStatesManager.IN_GAME_STATE] as InGameState;
                //inGameState.FoodManager.DestroyFood(this);
            }
        }

        #endregion
    }
}
