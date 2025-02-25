using System.Drawing;

namespace Snake.GameObjects
{
    public abstract class AbstractEntity
    {
        #region Constructor
        protected AbstractEntity(PointF location, float radius, Color color)
        {
            Color = color;
            Radius = radius;
            Location = location;
        }

        #endregion

        #region Attributes and Properties
        
        protected Color Color { get;  }
        protected PointF Location { get; set;  }
        protected float Radius { get; }
        protected PointF RealLocation
        {
            get
            {
                var realLocation = new PointF
                {
                    X = Location.X - Radius,
                    Y = Location.Y - Radius
                };
                return realLocation;
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void Render(Graphics graphics);

        public abstract void SolveCollision(AbstractEntity collider);

        public abstract void Update(float deltaTime);
        
        #endregion

        #region Public Methods

        public bool Collides(AbstractEntity collider)
        {
            var range = ((collider.Radius) + Radius);
            range *= range;

            var dx = Location.X - collider.Location.X;
            var dy = Location.Y - collider.Location.Y;
            var distance = (dx * dx) + (dy * dy);

            return (distance <= range);
        }

        #endregion
    }
}
