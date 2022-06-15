namespace PdfCreator.Models
{
    public class Result
    {
        public Result(string newFiles)
        {
            this.files = newFiles;
        }

        public string files { get; private set; }
    }
}