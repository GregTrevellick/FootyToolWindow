namespace FootieData.Entities
{
    public static class EntityConstants
    {
        public const string PoliteRequestLimitReached = "The free request limit has been reached - please w";
        public const string PotentialTimeout = "No data available - possible data retrieval timeout. Try increasing timeout value in Tools | Options.";
        public static string UnexpectedErrorOccured = "An unexpected exception error has occurred, please re-try.";
        public static string UnexpectedErrorOccuredActivityLog = $"{UnexpectedErrorOccured} Stack trace information may be logged within you Visual Studio activity log file (ActivityLog.xml).";
    }
}
