using System.Drawing;

namespace Snake.GameObjects
{
    public class Field
    {
        #region Constructor

        public Field(PointF location, SizeF size)
        {
            Location = location;
            Size = size;
        }

        #endregion

        #region Attributes and Properties

        public PointF Location { get;  }

        public SizeF Size { get;  }

        #endregion

        #region Virtual Methods

        public virtual void Render(Graphics graphics)
        {
            var borderColor = Color.White;
            var backgroundColor = Color.YellowGreen;
            var gridColor = Color.SaddleBrown;
            
            var thickness = 5f;
            var gridThickness = 1f;

            var x = Location.X;
            var y = Location.Y;
            var width = Size.Width;
            var height = Size.Height;

            graphics.DrawRectangle(new Pen(borderColor, thickness), 
                                   x - (thickness / 2f),
                                   y - (thickness / 2f),
                                   width + thickness + 1f,
                                   height + thickness + 1f);
            
            graphics.FillRectangle(new SolidBrush(backgroundColor), 
                Location.X - (thickness / 2f),
                Location.Y - (thickness / 2f),
                Size.Width + thickness, 
                Size.Height + +thickness);

            for (var i = 0; i < 39; i ++)
                for (var j = 0; j < 27; j++)
                    graphics.DrawRectangle(new Pen(gridColor, gridThickness),
                                           x + (i * 20f),
                                           y + (j * 20f),
                                           20f,
                                           20f);
        }

        #endregion
    }
}
