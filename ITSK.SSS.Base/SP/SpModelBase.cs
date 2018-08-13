
namespace ITSK.SSS.Base.SP
{
    public abstract class SpModelBase : ISpModel
    {
        public abstract int ID { get; set; }
        public abstract string Title { get; set; }

        protected SpModelBase(int id)
        {
            ID = id;
        }
    }
    public class Attachment
    {
        public string FileUrl { get; set; }
    }

    public class FileInfo
    {
        public string FullUrl { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}