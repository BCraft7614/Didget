using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BPA__Game
{
    class Buildings:Entity
    {
        //For Collsions Pictures
        public Buildings(Texture2D Image, Vector2 Position)
        {
            image = Image;
            position = Position;
            Width = image.Width;
            Height = image.Height;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, Color.White);

        }
    }
}
