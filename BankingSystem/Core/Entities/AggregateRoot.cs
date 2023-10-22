namespace BankingSystem.Core.Entities
{
    public class AggregateRoot
    {
        private bool _versionIncremented;
        public int Version { get; protected set; } = 1;

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }
    }
}
