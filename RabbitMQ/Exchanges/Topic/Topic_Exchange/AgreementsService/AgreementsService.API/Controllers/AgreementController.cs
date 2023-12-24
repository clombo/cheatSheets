using AgreementsService.API.Models;
using AgreementsService.API.Models.MessageDto;
using AutoMapper;
using Contracts;
using Contracts.Types;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AgreementsService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AgreementController : ControllerBase
{
    
    private readonly ILogger<AgreementController> _logger;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _endpoint;
    
    public AgreementController(ILogger<AgreementController> logger, IMapper mapper, IPublishEndpoint endpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _endpoint = endpoint;
    }

    [HttpPost(Name = "CaptureNewAgreement")]
    public async Task<IActionResult> Post([FromBody] AgreementsModel agreement)
    {
        var message = _mapper.Map<AgreementDetails>(agreement);
        switch (agreement.For)
        {
            case ForCompany.Office:
                await _endpoint.Publish<IOfficeAgreements>(message);
                break;
            case ForCompany.Store:
                await _endpoint.Publish<IStoreAgreements>(message);
                break;
        }
        return Ok();
    }
}