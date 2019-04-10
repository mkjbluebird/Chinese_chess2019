using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Input;
using System.Windows.Forms;

namespace Engine
{
    public class HorizontalMenu
    {
        Vector _position = new Vector();
        protected Input.Input _input;
        protected List<Button> _buttons = new List<Button>();
        public double _HSpacing { get; set; }

        public HorizontalMenu(double x, double y, Input.Input input, int hspacing = 60)
        {
            _input = input;
            _position = new Vector(x, y, 0);
            ///<remarks>菜单行字间距</remarks>
            _HSpacing = hspacing;
        }

        public void AddButton(Button button)
        {
            double _currentX = _position.X;

            if (_buttons.Count != 0)
            {
                _currentX = _buttons.Last().Position.X;
                _currentX += _HSpacing;
            }
            else
            {
                button.OnGainFocus();
            }

            button.Position = new Vector(_currentX, _position.Y,  0);
            _buttons.Add(button);
        }

        public void Render(Renderer renderer)
        {
            _buttons.ForEach(x => x.Render(renderer));
        }

        protected bool _inRight = false;
        protected bool _inLeft = false;
        protected int _currentFocus = 0;       
        public virtual void HandleInput()
        {
            bool controlPadRight = false;
            bool controlPadLeft = false;

            float invertX = _input.Controller.LeftControlStick.X * -1;

            if (invertX < -0.2)
            {
                //控制棒按钮按下
                if (_inRight == false)
                {
                    controlPadRight = true;
                    _inRight = true;
                }
            }
            else
            {
                _inRight = false;
            }

            if (invertX > 0.2)
            {
                if (_inLeft == false)
                {
                    controlPadLeft = true;
                    _inLeft = true;
                }
            }
            else
            {
                _inLeft = false;
            }

            if (_input.Keyboard.IsKeyPressed(Keys.Right) || controlPadRight)
            {
                OnRight();
            }
            else if (_input.Keyboard.IsKeyPressed(Keys.Left) || controlPadLeft)
            {
                OnLeft();
            }
            else if (_input.Keyboard.IsKeyPressed(Keys.Enter) || _input.Controller.ButtonA.Pressed)
            {
                OnButtonPress();
            }            
        }

        public void Update(double elapsedTime)
        {
            _buttons.ForEach(x => x.Update(elapsedTime));
        }

        protected void OnRight()
        {
            int oldFocus = _currentFocus;
            _currentFocus++;
            if (_currentFocus == _buttons.Count)
            {
                _currentFocus = 0;
            }
            ChangeFocus(oldFocus, _currentFocus);
        }

        protected void OnLeft()
        {
            int oldFocus = _currentFocus;
            _currentFocus--;
            if (_currentFocus == -1)
            {
                _currentFocus = (_buttons.Count - 1);
            }
            ChangeFocus(oldFocus, _currentFocus);
        }

        private void ChangeFocus(int from, int to)
        {
            if (from != to)
            {
                _buttons[from].OnLoseFocus();
                _buttons[to].OnGainFocus();
            }
        }

        protected virtual void OnButtonPress()
        {
            if (_buttons.Exists(t => t._checked == true))
            {
                _buttons[_buttons.FindIndex(t => t._checked == true)]._checked = false;                
            }
            _buttons[_currentFocus]._checked = true;
            _buttons[_currentFocus].OnPress();
        }
    }
}
