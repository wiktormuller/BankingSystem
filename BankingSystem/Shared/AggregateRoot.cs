namespace BankingSystem.Shared
{
    public class AggregateRoot
    {
        private bool _versionIncremented;
        private List<IDomainEvent> _events = new();

        public int Version { get; protected set; } = 1;
        public IEnumerable<IDomainEvent> Events => _events;

        public void ClearEvents() => _events.Clear();

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }

        protected void AddEvent(IDomainEvent domainEvent)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                IncrementVersion();
            }

            _events.Add(domainEvent);
        }
    }
}
