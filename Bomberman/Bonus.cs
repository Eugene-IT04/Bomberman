using System;
using System.Drawing;

namespace Bomberman
{
    class Bonus : Template
    {
        static Bitmap speedUp_texture, incrBombCount_texture, powerUpBomb_texture;
        BonusTypes bonusType;
        static Bonus()
        {
            speedUp_texture = Properties.Resources.speedUp_bonus;
            incrBombCount_texture = Properties.Resources.incrBombCount_bonus;
            powerUpBomb_texture = Properties.Resources.powerUpBomb_bonus;
        }
        public Bonus(PointF coord)
        {
            size = new Size(50, 50);
            coords = new PointF(coord.X, coord.Y);
            Random rand = new Random();
            int i = rand.Next(0, 30);
            if (i < 6) bonusType = BonusTypes.powerUpBomb;
            else if (i < 20) bonusType = BonusTypes.incrBombCount;
            else bonusType = BonusTypes.speedUp;
        }
        public override Bitmap getTexture()
        {
            if (bonusType == BonusTypes.speedUp) return speedUp_texture;
            if (bonusType == BonusTypes.powerUpBomb) return powerUpBomb_texture;
            return incrBombCount_texture;
        }

        public void improveBomberman(Bomberman b)
        {
            if (bonusType == BonusTypes.speedUp && b.speed <= 5) b.speed += 0.5f;
            else if (bonusType == BonusTypes.powerUpBomb && b.bombPower < 6) b.bombPower++;
            else if (b.maxBombsCount < 5) b.maxBombsCount++;
        }

    }

    
}

enum BonusTypes
{
    speedUp, incrBombCount, powerUpBomb
}
