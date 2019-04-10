using Engine;
using Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using Font = Engine.Font;
using System.Drawing;

namespace Chinese_chess
{
    class InnerGameState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Input _input;
        StateSystem _system;
        PersistantGameData _gameData;
        Font _generalFont;
        Font _titleFont;

        double _gameTime;
        Level _level;
        TextureManager _textureManager;
        Size _windowSize;

        public InnerGameState(StateSystem system, Input input, TextureManager textureManager, PersistantGameData gameData, Font generalFont, Font titleFont, Size windowSize)
        {
            _input = input;
            _system = system;
            _gameData = gameData;
            _generalFont = generalFont;
            _textureManager = textureManager;
            _titleFont = titleFont;
            _windowSize = windowSize;
            OnGameStart();
        }

        public void OnGameStart()
        {
            _level = new Level(_input, _textureManager, _gameData, _generalFont, _titleFont, _windowSize);
            _gameTime = _gameData.CurrentLevel.Time;
        }

        #region IGameObject Members

        public void Update(double elapsedTime)
        {
            _level.Update(elapsedTime, _gameTime);
            _level.Update(elapsedTime);
            _gameTime -= elapsedTime;

            //if(_gameTime <= 0)
            //{
            //    OnGameStart();
            //    _gameData.JustWon = true;
            //    ///<remarks>游戏终了</remarks>
            //    _system.ChangeState("game_over");
            //}

            if (_level.HasPlayerDied())
            {
                OnGameStart();
                _gameData.JustWon = false;
                ///<remarks>游戏终了</remarks>
                _system.ChangeState("game_over");
            }
        }

        public void Render()
        {
            _renderer.Render();
            _renderer.Render(_input);
            ///<remarks>渲染图片,棋子,特效</remarks>
            _level.Render(_renderer);
            ///<remarks>渲染菜单</remarks>
            _level.Render();
            //Gl.glClearColor(0, 1, 1, 0);
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_POINTS);
            {
                Gl.glVertex2f(_input.MousePosition.X, _input.MousePosition.Y);
            }
            Gl.glEnd();

        }
        #endregion
    }
}
