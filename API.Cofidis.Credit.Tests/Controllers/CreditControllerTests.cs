using Xunit;
using System.Threading.Tasks;
using Cofidis.Manager.Interfaces;
using Cofidis.Services.Interfaces;
using Moq;
using Cofidis.Data.Models.External;
using Microsoft.Extensions.Logging;
using API.Cofidis.Credit.Controllers;
using Cofidis.ViewModel.Credit;
using Cofidis.Services.Constants;

namespace API.Cofidis.Credit.Tests.Controllers
{
    public class CreditControllerTests
    {
        private readonly Mock<ICreditManager> _mockCreditManager;
        private readonly Mock<IHttpClientManager> _mockHttpClientManager;
        private readonly Mock<ICreditValidatorService> _mockCreditValidatorService;
        private readonly Mock<ILogger<CreditController>> _mockLogger;

        public CreditControllerTests()
        {
            _mockCreditManager = new Mock<ICreditManager>();
            _mockHttpClientManager = new Mock<IHttpClientManager>();
            _mockCreditValidatorService = new Mock<ICreditValidatorService>();
            _mockLogger = new Mock<ILogger<CreditController>>();
        }
        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_WhenAllOperationsAreSuccessful()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 50000;

            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
            {
                new Loan { Amount = 1000, IsActive = true }
            }
            };


            var creditResult = 10000;
            double riskIndexResult = 2;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;

            _mockCreditManager.Setup(m => m.GrantingCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockCreditValidatorService.Setup(m => m.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockCreditValidatorService.Setup(m => m.CreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);
            
             
            var getGratingCreditViewModel = new GetGratingCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 3000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockCreditManager.Object.GrantingCredit(baseSalary);
            var isAblecreditAvailability = _mockCreditValidatorService.Object.CreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockCreditValidatorService.Object.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.NotNull(availableCredit);
            Assert.NotNull(user);
            Assert.NotNull(riskIndex);
            Assert.NotNull(isAblecreditAvailability);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnNull_WhenCreditManagerReturnsNull()
        {
            // Arrange
            var nif = "123456785";

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif)).ReturnsAsync((User?)null);

            // Acts
            var user = await _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
      
            // Assert
            Assert.Null(user);  
        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserHasLoansActiveAndLowBaseSalary1000()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 1000;
            

            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
            {
                new Loan { Amount = 500, IsActive = true }
            }
            };


            decimal creditResult = 1000;
            double riskIndexResult = 52.099999999999994;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = false;

            _mockCreditManager.Setup(m => m.GrantingCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockCreditValidatorService.Setup(m => m.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockCreditValidatorService.Setup(m => m.CreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingCreditViewModel = new GetGratingCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockCreditManager.Object.GrantingCredit(baseSalary);
            var isAblecreditAvailability = _mockCreditValidatorService.Object.CreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockCreditValidatorService.Object.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.Equal(creditResult,availableCredit.Result);
            Assert.False(getGratingCreditViewModel.IsAbleToCredit);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserWithOutLoansActiveAndLowBaseSalary()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 1000;


            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif
            };


            decimal creditResult = 1000;
            double riskIndexResult = -97.7;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;
            int loansCount = 0;
            double loansAmount = 0;

            _mockCreditManager.Setup(m => m.GrantingCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockCreditValidatorService.Setup(m => m.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, loansCount, loansAmount, (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockCreditValidatorService.Setup(m => m.CreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingCreditViewModel = new GetGratingCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockCreditManager.Object.GrantingCredit(baseSalary);
            var isAblecreditAvailability = _mockCreditValidatorService.Object.CreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockCreditValidatorService.Object.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, loansCount, loansAmount, (double)baseSalary);

            // Asserts
            Assert.Equal(creditResult, availableCredit.Result);
            Assert.True(getGratingCreditViewModel.IsAbleToCredit);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserHasLoansActiveAndMediumBaseSalary2000()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 2000;


            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
                {
                    new Loan { Amount = 500, IsActive = true }
                }
            };


            decimal creditResult = 1000;
            double riskIndexResult = -47.900000000000006;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;

            _mockCreditManager.Setup(m => m.GrantingCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockCreditValidatorService.Setup(m => m.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockCreditValidatorService.Setup(m => m.CreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingCreditViewModel = new GetGratingCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockCreditManager.Object.GrantingCredit(baseSalary);
            var isAblecreditAvailability = _mockCreditValidatorService.Object.CreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockCreditValidatorService.Object.CalculateRiskIndex(ConstantsRiskAnalysis.UnemploymentTax, ConstantsRiskAnalysis.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.Equal(creditResult, availableCredit.Result);
            Assert.True(getGratingCreditViewModel.IsAbleToCredit);

        }


    }
}
