using Engine;
using Engine.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Engine.Color;
using Button = Engine.Button;
using Tao.OpenGl;

namespace Chinese_chess
{
    class StartMenuState : IGameObject
    {
        Engine.Font _generalFont;
        Input _input;
        RadioVerticalMenu _menu;

        Renderer _renderer = new Renderer();
        Text _title;
        StateSystem _system;
        TextureManager _textureManager;
        EffectsManager _effectsManager;
        Background _backgroundLayer;
        Size _windowSize;
        double widthScale;
        double heightScale;

        public StartMenuState(Engine.Font titleFont, Engine.Font generalFont, Input input, StateSystem system, TextureManager textureManager, Size windowSize)
        {
            _system = system;

            _input = input;
            _generalFont = generalFont;
            _textureManager = textureManager;
            _windowSize = windowSize;
            _effectsManager = new EffectsManager(_textureManager);
            InitializeMenu();
            _backgroundLayer = new Background(textureManager.Get("background_layer_1"));
            //_background.Speed = 0.15f;
            widthScale = _windowSize.Width / 500.0;
            heightScale = _windowSize.Height / 375.0;
            _backgroundLayer.SetScale(widthScale, heightScale);
            _title = new Text("中国象棋", titleFont);
            _title.SetColor(new Color(0, 0, 0, 1));
            // Center on the x and place somewhere near the top
            _title.SetPosition(-_title.Width / 2, 300);
        }

        private void InitializeMenu()
        {
            float buttomwidth = 100;
            float buttomheight = 20;
            _menu = new RadioVerticalMenu(0, 150, _input, 60);
            Button startGame = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    _system.ChangeState("inner_game");
                },
                new Text("开始", _generalFont), buttomwidth, buttomheight, _input);

            Button exitGame = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    System.Windows.Forms.Application.Exit();
                },
                new Text("退出", _generalFont), buttomwidth, buttomheight, _input);
            ///<remarks>添加菜单</remarks>
            _menu.AddButton(startGame);
            _menu.AddButton(exitGame);
        }

        public void Update(double elapsedTime)
        {
            _menu.HandleInput();
            //_menu.Update(elapsedTime);
            _effectsManager.Update(elapsedTime);
        }

        public void Render(Renderer renderer)
        {
            _backgroundLayer.Render(renderer);
        }

        public void Render()
        {
            //Gl.glClearColor(0, 1, 1, 0);
            //_backgroundLayer.Render(renderer);
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            this.Render(_renderer);
            _renderer.DrawText(_title);
            _menu.Render(_renderer);
            _renderer.Render();
        }
    }
}
