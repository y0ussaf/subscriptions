using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Subscriptions.Application.Commands.CreatePlan;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;
using Xunit;

namespace Subscriptions.Application.Tests.Commands.CreatePlan
{
    public class CreatePlanCommandHandlerTests
    {
        private readonly Mock<IAppsRepository> _appsRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IPlansRepository> _plansRepository;
        private readonly Mock<IUnitOfWorkContext> _unitOfWorkContext;

        public CreatePlanCommandHandlerTests()
        {
            _appsRepository = new Mock<IAppsRepository>();
            _mapper = new Mock<IMapper>();
            _plansRepository = new Mock<IPlansRepository>();
            _unitOfWorkContext = new Mock<IUnitOfWorkContext>();
            var unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWorkContext.Setup(x => x.CreateUnitOfWork()).ReturnsAsync(unitOfWork.Object);

        }

        [Fact]
        public async Task Command_Should_Raise_NotFoundException_If_App_Doesnt_Exist()
        {
            long appId = 1;
            _appsRepository.Setup(x => x.GetAppById(appId)).ReturnsAsync(()=> null);
            var commandHandler = new CreatePlanCommandHandler(_mapper.Object,_plansRepository.Object,_unitOfWorkContext.Object,_appsRepository.Object);
            var command = new CreatePlanCommand()
            {
                AppId = appId,
                Name = "plan 1"
            };
            Task<CreatePlanCommandResponse> HandleFunc() => commandHandler.Handle(command, CancellationToken.None);
            await Assert.ThrowsAsync<NotFoundException>((Func<Task<CreatePlanCommandResponse>>) HandleFunc);
        }
    }
}