using System;
using Xunit;

public class BankTests
{
    [Fact]
    public void Constructor_ValidData_CreatesInstance()
    {
        var bank = new Bank("ПриватБанк", 1992, 1000000m);

        Assert.Equal("ПриватБанк", bank.Name);
        Assert.Equal(1992, bank.FoundedYear);
        Assert.Equal(1000000m, bank.TotalAssets);
    }

    [Fact]
    public void Constructor_InvalidYear_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bank("ТестБанк", 3000, 1000m));
    }

    [Fact]
    public void Deposit_ValidAmount_IncreasesAssets()
    {
        var bank = new Bank("Ощадбанк", 1991, 5000m);

        bank.Deposit(2000m);

        Assert.Equal(7000m, bank.TotalAssets);
    }

    [Fact]
    public void Withdraw_AmountGreaterThanAssets_ThrowsInvalidOperationException()
    {
        var bank = new Bank("Монобанк", 2017, 5000m);

        Assert.Throws<InvalidOperationException>(() => bank.Withdraw(10000m));
    }

    [Fact]
    public void Equals_SameNameAndYear_ReturnsTrue()
    {
        var bank1 = new Bank("Укрсиббанк", 1990, 1000m);
        var bank2 = new Bank("Укрсиббанк", 1990, 5000m);

        bool areEqual = bank1.Equals(bank2);

        Assert.True(areEqual);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var bank = new Bank("Аваль", 1992, 1500.50m);
        string expected = $"Банк: Аваль, Рік заснування: 1992, Активи: {1500.50m:C}";

        string result = bank.ToString();

        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_EmptyName_ThrowsArgumentException(string invalidName)
    {
        Assert.Throws<ArgumentException>(() => new Bank(invalidName, 1990, 1000m));
    }

    [Fact]
    public void Constructor_YearTooOld_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bank("Старий Банк", 1000, 1000m));
    }

    [Fact]
    public void Constructor_NegativeAssets_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Bank("Банк", 2000, -500m));
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Deposit_ZeroOrNegativeAmount_ThrowsArgumentException(decimal invalidAmount)
    {
        var bank = new Bank("Банк", 2000, 1000m);
        Assert.Throws<ArgumentException>(() => bank.Deposit(invalidAmount));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Withdraw_ZeroOrNegativeAmount_ThrowsArgumentException(decimal invalidAmount)
    {
        var bank = new Bank("Банк", 2000, 1000m);
        Assert.Throws<ArgumentException>(() => bank.Withdraw(invalidAmount));
    }


    [Fact]
    public void Equals_NullObject_ReturnsFalse()
    {
        var bank = new Bank("Банк", 2000, 1000m);
        Assert.False(bank.Equals(null));
    }

    [Fact]
    public void Equals_DifferentTypeObject_ReturnsFalse()
    {
        var bank = new Bank("Банк", 2000, 1000m);
        string notABank = "Я рядок, а не банк";
        Assert.False(bank.Equals(notABank));
    }

    [Fact]
    public void Equals_DifferentProperties_ReturnsFalse()
    {
        var bank1 = new Bank("Банк А", 2000, 1000m);
        var bank2 = new Bank("Банк Б", 2000, 1000m);
        var bank3 = new Bank("Банк А", 2005, 1000m);

        Assert.False(bank1.Equals(bank2));
        Assert.False(bank1.Equals(bank3));
    }

    [Fact]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        var bank1 = new Bank("Укрсиббанк", 1990, 1000m);
        var bank2 = new Bank("Укрсиббанк", 1990, 5000m);

        Assert.Equal(bank1.GetHashCode(), bank2.GetHashCode());
    }
}