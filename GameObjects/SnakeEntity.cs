using System.Collections.Generic;
using System.Drawing;

using Snake.GameStates;
using Snake.Utility;

namespace Snake.GameObjects
{
    public class SnakeEntity : AbstractEntity
    {
        #region Constructor

        public SnakeEntity(PointF location, float radius, Field field)
            : base(location, radius, Color.Green)
        {
            _angle = 0f;
            _field = field;
            _sections = new List<SnakeSection>();
            _sections.Add(new SnakeSection(Location, _angle, 10f, Radius));
            _speed = 2f;
        }

        #endregion

        #region Private Fields

        private float _angle;
        private float _distanceTraveled;
        private Field _field;
        private List<SnakeSection> _sections;
        private float _speed;

        #endregion

        #region Overriden Methods

        public override void Render(Graphics graphics)
        {
            foreach (SnakeSection section in _sections)
                graphics.FillRectangle(new SolidBrush(Color.Green), section.Rectangle);

            graphics.FillRectangle(new SolidBrush(Color.DarkGreen), RealLocation.X, RealLocation.Y, Radius * 2f, Radius * 2f);
        }

        public override void SolveCollision(AbstractEntity collider)
        {
            _sections[0].Lenght += 25f;
        }

        public override void Update(float deltaTime)
        {
            Location = MathFunctions.PolarPoint(Location, _angle, _speed * deltaTime);

            if (Location.X > (_field.Location.X + _field.Size.Width))
                LethalCollision();
            else if (Location.X < _field.Location.X)
                LethalCollision();

            if (Location.Y > (_field.Location.Y + _field.Size.Height))
                LethalCollision();
            else if (Location.Y < _field.Location.Y)
                LethalCollision();

            float distanceTraveledNow = MathFunctions.PointDistance(Location, _sections[_sections.Count - 1].StartPoint);
            _distanceTraveled += distanceTraveledNow;

            _sections[_sections.Count - 1].StartPoint = Location;
            _sections[_sections.Count - 1].Lenght += distanceTraveledNow;
            _sections[0].Lenght -= distanceTraveledNow;

            if (_sections[0].Lenght == 0)
                _sections.RemoveAt(0);
            if (_sections[0].Lenght < 0)
            {
                _sections[1].Lenght += _sections[0].Lenght;
                _sections.RemoveAt(0);
            }

            foreach (SnakeSection section in _sections)
            {
                if (section.Collides(_sections))
                {
                    LethalCollision();
                    break;
                }
            }
        }

        #endregion

        #region Private Methods

        private void LethalCollision()
        {
            //InGameState inGameState = GameWindow.GetGameWindow().GameStatesManager[GameStatesManager.IN_GAME_STATE] as InGameState;
            //inGameState.Lifes--;
        }

        #endregion

        #region Public Methods

        public void ChangeDirection(float angle, bool forceDirectionChange)
        {
            if (!forceDirectionChange)
                if (_distanceTraveled < Radius * 2f) return;
            _distanceTraveled = 0;

            float compareAngle = MathFunctions.ClampDegreeAngle(angle + 180f);
            if (_angle == compareAngle) return;

            _angle = angle;
            _sections.Add(new SnakeSection(Location, _angle + 180f, 0f, Radius));
        }

        #endregion
    }
}
