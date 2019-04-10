using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine.Input;

namespace Engine
{
    public class RadioHorizontalMenu : HorizontalMenu
    {
        public bool _rhdisable;

        public RadioHorizontalMenu(double x, double y, Input.Input input, int hspacing = 60) : base(x, y, input, hspacing)
        {
        }

        public override void HandleInput()
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

            if (!_rhdisable && (_input.Keyboard.IsKeyPressed(Keys.Right) || controlPadRight))
            {
                OnRight();
            }
            else if (!_rhdisable && (_input.Keyboard.IsKeyPressed(Keys.Left) || controlPadLeft))
            {
                OnLeft();
            }
            else if (!_rhdisable && (_input.Keyboard.IsKeyPressed(Keys.Enter) || _input.Controller.ButtonA.Pressed))
            {
                OnButtonPress();
            }
        }

        //private void ChangeFocus(int from, int to)
        //{
        //    if (!_rhdisable)
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
