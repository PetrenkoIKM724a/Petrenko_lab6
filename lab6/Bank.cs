using System;

public class Bank
{
    private string _name;
    private int _foundedYear;
    private decimal _totalAssets;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва банку не може бути порожньою.");
            _name = value;
        }
    }

    public int FoundedYear
    {
        get => _foundedYear;
        private set
        {
            if (value < 1400 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(FoundedYear), "Некоректний рік заснування банку.");
            _foundedYear = value;
        }
    }

    public decimal TotalAssets
    {
        get => _totalAssets;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Активи банку не можуть бути від'ємними.");
            _totalAssets = value;
        }
    }

    public Bank(string name, int foundedYear, decimal initialAssets)
    {
        Name = name;
        FoundedYear = foundedYear;
        TotalAssets = initialAssets;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума поповнення має бути більшою за нуль.");

        TotalAssets += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума зняття має бути більшою за нуль.");
        if (amount > TotalAssets)
            throw new InvalidOperationException("Недостатньо активів для проведення операції.");

        TotalAssets -= amount;
    }

    public override string ToString()
    {
        return $"Банк: {Name}, Рік заснування: {FoundedYear}, Активи: {TotalAssets:C}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Bank otherBank = (Bank)obj;
        return Name == otherBank.Name && FoundedYear == otherBank.FoundedYear;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, FoundedYear);
    }
}