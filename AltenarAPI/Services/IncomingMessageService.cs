using AltenarAPI.Dto.IncomingMessages;
using AltenarAPI.interfaces;
using AltenarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI.Services
{
    // Внедрение сервиса IncomingMessageService, реализующего интерфейс IIncomingMessageService
    public class IncomingMessageService : IIncomingMessageService
    {
        // Определение закрытого и доступного только для чтения контекста базы данных AltenarAPIContext
        private readonly AltenarAPIContext _dbContext;

        // Добавление конструктора, который внедряет контекст базы данных AltenarAPIContext как зависимость сервиса IncomingMessageService
        public IncomingMessageService(AltenarAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Реализация асинхронного метода CreateIncomingMessageAsync интерфейса IIncomingMessageService
        public async Task CreateIncomingMessageAsync(CreateIncomingMessageRequest incomingMessageRequest)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Добавление новой записи IncomingMessage в базу данных
                var incomingMessage = new IncomingMessage 
                {
                    Session = incomingMessageRequest.Session,
                    IP = incomingMessageRequest.IP,
                    UserAgent = incomingMessageRequest.UserAgent,
                    VersionSystem = incomingMessageRequest.VersionSystem,
                    VersionBrowser = incomingMessageRequest.VersionBrowser,
                    EmptyField = incomingMessageRequest.EmptyField
                };
                await _dbContext.IncomingMessages.AddAsync(incomingMessage);
                await _dbContext.SaveChangesAsync();

                // Получение сгенерированного IncomingMessageId
                var incomingMessageId = incomingMessage.IncomingMessageId;

                if (incomingMessageRequest.Parameters != null)
                {
                    // Создание массива Parameters для связанного с созданным IncomingMessage
                    var parameters = new List<Parameter>();
                    foreach (var parameter in incomingMessageRequest.Parameters)
                    {
                        parameters.Add(new Parameter
                        {
                            IncomingMessageId = incomingMessageId,
                            Value = parameter.Value
                        });
                    }
                    // Добавление новых записией Parameters в базу данных
                    _dbContext.Parameters.AddRange(parameters);
                    await _dbContext.SaveChangesAsync();
                }
                // Подтверждение изменений в базе данных при успешных операциях
                await transaction.CommitAsync();
            }
            catch
            {
                // Отмена изменений в базе данных при возникновении ошибок
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
