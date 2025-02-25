using System;
using System.Drawing;

namespace Snake.Utility
{
    public static class MathFunctions
    {
        #region Static Methods

        public static float ClampDegreeAngle(float degrees)
        {
            if (degrees >= 360)
                degrees -= 360;
            if (degrees < 0)
                degrees += 360;
            return degrees;
        }

        public static float PointDistance(PointF pointOne, PointF pointTwo)
        {
            float YDistance = (pointTwo.Y - pointOne.Y);
            float XDistance = (pointTwo.X - pointOne.X);

            return (float)Math.Sqrt((YDistance * YDistance) + (XDistance * XDistance));
        }

        public static PointF PolarPoint(PointF point, float degrees, float distance)
        {
            float x = (float)(Math.Cos(degrees * Math.PI / 180f) * distance) + point.X;
            float y = (float)(-Math.Sin(degrees * Math.PI / 180f) * distance) + point.Y;
            return new PointF(x, y);
        }

        #endregion
    }
}
