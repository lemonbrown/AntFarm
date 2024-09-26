using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AntFarm {
    public class Game1 : Game {

        private readonly int WINDOW_WIDTH = 800;
        private readonly int WINDOW_HEIGHT = 600;
        private readonly int HORIZON;

        private int _dirtWidth = 2;
        private int _dirtHeight = 2;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Particle> _dirts = new();

        private FrameCounter _frameCounter = new FrameCounter();

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;   
            HORIZON = WINDOW_HEIGHT / 2;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);          

            // let colors = ['#ead0a8', '#b69f66', '#6b5428', '#76552b', '#402905']
            var dirtColors = new List<int[]>() {
                new int[] { 234, 208, 168, 1},
                new int[] { 182, 159, 102, 1},
                new int[] { 107, 84, 40, 1},
                new int[] { 118, 85, 43, 1},
                new int[] { 64, 41, 5, 1},
            };

            FastNoiseLite noise = new FastNoiseLite();
            noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

            Random random = new Random();

            for(var i = 0; i < HORIZON / _dirtHeight; i++) {

                for(var a = 0; a < WINDOW_WIDTH/_dirtWidth; a++) {

                    var dirtTexture = new Texture2D(_graphics.GraphicsDevice, _dirtWidth, _dirtHeight);

                    var colors = new Color[_dirtWidth * _dirtHeight];

                    var dirtColor = dirtColors[random.Next(dirtColors.Count - 1)];

                    for (var j = 0; j < colors.Length; j++) {

                        colors[j] = new Color(dirtColor[0], dirtColor[1], dirtColor[2]);
                    }

                    dirtTexture.SetData(colors);

                    var dirtParticle = new Particle() {
                        Position = new Vector2(a * _dirtWidth, i * _dirtHeight + HORIZON),
                        Texture = dirtTexture
                    };

                    _dirts.Add(dirtParticle);
                }
            }

     

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);


            Color color = new Color(135, 206, 245, 1);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            // TODO: Add your drawing code here

            for (var i = 0; i < _dirts.Count; i++) {

                _spriteBatch.Draw(_dirts[i].Texture, _dirts[i].Position, Color.White);

            }

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);

            //_spriteBatch.DrawString(_spriteFont, fps, new Vector2(1, 1), Color.Black);

            Debug.WriteLine(fps);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
