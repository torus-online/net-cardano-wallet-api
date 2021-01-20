using System;

namespace CardanoWallet
{
    public class ListTransactionsParamBuilder {

        public string WalletId { get => _walletId; }
        public int? MinWithdrawal { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public Order Order
        {
            get => _order == null ? CardanoWallet.Order.Descending : _order;
            set => _order = value;
        };

        private readonly string _walletId;
        private Order? _order = null;

        private ListTransactionsParamBuilder() {
            _walletId = null;
        }

        private ListTransactionsParamBuilder(string walletId) {
            if (walletId == null)
            {
                throw new ArgumentNullException(nameof(walletId));
            }
            this._walletId = walletId;
        }

        public static ListTransactionsParamBuilder Create(string walletId) => new ListTransactionsParamBuilder(walletId);

        public ListTransactionsParamBuilder WithEndTime(DateTime endTime) {
            EndTime = endTime;
            return this;
        }

        public ListTransactionsParamBuilder WithStartTime(DateTime startTime) {
            StartTime = startTime;
            return this;
        }

        public ListTransactionsParamBuilder WithOrder(Order order) {
            _order = order;
            return this;
        }

        public ListTransactionsParamBuilder WithMinWithdrawal(int minWithdrawal) {
            MinWithdrawal = minWithdrawal;
            return this;
        }
    }
}
