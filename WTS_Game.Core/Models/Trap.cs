using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public class Trap : GameObject
    {
        public override bool IsBlocking => false;
        public override bool IsLosing => true;

        public Trap(int x, int y) : base(x, y)
        {

        }

        public override bool CanMoveTo(int x, int y)
        {
            return false;
        }
    }
}
