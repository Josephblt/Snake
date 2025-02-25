using System.Collections.Generic;
using System.Drawing;

using Snake.Utility;

namespace Snake.GameObjects
{
    public class SnakeSection
    {
        #region Constructor

        public SnakeSection(PointF startPoint, float angle, float lenght, float thickness)
        {
            _angle = angle;
            _lenght = lenght;
            _startPoint = startPoint;
            _thickness = thickness;
        }

        #endregion

        #region Attributes and Properties

        public PointF EndPoint
        {
            get { return MathFunctions.PolarPoint(StartPoint, _angle, Lenght); }
        }

        private float _lenght;
        public float Lenght
        {
            get { return _lenght; }
            set { _lenght = value; }
        }

        private PointF _startPoint;
        public PointF StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public RectangleF Rectangle
        {
            get 
            {
                List<PointF> points = new List<PointF>();

                points.Add(MathFunctions.PolarPoint(EndPoint, _angle - 90, _thickness));
                points.Add(MathFunctions.PolarPoint(EndPoint, _angle + 90, _thickness));

                points.Add(MathFunctions.PolarPoint(StartPoint, _angle - 90, _thickness));
                points.Add(MathFunctions.PolarPoint(StartPoint, _angle + 90, _thickness));
                points.Add(MathFunctions.PolarPoint(StartPoint, _angle + 180f, _thickness));

                float startPointX = float.MaxValue;
                float startPointY = float.MaxValue;
                float endPointX = float.MinValue;
                float endPointY = float.MinValue;

                foreach (PointF point in points)
                {
                    if (point.X < startPointX)
                        startPointX = point.X;
                    if (point.X > endPointX)
                        endPointX = point.X;
                    if (point.Y < startPointY)
                        startPointY = point.Y;
                    if (point.Y > endPointY)
                        endPointY = point.Y;
                }

                float width = endPointX - startPointX;
                float height = endPointY - startPointY;

                return new RectangleF(startPointX, startPointY, width, height);
            }
        }

        #endregion

        #region Private Fields

        private float _angle;
        private float _thickness;

        #endregion

        #region Public Methods

        public bool Collides(List<SnakeSection> sections)
        {
            foreach(SnakeSection section in sections)
            {
                if (this == section) continue;
                
                int index = sections.IndexOf(this);
                int compareIndex = sections.IndexOf(section);

                if ((index == compareIndex - 1) || (index == compareIndex + 1))
                    continue;

                if (RectangleF.Intersect(Rectangle, section.Rectangle) != RectangleF.Empty)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
