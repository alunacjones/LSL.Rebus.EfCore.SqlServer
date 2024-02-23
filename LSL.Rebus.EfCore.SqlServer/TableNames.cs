namespace LSL.Rebus.EfCore.SqlServer
{
    /// <summary>
    /// A static class to hold the default table names
    /// </summary>
    public static class TableNames
    {
        /// <summary>
        /// Default name for the Sagas table
        /// </summary>
        public const string Sagas = "Sagas";

        /// <summary>
        /// Default name for the SagaIndex table
        /// </summary>    
        public const string SagaIndex = "SagaIndex";

        /// <summary>
        /// Default name for the Outbox table
        /// </summary>    
        public const string Outbox = "Outbox";

        /// <summary>
        /// Default name for the Timeouts table
        /// </summary>    
        public const string Timeouts = "Timeouts";

    }
}
