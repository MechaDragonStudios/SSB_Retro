using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;


namespace SSB_RETROGRADE
{

    enum AnimationStrategy
    {
        Cyclical, // 0,1,2,3,4,0,1,2,3,4...
        ReverseCyclical, // 4,3,2,1,0,4,3,2,1,0...
        PingPong // 0,1,2,3,4,3,2,1,2,3,4...
    }

    /**
     * 
     *  There are a number of things that will trigger a change to the currently displayed animation frame.
     *  1. Controller input
     *  2. Environment input enemies, spells, collisions with ceilings, and floors, spike traps, pits, lava, etc...
     *  3. 
     * 
     */



    class DrawingFramesManager
    {
        public List<Rectangle> listOfFrames;
        public int currentFrameIndex;
        public int FinalFrameIndex;
        public AnimationStrategy currentAnimationStrategy;

        public virtual Rectangle getNextFrame()
        {
            return new Rectangle();
        }
    }

    class MarioStandingFramesManager : DrawingFramesManager
    {

        public MarioStandingFramesManager(List<Rectangle> inputList)
        {
            this.listOfFrames = inputList;
            this.currentFrameIndex = 0;
            this.currentAnimationStrategy = AnimationStrategy.Cyclical;
        }
        public override Rectangle getNextFrame()
        {
            if (this.currentFrameIndex + 1 >= this.listOfFrames.Count)
            {
                this.currentFrameIndex = 0;
                return this.listOfFrames[this.currentFrameIndex];
            }
            else
            {
                Rectangle tangleToReturn = this.listOfFrames[this.currentFrameIndex];
                this.currentFrameIndex++;
                return tangleToReturn;
            }
            //return base.getNextFrame();
        }
    }

    class MarioJumpingFramesManager : DrawingFramesManager
    {

        public MarioJumpingFramesManager(List<Rectangle> inputList)
        {
            this.listOfFrames = inputList;
            this.currentFrameIndex = 0;
            this.currentAnimationStrategy = AnimationStrategy.Cyclical;
        }
        public override Rectangle getNextFrame()
        {
            if (this.currentFrameIndex + 1 >= this.listOfFrames.Count)
            {
                this.currentFrameIndex = 0;
                return this.listOfFrames[this.currentFrameIndex];
            }
            else
            {
                Rectangle tangleToReturn = this.listOfFrames[this.currentFrameIndex];
                this.currentFrameIndex++;
                return tangleToReturn;
            }
            //return base.getNextFrame();
        }
    }

    class MarioWalkingFramesManager : DrawingFramesManager
    {

        public MarioWalkingFramesManager(List<Rectangle> inputList)
        {
            this.listOfFrames = inputList;
            this.currentFrameIndex = 0;
            this.currentAnimationStrategy = AnimationStrategy.Cyclical;
        }
        public override Rectangle getNextFrame()
        {
            if (this.currentFrameIndex + 1 >= this.listOfFrames.Count)
            {
                Rectangle anotherTangleToReturn = this.listOfFrames[this.currentFrameIndex];
                this.currentFrameIndex = 0;
                return anotherTangleToReturn;
            } else
            {
                Rectangle tangleToReturn = this.listOfFrames[this.currentFrameIndex];
                this.currentFrameIndex++;
                return tangleToReturn;
            }
            //return base.getNextFrame();
        }
    }
 

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
                //System.Console.WriteLine(daArgs.CurrentState.ToString());
                //System.Console.WriteLine(daArgs.PreviousState.ToString());

                return new JumpingState();
            }
            if (daArgs.CurrentState.IsButtonDown(Buttons.DPadRight))
            {
                return new WalkingState();
            }
            return this;
        }

        public override void enter(MassiveMarioManager theMarioState)
        {
            theMarioState.setStandingGraphic();
            theMarioState.forceUpdateThisThing();
        }



    }
    class JumpingState : MarioState
    {
        public override MarioState handleInput(GamePadEventArgs daArgs)
        {
            if (daArgs.CurrentState.IsButtonUp(Buttons.A))
            {
                return new StandingState();
            }
            return this;
        }

        public override void enter(MassiveMarioManager theMarioState)
        {
            theMarioState.setJumpingGraphic();
            theMarioState.forceUpdateThisThing();
        }
    }

    class WalkingState : MarioState
    {
        public override MarioState handleInput(GamePadEventArgs daArgs)
        {
            if (daArgs.CurrentState.IsButtonUp(Buttons.DPadRight))
            {
                return new StandingState();
            }
            return this;
        }

        public override void enter(MassiveMarioManager theMarioState)
        {
            theMarioState.setWalkingGraphic();
            theMarioState.forceUpdateThisThing();
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
        private DrawingFramesManager dfm_;

        private Rectangle currentMarioFrame;
        private List<Rectangle> currentMarioFrames;

        private List<Rectangle> marioStandingFrames;
        private List<Rectangle> marioJumpingFrames;
        private List<Rectangle> marioWalkingFrames;
        private MarioStandingFramesManager marioStandingFramesManager;
        private MarioJumpingFramesManager marioJumpingFramesManager;
        private MarioWalkingFramesManager marioWalkingFramesManager;

        private int rollingAnimationInt;
        private Rectangle rectToDraw;







        public MassiveMarioManager(Texture2D inputSpriteAtlas)
        {
            this.marioSpriteAtlasPNG = inputSpriteAtlas;

            marioStanding = new Rectangle(new Point(1, 9), new Point(16, 16));
            marioWalking1 = new Rectangle(new Point(43, 9), new Point(16, 16));
            marioWalking2 = new Rectangle(new Point(60, 9), new Point(16, 16));
            marioWalking3 = new Rectangle(new Point(77, 9), new Point(16, 16));
            marioChangeDirection = new Rectangle(new Point(98, 9), new Point(16, 16));
            marioJump = new Rectangle(new Point(119, 9), new Point(16, 16));

            marioStandingFrames = new List<Rectangle>();
            marioJumpingFrames = new List<Rectangle>();
            marioWalkingFrames = new List<Rectangle>();

            marioStandingFrames.Add(marioStanding);
            marioJumpingFrames.Add(marioJump);
            marioWalkingFrames.Add(marioWalking1);
            marioWalkingFrames.Add(marioWalking2);
            marioWalkingFrames.Add(marioWalking3);

            marioStandingFramesManager = new MarioStandingFramesManager(marioStandingFrames);
            marioJumpingFramesManager = new MarioJumpingFramesManager(marioJumpingFrames);
            marioWalkingFramesManager = new MarioWalkingFramesManager(marioWalkingFrames);

            rollingAnimationInt = 0;






            dfm_ = new DrawingFramesManager();
            state_ = new StandingState();
            currentMarioFrames = marioStandingFrames;
            currentMarioFrame = currentMarioFrames[0];
            rectToDraw = new Rectangle();
            rectToDraw = dfm_.getNextFrame();

        }


        public void isThisCorrect(GamePadListener someGamePad)
        {
            someGamePad.ButtonDown += handleButtonDownInput;
            someGamePad.ButtonUp += handleButtonUpInput;

        }


        public void updateThisThing(GamePadListener someGamePad)
        {
            rollingAnimationInt++;
            if (rollingAnimationInt % 5 == 0)
            {
                forceUpdateThisThing();
            }

        }

        public void forceUpdateThisThing()
        {
            rectToDraw = dfm_.getNextFrame();
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

        public void handleButtonUpInput(Object sender, GamePadEventArgs args)
        {
            MarioState newState = state_.handleInput(args);
            if (newState != null)
            {
                state_ = newState;
                state_.enter(this);
            }
        }


        public void drawMe(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(marioSpriteAtlasPNG, new Rectangle(150, 50, 100, 200), rectToDraw, Color.White);
        }

        public void setStandingGraphic()
        {
            //currentMarioFrames = marioStandingFrames;
            dfm_ = marioStandingFramesManager;
            //Console.WriteLine("mario Stood!!");
        }
        public void setJumpingGraphic()
        {
            //currentMarioFrames = marioJumpingFrames;
            dfm_ = marioJumpingFramesManager;
            //System.Console.WriteLine("mario Jumped!");
        }

        public void setWalkingGraphic()
        {
            dfm_ = marioWalkingFramesManager;
        }

        private Rectangle getCurretntMarioDrawingBox()
        {
            return dfm_.getNextFrame();
        }

    }
}
