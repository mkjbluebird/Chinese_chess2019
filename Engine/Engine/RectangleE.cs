using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Input = Engine.Input.Input;

namespace Engine
{
    public class RectangleColor
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
    }
    public class RectangleE
    {
        //protected Sprite _sprite = new Sprite();
        Vector BottomLeft { get; set; }
        Vector TopRight { get; set; }
        Color _color = new Color(1, 1, 1, 1);
        protected Vector _position = new Vector();

        protected virtual RectangleF GetBoundingBox()
        {
            return new RectangleF((float) 0, (float) 0, 100, 100);
        }


        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public RectangleE(Vector bottomLeft, Vector topRight) : this()
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public RectangleE()
        {
        }

        RectangleF bounds;
        //{
        //    float width = (float)(_position.X);
        //    float height = (float)(_position.Y );
        //    return new RectangleF((float)_position.X - width / 2,
        //        (float)_position.Y - height / 2, width, height);
        //}

        

        //渲染检测方框
        protected virtual void Render_Debug(RectangleColor rectangleColor)
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            bounds = GetBoundingBox();
            Gl.glBegin(Gl.GL_LINE_LOOP);
            {
                Gl.glColor3f(rectangleColor.Red, rectangleColor.Green, rectangleColor.Blue);
                Gl.glVertex2f(bounds.Left, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Bottom);
                Gl.glVertex2f(bounds.Left, bounds.Bottom);
            }
            Gl.glEnd();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }

        public virtual bool Intersects(Point point)
        {
            if(point.X >= bounds.Left && 
               point.X <= bounds.Right &&
               point.Y <= bounds.Top &&
               point.Y >= bounds.Bottom)
            {
                return true;
            }
            return false;
        }



        public virtual void Update(double elapsedTime)
        {
            //throw new NotImplementedException();
        }

        public virtual void Render()
        {
            //throw new NotImplementedException();
        }
    }
}


