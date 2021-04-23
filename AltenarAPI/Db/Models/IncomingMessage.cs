using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI.Models
{
    public class Parameter
    {
        public int ParameterId { get; set; }
        public int IncomingMessageId { get; set; }
        public string Value { get; set; }

    }

    public class IncomingMessage
    {
        public int IncomingMessageId { get; set; }
        public string Session { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string VersionSystem { get; set; }
        public string VersionBrowser { get; set; }
        public string EmptyField { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
