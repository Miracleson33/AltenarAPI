using AltenarAPI.Dto.IncomingMessages;
using AltenarAPI.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace AltenarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingMessagesController : ControllerBase
    {
        private readonly IIncomingMessageService _incomingMessageService;
        public IncomingMessagesController(IIncomingMessageService incomingMessageService)
        {
            _incomingMessageService = incomingMessageService;
        }

        [HttpPost]
        public async Task<IActionResult> PostIncomingMessageAsync([FromBody] CreateIncomingMessageRequest incomingMessageRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            // Получение Id сессии клиента из запроса
            string session = HttpContext.Request.HttpContext.Session.Id;

            // Получение IP адреса клиента из запроса
            string ip = HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            // Получение версий системы и браузера клиента из запроса
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            // Парсинг версий системы и браузера
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            // Версия системы
            string versionSystem= c.OS.Family;
            // Версия браузера
            string versionBrowser = c.UA.Family;

            incomingMessageRequest.Session = session;
            incomingMessageRequest.IP = ip;
            incomingMessageRequest.UserAgent = userAgent;
            incomingMessageRequest.VersionSystem = versionSystem;
            incomingMessageRequest.VersionBrowser = versionBrowser;

            await _incomingMessageService.CreateIncomingMessageAsync(incomingMessageRequest);
            return Ok("Record has been added successfully.");
        }
    }
}
