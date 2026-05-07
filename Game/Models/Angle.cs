namespace Game.Models
{
    public class Angle
    {
        public static readonly int Denominator = 8;
        public int Numerator { get; }

        public Angle(int numerator)
        {
            int n = numerator % Denominator;
            Numerator = n >= 0 ? n : n + Denominator;
        }

        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Numerator + right.Numerator);
        }

        public static bool operator ==(Angle? left, Angle? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Angle? left, Angle? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is Angle other)
                return Numerator == other.Numerator;

            return false;
        }

        public override int GetHashCode()
        {
            return Numerator.GetHashCode();
        }

        public static implicit operator double(Angle angle)
        {
            return 2 * Math.PI * angle.Numerator / Denominator;
        }
    }
}
