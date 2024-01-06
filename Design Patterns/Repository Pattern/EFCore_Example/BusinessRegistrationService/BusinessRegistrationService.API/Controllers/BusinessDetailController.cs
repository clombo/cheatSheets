using AutoMapper;
using BusinessRegistrationService.API.Models;
using BusinessRegistrationService.API.Models.Types;
using BusinessRegistrationService.Data.Entities;
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
    public async Task<IActionResult> CreateNewBusinessDetailRecord([FromBody] CreateBusinessDetailRecordRequest request)
    {
        var newRecord = _mapper.Map<BusinessDetailEntity>(request);
        
        newRecord.AccountDetails.Select(s =>
        {
            s.BusinessDetailId = newRecord.Id;
            return s;
        }).ToList();
        
        newRecord.BankAccountId = newRecord.BankAccount.Id;
        
        await _unitOfWork.BusinessDetailRepository.AddAsync(newRecord);
        await _unitOfWork.CompleteAsync();
        
        return Ok(newRecord.Id);
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

    [HttpPut( "{id}")]

    public async Task<IActionResult> UpdateBusinessDetailRecord([FromBody] UpdateBusinessDetailRecordRequest request, Guid id, CancellationToken cancellationToken) //Add frombody
    {
        var recordToUpdate = await _unitOfWork.BusinessDetailRepository.Query(id, cancellationToken);
        
        _mapper.Map(request, recordToUpdate);
        _unitOfWork.BusinessDetailRepository.Update(recordToUpdate);
        await _unitOfWork.CompleteAsync();

        return Ok(recordToUpdate.Id);
    } 
}