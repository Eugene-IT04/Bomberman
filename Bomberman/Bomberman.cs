using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bomberman
{
    class Bomberman : Template
    {
        Bitmap texture_up, texture_down, texture_left, texture_right, texture_stop;
        public Keys upKey;
        public Keys downKey;
        public Keys rightKey;
        public Keys leftKey;
        public Keys plantBombKey;
        public float speed = 3f;
        public PointF moveVector;
        public Directions direction;
        public int maxBombsCount = 1;
        public int currentBombCount = 0;
        public int bombPower = 1;
        public String name;
        public bool bombPlanted = false;
        public int[,] objMap;
        private bool isBot;
        private List<Action> actions;
        private Action currentAction;
        private bool readyForAction = true;
        private PointF nextCoord;

        public Bomberman(Point startPoint, Keys upKey, Keys downKey, Keys rightKey, Keys leftKey, Keys plantBombKey, String name, Bitmap texture_up, Bitmap texture_down, Bitmap texture_right, Bitmap texture_left, Bitmap texture_stop)
        {
            isBot = false;
            this.texture_down = texture_down;
            this.texture_left = texture_left;
            this.texture_up = texture_up;
            this.texture_right = texture_right;
            this.texture_stop = texture_stop;
            this.name = name;
            direction = Directions.stop;
            this.coords = startPoint;
            this.upKey = upKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.leftKey = leftKey;
            this.plantBombKey = plantBombKey;
            size = new Size(45, 45);            
        }

        public Bomberman(Point startPoint, String name, Bitmap texture_up, Bitmap texture_down, Bitmap texture_right, Bitmap texture_left, Bitmap texture_stop)
        {
            isBot = true;
            this.texture_down = texture_down;
            this.texture_left = texture_left;
            this.texture_up = texture_up;
            this.texture_right = texture_right;
            this.texture_stop = texture_stop;
            this.name = name;
            direction = Directions.stop;
            this.coords = startPoint;
            size = new Size(45, 45);
            actions = new List<Action>();
        }

        private void generateActions()
        {
            Action lastAction = Action.doNothing;
            Action nextAction = Action.doNothing;
            Point currentPos = getCurrentPosition();
            List<Action> availableActs = getAvailableActions(currentPos);
            do
            {
                lastAction = nextAction;
                for (int i = 0; i < availableActs.Count; i++)
                {
                    nextAction = availableActs[i];
                    if (!opposite(lastAction, nextAction)) break;
                }
                if (opposite(lastAction, nextAction)) break;
                actions.Add(nextAction);
                currentPos = step(currentPos, nextAction);
                availableActs = getAvailableActions(currentPos);
            } while (actions.Count < 6);
            actions.Add(Action.plantBomb);
        }

        private bool opposite(Action a1, Action a2)
        {
            if (a1 == Action.goDown && a2 == Action.goUp) return true;
            if (a1 == Action.goUp && a2 == Action.goDown) return true;
            if (a1 == Action.goLeft && a2 == Action.goRight) return true;
            if (a1 == Action.goRight && a2 == Action.goLeft) return true;
            return false;
        }
        private bool isInSafe(Point p)
        {
            int x = p.X;
            int y = p.Y;
            for(int i = 0; ; i++)
            {
                if (objMap[x, ++y] == 5) return false;
                if (objMap[x, ++y] != 0) break;
            }
            for (int i = 0; ; i++)
            {
                if (objMap[x, --y] == 5) return false;
                if (objMap[x, --y] != 0) break;
            }
            for (int i = 0; ; i++)
            {
                if (objMap[++x, y] == 5) return false;
                if (objMap[++x, y] != 0) break;
            }
            for (int i = 0; ; i++)
            {
                if (objMap[--x, y + 1] == 5) return false;
                if (objMap[--x, y + 1] != 0) break;
            }
            return true;
        }

        private List<Action> getAvailableActions(Point p)
        {
            List<Action> d = new List<Action>();
            if (objMap[p.X, p.Y - 1] == 10 || objMap[p.X, p.Y - 1] == 0) d.Add(Action.goUp);
            if (objMap[p.X + 1, p.Y] == 10 || objMap[p.X + 1, p.Y] == 0) d.Add(Action.goRight);
            if (objMap[p.X - 1, p.Y] == 10 || objMap[p.X - 1, p.Y] == 0) d.Add(Action.goLeft);
            if (objMap[p.X, p.Y + 1] == 10 || objMap[p.X, p.Y + 1] == 0) d.Add(Action.goDown);
            return d;
        }

        private Point step(Point p, Action act)
        {
            if (act == Action.goUp) return new Point(p.X, p.Y - 1);
            else if (act == Action.goDown) return new Point(p.X, p.Y + 1);
            else if (act == Action.goRight) return new Point(p.X + 1, p.Y);
            else if (act == Action.goLeft) return new Point(p.X - 1, p.Y);
            else return p;
        }

        public void tic()
        {
            if (isBot)
            {
                if (actions.Count == 0) generateActions();
                currentAction = actions[0];
                if (readyForAction)
                {
                    Point p = getCurrentPosition();
                    if (currentAction == Action.goUp)
                    {
                        direction = Directions.up;
                        readyForAction = false;
                        nextCoord.X = p.X * 50;
                        nextCoord.Y = (p.Y - 1) * 50f;
                    }
                    else if(currentAction == Action.goDown)
                    {
                        direction = Directions.down;
                        readyForAction = false;
                        nextCoord.X = p.X * 50f;
                        nextCoord.Y = (p.Y + 1) * 50f;
                    }
                    else if(currentAction == Action.goRight)
                    {
                        direction = Directions.right;
                        readyForAction = false;
                        nextCoord.X = (p.X + 1) * 50f;
                        nextCoord.Y = p.Y * 50f;
                    }
                    else if (currentAction == Action.goLeft)
                    {
                        direction = Directions.left;
                        readyForAction = false;
                        nextCoord.X = (p.X - 1) * 50f;
                        nextCoord.Y = p.Y * 50f;
                    }
                    else
                    {
                        if (currentBombCount < 1)
                        {
                            bombPlanted = true;
                            actions.RemoveAt(0);
                        }
                    }
                }
                if(Math.Abs(coords.X - nextCoord.X) <= 1 && Math.Abs(coords.Y - nextCoord.Y) <= speed)
                {
                    nextCoord.X = -speed;
                    nextCoord.Y = -speed;
                    if (actions.Count > 0)actions.RemoveAt(0);
                    direction = Directions.stop;
                    readyForAction = true;
                }
            }
        }

        public override Bitmap getTexture()
        {
            if (direction == Directions.up) return texture_up;
            else if (direction == Directions.down) return texture_down;
            else if (direction == Directions.right) return texture_right;
            else if (direction == Directions.left) return texture_left;
            else return texture_stop;
        }

        public void move()
        {
            coords.X += moveVector.X;
            coords.Y += moveVector.Y;
        }

        private Point getCurrentPosition()
        {
            return new Point((int)Math.Round((double)coords.X / 50), (int)Math.Round((double)coords.Y / 50));
        }

        private PointF getCurrentPositionF()
        {
            return new PointF(coords.X / 50f, coords.Y / 50f);
        }
        public Bomb plantBomb() {
            currentBombCount++;
            Point p = getCurrentPosition();
            return new Bomb(new PointF(p.X * 50, p.Y * 50), name, bombPower);
        }

        private enum Action
        {
            goUp, goDown, goLeft, goRight, plantBomb, doNothing
        }

    }
}
