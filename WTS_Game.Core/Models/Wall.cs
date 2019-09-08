using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public class Wall : GameObject
    {
        public Wall(int x, int y) : base(x, y)
        {

        }

        public override bool CanMoveTo(int x, int y)
        {
            return false;
        }
    }
}
