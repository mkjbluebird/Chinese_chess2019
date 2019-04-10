using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_chess
{
    class MoveChess : Entity
    {
        public bool Dead { get; set; }
        //public Vector Direction { get; set; }
        //public double Speed { get; set; }
        public byte nChessID;
        public POINT ptMovePoint;// pixels per second


        public double X
        {
            get { return _sprite.GetPosition().X; }
        }

        public double Y
        {
            get { return _sprite.GetPosition().Y; }
        }

        public void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }

        //public void SetColor(Color color)
        //{
        //    _sprite.SetColor(color);
        //}



        public MoveChess(Texture moveChessTexture)
        {
            _sprite.Texture = moveChessTexture;
            // Some default values
            bool Dead = false;
        }

        public void Render(Renderer renderer)
        {
            if (Dead)
            {
                return;
            }
            renderer.DrawSprite(_sprite);
        }

        public void Update(double elapsedTime)
        {
            if (Dead)
            {
                return;
            }
            Vector position = _sprite.GetPosition();
            //position += Direction * Speed * elapsedTime;
            _sprite.SetPosition(position);
        }
    }
}
