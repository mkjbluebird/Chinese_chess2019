using Engine;
using Engine.Input;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using Button = Engine.Button;
using Color = Engine.Color;
using Font = Engine.Font;

namespace Chinese_chess
{
    class Level
    {
        MoveChessManager _moveChessManager = new MoveChessManager(new RectangleF(-1300 / 2, -750 / 2, 1300, 750));
        Input _input;
        PersistantGameData _gameData;
        ChessBoard _chessBoard;
        TextureManager _textureManager;
        EffectsManager _effectsManager;
        Background _background;
        Background _backgroundLayer;
        bool isEnd = true;
        //List<Enemy> _enemyList = new List<Enemy>();
        AnimatedSprite _testSprite = new AnimatedSprite();
        public static byte[,] m_ChessBoard = new byte[10, 9];

        Font _generalFont;
        Font _titleFont;
        //Input _menuinput;
        RadioVerticalMenu _optionmenu;
        RadioVerticalMenu _enginemenu;
        RadioHorizontalMenu _settingmenu;

        Renderer _renderer = new Renderer();
        Text _optiontitle;
        StateSystem _system;
        Setting settingChecked;
        double widthScale2;
        double heightScale2;
        Size _windowSize;

        bool optionChecked = true;
        bool engineChecked = false;
        bool playerChecked = true;
        bool computerChecked = false;
        bool NegamaxChecked = false;
        bool AlphaBetaChecked = false;
        bool FSABChecked = false;
        bool AspirationChecked = false;
        bool PVSChecked = false;
        bool IDAChecked = false;
        bool ATTChecked = false;
        bool AHHChecked = false;
        bool MdtfChecked = false;
        bool NTTHHChecked = true;
        public struct Setting
        {
            public bool optionChecked;
            public bool engineChecked;
        }

        public struct Option
        {
            public bool playerChecked;
            public bool computerChecked;
        }

        public struct Engines
        {
            public bool NegamaxChecked;
            public bool AlphaBetaChecked;
            public bool FSABChecked;
            public bool AspirationChecked;
            public bool PVSChecked;
            public bool IDAChecked;
            public bool ATTChecked;
            public bool AHHChecked;
            public bool MdtfChecked;
            public bool NTTHHChecked;
        }
        //EnemyManager _enemyManager;
        public Level(Input input, TextureManager textureManager, PersistantGameData gameData, Engine.Font generalFont, Engine.Font titleFont, Size windowSize)
        {
            //_testSprite.Texture = textureManager.Get("explosion");
            //_testSprite.SetAnimation(4, 4);

            _input = input;
            _gameData = gameData;
            _textureManager = textureManager;
            _effectsManager = new EffectsManager(_textureManager);

            _background = new Background(textureManager.Get("background"));
            //_background.Speed = 0.15f;
            widthScale2 = _windowSize.Width / 900.0;
            heightScale2 = _windowSize.Height / 695.0;
            _background.SetScale(2.0, 2.0);

            //_backgroundLayer = new Background(textureManager.Get("background_layer_1"));
            //_backgroundLayer.Speed = 0.1f;
            //_backgroundLayer.SetScale(2.0, 2.0);
            m_ChessBoard = Form1.InitChessBoard;
            _chessBoard = new ChessBoard(_textureManager, _moveChessManager);

            //_system = system;

            //_input = input;

            _generalFont = generalFont;
            _titleFont = titleFont;
            settingChecked = SettingMenu();
            Option optionChecked = OptionMenu();
            Engines engineChecked = EngineMenu();
            _enginemenu._rvdisable = true;

            _optiontitle = new Text("选项", _titleFont);
            _optiontitle.SetColor(new Color(0, 0, 0, 1));
            //Center on the x and place somewhere near the top
            _optiontitle.SetPosition(-_optiontitle.Width / 2 - 450, 300);

            //EngineMenu();
            _optiontitle = new Text("引擎", _titleFont);
            _optiontitle.SetColor(new Color(0, 0, 0, 1));
            // Center on the x and place somewhere near the top
            _optiontitle.SetPosition(-_optiontitle.Width / 2 + 450, 300);

            //_enemyManager = new EnemyManager(_textureManager, _effectsManager, _bulletManager, _playerCharacter, -1300);
        }

        private Setting SettingMenu()
        {
            _settingmenu = new RadioHorizontalMenu(-450, 300, _input, 900);
            Text label1 = new Text("选项", _titleFont);
            Text label2 = new Text("引擎", _titleFont);
            Setting _settingChecked = new Setting();
            float menuwidth = 200;
            float menuheight = 20;
            //Text label3 = new Text("引擎2", _titleFont);
            Button option = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label1.SetColor(new Color(0, 1, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    _settingChecked.optionChecked = true;
                    _settingChecked.engineChecked = false;
                    _optionmenu._rvdisable = false;
                    _enginemenu._rvdisable = true;


                    //OptionMenu();
                },
                label1, menuwidth, menuheight, _input);

            Button engine = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label2.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    _settingChecked.optionChecked = false;
                    _settingChecked.engineChecked = true;
                    _optionmenu._rvdisable = true;
                    _enginemenu._rvdisable = false;
                    //EngineMenu();
                },
                label2, menuwidth, menuheight, _input);
            _settingmenu.AddButton(option);
            _settingmenu.AddButton(engine);
            //_settingmenu.AddButton(engine2);
            return _settingChecked;
        }

        private Option OptionMenu()
        {
            _optionmenu = new RadioVerticalMenu(-450, 150, _input);
            Text label1 = new Text("玩家先行", _generalFont);
            Text label2 = new Text("电脑先行", _generalFont);
            float optionwidth = 100;
            float optionheight = 20;
            Option optionChecked = new Option();
            Button player = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label1.SetColor(new Color(0, 1, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    optionChecked.playerChecked = true;
                    optionChecked.computerChecked = false;
                },
                label1, optionwidth, optionheight, _input);

            Button computer = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label2.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    optionChecked.playerChecked = false;
                    optionChecked.computerChecked = true;
                },
                label2, optionwidth, optionheight, _input);
            ///<remarks>添加菜单</remarks>
            _optionmenu.AddButton(player);
            _optionmenu.AddButton(computer);

            return optionChecked;
        }
        private Engines EngineMenu()
        {
            _enginemenu = new RadioVerticalMenu(450, 150, _input, 40);
            Text label1 = new Text("负极大值引擎", _generalFont);
            Text label2 = new Text("AlphaBeta剪枝搜索引擎", _generalFont);
            Text label3 = new Text("Fail-Soft AlphaBeta剪枝搜索引擎", _generalFont);
            Text label4 = new Text("渴望搜索引擎", _generalFont);
            Text label5 = new Text("极小窗口搜索引擎", _generalFont);
            Text label6 = new Text("迭代深化AlphaBeta搜索引擎", _generalFont);
            Text label7 = new Text("AlphaBeta剪枝+置换表搜索引擎", _generalFont);
            Text label8 = new Text("Alphabeta剪枝+历史启发搜索引擎", _generalFont);
            Text label9 = new Text("Mdt(f)搜索引擎", _generalFont);
            Text label10 = new Text("NegaScout+置换表+历史启发", _generalFont);
            float enginewidth = 250;
            float engineheight = 15;
            Engines engineChecked = new Engines();
            Button Negamax = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label1.SetColor(new Color(0, 1, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = true;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label1, enginewidth, engineheight, _input);

            Button AlphaBeta = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label2.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = true;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label2, enginewidth, engineheight, _input);
            Button FSAB = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label3.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = true;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label3, enginewidth, engineheight, _input);

            Button Aspiration = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label4.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = true;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label4, enginewidth, engineheight, _input);
            Button PVS = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label5.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = true;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label5, enginewidth, engineheight, _input);

            Button IDA = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label6.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = true;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label6, enginewidth, engineheight, _input);
            Button ATT = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label7.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = true;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label7, enginewidth, engineheight, _input);

            Button AHH = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label8.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = true;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = false;
                },
                label8, enginewidth, engineheight, _input);
            Button Mdtf = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>正式开始游戏</remarks>
                    //_system.ChangeState("inner_game");
                    //label9.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label10.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = true;
                    engineChecked.NTTHHChecked = false;
                },
                label9, enginewidth, engineheight, _input);

            Button NTTHH = new Button(
                delegate (object o, EventArgs e)
                {
                    ///<remarks>退出</remarks>
                    //System.Windows.Forms.Application.Exit();
                    //label10.SetColor(new Color(0, 1, 0, 1));
                    //label1.SetColor(new Color(0, 0, 0, 1));
                    //label2.SetColor(new Color(0, 0, 0, 1));
                    //label3.SetColor(new Color(0, 0, 0, 1));
                    //label4.SetColor(new Color(0, 0, 0, 1));
                    //label5.SetColor(new Color(0, 0, 0, 1));
                    //label6.SetColor(new Color(0, 0, 0, 1));
                    //label7.SetColor(new Color(0, 0, 0, 1));
                    //label8.SetColor(new Color(0, 0, 0, 1));
                    //label9.SetColor(new Color(0, 0, 0, 1));
                    engineChecked.NegamaxChecked = false;
                    engineChecked.AlphaBetaChecked = false;
                    engineChecked.FSABChecked = false;
                    engineChecked.AspirationChecked = false;
                    engineChecked.PVSChecked = false;
                    engineChecked.IDAChecked = false;
                    engineChecked.ATTChecked = false;
                    engineChecked.AHHChecked = false;
                    engineChecked.MdtfChecked = false;
                    engineChecked.NTTHHChecked = true;
                },
                label10, enginewidth, engineheight, _input);
            ///<remarks>添加菜单</remarks>
            _enginemenu.AddButton(Negamax);
            _enginemenu.AddButton(AlphaBeta);
            _enginemenu.AddButton(FSAB);
            _enginemenu.AddButton(Aspiration);
            _enginemenu.AddButton(PVS);
            _enginemenu.AddButton(IDA);
            _enginemenu.AddButton(ATT);
            _enginemenu.AddButton(AHH);
            _enginemenu.AddButton(Mdtf);
            _enginemenu.AddButton(NTTHH);

            return engineChecked;
        }

        private void UpdateCollisions()
        {
            //_bulletManager.UpdatePlayerCollision(_playerCharacter);
            //foreach (Enemy enemy in _enemyManager.EnemyList)
            //{
            //    if (enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
            //    {
            //        enemy.OnCollision(_playerCharacter);
            //        _playerCharacter.OnCollision(enemy);
            //    }

            //    _bulletManager.UpdateEnemyCollisions(enemy);
            //}
        }

        public bool HasPlayerDied()
        {
            return _chessBoard.IsDead;
        }

        public void Update(double elapsedTime, double gameTime)
        {
            //_testSprite.Update(elapsedTime);
            //UpdateCollisions();
            _moveChessManager.Update(elapsedTime);
            //提供和控制游戏玩家
            double _x = _input.Controller.LeftControlStick.X;
            double _y = _input.Controller.LeftControlStick.Y * -1;
            Vector controlInput = new Vector(_x, _y, 0);

            if (Math.Abs(controlInput.Length()) < 0.0001)
            {
                //如果游戏角色输入非常小，哪可能不是用摇杆控制，而是用键盘控制
                if (_input.Keyboard.IsKeyHeld(Keys.Left))
                {
                    controlInput.X = -1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Right))
                {
                    controlInput.X = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Up))
                {
                    controlInput.Y = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Down))
                {
                    controlInput.Y = -1;
                }
            }
            _effectsManager.Update(elapsedTime);
            //_chessBoard.Move(controlInput * elapsedTime);
            _chessBoard.Update(elapsedTime);
            _background.Update((float)elapsedTime);

            //_backgroundLayer.Update((float)elapsedTime);
            //_enemyList.ForEach(x => x.Update(elapsedTime));
            //_enemyManager.Update(elapsedTime, gameTime);
            //在这个方法里面处理输入
            UpdateInput(elapsedTime);
        }

        public void Update(double elapsedTime)
        {
            _settingmenu.HandleInput();
            if (_optionmenu != null)
                _optionmenu.HandleInput();
            if (_enginemenu != null)
                _enginemenu.HandleInput();
        }

        private void UpdateInput(double elapsedTime)
        {

            if ((_input.Keyboard.IsKeyPressed(Keys.Space) || _input.Controller.ButtonA.Pressed) && isEnd == true)
            {
                _chessBoard.Restart();
                isEnd = false;
            }

            if (_input.Keyboard.IsKeyPressed(Keys.Enter))
            {
                _chessBoard.End();
                isEnd = true;
            }
        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            //_backgroundLayer.Render(renderer);

            //_enemyList.ForEach(x => x.Render(renderer));
            //_enemyManager.Render(renderer);
            _chessBoard.Render(renderer);
            _moveChessManager.Render(renderer);



            renderer.DrawSprite(_testSprite);
            renderer.Render();
            _effectsManager.Render(renderer);
            renderer.Render();
        }

        public void Render()
        {
            //Gl.glClearColor(1, 1, 1, 0);
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            //_renderer.DrawText(_optiontitle);
            _settingmenu.Render(_renderer);
            if (_optionmenu != null)
                _optionmenu.Render(_renderer);
            if (_enginemenu != null)
                _enginemenu.Render(_renderer);
            _renderer.Render();
        }
    }
}
