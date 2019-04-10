using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Input;
using static System.Console;

namespace Chinese_chess
{
    class ChessBoard : Entity
    {
        double _speed = 512;  //每秒多少像素
        bool _dead = false;
        MoveChessManager _moveChessManager;
        Texture _moveChess_R_CAR_Texture;
        Texture _moveChess_R_HORSE_Texture;
        Texture _moveChess_R_CANON_Texture;
        Texture _moveChess_R_ELEPHANT_Texture;
        Texture _moveChess_R_BISHOP_Texture;
        Texture _moveChess_R_KING_Texture;
        Texture _moveChess_R_PAWN_Texture;
        Texture _moveChess_B_CAR_Texture;
        Texture _moveChess_B_HORSE_Texture;
        Texture _moveChess_B_CANON_Texture;
        Texture _moveChess_B_ELEPHANT_Texture;
        Texture _moveChess_B_BISHOP_Texture;
        Texture _moveChess_B_KING_Texture;
        Texture _moveChess_B_PAWN_Texture;
        Texture _moveChessTexture;

        MoveChess moveChessR_KING;
        MoveChess moveChessR_CAR1;
        MoveChess moveChessR_CAR2;
        MoveChess moveChessR_HORSE1;
        MoveChess moveChessR_HORSE2;
        MoveChess moveChessR_CANON1;
        MoveChess moveChessR_CANON2;
        MoveChess moveChessR_BISHOP1;
        MoveChess moveChessR_BISHOP2;
        MoveChess moveChessR_ELEPHANT1;
        MoveChess moveChessR_ELEPHANT2;
        MoveChess moveChessR_PAWN1;
        MoveChess moveChessR_PAWN2;
        MoveChess moveChessR_PAWN3;
        MoveChess moveChessR_PAWN4;
        MoveChess moveChessR_PAWN5;
        MoveChess moveChessB_KING;
        MoveChess moveChessB_CAR1;
        MoveChess moveChessB_CAR2;
        MoveChess moveChessB_HORSE1;
        MoveChess moveChessB_HORSE2;
        MoveChess moveChessB_CANON1;
        MoveChess moveChessB_CANON2;
        MoveChess moveChessB_BISHOP1;
        MoveChess moveChessB_BISHOP2;
        MoveChess moveChessB_ELEPHANT1;
        MoveChess moveChessB_ELEPHANT2;
        MoveChess moveChessB_PAWN1;
        MoveChess moveChessB_PAWN2;
        MoveChess moveChessB_PAWN3;
        MoveChess moveChessB_PAWN4;
        MoveChess moveChessB_PAWN5;

        public bool IsDead
        {
            get
            {
                return _dead;
            }

            set
            {

            }
        }
        public ChessBoard(TextureManager textureManager, MoveChessManager moveChessManager)
        {
            _moveChessManager = moveChessManager;
            //_moveChessTexture = textureManager.Get("moveChess");
            _moveChess_R_CAR_Texture = textureManager.Get("moveChessR_CAR");
            moveChessR_CAR1 = new MoveChess(_moveChess_R_CAR_Texture);
            moveChessR_CAR2 = new MoveChess(_moveChess_R_CAR_Texture);
            _moveChess_R_HORSE_Texture = textureManager.Get("moveChessR_HORSE");
            moveChessR_HORSE1 = new MoveChess(_moveChess_R_HORSE_Texture);
            moveChessR_HORSE2 = new MoveChess(_moveChess_R_HORSE_Texture);
            _moveChess_R_CANON_Texture = textureManager.Get("moveChessR_CANON");
            moveChessR_CANON1 = new MoveChess(_moveChess_R_CANON_Texture);
            moveChessR_CANON2 = new MoveChess(_moveChess_R_CANON_Texture);
            _moveChess_R_ELEPHANT_Texture = textureManager.Get("moveChessR_ELEPHANT");
            moveChessR_ELEPHANT1 = new MoveChess(_moveChess_R_ELEPHANT_Texture);
            moveChessR_ELEPHANT2 = new MoveChess(_moveChess_R_ELEPHANT_Texture);
            _moveChess_R_BISHOP_Texture = textureManager.Get("moveChessR_BISHOP");
            moveChessR_BISHOP1 = new MoveChess(_moveChess_R_BISHOP_Texture);
            moveChessR_BISHOP2 = new MoveChess(_moveChess_R_BISHOP_Texture);
            _moveChess_R_KING_Texture = textureManager.Get("moveChessR_KING");
            moveChessR_KING = new MoveChess(_moveChess_R_KING_Texture);
            _moveChess_R_PAWN_Texture = textureManager.Get("moveChessR_PAWN");
            moveChessR_PAWN1 = new MoveChess(_moveChess_R_PAWN_Texture);
            moveChessR_PAWN2 = new MoveChess(_moveChess_R_PAWN_Texture);
            moveChessR_PAWN3 = new MoveChess(_moveChess_R_PAWN_Texture);
            moveChessR_PAWN4 = new MoveChess(_moveChess_R_PAWN_Texture);
            moveChessR_PAWN5 = new MoveChess(_moveChess_R_PAWN_Texture);
            _moveChess_B_CAR_Texture = textureManager.Get("moveChessB_CAR");
            moveChessB_CAR1 = new MoveChess(_moveChess_B_CAR_Texture);
            moveChessB_CAR2 = new MoveChess(_moveChess_B_CAR_Texture);
            _moveChess_B_HORSE_Texture = textureManager.Get("moveChessB_HORSE");
            moveChessB_HORSE1 = new MoveChess(_moveChess_B_HORSE_Texture);
            moveChessB_HORSE2 = new MoveChess(_moveChess_B_HORSE_Texture);
            _moveChess_B_CANON_Texture = textureManager.Get("moveChessB_CANON");
            moveChessB_CANON1 = new MoveChess(_moveChess_B_CANON_Texture);
            moveChessB_CANON2 = new MoveChess(_moveChess_B_CANON_Texture);
            _moveChess_B_ELEPHANT_Texture = textureManager.Get("moveChessB_ELEPHANT");
            moveChessB_ELEPHANT1 = new MoveChess(_moveChess_B_ELEPHANT_Texture);
            moveChessB_ELEPHANT2 = new MoveChess(_moveChess_B_ELEPHANT_Texture);
            _moveChess_B_BISHOP_Texture = textureManager.Get("moveChessB_BISHOP");
            moveChessB_BISHOP1 = new MoveChess(_moveChess_B_BISHOP_Texture);
            moveChessB_BISHOP2 = new MoveChess(_moveChess_B_BISHOP_Texture);
            _moveChess_B_KING_Texture = textureManager.Get("moveChessB_KING");
            moveChessB_KING = new MoveChess(_moveChess_B_KING_Texture);
            _moveChess_B_PAWN_Texture = textureManager.Get("moveChessB_PAWN");
            moveChessB_PAWN1 = new MoveChess(_moveChess_B_PAWN_Texture);
            moveChessB_PAWN2 = new MoveChess(_moveChess_B_PAWN_Texture);
            moveChessB_PAWN3 = new MoveChess(_moveChess_B_PAWN_Texture);
            moveChessB_PAWN3 = new MoveChess(_moveChess_B_PAWN_Texture);
            moveChessB_PAWN4 = new MoveChess(_moveChess_B_PAWN_Texture);
            moveChessB_PAWN5 = new MoveChess(_moveChess_B_PAWN_Texture);

            _sprite.Texture = textureManager.Get("chessboard");
            _sprite.SetMatrixScale(1, 1); //飞船图标太大，缩小它
        }

        Vector _gunOffset = new Vector(55, 0, 0);
        static readonly double FireRecovery = 0.25;
        double _fireRecoveryTime = FireRecovery;

        public void Update(double elapsedTime)
        {
            _fireRecoveryTime = Math.Max(0, (_fireRecoveryTime - elapsedTime));
        }

        public void Fire()
        {
            if (_fireRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _fireRecoveryTime = FireRecovery;
            }
            MoveChess moveChess = new MoveChess(_moveChessTexture);
            //bullet.SetColor(new Color(0, 1, 0, 1));
            moveChess.SetPosition(_sprite.GetPosition() + _gunOffset);
            //_moveChessManager.Shoot(moveChess);
        }

        public void Restart()
        {
            POINT pt = new POINT();
            Vector firstPosition = _sprite.GetPosition();
            Vector nextPosition = _sprite.GetPosition();
            firstPosition.X = _sprite.GetPosition().X - 228.5;
            firstPosition.Y = _sprite.GetPosition().Y - 256.5;

            moveChessR_KING.Dead = false;
            moveChessR_CAR1.Dead = false;
            moveChessR_CAR2.Dead = false;
            moveChessR_HORSE1.Dead = false;
            moveChessR_HORSE2.Dead = false;
            moveChessR_CANON1.Dead = false;
            moveChessR_CANON2.Dead = false;
            moveChessR_BISHOP1.Dead = false;
            moveChessR_BISHOP2.Dead = false;
            moveChessR_ELEPHANT1.Dead = false;
            moveChessR_ELEPHANT2.Dead = false;
            moveChessR_PAWN1.Dead = false;
            moveChessR_PAWN2.Dead = false;
            moveChessR_PAWN3.Dead = false;
            moveChessR_PAWN4.Dead = false;
            moveChessR_PAWN5.Dead = false;

            moveChessB_KING.Dead = false;
            moveChessB_CAR1.Dead = false;
            moveChessB_CAR2.Dead = false;
            moveChessB_HORSE1.Dead = false;
            moveChessB_HORSE2.Dead = false;
            moveChessB_CANON1.Dead = false;
            moveChessB_CANON2.Dead = false;
            moveChessB_BISHOP1.Dead = false;
            moveChessB_BISHOP2.Dead = false;
            moveChessB_ELEPHANT1.Dead = false;
            moveChessB_ELEPHANT2.Dead = false;
            moveChessB_PAWN1.Dead = false;
            moveChessB_PAWN2.Dead = false;
            moveChessB_PAWN3.Dead = false;
            moveChessB_PAWN4.Dead = false;
            moveChessB_PAWN5.Dead = false;

            bool isR_CAR1 = true;
            bool isR_HORSE1 = true;
            bool isR_CANON1 = true;
            bool isR_BISHOP1 = true;
            bool isR_ELEPHANT1 = true;
            bool isB_CAR1 = true;
            bool isB_HORSE1 = true;
            bool isB_CANON1 = true;
            bool isB_BISHOP1 = true;
            bool isB_ELEPHANT1 = true;
            int u = 1;
            int v = 1;
            // moveChess.SetPosition(firstPostion);
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (Level.m_ChessBoard[i, j] == Form1.NOCHESS)
                        continue;
                    pt.x = j * 57;
                    pt.y = i * 57;
                    nextPosition.X = firstPosition.X + pt.x;
                    nextPosition.Y = firstPosition.Y + pt.y;

                    switch (Level.m_ChessBoard[i, j])
                    {
                        case Form1.R_KING:
                            moveChessR_KING.nChessID = Form1.R_KING;
                            moveChessR_KING.ptMovePoint = pt;
                            moveChessR_KING.SetPosition(nextPosition);
                            _moveChessManager.InitializeChessman(moveChessR_KING);
                            break;
                        case Form1.R_CAR:
                            if (isR_CAR1)
                            {
                                moveChessR_CAR1.nChessID = Form1.R_CAR;
                                moveChessR_CAR1.ptMovePoint = pt;
                                moveChessR_CAR1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_CAR1);
                                isR_CAR1 = false;
                            }
                            else
                            {
                                moveChessR_CAR2.nChessID = Form1.R_CAR;
                                moveChessR_CAR2.ptMovePoint = pt;
                                moveChessR_CAR2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_CAR2);
                                isR_CAR1 = true;
                            }
                            break;
                        case Form1.R_HORSE:
                            if (isR_HORSE1)
                            {
                                moveChessR_HORSE1.nChessID = Form1.R_HORSE;
                                moveChessR_HORSE1.ptMovePoint = pt;
                                moveChessR_HORSE1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_HORSE1);
                                isR_HORSE1 = false;
                            }
                            else
                            {
                                moveChessR_HORSE2.nChessID = Form1.R_HORSE;
                                moveChessR_HORSE2.ptMovePoint = pt;
                                moveChessR_HORSE2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_HORSE2);
                                isR_HORSE1 = true;
                            }
                            break;
                        case Form1.R_CANON:
                            if (isR_CANON1)
                            {
                                moveChessR_CANON1.nChessID = Form1.R_CANON;
                                moveChessR_CANON1.ptMovePoint = pt;
                                moveChessR_CANON1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_CANON1);
                                isR_CANON1 = false;
                            }
                            else
                            {
                                moveChessR_CANON2.nChessID = Form1.R_CANON;
                                moveChessR_CANON2.ptMovePoint = pt;
                                moveChessR_CANON2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_CANON2);
                                isR_CANON1 = true;
                            }
                            break;
                        case Form1.R_BISHOP:
                            if (isR_BISHOP1)
                            {
                                moveChessR_BISHOP1.nChessID = Form1.R_BISHOP;
                                moveChessR_BISHOP1.ptMovePoint = pt;
                                moveChessR_BISHOP1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_BISHOP1);
                                isR_BISHOP1 = false;
                            }
                            else
                            {
                                moveChessR_BISHOP2.nChessID = Form1.R_BISHOP;
                                moveChessR_BISHOP2.ptMovePoint = pt;
                                moveChessR_BISHOP2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_BISHOP2);
                                isR_BISHOP1 = true;
                            }
                            break;
                        case Form1.R_ELEPHANT:
                            if (isR_ELEPHANT1)
                            {
                                moveChessR_ELEPHANT1.nChessID = Form1.R_ELEPHANT;
                                moveChessR_ELEPHANT1.ptMovePoint = pt;
                                moveChessR_ELEPHANT1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_ELEPHANT1);
                                isR_ELEPHANT1 = false;
                            }
                            else
                            {
                                moveChessR_ELEPHANT2.nChessID = Form1.R_ELEPHANT;
                                moveChessR_ELEPHANT2.ptMovePoint = pt;
                                moveChessR_ELEPHANT2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessR_ELEPHANT2);
                                isR_ELEPHANT1 = true;
                            }
                            break;
                        case Form1.R_PAWN:
                            switch (u)
                            {
                                case 1:
                                    moveChessR_PAWN1.nChessID = Form1.R_PAWN;
                                    moveChessR_PAWN1.ptMovePoint = pt;
                                    moveChessR_PAWN1.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessR_PAWN1);
                                    u++;
                                    break;
                                case 2:
                                    moveChessR_PAWN2.nChessID = Form1.R_PAWN;
                                    moveChessR_PAWN2.ptMovePoint = pt;
                                    moveChessR_PAWN2.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessR_PAWN2);
                                    u++;
                                    break;
                                case 3:
                                    moveChessR_PAWN3.nChessID = Form1.R_PAWN;
                                    moveChessR_PAWN3.ptMovePoint = pt;
                                    moveChessR_PAWN3.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessR_PAWN3);
                                    u++;
                                    break;
                                case 4:
                                    moveChessR_PAWN4.nChessID = Form1.R_PAWN;
                                    moveChessR_PAWN4.ptMovePoint = pt;
                                    moveChessR_PAWN4.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessR_PAWN4);
                                    u++;
                                    break;
                                case 5:
                                    moveChessR_PAWN5.nChessID = Form1.R_PAWN;
                                    moveChessR_PAWN5.ptMovePoint = pt;
                                    moveChessR_PAWN5.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessR_PAWN5);
                                    u = 1;
                                    break;
                            }
                            break;
                        case Form1.B_KING:
                            moveChessB_KING.nChessID = Form1.B_KING;
                            moveChessB_KING.ptMovePoint = pt;
                            moveChessB_KING.SetPosition(nextPosition);
                            _moveChessManager.InitializeChessman(moveChessB_KING);
                            break;
                        case Form1.B_CAR:
                            if (isB_CAR1)
                            {
                                moveChessB_CAR1.nChessID = Form1.B_CAR;
                                moveChessB_CAR1.ptMovePoint = pt;
                                moveChessB_CAR1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_CAR1);
                                isB_CAR1 = false;
                            }
                            else
                            {
                                moveChessB_CAR2.nChessID = Form1.B_CAR;
                                moveChessB_CAR2.ptMovePoint = pt;
                                moveChessB_CAR2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_CAR2);
                                isB_CAR1 = true;
                            }
                            break;
                        case Form1.B_HORSE:
                            if (isB_HORSE1)
                            {
                                moveChessB_HORSE1.nChessID = Form1.B_HORSE;
                                moveChessB_HORSE1.ptMovePoint = pt;
                                moveChessB_HORSE1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_HORSE1);
                                isB_HORSE1 = false;
                            }
                            else
                            {
                                moveChessB_HORSE2.nChessID = Form1.B_HORSE;
                                moveChessB_HORSE2.ptMovePoint = pt;
                                moveChessB_HORSE2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_HORSE2);
                                isB_HORSE1 = true;
                            }
                            break;
                        case Form1.B_CANON:
                            if (isB_CANON1)
                            {
                                moveChessB_CANON1.nChessID = Form1.B_CANON;
                                moveChessB_CANON1.ptMovePoint = pt;
                                moveChessB_CANON1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_CANON1);
                                isB_CANON1 = false;
                            }
                            else
                            {
                                moveChessB_CANON2.nChessID = Form1.B_CANON;
                                moveChessB_CANON2.ptMovePoint = pt;
                                moveChessB_CANON2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_CANON2);
                                isB_CANON1 = true;
                            }
                            break;
                        case Form1.B_BISHOP:
                            if (isB_BISHOP1)
                            {
                                moveChessB_BISHOP1.nChessID = Form1.B_BISHOP;
                                moveChessB_BISHOP1.ptMovePoint = pt;
                                moveChessB_BISHOP1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_BISHOP1);
                                isB_BISHOP1 = false;
                            }
                            else
                            {
                                moveChessB_BISHOP2.nChessID = Form1.B_BISHOP;
                                moveChessB_BISHOP2.ptMovePoint = pt;
                                moveChessB_BISHOP2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_BISHOP2);
                                isB_BISHOP1 = true;
                            }
                            break;
                        case Form1.B_ELEPHANT:
                            if (isB_ELEPHANT1)
                            {
                                moveChessB_ELEPHANT1.nChessID = Form1.B_ELEPHANT;
                                moveChessB_ELEPHANT1.ptMovePoint = pt;
                                moveChessB_ELEPHANT1.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_ELEPHANT1);
                                isB_ELEPHANT1 = false;
                            }
                            else
                            {
                                moveChessB_ELEPHANT2.nChessID = Form1.B_ELEPHANT;
                                moveChessB_ELEPHANT2.ptMovePoint = pt;
                                moveChessB_ELEPHANT2.SetPosition(nextPosition);
                                _moveChessManager.InitializeChessman(moveChessB_ELEPHANT2);
                                isB_ELEPHANT1 = true;
                            }
                            break;
                        case Form1.B_PAWN:
                            switch (v)
                            {
                                case 1:
                                    moveChessB_PAWN1.nChessID = Form1.B_PAWN;
                                    moveChessB_PAWN1.ptMovePoint = pt;
                                    moveChessB_PAWN1.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessB_PAWN1);
                                    v++;
                                    break;
                                case 2:
                                    moveChessB_PAWN2.nChessID = Form1.B_PAWN;
                                    moveChessB_PAWN2.ptMovePoint = pt;
                                    moveChessB_PAWN2.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessB_PAWN2);
                                    v++;
                                    break;
                                case 3:
                                    moveChessB_PAWN3.nChessID = Form1.B_PAWN;
                                    moveChessB_PAWN3.ptMovePoint = pt;
                                    moveChessB_PAWN3.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessB_PAWN3);
                                    v++;
                                    break;
                                case 4:
                                    moveChessB_PAWN4.nChessID = Form1.B_PAWN;
                                    moveChessB_PAWN4.ptMovePoint = pt;
                                    moveChessB_PAWN4.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessB_PAWN4);
                                    v++;
                                    break;
                                case 5:
                                    moveChessB_PAWN5.nChessID = Form1.B_PAWN;
                                    moveChessB_PAWN5.ptMovePoint = pt;
                                    moveChessB_PAWN5.SetPosition(nextPosition);
                                    _moveChessManager.InitializeChessman(moveChessB_PAWN5);
                                    v = 1;
                                    break;
                            }
                            break;
                    }


                    //m_Chessman.Draw(&MemDC, m_ChessBoard[i][j] - 1, pt, ILD_TRANSPARENT);
                }
            //MoveChess moveChess = new MoveChess(_moveChessTexture);
            //bullet.SetColor(new Color(0, 1, 0, 1));
            //Vector firstPostion = _sprite.GetPosition();
            //firstPostion.X = _sprite.GetPosition().X - 228.5;
            //firstPostion.Y = _sprite.GetPosition().Y - 256.5;
            //moveChess.SetPosition(firstPostion);
            //_moveChessManager.InitializeChessman(moveChess);
        }

        public void End()
        {
            moveChessR_KING.Dead = true;
            moveChessR_CAR1.Dead = true;
            moveChessR_CAR2.Dead = true;
            moveChessR_HORSE1.Dead = true;
            moveChessR_HORSE2.Dead = true;
            moveChessR_CANON1.Dead = true;
            moveChessR_CANON2.Dead = true;
            moveChessR_BISHOP1.Dead = true;
            moveChessR_BISHOP2.Dead = true;
            moveChessR_ELEPHANT1.Dead = true;
            moveChessR_ELEPHANT2.Dead = true;
            moveChessR_PAWN1.Dead = true;
            moveChessR_PAWN2.Dead = true;
            moveChessR_PAWN3.Dead = true;
            moveChessR_PAWN4.Dead = true;
            moveChessR_PAWN5.Dead = true;

            moveChessB_KING.Dead = true;
            moveChessB_CAR1.Dead = true;
            moveChessB_CAR2.Dead = true;
            moveChessB_HORSE1.Dead = true;
            moveChessB_HORSE2.Dead = true;
            moveChessB_CANON1.Dead = true;
            moveChessB_CANON2.Dead = true;
            moveChessB_BISHOP1.Dead = true;
            moveChessB_BISHOP2.Dead = true;
            moveChessB_ELEPHANT1.Dead = true;
            moveChessB_ELEPHANT2.Dead = true;
            moveChessB_PAWN1.Dead = true;
            moveChessB_PAWN2.Dead = true;
            moveChessB_PAWN3.Dead = true;
            moveChessB_PAWN4.Dead = true;
            moveChessB_PAWN5.Dead = true;

            _moveChessManager.End();
        }

        public void Move(Vector amount)
        {
            amount *= _speed;
            //WriteLine($"ChessBoard_X:{_sprite.GetWidth() / 2} ChessBoard_Y:{_sprite.GetHeight() / 2}");
            _sprite.SetPosition(_sprite.GetPosition() + amount);
        }

        //internal void OnCollision(Enemy enemy)
        //{
        //    _dead = true;
        //}

        //internal void OnCollision(Bullet bullet)
        //{
        //    _dead = true;
        //}

        internal Vector GetPosition()
        {
            return _sprite.GetPosition();
        }

        public void Render(Renderer renderer)
        {
            Render_Debug();
            renderer.DrawSprite(_sprite);
        }
    }
}
