using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using System.Collections.Generic;

namespace SSB_RETROGRADE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private readonly GamePadListener _gamePadListener1;
        private readonly GamePadListener _gamePadListener2;

        private GamePadListenerSettings gplsobj1;
        private GamePadListenerSettings gplsobj2;








        //private Texture2D marioSpriteTexture;

        //private Rectangle spriteAtlasSize;

        //private Rectangle marioStanding;
        //private Rectangle marioWalking1;
        //private Rectangle marioWalking2;
        //private Rectangle marioWalking3;
        //private Rectangle marioChangeDirection;
        //private Rectangle marioJump;

        //private List<Rectangle> marioTangle;

        //private List<int> badList;

        //private int badIncrementor;
        //private int someCounter;
        //private int tangleCounter;




        private MassiveMarioManager marioInstance;







        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            gplsobj1 = new GamePadListenerSettings(PlayerIndex.One);
            gplsobj2 = new GamePadListenerSettings(PlayerIndex.Two);
            _gamePadListener1 = new GamePadListener(gplsobj1);
            _gamePadListener2 = new GamePadListener(gplsobj2);
            Components.Add(new InputListenerComponent(this, _gamePadListener1));
            Components.Add(new InputListenerComponent(this, _gamePadListener2));

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_gamePadListener = new GamePadListener();




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            System.Console.WriteLine("About to create the mario manager");

            marioInstance = new MassiveMarioManager(Content.Load<Texture2D>("CharacterSprites/MarioAndLuigiSprites"));
            marioInstance.isThisCorrect(_gamePadListener1);







           

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            marioInstance.updateThisThing(_gamePadListener1);


            //_gamePadListener1.ButtonDown += (sender, args) => { Window.Title = $"Key {args.Button} Down"; };
            //_gamePadListener2.ButtonDown += (sender, args) => { Window.Title = $"P@P@P@P@P@@222Key {args.Button} Down"; };


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            marioInstance.drawMe(_spriteBatch);
            //_spriteBatch.Draw(marioSpriteTexture, new Rectangle(50,50,200,200), marioTangle[tangleCounter], Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
