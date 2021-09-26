using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using System;

namespace SSB_RETROGRADE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private readonly GamePadListener _gamePadListener1;
        private readonly GamePadListener _gamePadListener2;
        private readonly KeyboardListener _keyboardListener;

        private GamePadListenerSettings gplsobj1;
        private GamePadListenerSettings gplsobj2;
        private KeyboardListenerSettings kbls;

        private Point camera;








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

        private Texture2D levelpng;





        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            gplsobj1 = new GamePadListenerSettings(PlayerIndex.One);
            gplsobj2 = new GamePadListenerSettings(PlayerIndex.Two);
            kbls = new KeyboardListenerSettings();
            _gamePadListener1 = new GamePadListener(gplsobj1);
            _gamePadListener2 = new GamePadListener(gplsobj2);
            _keyboardListener = new KeyboardListener(kbls);
            Components.Add(new InputListenerComponent(this, _gamePadListener1));
            Components.Add(new InputListenerComponent(this, _gamePadListener2));
            Components.Add(new InputListenerComponent(this, _keyboardListener));


            camera = new Point(0);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_gamePadListener = new GamePadListener();





            base.Initialize();
        }

        private void _keyboardListener_KeyPressed(object sender, KeyboardEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //System.Console.WriteLine("About to create the mario manager");

            marioInstance = new MassiveMarioManager(Content.Load<Texture2D>("CharacterSprites/MarioAndLuigiSprites"));
            levelpng = this.Content.Load<Texture2D>("Levels/smw1");
            marioInstance.isThisCorrect(_gamePadListener1);







           

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.X -= 10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera.Y += 10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.X += 10;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera.Y -= 10;
            }

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
            /*
             * The idea of a Camera.
             * This camera is an abstraction of something else that is going on behind the scenes
             * It must really be thought like unto the following.
             * Imagine, a infinitly large canvas.
             * upon this canvas can be placed any 2d image that we care about.
             * Naturally there is a 0,0 point on this "Infinite" canvas, but for our purposes that doesn't matter
             * Now that we have imagined this "infinite" canas that can receive any image we care about....
             * Now consider this. 
             * There is a window into this world of this canvas, and it has a widht and a height.
             * This window can never move.
             * Thus the illusion of movement is achieved when images are drawn, deleted, and redrawn at differnet points
             * relative to the 0,0 point which just so happens to be the top left corner of the "game window"
             * Now back to this idea of a camera.
             * 
             * Our camera should be able to do a few things.
             * 1. Provide the illusion of any type of 2d translation
             *      This is accomplished by redrawing our images at different locations on the canvas.
             * 2. Zoom in and Zoom out.
             *      This may be a little ambitious, but when zooming occurs,everything in the game window would be stretched to take up 
             *      more room on this canvas or less room based on the desired effect.
             *
             *I guess we could talk about the camera, but that doesn't seem right.
             *For example if you were playing a mario game, 
             *and you where in the current level or in the menu, and you pushed left,... what would you expect?
             *
             *Would there be some grand abstracted manager thing moving the images, npc's, and buildings? (camera?)
             *
             *Or would there be a canas item manager, that could do that for you?
             *How would you handle traps, pits, one-hit-kills, walls, floors, and ceilings?
             *
             *How would all these things play together and still be well coded?
             *
             *
             *
             *
             *
             *
             *
             */
            _spriteBatch.Draw(levelpng,new Rectangle(0+camera.X,0+camera.Y,10240,1400),Color.White);
            marioInstance.drawMe(_spriteBatch);
            //_spriteBatch.Draw(marioSpriteTexture, new Rectangle(50,50,200,200), marioTangle[tangleCounter], Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void handleKeyboardButtonDownInput(Object sender, GamePadEventArgs args)
        {

        }
    }
}
