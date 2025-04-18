using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IAssistantService
    {
        object GetAssistantResponse(string text);
    }
}
