using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ThursdayMonoGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _gameFont, _debugFont;
        private float _textX, _textY;
        private string _text;
        private int _counter;
        private int _timer;
        private bool _movingLeft, _movingUp;
        private float _speed;
        private Random _rng;

        private List<Color> _textColors;
        private int _colorIndex;
        private Color _backGroundColor;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            SpriteFont font;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _counter = 0;
            _timer = 0;
            _movingLeft = true;
            _movingUp = true;
            _speed = 3;


            _rng = new Random();
            _textX = _rng.Next(0, 480);
            _textY = _rng.Next(0, 300);

            _colorIndex = 0;

            _textColors = new List<Color>();
            _textColors.Add(Color.Black); 
            _textColors.Add(Color.White);
            _textColors.Add(Color.Red);
            _textColors.Add(Color.Green);
            _textColors.Add(Color.Blue);
            _textColors.Add(Color.Yellow);
            _textColors.Add(Color.AliceBlue);
            _textColors.Add(Color.Aqua);

            _backGroundColor = _textColors[_rng.Next(0, _textColors.Count)];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            _gameFont = Content.Load<SpriteFont>("GameFont");
            _debugFont = Content.Load<SpriteFont>("DebugFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _counter++;
            if (_counter % 60 == 0)
                _timer++;


            if (_movingLeft == true)
                _textX-=_speed;
            else
                _textX+=_speed;

            if (_movingUp == true)
                _textY -= _speed;
            else
                _textY += _speed;


            if (_textX <= 0)
            {
                _movingLeft = false;
                _colorIndex++;
            }

            if (_textX >= 750)
            {
                _movingLeft = true;
                _colorIndex++;
            }


            if (_textY <= 0)
            {
                _movingUp = false;
                _colorIndex++;

            }

            if (_textY >= 440)
            {
                _movingUp = true;
                _colorIndex++;
            }

            if (_colorIndex > _textColors.Count - 1)
                _colorIndex = 0;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backGroundColor);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            //all our drawing
            _spriteBatch.DrawString(_debugFont, "Moving Left:" + _movingLeft, new Vector2(20, 425), Color.White);
            _spriteBatch.DrawString(_debugFont, "Moving Up:" + _movingUp, new Vector2(20,450), Color.White);

            _spriteBatch.DrawString(_gameFont, "" + _timer, new Vector2(_textX, _textY), _textColors[_colorIndex]);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}