using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

using SpriteLibrary;

namespace ExperimentalFloss
{
    class IsPressed
    {
        // Checks if a certain key is pressed for a period of time

        public PrintString printString = new PrintString();

        public KeyboardState currentKeyState = Keyboard.GetState();
        public KeyboardState oldKeyState;
        public MouseState currentMouseState = Mouse.GetState();
        public MouseState oldMouseState;
        
        public GamePadState currentPadState1 = GamePad.GetState(PlayerIndex.One);
        public GamePadState oldPadState1;

        public GamePadState currentPadState2 = GamePad.GetState(PlayerIndex.Two);
        public GamePadState oldPadState2;

        public GamePadState currentPadState3 = GamePad.GetState(PlayerIndex.Three);
        public GamePadState oldPadState3;

        public GamePadState currentPadState4 = GamePad.GetState(PlayerIndex.Four);
        public GamePadState oldPadState4;

        public int elapsedMilliseconds = 0;

        bool[] timeBool; // An array holding boolean values for if the timer is up, based on FunctionIndex
        float[][] timeArray = new float[][] {}; // An array holding the desired times for button presses, based on FunctionIndex

        public bool Do(Game core, GameTime gameTime, Keys key, Buttons button, bool tapped)
        {

            if (core.keyboardActive)
            {
                if ((currentKeyState.IsKeyDown(key)) && (oldKeyState.IsKeyUp(key)))
                {
                    return true;
                }
            }
            else
            {
                if ((currentPadState1.IsButtonDown(button)) && (oldPadState1.IsButtonUp(button)))
                {
                    return true;
                }
            }
            return false;
            //}
            //public bool[] Do(Game core, GameTime gameTime, Keys key, Buttons button, float[] heldTime, int functionIndex)
            //{
            //    if(Do(core, gameTime, key, button, false))
            //    {

            //         timeArray[functionIndex] = heldTime;

            //         for (int i = 1; i < heldTime.GetLength(0); i++)
            //         {
            //            heldTime[i] -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //            if (heldTime[i] <= 0)
            //            {
            //                timeBool[i] = true;
            //            }
            //         }
            //    }

            //    return(timeBool);
            //}
            //public bool Do(Game core, GameTime gameTime, float[][] timeArray, int functionIndex) // To be called internally, finds out if HeldTime is up based on FunctionIndex and returns a flat boolean value
            //{
            //    return true;
            //}
        }
    }
}
