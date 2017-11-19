using System.IO;

namespace SpecFlow.TestProjectGenerator.Inputs
{
    public abstract class FileInput
    {
        protected FileInput(string fileName, string folder, string content = null)
        {
            FileName = fileName;
            Folder = folder;
            Content = content;
        }

        public string FileName { get; }
        public string Folder { get; }
        public virtual string Content { get; }
        public string ProjectRelativePath => Folder == "." ? FileName : Path.Combine(Folder, FileName);

        public string Name => Path.GetFileNameWithoutExtension(FileName);
    }
}