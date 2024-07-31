namespace Truefit_CashRegister.Services
{
    public interface ICashRegisterService
    {
        string GetChange();
        string CalculateChange();
        string CalculateRandomChange();
    }

    public enum CurrencyType
    {
        USD,
        EUR
    }

    public enum CurrencyValues
    {
        Dollar,
        Quarter,
        Dime,
        Nickle,
        Penny,

        Euro_2,
        Euro_1,
        Cent_50,
        Cent_20,
        Cent_10,
        Cent_5,
        Cent_2,
        Cent_1
    }

    class Currency
    {
        public Currency()
        {
            USCoins = new CurrencyValues[] 
            { 
                CurrencyValues.Penny, 
                CurrencyValues.Nickle, 
                CurrencyValues.Dime, 
                CurrencyValues.Quarter, 
                CurrencyValues.Dollar 
            };

            EUCoins = new CurrencyValues[] 
            { 
                CurrencyValues.Cent_1, 
                CurrencyValues.Cent_2, 
                CurrencyValues.Cent_5, 
                CurrencyValues.Cent_10, 
                CurrencyValues.Cent_20, 
                CurrencyValues.Cent_50, 
                CurrencyValues.Euro_1, 
                CurrencyValues.Euro_2 
            };
        }
        private CurrencyValues[] USCoins { get; }
        private CurrencyValues[] EUCoins { get; }

        public CurrencyValues GetRandomUSDCoin(double remainingChange)
        {
            var rng = new Random();
            if ((remainingChange / 1.00) >= 1 || remainingChange == 1.00)
                return this.USCoins[rng.Next(0, 5)];
            else if ((remainingChange / 0.25) >= 1 || remainingChange == 0.25)
                return this.USCoins[rng.Next(0, 4)];
            else if ((remainingChange / 0.10) >= 1 || remainingChange == 0.10)
                return this.USCoins[rng.Next(0, 3)];
            else if ((remainingChange / 0.05) >= 1 || remainingChange == 0.05)
                return this.USCoins[rng.Next(0, 2)];
            else
                return CurrencyValues.Penny;
        }

        public CurrencyValues GetRandomEURCoin(double remainingChange)
        {
            var rng = new Random();
            if ((remainingChange / 2.00) >= 1 || remainingChange == 2.00)
                return this.EUCoins[rng.Next(0, 8)];
            if ((remainingChange / 1.00) >= 1 || remainingChange == 1.00)
                return this.EUCoins[rng.Next(0, 7)];
            if ((remainingChange / 0.50) >= 1 || remainingChange == 0.50)
                return this.EUCoins[rng.Next(0, 6)];
            if ((remainingChange / 0.20) >= 1 || remainingChange == 0.20)
                return this.EUCoins[rng.Next(0, 5)];
            if ((remainingChange / 0.10) >= 1 || remainingChange == 0.10)
                return this.EUCoins[rng.Next(0, 4)];
            if ((remainingChange / 0.05) >= 1 || remainingChange == 0.05)
                return this.EUCoins[rng.Next(0, 3)];
            if ((remainingChange / 0.02) >= 1 || remainingChange == 0.02)
                return this.EUCoins[rng.Next(0, 2)];
            else
                return CurrencyValues.Cent_1;
        }

        public static CurrencyValues GetUSDCoin(double remainingChange)
        {
            var rng = new Random();
            if ((remainingChange / 1.00) >= 1 || remainingChange == 1.00)
                return CurrencyValues.Dollar;
            else if ((remainingChange / 0.25) >= 1 || remainingChange == 0.25)
                return CurrencyValues.Quarter;
            else if ((remainingChange / 0.10) >= 1 || remainingChange == 0.10)
                return CurrencyValues.Dime;
            else if ((remainingChange / 0.05) >= 1 || remainingChange == 0.05)
                return CurrencyValues.Nickle;
            else
                return CurrencyValues.Penny;
        }

        public static CurrencyValues GetEURCoin(double remainingChange)
        {
            var rng = new Random();
            if ((remainingChange / 2.00) >= 1 || remainingChange == 2.00)
                return CurrencyValues.Euro_2;
            if ((remainingChange / 1.00) >= 1 || remainingChange == 1.00)
                return CurrencyValues.Euro_1;
            if ((remainingChange / 0.50) >= 1 || remainingChange == 0.50)
                return CurrencyValues.Cent_50;
            if ((remainingChange / 0.20) >= 1 || remainingChange == 0.20)
                return CurrencyValues.Cent_20;
            if ((remainingChange / 0.10) >= 1 || remainingChange == 0.10)
                return CurrencyValues.Cent_10;
            if ((remainingChange / 0.05) >= 1 || remainingChange == 0.05)
                return CurrencyValues.Cent_5;
            if ((remainingChange / 0.02) >= 1 || remainingChange == 0.02)
                return CurrencyValues.Cent_2;
            else
                return CurrencyValues.Cent_1;
        }
    }
}
