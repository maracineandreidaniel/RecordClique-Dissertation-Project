using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordClique_BusinessLogic.DTOs
{
    public class EmailDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailDto(string to, string subject, string content)
        {
            this.To = to;
            this.Subject = subject;
            this.Content = content;
        }
    }
}
