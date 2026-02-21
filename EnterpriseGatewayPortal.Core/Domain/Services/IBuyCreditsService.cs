using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IBuyCreditsService
    {
        Task<ServiceResult> GetAllPrevillages(ServicesBySUIDDTO servicesBySUIDDTO);
        Task<ServiceResult> GetAllAvailableCredits(AvailableCreditsDTO availableCreditsDTO);
        Task<ServiceResult> GetRateCard(GetRateCardDTO getRateCardDTO);
        Task<ServiceResult> ProceedCheckout(PaymentBillCreditsServiceDTO paymentBillCreditsServiceDTO);

        
    }
}
