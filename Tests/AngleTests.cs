using Xunit;
using Game.Models;

namespace Tests;

public class AngleTests
{
    [Fact]
    public void Constructor_SetsNormalizedAngle()
    {
        var angle = new Angle(-5);
        var expected = new Angle(3);

        Assert.True(angle.Equals(expected));
    }

    [Fact]
    public void Add_Angles_ReturnsCorrectAngle()
    {
        var angle1 = new Angle(5);
        var angle2 = new Angle(7);

        var expected = new Angle(4);

        var result = angle1 + angle2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Equals_ReturnsTrueForEqualAngles()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void EqualityOperator_ReturnsTrueForEqualAngles()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        Assert.True(angle1 == angle2);
    }

    [Fact]
    public void Equals_ReturnsFalseForUnequalAngles()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);

        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void InequalityOperator_ReturnsTrueForUnequalAngles()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);

        Assert.True(angle1 != angle2);
    }

    [Fact]
    public void GetHashCode_EquivalentAngles_ReturnsSameHashCode()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        Assert.Equal(angle1.GetHashCode(), angle2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentAngles_ReturnsDifferentHashCode()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(25);

        Assert.NotEqual(angle1.GetHashCode(), angle2.GetHashCode());
    }

    [Fact]
    public void Equals_ReturnsTrueForSameReference()
    {
        var angle1 = new Angle(1);
        var angle2 = angle1;

        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void Equals_ReturnsFalseForNonAngleOrNull()
    {
        var angle = new Angle(1);

        Assert.False(angle.Equals(null));
        Assert.False(angle.Equals("string"));
    }

    [Fact]
    public void EqualityOperator_HandlesNullCorrectly()
    {
        Angle? nullAngle = null;
        var angle = new Angle(1);

        Assert.True(nullAngle == null);
        Assert.False(angle == null);
    }

    [Fact]
    public void ImplicitConversionToDouble_ReturnsCorrectValue()
    {
        var angle = new Angle(0);

        double cosValue = Math.Cos(angle);
        double sinValue = Math.Sin(angle);

        Assert.Equal(1.0, cosValue);
        Assert.Equal(0.0, sinValue);
    }
}
