using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine.Input;

namespace Engine
{
    public class RadioVerticalMenu : VerticalMenu
    {
        public bool _rvdisable;

        public RadioVerticalMenu(double x, double y, Input.Input input, int vspacing = 30) : base(x, y, input, vspacing)
        {
        }

        public override void HandleInput()
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

            if (!_rvdisable && (_input.Keyboard.IsKeyPressed(Keys.Down) || controlPadDown))
            {
                OnDown();
            }
            else if (!_rvdisable && (_input.Keyboard.IsKeyPressed(Keys.Up) || controlPadUp))
            {
                OnUp();
            }
            else if (!_rvdisable && (_input.Keyboard.IsKeyPressed(Keys.Enter) || _input.Controller.ButtonA.Pressed))
            {
                OnButtonPress();
            }
        }

        //protected override void ChangeFocus(int from, int to)
        //{
        //    if (!_rvdisable)
        //    {
        //        if (from != to)
        //        {
        //            _buttons[from].OnLoseFocus();
        //            _buttons[to].OnGainFocus();
        //        }
        //    }           
        //}

        protected override void OnButtonPress()
        {
            if (_buttons.Exists(t => t._checked == true))
            {
                Button temp = _buttons[_buttons.FindIndex(t => t._checked == true)];
                temp._checked = false;
                temp.OnLoseFocus();
            }
            _buttons[_currentFocus]._checked = true;
            _buttons[_currentFocus].OnPress();
        }
    }
}
