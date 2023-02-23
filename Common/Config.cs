namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "C:/GitHub/SearchEngine-Baseline/Data/database.db";
        public static string DataSourcePath { get; } = "C:/GitHub/SearchEngine-Baseline/source";
        public static int NumberOfFoldersToIndex { get; } = 2; // Use 0 or less for indexing all folders
    }
}