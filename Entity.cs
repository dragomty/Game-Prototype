#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using System.Text;
#endregion

namespace GameStateManagementSample
{
    public abstract class Entity : IEntity
    {
        //declare variables as protected so that they are accessible to subclasses
        protected Texture2D image;
        protected Vector2 initialPos;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Boolean isBullet = false;
        protected Boolean isBulletNew = false;
        protected int health;
        protected int bulletDamage;

        public int BulletDamage
        {
            get { return bulletDamage; }
            set { bulletDamage = value; }
        }

        public int Health 
        {
            get { return health; }
            set { health = value; }
        }

        public Boolean IsBullet
        {
            get { return isBullet; }
            set { isBullet = value; }
        }

        public Boolean IsBulletNew
        {
            get { return isBulletNew; }
            set { isBulletNew = value; }
        }


        // public accessor so that other classes can get/and set the value of position
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 InitialPos
        {
            get
            {
                return initialPos;
            }
            set
            {
                initialPos = value;
            }
        }

        // public accessor so that other classes can get/and set the value of Image
        public Texture2D Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }
        
        // public accessor so that other classes can get/and set the value of Velocity
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }
        
        //HitBox is a variable of type rectangle that is to be used for detecting collision between entities
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            }
        }

        //A method to declare the centre point of an object, so that the radius of a circle may be applied from it
        public Vector2 centrePoint()
        {
            float midX = Position.X + Image.Width / 2; // x coordinate for the first entity
            float midY = Position.Y + Image.Height / 2; // y coordinate for the first entity

            Vector2 centre = new Vector2(midX, midY); // making coordinates into a new vector for the first entity

            return centre;
        }

        //declaring the radius based off the height of an object
        public float radiusFromHeight()
        {
            float radius = Image.Height / 2;
            return radius;
        }
        //declaring the radius based off the width of an object
        public float radiusFromWidth()
        {
            float radius = Image.Width / 2;
            return radius;
        }

        public virtual void Update()
        {
            
        }

        public virtual Vector2 Update(Vector2 playerPos)
        {
            return playerPos;
        }

        //all objects of IEntity must be drawn so that they can appear in the scene
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.AntiqueWhite);
        }

        //must be present so that the event can be applied to the paddle when it is delcared as type IEntity
        public virtual void GetInput(Object source, myEventArgs args)
        { 
        
        }

        public virtual void GetKeyUp(Object source, myEventArgs args)
        {

        }

        public virtual void GetSpacebar(Object source, myEventArgs args)
        { 
        
        }
    }
}
