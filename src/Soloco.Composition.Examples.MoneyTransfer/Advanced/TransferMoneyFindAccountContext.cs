
namespace Soloco.Composition.Examples.MoneyTransfer.Advanced
{
    /// <summary>
    /// Create a TransferMoney interaction with accounts according to given account id's
    /// </summary>
    public class TransferMoneyFindAccountContext
    {
        private readonly IRepository<TransferMoney.IMoneySource> _moneySourceRepository;
        private readonly IRepository<TransferMoney.IMoneySink> _moneySinkRepository;

        public string SourceId { get; set; }

        public string SinkId { get; set; }

        public TransferMoneyFindAccountContext(IRepository<TransferMoney.IMoneySource> moneySourceRepository, IRepository<TransferMoney.IMoneySink> moneySinkRepository)
        {
            _moneySourceRepository = moneySourceRepository;
            _moneySinkRepository = moneySinkRepository;
        }

        public TransferMoney Call()
        {
            var source = _moneySourceRepository.GetById(SourceId);
            var sink = _moneySinkRepository.GetById(SinkId);

            return new TransferMoney(source, sink);
        }
    }
}