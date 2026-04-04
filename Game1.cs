using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Casino_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        enum Screen
        {
            Idle,
            Bet,
            Race
        }

        Screen screen;

        //Fonts
        SpriteFont largeFont,
            mediumFont;

        //Selection UI
        Rectangle horse1Rect,
            horse2Rect,
            horse3Rect,
            horse4Rect,
            horse5Rect,
            horse6Rect,
            horse7Rect,
            horse8Rect;

        Texture2D selectionButtonTexture,
            selectionButton1, selectionButton2, selectionButton3, selectionButton4,
            selectionButton5, selectionButton6, selectionButton7, selectionButton8;

        Color horse1Color,
            horse2Color,
            horse3Color,
            horse4Color,
            horse5Color,
            horse6Color,
            horse7Color,
            horse8Color;

        //Horses
        Rectangle stormfuhrRect,
            beyonceneighRect,
            greasedLightningRect,
            twilightSparkleRect,
            hoofJackmanRect,
            shadowfaxRect,
            ponyStarkRect,
            potooooooooRect;

        Texture2D stormfuhrTexture,
                stormfuhr1, stormfuhr2, stormfuhr3,
                stormfuhr4, stormfuhr5, stormfuhr6,

            beyonceneighTexture,
                beyonceneigh1, beyonceneigh2, beyonceneigh3,
                beyonceneigh4, beyonceneigh5, beyonceneigh6,

            greasedLightningTexture,
                greasedLightning1, greasedLightning2, greasedLightning3,
                greasedLightning4, greasedLightning5, greasedLightning6,

            twilightSparkleTexture,
                twilightSparkle1, twilightSparkle2, twilightSparkle3,
                twilightSparkle4, twilightSparkle5, twilightSparkle6,

            hoofJackmanTexture,
                hoofJackman1, hoofJackman2, hoofJackman3,
                hoofJackman4, hoofJackman5, hoofJackman6,

            shadowfaxTexture,
                shadowfax1, shadowfax2, shadowfax3,
                shadowfax4, shadowfax5, shadowfax6,

            ponyStarkTexture,
                ponyStark1, ponyStark2, ponyStark3,
                ponyStark4, ponyStark5, ponyStark6,

            potooooooooTexture,
                potoooooooo1, potoooooooo2, potoooooooo3,
                potoooooooo4, potoooooooo5, potoooooooo6;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.Window.Title = "Casino Game";

            window = new Rectangle(0, 0, 1900, 980);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            screen = Screen.Bet;

            //Selection UI
            horse1Rect = new Rectangle(200, 300, 664, 120);
            horse2Rect = new Rectangle(200, 450, 664, 120);
            horse3Rect = new Rectangle(200, 600, 664, 120);
            horse4Rect = new Rectangle(200, 750, 664, 120);
            horse5Rect = new Rectangle(1000, 300, 664, 120);
            horse6Rect = new Rectangle(1000, 450, 664, 120);
            horse7Rect = new Rectangle(1000, 600, 664, 120);
            horse8Rect = new Rectangle(1000, 750, 664, 120);

            horse1Color = Color.White;
            horse2Color = Color.White;
            horse3Color = Color.White;
            horse4Color = Color.White;
            horse5Color = Color.White;
            horse6Color = Color.White;
            horse7Color = Color.White;
            horse8Color = Color.White;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Fonts
            largeFont = Content.Load<SpriteFont>("largeFont");
            mediumFont = Content.Load<SpriteFont>("mediumFont");

            //UI
            selectionButtonTexture = Content.Load<Texture2D>("SelectionButton");
            selectionButton1 = Content.Load<Texture2D>("Select1");
            selectionButton2 = Content.Load<Texture2D>("Select2");
            selectionButton3 = Content.Load<Texture2D>("Select3");           
            selectionButton4 = Content.Load<Texture2D>("Select4");
            selectionButton5 = Content.Load<Texture2D>("Select5");
            selectionButton6 = Content.Load<Texture2D>("Select6");
            selectionButton7 = Content.Load<Texture2D>("Select7");
            selectionButton8 = Content.Load<Texture2D>("Select8");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Bet)
            {
                _spriteBatch.DrawString(largeFont, "Choose a horse to bet on", new Vector2(400, 125), Color.White);

                _spriteBatch.Draw(selectionButtonTexture, horse1Rect, Color.White);
                _spriteBatch.Draw(selectionButton1, horse1Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Stormfuhr", new Vector2(340, 332), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse2Rect, Color.White);
                _spriteBatch.Draw(selectionButton2, horse2Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Beyonceneigh", new Vector2(340, 482), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse3Rect, Color.White);
                _spriteBatch.Draw(selectionButton3, horse3Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Greased Lightning", new Vector2(340, 632), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse4Rect, Color.White);
                _spriteBatch.Draw(selectionButton4, horse4Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Twilight Sparkle", new Vector2(340, 782), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse5Rect, Color.White);
                _spriteBatch.Draw(selectionButton5, horse5Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Hoof Jackman", new Vector2((990 + 150), 332), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse6Rect, Color.White);
                _spriteBatch.Draw(selectionButton6, horse6Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Shadowfax", new Vector2((990 + 150), 482), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse7Rect, Color.White);
                _spriteBatch.Draw(selectionButton7, horse7Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Pony Stark", new Vector2((990 + 150), 632), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse8Rect, Color.White);
                _spriteBatch.Draw(selectionButton8, horse8Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Potoooooooo", new Vector2((990 + 150), 782), horse1Color);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
