using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Tao.Sdl;
using Engine;
using System.Windows.Forms;
using static Engine.Input.Mouse;

namespace Engine.Input
{
    public class Input
    {
        public Mouse Mouse { get; set; }
        public Keyboard Keyboard { get; set; }
        public Point MousePosition { get; set; }
        public MouseButton mouseButton = new MouseButton();
        bool _usingController = false;
        public XboxController Controller { get; set; }

        public Input()
        {
            Sdl.SDL_InitSubSystem(Sdl.SDL_INIT_JOYSTICK);
            if (Sdl.SDL_NumJoysticks() > 0)
            {
                Controller = new XboxController(0);
                _usingController = true;
            }
        }

        public void Update(double elapsedTime)
        {
            if (_usingController)
            {
                Sdl.SDL_JoystickUpdate();
                Controller.Update();
            }
            Mouse.Update(elapsedTime);
            Keyboard.Process();
        }

    }
}
