using ClientNotification.Common.Abstractions;
using ClientNotification.Common.Exceptions;
using ClientNotification.Common.Persistence;
using ClientNotification.Domain.Entities;
using ClientNotification.Domain.Items;
using ClientNotification.Domain.Requests.Commands;
using ClientNotification.Persistence.Repositories;
using ClientNotification.Services;
using MediatR;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClientNotification.Application.Handlers
{
    public class SendNotifyCommandHandler : ICommandHandler<SendNotifyCommand, NotifyResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerRepository customerRepository;
        private readonly IEMailNotifyService mailNotifyService;
        private readonly IMessageTemplateRepository messageTemplateRepository;
        private readonly ISendGridResponseRepository sendGridResponseRepository;

        public SendNotifyCommandHandler(
                    IUnitOfWork unitOfWork,
                    IEMailNotifyService mailNotifyService,
                    ICustomerRepository customerRepository,
                    IMessageTemplateRepository messageTemplateRepository,
                    ISendGridResponseRepository sendGridResponseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
            this.mailNotifyService = mailNotifyService;
            this.messageTemplateRepository = messageTemplateRepository;
            this.sendGridResponseRepository = sendGridResponseRepository;
        }

        public async Task<NotifyResponse> Handle(SendNotifyCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetCustomerByCreditNumberAsync(request.CreditNumber, cancellationToken);
            if (customer == null)
                throw new NotFoundApiException($"Customer with Credit Number = {request.CreditNumber} not found");

            var messageTemplate = await messageTemplateRepository.GetMessageTemplateByName(request.Template, cancellationToken);
            if (messageTemplate == null)
                throw new NotFoundApiException($"Template with Name = {request.Template} not found");

            var ps = new Dictionary<string, string>()
            {
                { "Creditnumber", customer.CreditNumber },
                { "Name", customer.Name },
                { "dueDate", customer.DueDate.ToString() },
                { "amount", customer.Amount.ToString() }
            };

            var body = Smart.Format(messageTemplate.Message, ps);

            var response = await mailNotifyService.SendMessage(new NotifyMessage()
            {
                Body = body,
                Subject = messageTemplate.Name,
                ToEMail = customer.EMail,
                ToUser = customer.Name
            }, cancellationToken);

            var sendGridEntity = new SendGridResponse()
            {
                Created = DateTime.UtcNow,
                CustomerId = customer.Id,
                TemplateId = messageTemplate.Id,
                Response = response
            };

            await sendGridResponseRepository.AddSendGridResponseAsync(sendGridEntity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new NotifyResponse()
            {
                Body = response
            };
        }
    }
}
