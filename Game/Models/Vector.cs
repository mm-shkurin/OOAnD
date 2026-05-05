namespace Game.Models;

public class Vector
{
    public int[] Coordinates { get; }
    public int Dimension => Coordinates.Length;

    public Vector(params int[] coordinates)
    {
        if (coordinates.Length == 0)
            throw new ArgumentException("Vector must have at least one coordinate.", nameof(coordinates));

        Coordinates = coordinates;
    }

    public Vector Add(Vector other)
    {
        if (Dimension != other.Dimension)
            throw new ArgumentException($"Cannot add vectors of different dimensions: {Dimension} != {other.Dimension}.");

        int[] result = Coordinates.Zip(other.Coordinates, (x, y) => x + y).ToArray();
        return new Vector(result);
    }

    public static Vector operator +(Vector left, Vector right)
    {
        return left.Add(right);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is Vector other)
            return Coordinates.SequenceEqual(other.Coordinates);

        return false;
    }

    public static bool operator ==(Vector? left, Vector? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Vector left, Vector right) => !(left == right);

    public override int GetHashCode()
    {
        unchecked
        {
            return Coordinates.Aggregate(17, (hash, coord) => hash * 23 + coord.GetHashCode());
        }
    }
}
