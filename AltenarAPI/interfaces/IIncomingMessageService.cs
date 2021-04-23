using AltenarAPI.Dto.IncomingMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI.interfaces
{
    // Определяем интерфейс IIncomingMessageService
    public interface IIncomingMessageService
    {
        // Определяем асихронный метод, принимающий CreateIncomingMessageRequest
        Task CreateIncomingMessageAsync(CreateIncomingMessageRequest incomingMessageRequest);
    }
}
