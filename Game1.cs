using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.WebSockets;

namespace Casino_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Basic game info
        Rectangle window;

        Screen screen;
        enum Screen
        {
            Idle,
            SelectHorse,
            Bet,
            Race
        }

        MouseState mouseState,
            prevMouseState;

        Random generator = new Random();

        //Fonts
        SpriteFont XLFont,
            largeFont,
            mediumFont;

        //Backgrounds
        Rectangle backgroundRect,
            altBackgroundRect;

        Texture2D backgroundTexture,
            background1, background2, raceTrack;

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

        //Betting UI
        Rectangle bet1Rect,
            bet2Rect,
            bet3Rect;

        Texture2D betButtonTexture;

        //Betting system
        int houseBalance,
            betAmount,
            horseSelected;

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
                stormfuhr4, stormfuhr5,

            beyonceneighTexture,
                beyonceneigh1, beyonceneigh2, beyonceneigh3,
                beyonceneigh4, beyonceneigh5,

            greasedLightningTexture,
                greasedLightning1, greasedLightning2, greasedLightning3,
                greasedLightning4, greasedLightning5,

            twilightSparkleTexture,
                twilightSparkle1, twilightSparkle2, twilightSparkle3,
                twilightSparkle4, twilightSparkle5,

            hoofJackmanTexture,
                hoofJackman1, hoofJackman2, hoofJackman3,
                hoofJackman4, hoofJackman5,

            shadowfaxTexture,
                shadowfax1, shadowfax2, shadowfax3,
                shadowfax4, shadowfax5,

            ponyStarkTexture,
                ponyStark1, ponyStark2, ponyStark3,
                ponyStark4, ponyStark5,

            potooooooooTexture,
                potoooooooo1, potoooooooo2, potoooooooo3,
                potoooooooo4, potoooooooo5;

        //Race
        string countdownString,
            raceTimeString;

        int countdownTimer,
            changeSpeed,
            firstPlace,
            secondPlace,
            thirdPlace;

        float elapsedTime,
            countdownTime,
            horseAnimation,
            changeSpeedTimer;

        Rectangle finishLineRect;

        Texture2D finishLineTexture,
            leaderboardTexture;

        Boolean firstPlaceSet,
            secondPlaceSet,
            thirdPlaceSet;

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

            screen = Screen.SelectHorse;

            //Backgrounds
            backgroundRect = window;
            altBackgroundRect = new Rectangle(0, 0, 4939, window.Height);

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

            //Betting UI
            bet1Rect = new Rectangle(300, 440, 380, 200);
            bet2Rect = new Rectangle(750, 440, 380, 200);
            bet3Rect = new Rectangle(1200, 440, 380, 200);

            betAmount = 0;
            horseSelected = 0;

            //Race
            countdownString = "3";
            raceTimeString = "0";

            finishLineRect = new Rectangle((window.Width + 150), 0, 150, window.Height);

            firstPlace = 0;
            secondPlace = 0;
            thirdPlace = 0;

            firstPlaceSet = false;
            secondPlaceSet = false;
            thirdPlaceSet = false;

            //Horses
            horseAnimation = 0;
            stormfuhrRect = new Rectangle(-638, 0, 425, 300);
            beyonceneighRect = new Rectangle(-638, 100, 425, 300);
            greasedLightningRect = new Rectangle(-638, 200, 425, 300);
            twilightSparkleRect = new Rectangle(-638, 300, 425, 300);
            hoofJackmanRect = new Rectangle(-638, 400, 425, 300);
            shadowfaxRect = new Rectangle(-638, 500, 425, 300);
            ponyStarkRect = new Rectangle(-638, 600, 425, 300);
            potooooooooRect = new Rectangle(-638, 700, 425, 300);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Fonts
            XLFont = Content.Load<SpriteFont>("XLFont");
            largeFont = Content.Load<SpriteFont>("largeFont");
            mediumFont = Content.Load<SpriteFont>("mediumFont");

            //Backgrounds
            background2 = Content.Load<Texture2D>("testBackground");
            raceTrack = Content.Load<Texture2D>("Racetrack");

            //Selection UI
            selectionButtonTexture = Content.Load<Texture2D>("SelectionButton");
            selectionButton1 = Content.Load<Texture2D>("Select1");
            selectionButton2 = Content.Load<Texture2D>("Select2");
            selectionButton3 = Content.Load<Texture2D>("Select3");           
            selectionButton4 = Content.Load<Texture2D>("Select4");
            selectionButton5 = Content.Load<Texture2D>("Select5");
            selectionButton6 = Content.Load<Texture2D>("Select6");
            selectionButton7 = Content.Load<Texture2D>("Select7");
            selectionButton8 = Content.Load<Texture2D>("Select8");

            //Betting UI
            betButtonTexture = Content.Load<Texture2D>("Button");

            //Race
            finishLineTexture = Content.Load<Texture2D>("end");
            leaderboardTexture = Content.Load<Texture2D>("Leaderboard");

            //Horses
            stormfuhr1 = Content.Load<Texture2D>("stormfuhr0");
            stormfuhr2 = Content.Load<Texture2D>("stormfuhr1");
            stormfuhr3 = Content.Load<Texture2D>("stormfuhr2");
            stormfuhr4 = Content.Load<Texture2D>("stormfuhr3");
            stormfuhr5 = Content.Load<Texture2D>("stormfuhr4");
            stormfuhrTexture = stormfuhr1;

            beyonceneigh1 = Content.Load<Texture2D>("beyonceneigh0");
            beyonceneigh2 = Content.Load<Texture2D>("beyonceneigh1");
            beyonceneigh3 = Content.Load<Texture2D>("beyonceneigh2");
            beyonceneigh4 = Content.Load<Texture2D>("beyonceneigh3");
            beyonceneigh5 = Content.Load<Texture2D>("beyonceneigh4");
            beyonceneighTexture = beyonceneigh1;

            greasedLightning1 = Content.Load<Texture2D>("greasedlightning0");
            greasedLightning2 = Content.Load<Texture2D>("greasedlightning1");
            greasedLightning3 = Content.Load<Texture2D>("greasedlightning2");
            greasedLightning4 = Content.Load<Texture2D>("greasedlightning3");
            greasedLightning5 = Content.Load<Texture2D>("greasedlightning4");
            greasedLightningTexture = greasedLightning1;

            twilightSparkle1 = Content.Load<Texture2D>("twilightsparkle0");
            twilightSparkle2 = Content.Load<Texture2D>("twilightsparkle1");
            twilightSparkle3 = Content.Load<Texture2D>("twilightsparkle2");
            twilightSparkle4 = Content.Load<Texture2D>("twilightsparkle3");
            twilightSparkle5 = Content.Load<Texture2D>("twilightsparkle4");
            twilightSparkleTexture = twilightSparkle1;

            hoofJackman1 = Content.Load<Texture2D>("hoofjackman0");
            hoofJackman2 = Content.Load<Texture2D>("hoofjackman1");
            hoofJackman3 = Content.Load<Texture2D>("hoofjackman2");
            hoofJackman4 = Content.Load<Texture2D>("hoofjackman3");
            hoofJackman5 = Content.Load<Texture2D>("hoofjackman4");
            hoofJackmanTexture = hoofJackman1;

            shadowfax1 = Content.Load<Texture2D>("shadowfax0");
            shadowfax2 = Content.Load<Texture2D>("shadowfax1");
            shadowfax3 = Content.Load<Texture2D>("shadowfax2");
            shadowfax4 = Content.Load<Texture2D>("shadowfax3");
            shadowfax5 = Content.Load<Texture2D>("shadowfax4");
            shadowfaxTexture = shadowfax1;

            ponyStark1 = Content.Load<Texture2D>("ponystark0");
            ponyStark2 = Content.Load<Texture2D>("ponystark1");
            ponyStark3 = Content.Load<Texture2D>("ponystark2");
            ponyStark4 = Content.Load<Texture2D>("ponystark3");
            ponyStark5 = Content.Load<Texture2D>("ponystark4");
            ponyStarkTexture = ponyStark1;

            potoooooooo1 = Content.Load<Texture2D>("potoooooooo0");
            potoooooooo2 = Content.Load<Texture2D>("potoooooooo1");
            potoooooooo3 = Content.Load<Texture2D>("potoooooooo2");
            potoooooooo4 = Content.Load<Texture2D>("potoooooooo3");
            potoooooooo5 = Content.Load<Texture2D>("potoooooooo4");
            potooooooooTexture = potoooooooo1;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (screen == Screen.Idle)
            {
                horseSelected = 0;

                betAmount = 0;

                backgroundRect = window;

                if (mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.SelectHorse;
                }
            }
            if (screen == Screen.SelectHorse)
            {
                backgroundTexture = background2;

                if (horse1Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 1;
                    screen = Screen.Bet;
                }
                else if (horse1Rect.Contains(mouseState.Position))
                {
                    horse1Color = Color.Green;
                }
                else
                {
                    horse1Color = Color.White;
                }

                if (horse2Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 2;
                    screen = Screen.Bet;
                }
                else if (horse2Rect.Contains(mouseState.Position))
                {
                    horse2Color = Color.Green;
                }
                else
                {
                    horse2Color = Color.White;
                }

                if (horse3Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 3;
                    screen = Screen.Bet;
                }
                else if (horse3Rect.Contains(mouseState.Position))
                {
                    horse3Color = Color.Green;
                }
                else
                {
                    horse3Color = Color.White;
                }

                if (horse4Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 4;
                    screen = Screen.Bet;
                }
                else if (horse4Rect.Contains(mouseState.Position))
                {
                    horse4Color = Color.Green;
                }
                else
                {
                    horse4Color = Color.White;
                }

                if (horse5Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 5;
                    screen = Screen.Bet;
                }
                else if (horse5Rect.Contains(mouseState.Position))
                {
                    horse5Color = Color.Green;
                }
                else
                {
                    horse5Color = Color.White;
                }

                if (horse6Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 6;
                    screen = Screen.Bet;
                }
                else if (horse6Rect.Contains(mouseState.Position))
                {
                    horse6Color = Color.Green;
                }
                else
                {
                    horse6Color = Color.White;
                }

                if (horse7Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 7;
                    screen = Screen.Bet;
                }
                else if (horse7Rect.Contains(mouseState.Position))
                {
                    horse7Color = Color.Green;
                }
                else
                {
                    horse7Color = Color.White;
                }

                if (horse8Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    horseSelected = 8;
                    screen = Screen.Bet;
                }
                else if (horse8Rect.Contains(mouseState.Position))
                {
                    horse8Color = Color.Green;
                }
                else
                {
                    horse8Color = Color.White;
                }

            }
            else if (screen == Screen.Bet)
            {
                if (bet1Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    betAmount = 10;
                    screen = Screen.Race;
                }
                else if (bet1Rect.Contains(mouseState.Position))
                {
                    horse1Color = Color.Green;
                }
                else
                {
                    horse1Color = Color.White;
                }

                if (bet2Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    betAmount = 50;
                    screen = Screen.Race;
                }
                else if (bet2Rect.Contains(mouseState.Position))
                {
                    horse2Color = Color.Green;
                }
                else
                {
                    horse2Color = Color.White;
                }

                if (bet3Rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed
                    && prevMouseState.LeftButton == ButtonState.Released)
                {
                    betAmount = 100;
                    screen = Screen.Race;
                }
                else if (bet3Rect.Contains(mouseState.Position))
                {
                    horse3Color = Color.Green;
                }
                else
                {
                    horse3Color = Color.White;
                }
            }
            else if (screen == Screen.Race)
            {
                if (countdownTime < 4)
                {
                    countdownTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    countdownTimer = (3 - (int)countdownTime);
                    countdownString = countdownTimer.ToString();
                }
                else
                {
                    elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (elapsedTime <= 30)
                    {
                        raceTimeString = elapsedTime.ToString("0.0");
                    }
                    else
                    {
                        raceTimeString = "FINISHED";
                    }

                    if (altBackgroundRect.X > (-4939 / 2))
                    {
                        altBackgroundRect.X -= 10;
                    }
                    else
                    {
                        altBackgroundRect.X = 0;
                    }

                    if (elapsedTime > 29)
                    {
                        if (finishLineRect.X > 1200)
                        {
                            finishLineRect.X -= 10;
                        }
                        else if (finishLineRect.X <= 1200)
                        {
                            altBackgroundRect.X += 10;
                        }
                    }

                    //Animate horses

                    horseAnimation += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (horseAnimation <= 0.1f)
                    {
                        stormfuhrTexture = stormfuhr1;
                        beyonceneighTexture = beyonceneigh1;
                        greasedLightningTexture = greasedLightning1;
                        twilightSparkleTexture = twilightSparkle1;
                        hoofJackmanTexture = hoofJackman1;
                        shadowfaxTexture = shadowfax1;
                        ponyStarkTexture = ponyStark1;
                        potooooooooTexture = potoooooooo1;
                    }
                    else if (horseAnimation > 0.1f && horseAnimation <= 0.2f)
                    {
                        stormfuhrTexture = stormfuhr2;
                        beyonceneighTexture = beyonceneigh2;
                        greasedLightningTexture = greasedLightning2;
                        twilightSparkleTexture = twilightSparkle2;
                        hoofJackmanTexture = hoofJackman2;
                        shadowfaxTexture = shadowfax2;
                        ponyStarkTexture = ponyStark2;
                        potooooooooTexture = potoooooooo2;
                    }
                    else if (horseAnimation > 0.2f && horseAnimation <= 0.3f)
                    {
                        stormfuhrTexture = stormfuhr3;
                        beyonceneighTexture = beyonceneigh3;
                        greasedLightningTexture = greasedLightning3;
                        twilightSparkleTexture = twilightSparkle3;
                        hoofJackmanTexture = hoofJackman3;
                        shadowfaxTexture = shadowfax3;
                        ponyStarkTexture = ponyStark3;
                        potooooooooTexture = potoooooooo3;
                    }
                    else if (horseAnimation > 0.3f && horseAnimation <= 0.4f)
                    {
                        stormfuhrTexture = stormfuhr4;
                        beyonceneighTexture = beyonceneigh4;
                        greasedLightningTexture = greasedLightning4;
                        twilightSparkleTexture = twilightSparkle4;
                        hoofJackmanTexture = hoofJackman4;
                        shadowfaxTexture = shadowfax4;
                        ponyStarkTexture = ponyStark4;
                        potooooooooTexture = potoooooooo4;
                    }
                    else if (horseAnimation > 0.4f && horseAnimation <= 0.5f)
                    {
                        stormfuhrTexture = stormfuhr5;
                        beyonceneighTexture = beyonceneigh5;
                        greasedLightningTexture = greasedLightning5;
                        twilightSparkleTexture = twilightSparkle5;
                        hoofJackmanTexture = hoofJackman5;
                        shadowfaxTexture = shadowfax5;
                        ponyStarkTexture = ponyStark5;
                        potooooooooTexture = potoooooooo5;
                    }
                    else
                    {
                        horseAnimation = 0;
                    }

                    //Move horses
                    if (stormfuhrRect.X < 0 || beyonceneighRect.X < 0 || greasedLightningRect.X < 0 || twilightSparkleRect.X < 0
                        || hoofJackmanRect.X < 0 || shadowfaxRect.X < 0 || ponyStarkRect.X < 0 || potooooooooRect.X < 0)
                    {
                        stormfuhrRect.X += 10;
                        beyonceneighRect.X += 10;
                        greasedLightningRect.X += 10;
                        twilightSparkleRect.X += 10;
                        hoofJackmanRect.X += 10;
                        shadowfaxRect.X += 10;
                        ponyStarkRect.X += 10;
                        potooooooooRect.X += 10;
                    }
                    if (finishLineRect.X <= 1200)
                    {
                        stormfuhrRect.X += 10;
                        beyonceneighRect.X += 10;
                        greasedLightningRect.X += 10;
                        twilightSparkleRect.X += 10;
                        hoofJackmanRect.X += 10;
                        shadowfaxRect.X += 10;
                        ponyStarkRect.X += 10;
                        potooooooooRect.X += 10;
                    }

                    if (elapsedTime > 1 && elapsedTime < 30)
                    {
                        changeSpeedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (changeSpeedTimer > 0.3f)
                        {
                            changeSpeed = generator.Next(0, 33);
                            changeSpeedTimer = 0;
                        }

                        if (changeSpeed == 1)
                        {
                            stormfuhrRect.X += 5;
                        }
                        else if (changeSpeed == 2)
                        {
                            beyonceneighRect.X += 5;
                        }
                        else if (changeSpeed == 3)
                        {
                            greasedLightningRect.X += 5;
                        }
                        else if (changeSpeed == 4)
                        {
                            twilightSparkleRect.X += 5;
                        }
                        else if (changeSpeed == 5)
                        {
                            hoofJackmanRect.X += 5;
                        }
                        else if (changeSpeed == 6)
                        {
                            shadowfaxRect.X += 5;
                        }
                        else if (changeSpeed == 7)
                        {
                            ponyStarkRect.X += 5;
                        }
                        else if (changeSpeed == 8)
                        {
                            potooooooooRect.X += 5;
                        }
                        else if (changeSpeed == 9)
                        {
                            stormfuhrRect.X -= 5;
                        }
                        else if (changeSpeed == 10)
                        {
                            beyonceneighRect.X -= 5;
                        }
                        else if (changeSpeed == 11)
                        {
                            greasedLightningRect.X -= 5;
                        }
                        else if (changeSpeed == 12)
                        {
                            twilightSparkleRect.X -= 5;
                        }
                        else if (changeSpeed == 13)
                        {
                            hoofJackmanRect.X -= 5;
                        }
                        else if (changeSpeed == 14)
                        {
                            shadowfaxRect.X -= 5;
                        }
                        else if (changeSpeed == 15)
                        {
                            ponyStarkRect.X -= 5;
                        }
                        else if (changeSpeed == 16)
                        {
                            potooooooooRect.X -= 5;
                        }
                    }

                    if (!firstPlaceSet)
                    {
                        if (stormfuhrRect.X >= 1200)
                        {
                            firstPlace = 1;
                            firstPlaceSet = true;
                        }
                        else if (beyonceneighRect.X >= 1200)
                        {
                            firstPlace = 2;
                            firstPlaceSet = true;
                        }
                        else if (greasedLightningRect.X >= 1200)
                        {
                            firstPlace = 3;
                            firstPlaceSet = true;
                        }
                        else if (twilightSparkleRect.X >= 1200)
                        {
                            firstPlace = 4;
                            firstPlaceSet = true;
                        }
                        else if (hoofJackmanRect.X >= 1200)
                        {
                            firstPlace = 5;
                            firstPlaceSet = true;
                        }
                        else if (shadowfaxRect.X >= 1200)
                        {
                            firstPlace = 6;
                            firstPlaceSet = true;
                        }
                        else if (ponyStarkRect.X >= 1200)
                        {
                            firstPlace = 7;
                            firstPlaceSet = true;
                        }
                        else if (potooooooooRect.X >= 1200)
                        {
                            firstPlace = 8;
                            firstPlaceSet = true;
                        }
                    }
                    if (!secondPlaceSet)
                    {
                        if (stormfuhrRect.X >= 1200 && firstPlace != 1)
                        {
                            secondPlace = 1;
                            secondPlaceSet = true;
                        }
                        else if (beyonceneighRect.X >= 1200 && firstPlace != 2)
                        {
                            secondPlace = 2;
                            secondPlaceSet = true;
                        }
                        else if (greasedLightningRect.X >= 1200 && firstPlace != 3)
                        {
                            secondPlace = 3;
                            secondPlaceSet = true;
                        }
                        else if (twilightSparkleRect.X >= 1200 && firstPlace != 4)
                        {
                            secondPlace = 4;
                            secondPlaceSet = true;
                        }
                        else if (hoofJackmanRect.X >= 1200 && firstPlace != 5)
                        {
                            secondPlace = 5;
                            secondPlaceSet = true;
                        }
                        else if (shadowfaxRect.X >= 1200 && firstPlace != 6)
                        {
                            secondPlace = 6;
                            secondPlaceSet = true;
                        }
                        else if (ponyStarkRect.X >= 1200 && firstPlace != 7)
                        {
                            secondPlace = 7;
                            secondPlaceSet = true;
                        }
                        else if (potooooooooRect.X >= 1200 && firstPlace != 8)
                        {
                            secondPlace = 8;
                            secondPlaceSet = true;
                        }
                    }
                    if (!thirdPlaceSet)
                    {
                        if (stormfuhrRect.X >= 1200 && firstPlace != 1 && secondPlace != 1)
                        {
                            thirdPlace = 1;
                            thirdPlaceSet = true;
                        }
                        else if (beyonceneighRect.X >= 1200 && firstPlace != 2 && secondPlace != 2)
                        {
                            thirdPlace = 2;
                            thirdPlaceSet = true;
                        }
                        else if (greasedLightningRect.X >= 1200 && firstPlace != 3 && secondPlace != 3)
                        {
                            thirdPlace = 3;
                            thirdPlaceSet = true;
                        }
                        else if (twilightSparkleRect.X >= 1200 && firstPlace != 4 && secondPlace != 4)
                        {
                            thirdPlace = 4;
                            thirdPlaceSet = true;
                        }
                        else if (hoofJackmanRect.X >= 1200 && firstPlace != 5 && secondPlace != 5)
                        {
                            thirdPlace = 5;
                            thirdPlaceSet = true;
                        }
                        else if (shadowfaxRect.X >= 1200 && firstPlace != 6 && secondPlace != 6)
                        {
                            thirdPlace = 6;
                            thirdPlaceSet = true;
                        }
                        else if (ponyStarkRect.X >= 1200 && firstPlace != 7 && secondPlace != 7)
                        {
                            thirdPlace = 7;
                            thirdPlaceSet = true;
                        }
                        else if (potooooooooRect.X >= 1200 && firstPlace != 8 && secondPlace != 8)
                        {
                            thirdPlace = 8;
                            thirdPlaceSet = true;
                        }
                    }
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            if (screen == Screen.SelectHorse)
            {
                _spriteBatch.DrawString(largeFont, "Select a horse to bet on", new Vector2(420, 125), Color.White);

                _spriteBatch.Draw(selectionButtonTexture, horse1Rect, Color.White);
                _spriteBatch.Draw(selectionButton1, horse1Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Stormfuhr", new Vector2(340, 332), horse1Color);

                _spriteBatch.Draw(selectionButtonTexture, horse2Rect, Color.White);
                _spriteBatch.Draw(selectionButton2, horse2Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Beyonceneigh", new Vector2(340, 482), horse2Color);

                _spriteBatch.Draw(selectionButtonTexture, horse3Rect, Color.White);
                _spriteBatch.Draw(selectionButton3, horse3Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Greased Lightning", new Vector2(340, 632), horse3Color);

                _spriteBatch.Draw(selectionButtonTexture, horse4Rect, Color.White);
                _spriteBatch.Draw(selectionButton4, horse4Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Twilight Sparkle", new Vector2(340, 782), horse4Color);

                _spriteBatch.Draw(selectionButtonTexture, horse5Rect, Color.White);
                _spriteBatch.Draw(selectionButton5, horse5Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Hoof Jackman", new Vector2((990 + 150), 332), horse5Color);

                _spriteBatch.Draw(selectionButtonTexture, horse6Rect, Color.White);
                _spriteBatch.Draw(selectionButton6, horse6Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Shadowfax", new Vector2((990 + 150), 482), horse6Color);

                _spriteBatch.Draw(selectionButtonTexture, horse7Rect, Color.White);
                _spriteBatch.Draw(selectionButton7, horse7Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Pony Stark", new Vector2((990 + 150), 632), horse7Color);

                _spriteBatch.Draw(selectionButtonTexture, horse8Rect, Color.White);
                _spriteBatch.Draw(selectionButton8, horse8Rect, Color.White);
                _spriteBatch.DrawString(mediumFont, "Potoooooooo", new Vector2((990 + 150), 782), horse8Color);
            }
            else if (screen == Screen.Bet)
            {
                _spriteBatch.DrawString(largeFont, "Select your bet amount", new Vector2(460, 300), Color.White);

                _spriteBatch.Draw(betButtonTexture, bet1Rect, Color.White);
                _spriteBatch.DrawString(XLFont, "10", new Vector2(440, 460), horse1Color);

                _spriteBatch.Draw(betButtonTexture, bet2Rect, Color.White);
                _spriteBatch.DrawString(XLFont, "50", new Vector2(855, 460), horse2Color);

                _spriteBatch.Draw(betButtonTexture, bet3Rect, Color.White);
                _spriteBatch.DrawString(XLFont, "100", new Vector2(1290, 460), horse3Color);
            }
            else if (screen == Screen.Race)
            {
                _spriteBatch.Draw(raceTrack, altBackgroundRect, Color.White);

                if (countdownTimer < 3)
                {
                    _spriteBatch.Draw(finishLineTexture, finishLineRect, Color.White);
                }

                _spriteBatch.Draw(stormfuhrTexture, stormfuhrRect, Color.White);
                _spriteBatch.Draw(beyonceneighTexture, beyonceneighRect, Color.White);
                _spriteBatch.Draw(greasedLightningTexture, greasedLightningRect, Color.White);
                _spriteBatch.Draw(twilightSparkleTexture, twilightSparkleRect, Color.White);
                _spriteBatch.Draw(hoofJackmanTexture, hoofJackmanRect, Color.White);
                _spriteBatch.Draw(shadowfaxTexture, shadowfaxRect, Color.White);
                _spriteBatch.Draw(ponyStarkTexture, ponyStarkRect, Color.White);
                _spriteBatch.Draw(potooooooooTexture, potooooooooRect, Color.White);

                if (countdownTimer >= 0 && countdownTimer <= 3)
                {
                    _spriteBatch.DrawString(XLFont, countdownString, new Vector2(900, 400), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(mediumFont, raceTimeString, new Vector2(10, 900), Color.White);
                }

                if (firstPlaceSet && secondPlaceSet && thirdPlaceSet)
                {
                    _spriteBatch.Draw(leaderboardTexture, new Rectangle(150, 0, 1600, window.Height), Color.White);
                    _spriteBatch.DrawString(largeFont, "1st: " + firstPlace, new Vector2(300, 280), Color.White);
                    _spriteBatch.DrawString(largeFont, "2nd: " + secondPlace, new Vector2(300, 440), Color.White);
                    _spriteBatch.DrawString(largeFont, "3rd: " + thirdPlace, new Vector2(300, 605), Color.White);
                }
            }

                _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
