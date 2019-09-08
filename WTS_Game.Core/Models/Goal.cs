using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public class Goal : GameObject
    {
        public override bool IsBlocking => false;

        public Goal(int x, int y) : base(x, y)
        {

        }

        public override bool CanMoveTo(int x, int y)
        {
            return false;
        }
    }
}
