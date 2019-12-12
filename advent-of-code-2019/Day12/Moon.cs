namespace advent_of_code_2019.Day12
{
    public class Moon
    {
        public Vector3 Position { get; set; }

        public Vector3 Velocity { get; set; }

        public int PotentialEnergy => Position.AbsSum();

        public int KineticEnergy => Velocity.AbsSum();

        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        public void ApplyVelocity()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public void ApplyGravity(Moon moon, bool applyX = true, bool applyY = true, bool applyZ = true)
        {
            if (applyX)
            {
                if (moon.Position.X != this.Position.X)
                {
                    if (moon.Position.X > this.Position.X)
                    {
                        moon.Velocity.X--;
                        this.Velocity.X++;
                    }
                    else
                    {
                        moon.Velocity.X++;
                        this.Velocity.X--;
                    }
                }
            }

            if (applyY)
            {
                if (moon.Position.Y != this.Position.Y)
                {
                    if (moon.Position.Y > this.Position.Y)
                    {
                        moon.Velocity.Y--;
                        this.Velocity.Y++;
                    }
                    else
                    {
                        moon.Velocity.Y++;
                        this.Velocity.Y--;
                    }
                }
            }

            if (applyZ)
            {
                if (moon.Position.Z != this.Position.Z)
                {
                    if (moon.Position.Z > this.Position.Z)
                    {
                        moon.Velocity.Z--;
                        this.Velocity.Z++;
                    }
                    else
                    {
                        moon.Velocity.Z++;
                        this.Velocity.Z--;
                    }
                }
            }
        }
    }
}
