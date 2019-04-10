using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.DevIl;
using Engine.Input;
using Input = Engine.Input.Input;
using Tao.Platform.Windows;
using static Engine.Input.Mouse;

namespace Engine
{
    //public struct MouseButtonStated
    //{
    //    public bool leftToggle;
    //    public bool rightToggle;
    //    public bool middleToggle;
    //    public bool LeftHeld;
    //    public bool RightHeld;
    //    public bool MiddleHeld;
    //}
    public class MouseButtonState : IGameObject
    {
        Input.Input _input = new Input.Input();
        //MouseButtonStated mouseButtonStated = new MouseButtonStated();
        //Input.Mouse = new Mouse(this, SimpleOpenGlControl);
        MouseButton _mouseButton;
        bool _leftToggle;
        bool _rightToggle;
        bool _middleToggle;

        public bool leftToggle;
        public bool rightToggle;
        public bool middleToggle;
        public bool LeftHeld;
        public bool RightHeld;
        public bool MiddleHeld; 

        public MouseButtonState(Input.Input input)
        {
            _input = input;
            _mouseButton = _input.Mouse.mouseButton;
        }

        //public MouseClicked(Input.Input input)
        //{
        //    if (_input.Mouse.LeftPressed)
        //    {
        //        leftToggle = _leftToggle = !_leftToggle;
        //    }

        //    if (_input.Mouse.RightPressed)
        //    {
        //        rightToggle = _rightToggle = !_rightToggle;
        //    }

        //    if (_input.Mouse.MiddlePressed)
        //    {
        //        middleToggle = _middleToggle = !_middleToggle;
        //    }

        //    LeftHeld = _input.Mouse.LeftHeld;
        //    RightHeld = _input.Mouse.RightHeld;
        //    MiddleHeld = _input.Mouse.MiddleHeld;
            
        //}
        
        private void DrawButtonPoint(bool held, int yPos)
        {
            if (held)
            {
                Gl.glColor3f(0, 1, 0);
            }
            else
            {
                Gl.glColor3f(0, 0, 0);
            }
            Gl.glVertex2f(-400, yPos);
        }

        public void Render()
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glClearColor(1, 1, 1, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPointSize(10.0f);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2f(_input.Mouse.Position.X, _input.Mouse.Position.Y);

                if (_mouseButton.LeftPressed)
                {
                    _leftToggle = !_leftToggle;
                }

                if (_mouseButton.RightPressed)
                {
                    _rightToggle = !_rightToggle;
                }

                if (_mouseButton.MiddlePressed)
                {
                    _middleToggle = !_middleToggle;
                }


                DrawButtonPoint(_mouseButton.MiddleHeld, 80);
                DrawButtonPoint(_mouseButton.RightHeld, 60);                              
                DrawButtonPoint(_mouseButton.LeftHeld, 40);

                DrawButtonPoint(_middleToggle, 0);
                DrawButtonPoint(_rightToggle, -20);
                DrawButtonPoint(_leftToggle, -40);
                
                
            }
            Gl.glEnd();
        }

        public void Update(double elapsedTime)
        {
        }
    }
}
