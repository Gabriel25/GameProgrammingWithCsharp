﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment3
{
    /// <summary>
    /// A rock
    /// </summary>
    public class Rock
    {
        #region Fields

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        //temp variable to hold the velocity float
        //It's used to move the rocks according to the 
        //desired velocity since the rectangle.X and y 
        //positions are integers and this can't be 
        //done by directly adding the velocity to the 
        //x and y coordinates of the rectangle
        private float tempVelocityX, tempVelocityY;

        // moving support
        Vector2 velocity;


        // window containment support
        int windowWidth;
        int windowHeight;
        bool outsideWindow = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for the rock</param>
        /// <param name="location">location of the center of the rock</param>
        /// <param name="velocity">velocity of the rock</param>
        /// <param name="windowWidth">window width</param>
        /// <param name="windowHeight">window height</param>
        public Rock(Texture2D sprite, Vector2 location, Vector2 velocity,
            int windowWidth, int windowHeight)
        {
            // save window dimensions
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            // save sprite and set draw rectangle
            this.sprite = sprite;
            drawRectangle = new Rectangle((int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2, sprite.Width, sprite.Height);

            // save velocity
            this.velocity = velocity;

            //initialize the temp velocity to zero
            tempVelocityX = 0f;
            tempVelocityY = 0f;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets the location of the center of the rock
        /// </summary>
        public Vector2 Location
        {
            set
            {
                drawRectangle.X = (int)(value.X - drawRectangle.Width / 2);
                drawRectangle.Y = (int)(value.Y - drawRectangle.Height / 2);


                // set outside window flag based on new location
                outsideWindow = drawRectangle.Right < 0 ||
                    drawRectangle.Left > windowWidth ||
                    drawRectangle.Bottom < 0 ||
                    drawRectangle.Top > windowHeight;
            }
        }

        /// <summary>
        /// Sets the rock's velocity
        /// </summary>
        public Vector2 Velocity
        {
            set
            {
                velocity.X = value.X;
                velocity.Y = value.Y;
            }
        }

        /// <summary>
        /// Gets whether or not the rock is outside the window
        /// </summary>
        public bool OutsideWindow
        {
            get { return outsideWindow; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the rock
        /// </summary>
        /// <param name="gameTime">game time</param>
        public void Update(GameTime gameTime)
        {
            // STUDENTS: Only update the rock if it's inside the window
            if (!outsideWindow)
            {

                // STUDENTS: Update the rock's location

                // U = x/t 
                // So the distance that the rock will need to cover in every update will be x = U * t
                // U is the known velocity and t is the elapsed time between every update
                // I will assume the time is in ms since if it was in seconds casting the final result to an int would produce 0
                // and the rock would not be moving
                float elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds;
                float distanceX = velocity.X * elapsedTime;
                float distanceY = velocity.Y * elapsedTime;

                drawRectangle.X += (int)distanceX;
                drawRectangle.Y += (int)distanceY;

            }

            // STUDENTS: Set outsideWindow to true if the rock is outside the window
            if (drawRectangle.Right < 0 || drawRectangle.Left > windowWidth
                   || drawRectangle.Top > windowHeight || drawRectangle.Bottom < 0)
            {
                outsideWindow = true;
            }

        }

        /// <summary>
        /// Draws the rock
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // STUDENTS: Only draw the rock if it's inside the window
            if (!outsideWindow)
            {
                // STUDENTS: Draw the rock
                // Caution: Don't include spriteBatch.Begin or spriteBatch.End here
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        #endregion
    }
}