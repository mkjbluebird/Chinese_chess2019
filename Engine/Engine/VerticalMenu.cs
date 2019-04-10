using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Input;
using System.Windows.Forms;

namespace Engine
{
    public class VerticalMenu
    {
        Vector _position = new Vector();
        protected Input.Input _input;
        public List<Button> _buttons = new List<Button>();
        public double _VSpacing { get; set; }

        public VerticalMenu(double x, double y, Input.Input input, int vspacing = 40)
        {
            _input = input;
            _position = new Vector(x, y, 0);
            ///<remarks>菜单行字间距</remarks>
            _VSpacing = vspacing;
        }

        public void AddButton(Button button)
        {
            double _currentY = _position.Y;

            if (_buttons.Count != 0)
            {
                _currentY = _buttons.Last().Position.Y;
                _currentY -= _VSpacing;
            }
            else
            {
                button.OnGainFocus();
            }

            button.Position = new Vector(_position.X, _currentY, 0);
            _buttons.Add(button);
        }

        public void Render(Renderer renderer)
        {
            _buttons.ForEach(x => x.Render(renderer));
        }

        protected bool _inDown = false;
        protected bool _inUp = false;
        protected int _currentFocus = 0;        
        public virtual void HandleInput()
        {
            bool controlPadDown = false;
            bool controlPadUp = false;

            float invertY = _input.Controller.LeftControlStick.Y * -1;

            if (invertY < -0.2)
            {
                //控制棒按钮按下
                if (_inDown == false)
                {
                    controlPadDown = true;
                    _inDown = true;
                }
            }
            else
            {
                _inDown = false;
            }

            if (invertY > 0.2)
            {
                if (_inUp == false)
                {
                    controlPadUp = true;
                    _inUp = true;
                }
            }
            else
            {
                _inUp = false;
            }

            if (_input.Keyboard.IsKeyPressed(Keys.Down) || controlPadDown)
            {
                OnDown();
            }
            else if (_input.Keyboard.IsKeyPressed(Keys.Up) || controlPadUp)
            {
                OnUp();
            }
            else if(_input.Keyboard.IsKeyPressed(Keys.Enter) || _input.Controller.ButtonA.Pressed)
            {
                OnButtonPress();
            }            
        }

        public void Update(double elapsedTime)
        {
            _buttons.ForEach(x => x.Update(elapsedTime));
        }

        protected virtual void OnDown()
        {
            int oldFocus = _currentFocus;
            _currentFocus++;
            if(_currentFocus == _buttons.Count)
            {
                _currentFocus = 0;
            }
            ChangeFocus(oldFocus, _currentFocus);
        }

        protected virtual void OnUp()
        {
            int oldFocus = _currentFocus;
            _currentFocus--;
            if(_currentFocus == -1)
            {
                _currentFocus = (_buttons.Count - 1);
            }
            ChangeFocus(oldFocus, _currentFocus);
        }


        protected virtual void ChangeFocus(int from, int to)
        {
            if(from != to)
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
