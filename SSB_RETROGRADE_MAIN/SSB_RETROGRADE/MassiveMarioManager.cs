using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;


namespace SSB_RETROGRADE
{
 

    class MarioState
    {

        ~MarioState()
        {

        }
        
        public virtual MarioState handleInput(GamePadEventArgs args)
        {
            System.Console.WriteLine("Null Class!!!" + args.Button.ToString());
            return null;
        }
        public virtual void update()
        {

        }
        public virtual void enter(MassiveMarioManager theMarioState)
        {

        }
    }

    class StandingState : MarioState
    {

        public override MarioState handleInput(GamePadEventArgs daArgs)
        {
            //base.handleInput();
            if (daArgs.CurrentState.IsButtonDown(Buttons.A))
            {
                return new JumpingState();
            }
            return null;
        }

        public override void enter(MassiveMarioManager theMarioState)
        {
            theMarioState.setStandingGraphic();
        }



    }
    class JumpingState : MarioState
    {
        public override MarioState handleInput(GamePadEventArgs daArgs)
        {
            if (daArgs.CurrentState.IsButtonDown(Buttons.A))
            {
                return new StandingState();
            }
            return null;
        }

        public override void enter(MassiveMarioManager theMarioState)
        {
            theMarioState.setJumpingGraphic();
        }
    }



    class MassiveMarioManager
    {
        private Texture2D marioSpriteAtlasPNG;

        // sprite zones
        private Rectangle marioStanding;
        private Rectangle marioWalking1;
        private Rectangle marioWalking2;
        private Rectangle marioWalking3;
        private Rectangle marioChangeDirection;
        private Rectangle marioJump;

        private MarioState state_;


        private Rectangle currentMarioDrawingBox;







        public MassiveMarioManager(Texture2D inputSpriteAtlas)
        {
            this.marioSpriteAtlasPNG = inputSpriteAtlas;

            marioStanding = new Rectangle(new Point(1, 9), new Point(16, 16));
            marioWalking1 = new Rectangle(new Point(43, 9), new Point(16, 16));
            marioWalking2 = new Rectangle(new Point(60, 9), new Point(16, 16));
            marioWalking3 = new Rectangle(new Point(77, 9), new Point(16, 16));
            marioChangeDirection = new Rectangle(new Point(98, 9), new Point(16, 16));
            marioJump = new Rectangle(new Point(119, 9), new Point(16, 16));


            state_ = new StandingState();
            currentMarioDrawingBox = marioStanding;

        }

        

        //private void handleInput(GamePadEventArgs daArgs)
        //{
        //    MarioState newState = state_.handleInput(daArgs);
        //    if (newState != null)
        //    {
        //        state_ = newState;
        //        state_.enter(this);
        //    }
        //}
        public void isThisCorrect(GamePadListener someGamePad)
        {
            someGamePad.ButtonDown += handleButtonDownInput;
        }


        public void updateThisThing(GamePadListener someGamePad)
        {
            //MarioState newState = state_.handleInput(someGamePad);
            //if (newState != null)
            //{
            //    state_ = newState;
            //    state_.enter(this);
            //}

            

            //someGamePad.ButtonDown += (sender, args) =>
            //{
            //    handleInput(args);
            //    System.Console.WriteLine(args.Button);
            //    System.Console.WriteLine("Not sure what is happening!");
            //};
        }

        public void handleButtonDownInput(Object sender, GamePadEventArgs args)
        {
            //System.Console.WriteLine(args.Button.ToString());
            MarioState newState = state_.handleInput(args);
            if (newState != null)
            {
                state_ = newState;
                state_.enter(this);
            }
        }


        public void drawMe(SpriteBatch spriteBatch)
        {
            Rectangle tempRectangle = getCurretntMarioDrawingBox();
            spriteBatch.Draw(marioSpriteAtlasPNG, new Rectangle(50, 50, 200, 200), tempRectangle, Color.White);
        }

        public void setStandingGraphic()
        {
            currentMarioDrawingBox = marioStanding;
            Console.WriteLine("mario Stood!!");
        }
        public void setJumpingGraphic()
        {
            currentMarioDrawingBox = marioJump;
            System.Console.WriteLine("mario Jumped!");
        }

        private Rectangle getCurretntMarioDrawingBox()
        {
            return currentMarioDrawingBox;
        }

    }
}
