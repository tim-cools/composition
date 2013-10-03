
using System;

namespace Soloco.Composition.Examples.MoneyTransfer
{
    /// <summary>
    /// Money transfer interaction
    /// </summary>
    public class TransferMoney  // : IRunnable
    {
        // Roles used by this interaction
        [Implementation(typeof(MoneySourceAccountMixin))]
        public interface IMoneySource
        {
            void TransferTo(int amount, IMoneySink sink);
        }

        [Implementation(typeof(MoneySinkAccountMixin))]
        public interface IMoneySink
        {
            void Receive(int amount);
        }

        private readonly IMoneySource _source;
        private readonly IMoneySink _sink;

        public int Amount { get; set; }

        public TransferMoney(IMoneySource source, IMoneySink sink)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (sink == null) throw new ArgumentNullException("sink");

            _source = source;
            _sink = sink;
        }

        public void Run()
        {
            // Perform the actual transfer
            _source.TransferTo(Amount, _sink);
        }
    }
}
