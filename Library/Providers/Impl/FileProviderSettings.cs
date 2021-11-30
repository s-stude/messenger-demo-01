namespace Library.Providers.Impl
{
    public class FileProviderSettings : IFileProviderSettings
    {
        public FileProviderSettings(MessagePriority priority, string filePath)
        {
            Priority = priority;
            FilePath = filePath;
        }
        public MessagePriority Priority { get; }
        public string FilePath { get; set; }
    }
}