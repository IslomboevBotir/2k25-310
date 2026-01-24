
using System.Text;

namespace Builders
{
    public class ReportBuilder
    {
        private StringBuilder _sb = new StringBuilder();

        public ReportBuilder StartReport(string title)
        {
            _sb.Clear();
            _sb.AppendLine($"=== {title} ===");
            _sb.AppendLine($"Generated at: {System.DateTime.Now}");
            return this;
        }

        public ReportBuilder AddSection(string heading, string body)
        {
            _sb.AppendLine($"\n-- {heading} --");
            _sb.AppendLine(body);
            return this;
        }

        public string Finish()
        {
            _sb.AppendLine("\n=== End of report ===");
            return _sb.ToString();
        }
    }
}
