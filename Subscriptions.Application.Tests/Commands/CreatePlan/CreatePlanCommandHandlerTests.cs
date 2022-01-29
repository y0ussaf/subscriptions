using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Subscriptions.Application.Common.Interfaces;
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
         
        }
    }
}