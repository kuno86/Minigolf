using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Minigolf
{
    class ColType
    {
        private Vector2d vel = new Vector2d(0, 0);
        public Vector2d Vel { get { return this.vel; } set { this.vel = value; } }
        
        public virtual void render()
        { ; }

        public virtual bool collide(ColCircle ball, out Vector2d depth)
        {
            depth = new Vector2d(0, 0);
            return false; 
        }
    }
}
