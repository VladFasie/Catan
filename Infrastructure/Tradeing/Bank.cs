using Infrastructure.PlayerDetails;
using System;

namespace Infrastructure.Tradeing
{
    public class Bank
    {
        private const int _standardRatio = 4;

        public static Bank Instance { get; } = new Bank();

        private Bank()
        {
        }

        public void Change(ResourceBag resources, SimpleTrade trade)
        {
            // check ratio 4 -> 1
            if (trade.FromAmmount != trade.ToAmmount * _standardRatio)
                throw new Exception("wrong trade");

            // check ammount
            if (resources[trade.FromResource] < trade.FromAmmount)
                throw new Exception("wrong trade");

            resources[trade.FromResource] -= trade.FromAmmount;
            resources[trade.ToResource] += trade.ToAmmount;
        }

    }
}