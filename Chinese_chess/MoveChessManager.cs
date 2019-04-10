using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_chess
{
    class MoveChessManager
    {
        List<MoveChess> _moveChesses = new List<MoveChess>();
        List<MoveChess> _enemyBullets = new List<MoveChess>();

        RectangleF _bounds;

        public MoveChessManager(RectangleF playArea)
        {
            _bounds = playArea;
        }

        //public void Shoot(MoveChess moveChess)
        //{
        //    _moveChesses.Add(moveChess);
        //}

        public void InitializeChessman(MoveChess moveChess)
        {
            _moveChesses.Add(moveChess);
        }

        //public void EnemyShoot(MoveChess bullet)
        //{
        //    _enemyBullets.Add(bullet);
        //}

        public void UpdatePlayerCollision(ChessBoard playerCharacter)
        {
            foreach (MoveChess bullet in _enemyBullets)
            {
                if (bullet.GetBoundingBox().IntersectsWith(playerCharacter.GetBoundingBox()))
                {
                    bullet.Dead = true;
                    //playerCharacter.OnCollision(bullet);
                }
            }
        }


        public void Update(double elapsedTime)
        {
            UpdateMoveChessList(_moveChesses, elapsedTime);
            UpdateMoveChessList(_enemyBullets, elapsedTime);
        }

        public void UpdateMoveChessList(List<MoveChess> moveChessList, double elapsedTime)
        {
            moveChessList.ForEach(x => x.Update(elapsedTime));
            CheckOutOfBounds(_moveChesses);
            RemoveDeadMoveChesses(moveChessList);
        }

        public void End() => RemoveDeadMoveChesses(_moveChesses);

        private void CheckOutOfBounds(List<MoveChess> _moveChessList)
        {
            foreach (MoveChess moveChess in _moveChessList)
            {
                if (!moveChess.GetBoundingBox().IntersectsWith(_bounds))
                {
                    moveChess.Dead = true;
                }
            }
        }

        private void RemoveDeadMoveChesses(List<MoveChess> moveChessList)
        {
            for (int i = moveChessList.Count - 1; i >= 0; i--)
            {
                if (moveChessList[i].Dead)
                {
                    moveChessList.RemoveAt(i);
                }
            }
        }

        internal void Render(Renderer renderer)
        {
            _moveChesses.ForEach(x => x.Render(renderer));
            //_enemyBullets.ForEach(x => x.Render(renderer));
        }


        //internal void UpdateEnemyCollisions(Enemy enemy)
        //{
        //    foreach (MoveChess bullet in _bullets)
        //    {
        //        if (bullet.GetBoundingBox().IntersectsWith(enemy.GetBoundingBox()))
        //        {
        //            bullet.Dead = true;
        //            enemy.OnCollision(bullet);
        //        }
        //    }

        //}
    }
}
