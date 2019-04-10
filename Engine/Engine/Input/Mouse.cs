using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Engine.Input
{
    public class MouseButton
    {
        public bool leftClickDetect;
        public bool rightClickDetect;
        public bool middleClickDetect;

        public bool MiddlePressed;
        public bool LeftPressed;
        public bool RightPressed;

        public bool MiddleHeld;
        public bool LeftHeld;
        public bool RightHeld;
    }

    //public interface IEventHandler
    //{
    //    void SendEvent(EventBase e);

    //    void HandleEvent(EventBase evt);

    //    bool HasTrickleDownHandlers();

    //    bool HasBubbleUpHandlers();

    //    [Obsolete("Use HasTrickleDownHandlers instead of HasCaptureHandlers.")]
    //    bool HasCaptureHandlers();
    //    [Obsolete("Use HasBubbleUpHandlers instead of HasBubbleHandlers.")]
    //    bool HasBubbleHandlers();
    //}

    //public static class MouseCaptureController
    //{
    //    internal static IEventHandler mouseCapture { get; private set; }

    //    [Obsolete("Use IsMouseCaptured instead of IsMouseCaptureTaken.")]
    //    public static bool IsMouseCaptureTaken()
    //    {
    //        return IsMouseCaptured();
    //    }

    //    public static bool IsMouseCaptured()
    //    {
    //        return mouseCapture != null;
    //    }

    //    public static bool HasMouseCapture(this IEventHandler handler)
    //    {
    //        return mouseCapture == handler;
    //    }

    //    [Obsolete("Use CaptureMouse instead of TakeMouseCapture.")]
    //    public static void TakeMouseCapture(this IEventHandler handler)
    //    {
    //        CaptureMouse(handler);
    //    }

    //    public static void CaptureMouse(this IEventHandler handler)
    //    {
    //        if (mouseCapture == handler)
    //            return;

    //        if (handler == null)
    //        {
    //            ReleaseMouse();
    //            return;
    //        }

    //        if (GUIUtility.hotControl != 0)
    //        {
    //            Debug.Log("Should not be capturing when there is a hotcontrol");
    //            return;
    //        }

    //    TODO: assign a reserved control id to hotControl so that repaint events in OnGUI() have their hotcontrol check behave normally

    //        IEventHandler currentMouseCapture = mouseCapture;
    //        mouseCapture = handler;

    //        if (currentMouseCapture != null)
    //        {
    //            using (MouseCaptureOutEvent releaseEvent = MouseCaptureOutEvent.GetPooled(currentMouseCapture, mouseCapture))
    //            {
    //                currentMouseCapture.SendEvent(releaseEvent);
    //            }
    //        }

    //        using (MouseCaptureEvent captureEvent = MouseCaptureEvent.GetPooled(mouseCapture, currentMouseCapture))
    //        {
    //            mouseCapture.SendEvent(captureEvent);
    //        }
    //    }

    //    [Obsolete("Use ReleaseMouse instead of ReleaseMouseCapture.")]
    //    public static void ReleaseMouseCapture(this IEventHandler handler)
    //    {
    //        ReleaseMouse(handler);
    //    }

    //    public static void ReleaseMouse(this IEventHandler handler)
    //    {
    //        if (handler == mouseCapture)
    //        {
    //            ReleaseMouse();
    //        }
    //    }

    //    [Obsolete("Use ReleaseMouse instead of ReleaseMouseCapture.")]

    //    public static void ReleaseMouseCapture()
    //    {
    //        ReleaseMouse();
    //    }

    //    public static void ReleaseMouse()
    //    {
    //        if (mouseCapture != null)
    //        {
    //            IEventHandler currentMouseCapture = mouseCapture;
    //            mouseCapture = null;

    //            using (MouseCaptureOutEvent e = MouseCaptureOutEvent.GetPooled(currentMouseCapture, null))
    //            {
    //                currentMouseCapture.SendEvent(e);
    //            }
    //        }
    //    }
    //}
    public interface IMouseButton
    {
        void OnLButtonDown(uint nFlags, Engine.Point point);
        void OnLButtonUp(uint nFlags, Engine.Point point);
        void OnMouseMove(uint nFlags, Engine.Point point);
    }
    
    public class Mouse
    {
        Form _parentForm;
        Control _openGLControl;

        public Point Position { get; set; }

        public MouseButton mouseButton = new MouseButton();

        public bool leftClickDetect;
        public bool rightClickDetect;
        public bool middleClickDetect;

        public bool MiddlePressed { get; set; }
        public bool LeftPressed { get; set; }
        public bool RightPressed { get; set; }

        public bool MiddleHeld { get; set; }
        public bool LeftHeld { get; set; }
        public bool RightHeld { get; set; }

        public Mouse(Form form, Control openGLControl)
        {
            _parentForm = form;
            _openGLControl = openGLControl;
            _openGLControl.MouseClick += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    leftClickDetect = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    rightClickDetect = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    middleClickDetect = true;
                }
            };

            _openGLControl.MouseDown += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = true;
                }
            };

            _openGLControl.MouseUp += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = false;                    
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = false;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = false;
                }
            };

            _openGLControl.MouseLeave += delegate(object obj, EventArgs e)
            {
                // If you move the mouse out the window then release all held buttons
                LeftHeld = false;
                RightHeld = false;
                MiddleHeld = false;
            };
        }

        public void Update(double elapsedTime)
        {
            UpdateMousePosition();
            UpdateMouseButtons();
        }

        public Point UpdateMousePosition()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = _openGLControl.PointToClient(mousePos);

            // Now use our point definition, 
            Engine.Point adjustedMousePoint = new Engine.Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)_parentForm.ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)_parentForm.ClientSize.Height / 2) - (float)mousePos.Y;
            Position = adjustedMousePoint;
            return Position;
        }

        public MouseButton UpdateMouseButtons()
        {
            if (leftClickDetect)
            {
                mouseButton.LeftPressed = true;
                leftClickDetect = false;
            }

            if (rightClickDetect)
            {
                mouseButton.RightPressed = true;
                rightClickDetect = false;
            }

            if (middleClickDetect)
            {
                mouseButton.MiddlePressed = true;
                middleClickDetect = false;
            }

            if (LeftHeld)
            {
                mouseButton.LeftHeld = true;
            }
            else
            {
                mouseButton.LeftHeld = false;
            }

            if (RightHeld)
            {
                mouseButton.RightHeld = true;
            }
            else
            {
                mouseButton.RightHeld = false;
            }

            if (MiddleHeld)
            {
                mouseButton.MiddleHeld = true;
            }
            else
            {
                mouseButton.MiddleHeld = false;
            }

            return mouseButton;
        }

    }
}
