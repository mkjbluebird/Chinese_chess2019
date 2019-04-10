using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Input = Engine.Input.Input;

namespace Engine
{
    public class Button : RectangleE
    {
        EventHandler _onPressEvent;
        Text _label;
        //Rectangle _rectangle;
        //Vector _position = new Vector();
        public bool _checked;
        Input.Input _input= new Input.Input();
        float _buttomwidth;
        float _buttomheight;
        RectangleColor rectangleColor = new RectangleColor();

        public Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                UpdatePosition();
            }
        }

        Vector BottomLeft { get; set; }
        Vector TopRight { get; set; }

        public Button(EventHandler onPressEvent, Text label, float buttomwidth, float buttomheight, Input.Input input)
        {
            _onPressEvent = onPressEvent;
            _label = label;
            _label.SetColor(new Color(0, 0, 0, 1));
            _buttomwidth = buttomwidth;
            _buttomheight = buttomheight;
            UpdatePosition();
        }

        protected override RectangleF GetBoundingBox()
        {
            float width = (float)(_buttomwidth);
            float height = (float)(_label.Height);
            return new RectangleF((float)_position.X - width / 2 - 7,
                (float)_position.Y - (float)(_label.Height / 2) - _buttomheight / 2, width, height + _buttomheight);
        }

        private void UpdatePosition()
        {
            // Center label text on position.
            _label.SetPosition(_position.X - (_label.Width / 2), _position.Y + (_label.Height / 2));            
        }

        public override void Update(double elapsedTime)
        {
            if (Intersects(_input.MousePosition))
            {
                OnGainFocus();
            }
            else
            {
                OnLoseFocus();
            }
        }

        public void OnGainFocus()
        {
            if (this._checked == false)
            {
                _label.SetColor(new Color(1, 0, 0, 1));
                rectangleColor.Red = 1.0f;
                rectangleColor.Green = 0.0f;
                rectangleColor.Blue = 0.0f;
            }
            else
            {
                _label.SetColor(new Color(0, 1, 0, 1));
                rectangleColor.Red = 0.0f;
                rectangleColor.Green = 1.0f;
                rectangleColor.Blue = 0.0f;
            }                
        }

        public void OnLoseFocus()
        {
            if(this._checked == false)
            {
                _label.SetColor(new Color(0, 0, 0, 1));
                rectangleColor.Red = 0.0f;
                rectangleColor.Green = 0.0f;
                rectangleColor.Blue = 0.0f;
            }

            else
            {
                _label.SetColor(new Color(0, 1, 0, 1));
                rectangleColor.Red = 0.0f;
                rectangleColor.Green = 1.0f;
                rectangleColor.Blue = 0.0f;
            }
                
        }
        
        public void OnPress()
        {
            this._checked = true;
            this.OnGainFocus();
            _onPressEvent(this, EventArgs.Empty);
        }

        public void Render(Renderer renderer)
        {
            //_rectangle.Render();
            Render_Debug(rectangleColor);
            renderer.DrawText(_label);

        }

        protected override void Render_Debug(RectangleColor rectangleColor)
        {
            base.Render_Debug(rectangleColor);
        }

        //public void Render()
        //{
        //    _rectangle.Render();
        //}
    }
}
