namespace Library.Providers.Impl
{
    public interface IFileProviderSettings : IProviderSettings
    {
        string FilePath { get; set; }
    }
}