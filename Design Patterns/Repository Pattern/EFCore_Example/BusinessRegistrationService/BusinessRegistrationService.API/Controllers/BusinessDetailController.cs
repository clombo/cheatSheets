using AutoMapper;
using BusinessRegistrationService.API.Models;
using BusinessRegistrationService.API.Models.Types;
using BusinessRegistrationService.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessRegistrationService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessDetailController: ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BusinessDetailController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewBusinessDetailRecord() //Add frombody
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> GetBusinessDetailRecord(
        [FromQuery] GenericGetRecordRequest<IncludeInBusinessRecordEnum> query, 
        CancellationToken cancellationToken)
    {
        switch (query.Include)
        {
            case IncludeInBusinessRecordEnum.All:
                return Ok(await _unitOfWork.BusinessDetailRepository.QueryAndIncludeAll(query.Id, cancellationToken));
            case IncludeInBusinessRecordEnum.AccountDetail:
                return Ok(await _unitOfWork.BusinessDetailRepository.QueryAndIncludeAccountDetails(query.Id,
                    cancellationToken));
            case IncludeInBusinessRecordEnum.BankAccount:
                return Ok(await _unitOfWork.BusinessDetailRepository.QueryAndIncludeBankAccount(query.Id,
                    cancellationToken));
            default:
                return Ok(await _unitOfWork.BusinessDetailRepository.Query(query.Id, cancellationToken));
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBusinessDetailRecord() //Add frombody
    {
        
    } 
}