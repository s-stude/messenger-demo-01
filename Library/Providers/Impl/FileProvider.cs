using System;
using System.IO;

namespace Library.Providers.Impl
{
    public class FileProvider : IProvider
    {
        private readonly string _filePath;
        public IProviderSettings Settings { get; }

        public FileProvider(IFileProviderSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            _filePath = settings.FilePath;
            Settings = settings;
        }

        public void Write(string message)
        {
            File.AppendAllText(_filePath, message);
        }
    }
}