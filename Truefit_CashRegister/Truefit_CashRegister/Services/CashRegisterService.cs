namespace Truefit_CashRegister.Services
{
    /***************************************************************************
     *                                                                         *
     *  The GetChange() method executes the primary assignment. The            *
     *  CalculateChange() and CalculateRandomChange() methods are also public  *
     *  to accommodate potential future requirements. Each public method is    *
     *  designed to work without any additional configuration.                 *
     *                                                                         *
     ***************************************************************************/

    public class CashRegisterService(double _total, double _paid, CurrencyType _currency, int _divisible = 3) : ICashRegisterService
    {
        public CurrencyType currencyType = _currency;
        public double total = _total;
        public double change = Math.Round(_paid - _total, 2);
        public int divisible = _divisible;

        private readonly Currency currency = new Currency();
        private int dollarCount = 0, quarterCount = 0, dimeCount = 0, nickleCount = 0, pennyCount = 0;
        private int c1Count = 0, c2Count = 0, c5Count = 0, c10Count = 0, c20Count = 0, c50Count = 0, eur1Count = 0, eur2Count = 0;

        public string GetChange()
        {
            if (IsDivisible())
            {
                return CalculateRandomChange();
            }

            return CalculateChange();
        }

        public string CalculateChange()
        {
            string output = string.Empty;
            switch (this.currencyType)
            {
                case CurrencyType.USD:
                    output = CalculateChange_USD();
                    break;
                case CurrencyType.EUR:
                    output = CalculateChange_EUR();
                    break;
            }
            return output;
        }

        public string CalculateRandomChange()
        {
            string output = string.Empty;
            switch (this.currencyType)
            {
                case CurrencyType.USD:
                    output = CalculateRandomChange_USD();
                    break;
                case CurrencyType.EUR:
                    output = CalculateRandomChange_EUR();
                    break;
            }
            return output;
        }

        private string CalculateChange_USD() 
        {
            var change = this.change;

            while(change > 0)
            {
                change = AddCoinToChange(change, Currency.GetUSDCoin(change));
            }

            return BuildChangeOutput_USD();
        }

        private string CalculateChange_EUR()
        {
            var change = this.change;

            while(change > 0)
            {
                change = AddCoinToChange(change, Currency.GetEURCoin(change));
            }

            return BuildChangeOutput_EUR();
        }

        private string CalculateRandomChange_USD()
        {
            var change = this.change;

            while(change > 0)
            {
                change = AddCoinToChange(change, currency.GetRandomUSDCoin(change));
            }

            return BuildChangeOutput_USD();
        }

        private string CalculateRandomChange_EUR()
        {
            var change = this.change;

            while(change > 0)
            {
                change = AddCoinToChange(change, currency.GetRandomEURCoin(change));
            }

            return BuildChangeOutput_EUR();
        }

        private string BuildChangeOutput_USD()
        {
            string output = string.Empty;
            string dollars = this.dollarCount > 1 ? $"{this.dollarCount} dollars" : $"{this.dollarCount} dollar";
            string quarters = this.quarterCount > 1 ? $"{this.quarterCount} quarters" : $"{this.quarterCount} quarter";
            string dimes = this.dimeCount > 1 ? $"{this.dimeCount} dimes" : $"{this.dimeCount} dime";
            string nickles = this.nickleCount > 1 ? $"{this.nickleCount} nickles" : $"{this.nickleCount} nickle";
            string pennies = this.pennyCount > 1 ? $"{this.pennyCount} pennies" : $"{this.pennyCount} penny";

            output = AddToOutput(output, dollars, this.dollarCount);
            output = AddToOutput(output, quarters, this.quarterCount);
            output = AddToOutput(output, dimes, this.dimeCount);
            output = AddToOutput(output, nickles, this.nickleCount);
            output = AddToOutput(output, pennies, this.pennyCount);

            return output;
        }

        private string BuildChangeOutput_EUR()
        {
            string output = string.Empty;
            string eur_1 = $"{this.eur1Count} 1\u00A3";
            string eur_2 = $"{this.eur2Count} 2\u00A3";
            string eur_50c = $"{this.c50Count} 50c";
            string eur_20c = $"{this.c20Count} 20c";
            string eur_10c = $"{this.c10Count} 10c";
            string eur_5c = $"{this.c5Count} 5c";
            string eur_2c = $"{this.c2Count} 2c";
            string eur_1c = $"{this.c1Count} 1c";

            output = AddToOutput(output, eur_2, this.eur2Count);
            output = AddToOutput(output, eur_1, this.eur1Count);
            output = AddToOutput(output, eur_50c, this.c50Count);
            output = AddToOutput(output, eur_20c, this.c20Count);
            output = AddToOutput(output, eur_10c, this.c10Count);
            output = AddToOutput(output, eur_5c, this.c5Count);
            output = AddToOutput(output, eur_2c, this.c2Count);
            output = AddToOutput(output, eur_1c, this.c1Count);

            return output;
        }

        private static string AddToOutput(string output, string message, int count)
        {
            if (count > 0)
            {
                if (output != string.Empty)
                    output += "," + message;
                else
                    output += message;
            }
            return output;
        }

        private double AddCoinToChange(double amount, CurrencyValues coin)
        {
            switch (coin)
            {
                case CurrencyValues.Dollar:
                    amount = amount - 1.00;
                    this.dollarCount++;
                    break;
                case CurrencyValues.Quarter:
                    amount = amount - 0.25;
                    this.quarterCount++;
                    break;
                case CurrencyValues.Dime:
                    amount = amount - 0.10;
                    this.dimeCount++;
                    break;
                case CurrencyValues.Nickle:
                    amount = amount - 0.05;
                    this.nickleCount++;
                    break;
                case CurrencyValues.Penny:
                    amount = amount - 0.01;
                    this.pennyCount++;
                    break;
                case CurrencyValues.Euro_1:
                    amount = amount - 1.00;
                    this.eur1Count++;
                    break;
                case CurrencyValues.Euro_2:
                    amount = amount - 2.00;
                    this.eur2Count++;
                    break;
                case CurrencyValues.Cent_50:
                    amount = amount - 0.50;
                    this.c50Count++;
                    break;
                case CurrencyValues.Cent_20:
                    amount = amount - 0.20;
                    this.c20Count++;
                    break;
                case CurrencyValues.Cent_10:
                    amount = amount - 0.10;
                    this.c10Count++;
                    break;
                case CurrencyValues.Cent_5:
                    amount = amount - 0.05;
                    this.c5Count++;
                    break;
                case CurrencyValues.Cent_2:
                    amount = amount - 0.02;
                    this.c2Count++;
                    break;
                case CurrencyValues.Cent_1:
                    amount = amount - 0.01;
                    this.c1Count++;
                    break;
            }

            return Math.Round(amount, 2);
        }

        private bool IsDivisible()
        {
            if(this.divisible == 0) return false;

            var nums = this.total.ToString().Split('.');
            if (nums.Length == 2)
            {
                // Check both Dollar amount and Loose Change amount for divisible logic.
                if (int.Parse(nums[0].ToString() + "00") % this.divisible == 0 && int.Parse(nums[1]) % this.divisible == 0)
                { return true; }
            }

            return this.total % this.divisible == 0;
        }
    }
}
