using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using Engine.Input;
using Tao.OpenGl;
using Tao.DevIl;
using static Engine.Input.Mouse;

namespace Chinese_chess
{
    struct POINT
    {
        public long x;
        public long y;
    }

    public partial class Form1 : Form
    {
        #region
        public const int NOCHESS = 0;
        public const int B_KING = 1;
        public const int B_CAR = 2;
        public const int B_HORSE = 3;
        public const int B_CANON = 4;
        public const int B_BISHOP = 5;
        public const int B_ELEPHANT = 6;
        public const int B_PAWN = 7;
        public const int B_BEGIN = B_KING;
        public const int B_END = B_PAWN;

        public const int R_KING = 8;
        public const int R_CAR = 9;
        public const int R_HORSE = 10;
        public const int R_CANON = 11;
        public const int R_BISHOP = 12;
        public const int R_ELEPHANT = 13;
        public const int R_PAWN = 14;
        public const int R_BEGIN = R_KING;
        public const int R_END = R_PAWN;
        #endregion

        public static byte[,] InitChessBoard1 = new byte[10, 9]{
            { B_CAR,   B_HORSE, B_ELEPHANT, B_BISHOP, B_KING,  B_BISHOP, B_ELEPHANT, B_HORSE, B_CAR},
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS},
            { NOCHESS, B_CANON, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    B_CANON, NOCHESS },
            { B_PAWN,  NOCHESS, B_PAWN,     NOCHESS,  B_PAWN,  NOCHESS,  B_PAWN,     NOCHESS, B_PAWN },
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },

            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },
            { R_PAWN,  NOCHESS, R_PAWN,     NOCHESS,  R_PAWN,  NOCHESS,  R_PAWN,     NOCHESS, R_PAWN },
            { NOCHESS, R_CANON, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    R_CANON, NOCHESS },
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },
            { R_CAR,   R_HORSE, R_ELEPHANT, R_BISHOP, R_KING,  R_BISHOP, R_ELEPHANT, R_HORSE, R_CAR }};

        public static byte[,] InitChessBoard = new byte[10, 9]{
            { R_CAR,   R_HORSE, R_ELEPHANT, R_BISHOP, R_KING,  R_BISHOP, R_ELEPHANT, R_HORSE, R_CAR },
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },
            { NOCHESS, R_CANON, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    R_CANON, NOCHESS },
            { R_PAWN,  NOCHESS, R_PAWN,     NOCHESS,  R_PAWN,  NOCHESS,  R_PAWN,     NOCHESS, R_PAWN },
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },

            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },
            { B_PAWN,  NOCHESS, B_PAWN,     NOCHESS,  B_PAWN,  NOCHESS,  B_PAWN,     NOCHESS, B_PAWN },
            { NOCHESS, B_CANON, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    B_CANON, NOCHESS },
            { NOCHESS, NOCHESS, NOCHESS,    NOCHESS,  NOCHESS, NOCHESS,  NOCHESS,    NOCHESS, NOCHESS },
            { B_CAR,   B_HORSE, B_ELEPHANT, B_BISHOP, B_KING,  B_BISHOP, B_ELEPHANT, B_HORSE, B_CAR }};

        bool _fullscreen = false;
        FastLoop _fastLoop;
        StateSystem _system = new StateSystem();
        Input _input = new Input();
        TextureManager _textureManager = new TextureManager();
        SoundManager _soundManager = new SoundManager();
        //MouseButton mouseButton = new MouseButton();
        Engine.Font _generalFont;
        Engine.Font _titleFont;
        Size WindowSize;

        //public static byte[,] m_ChessBoard = new byte[10, 9];
        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl.InitializeContexts();

            _input.Mouse = new Mouse(this, simpleOpenGlControl);
            _input.Keyboard = new Keyboard(simpleOpenGlControl);

            WindowSize = InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeGameData();
            InitializeFonts();
            InitializeGameState(WindowSize);

            _fastLoop = new FastLoop(GameLoop);
        }
        PersistantGameData _persistantGameData = new PersistantGameData();

        public bool IsBlack(int x)
        {
            return (x >= B_BEGIN && x <= B_END);
        }

        public bool IsRed(int x)
        {
            return (x >= R_BEGIN && x <= R_END);
        }

        public bool IsSameSide(int x, int y)
        {
            return ((IsBlack(x) && IsBlack(y)) || (IsRed(x) && IsRed(y)));
        }

        private void InitializeGameData()
        {
            LevelDescription level = new LevelDescription();
            ///<remarks>关卡持续时间</remarks>
            level.Time = 300;
            _persistantGameData.CurrentLevel = level;
        }
        /// <summary>
        /// 游戏循环处理
        /// </summary>
        /// <param name="elapsedTime">多少时间循环一次</param>
        private void GameLoop(double elapsedTime)
        {
            _input.MousePosition = _input.Mouse.UpdateMousePosition();
            _input.mouseButton = _input.Mouse.UpdateMouseButtons();
            //UpdateInput();
            simpleOpenGlControl.Refresh();
            UpdateInput(elapsedTime);
            _system.Update(elapsedTime);
            _system.Render();
        }

        /// <summary>
        /// 提交光标位置
        /// </summary>
        //private void UpdateInput()
        //{
        //    // Reset buttons
        //    _input.Mouse.MiddlePressed = false;
        //    _input.Mouse.LeftPressed = false;
        //    _input.Mouse.RightPressed = false;

        //    if (_input.Mouse._leftClickDetect)
        //    {
        //        _input.Mouse.LeftPressed = true;
        //        _input.Mouse._leftClickDetect = false;
        //    }

        //    if (_input.Mouse._rightClickDetect)
        //    {
        //        _input.Mouse.RightPressed = true;
        //        _input.Mouse._rightClickDetect = false;
        //    }

        //    if (_input.Mouse._middleClickDetect)
        //    {
        //        _input.Mouse.MiddlePressed = true;
        //        _input.Mouse._middleClickDetect = false;
        //    }
        //}

        private void UpdateInput(double elapsedTime)
        {
            _input.Update(elapsedTime);
        }
        /// <summary>
        /// 全屏显示或窗口
        /// </summary>
        private Size InitializeDisplay()
        {
            if (_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(1400, 768);
            }
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
            return ClientSize;
        }

        private void InitializeTextures()
        {
            // Init DevIl
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

            // Textures are loaded here.
            _textureManager.LoadTexture("moveChessR_CAR", "./Chess/RR.GIF");
            _textureManager.LoadTexture("selectedChessR_CAR", "./Chess/RRS.GIF");
            _textureManager.LoadTexture("moveChessR_HORSE", "./Chess/RN.GIF");
            _textureManager.LoadTexture("selectedChessR_HORSE", "./Chess/RNS.GIF");
            _textureManager.LoadTexture("moveChessR_CANON", "./Chess/RC.GIF");
            _textureManager.LoadTexture("selectedChessR_CANON", "./Chess/RCS.GIF");
            _textureManager.LoadTexture("moveChessR_ELEPHANT", "./Chess/RB.GIF");
            _textureManager.LoadTexture("selectedChessR_ELEPHANT", "./Chess/RBS.GIF");
            _textureManager.LoadTexture("moveChessR_BISHOP", "./Chess/RA.GIF");
            _textureManager.LoadTexture("selectedChessR_BISHOP", "./Chess/RAS.GIF");
            _textureManager.LoadTexture("moveChessR_KING", "./Chess/RK.GIF");
            _textureManager.LoadTexture("selectedChessR_KING", "./Chess/RKS.GIF");
            _textureManager.LoadTexture("moveChessR_PAWN", "./Chess/RP.GIF");
            _textureManager.LoadTexture("selectedChessR_PAWN", "./Chess/RPS.GIF");
            _textureManager.LoadTexture("moveChessB_CAR", "./Chess/BR.GIF");
            _textureManager.LoadTexture("selectedChessB_CAR", "./Chess/BRS.GIF");
            _textureManager.LoadTexture("moveChessB_HORSE", "./Chess/BN.GIF");
            _textureManager.LoadTexture("selectedChessB_HORSE", "./Chess/BNS.GIF");
            _textureManager.LoadTexture("moveChessB_CANON", "./Chess/BC.GIF");
            _textureManager.LoadTexture("selectedChessB_CANON", "./Chess/BCS.GIF");
            _textureManager.LoadTexture("moveChessB_ELEPHANT", "./Chess/BB.GIF");
            _textureManager.LoadTexture("selectedChessB_ELEPHANT", "./Chess/BBS.GIF");
            _textureManager.LoadTexture("moveChessB_BISHOP", "./Chess/BA.GIF");
            _textureManager.LoadTexture("selectedChessB_BISHOP", "./Chess/BAS.GIF");
            _textureManager.LoadTexture("moveChessB_KING", "./Chess/BK.GIF");
            _textureManager.LoadTexture("selectedChessB_KING", "./Chess/BKS.GIF");
            _textureManager.LoadTexture("moveChessB_PAWN", "./Chess/BP.GIF");
            _textureManager.LoadTexture("selectedChessB_PAWN", "./Chess/BPS.GIF");
            _textureManager.LoadTexture("moveChessNOCHESS", "./Chess/OO.GIF");
            _textureManager.LoadTexture("selectedChessNOCHESS", "./Chess/OOS.GIF");

            //_textureManager.LoadTexture("moveChessR_CAR", "./Chess/RR.GIF");
            //_textureManager.LoadTexture("selectedChessR_CAR", "./Chess/RR.GIF");
            //_textureManager.LoadTexture("font", "font.tga");
            _textureManager.LoadTexture("enemy_ship", "./picture/spaceship2.tga");
            _textureManager.LoadTexture("chinese_font", "./fonts/chinese_font_0.tga", "./fonts/chinese_font_1.tga");
            _textureManager.LoadTexture("chinese_yy", "./fonts/chinese_yy_0.tga");
            _textureManager.LoadTexture("chessboard", "./picture/CHESSBOARD.JPG");
            _textureManager.LoadTexture("background", "./picture/background.png");
            _textureManager.LoadTexture("background_layer_1", "./picture/windowBkg.jpg");
            _textureManager.LoadTexture("explosion", "./picture/explode.tga");
        }
        /// <summary>
        /// 初始化游戏状态
        /// </summary>
        private void InitializeGameState(Size _windowSize)
        {
            // Game states are loaded here                        
            _system.AddState("start_menu", new StartMenuState(_titleFont, _generalFont, _input, _system, _textureManager, _windowSize));
            _system.AddState("inner_game", new InnerGameState(_system, _input, _textureManager, _persistantGameData, _generalFont, _titleFont, _windowSize));
            _system.AddState("game_over", new GameOverState(_persistantGameData, _system, _input, _generalFont, _titleFont));
            //system.AddState("circle_state", new CircleIntersectionState(_input));
            //_system.AddState("mouse_test", new MouseButtonState(_input));
            _system.ChangeState("start_menu");
            //_system.ChangeState("circle_state");
            //_system.ChangeState("mouse_test");
        }


        private void InitializeSounds()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化字体
        /// </summary>
        private void InitializeFonts()
        {
            _titleFont = new Engine.Font(_textureManager.Get("chinese_font"), FontParser.Parse("./fonts/chinese_font.fnt", 2));

            _generalFont = new Engine.Font(_textureManager.Get("chinese_yy"), FontParser.Parse("./fonts/chinese_yy.fnt", 1));
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(int width, int height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
    }
}
