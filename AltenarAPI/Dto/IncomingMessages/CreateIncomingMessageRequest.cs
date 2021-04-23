using AltenarAPI.Dto.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI.Dto.IncomingMessages
{
    public class CreateIncomingMessageRequest
    {
        public string Session { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string VersionSystem { get; set; }
        public string VersionBrowser { get; set; }
        public string EmptyField { get; set; }

        //[Required]
        public List<CreateParameterRequest> Parameters { get; set; }
    }
}
