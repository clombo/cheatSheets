using AutoMapper;
using BPEDI.Domain.Interfaces.Repositories;
using BPEDI.Domain.Mappings;

namespace BPEDI.Domain.Dtos.Builders;

public class StatusUpdateBuilder
{
    //Private properties used to construct Dto manually
    private string? _requestReferenceNumber;

    //Private readonly objects for internal use
    private StatusUpdateDto _statusUpdateDto;
    
    private readonly IMapper _mapper = 
        new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StatusUpdateDtoMapping>();
            }).CreateMapper();

    private readonly IUnitOfWork _unitOfWork;

    private readonly StatusUpdateValidationBuilder _validationBuilder;
    
    
    // State tracking
    private readonly bool _isMapped;

    private StatusUpdateBuilder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _statusUpdateDto = new StatusUpdateDto();
        _validationBuilder = StatusUpdateValidationBuilder.Empty(_statusUpdateDto);
        _isMapped = false;
    }
    
    private StatusUpdateBuilder(object? request, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _statusUpdateDto = _mapper.Map<StatusUpdateDto>(request);
        _validationBuilder = StatusUpdateValidationBuilder.Empty(_statusUpdateDto);
        _isMapped = true;
    }
    
    public static StatusUpdateBuilder Create(IUnitOfWork unitOfWork) => new(unitOfWork);

    public static StatusUpdateBuilder MapFrom<T>(T? request, IUnitOfWork unitOfWork) => new(request, unitOfWork);

    public StatusUpdateBuilder WithReferenceNumber(string referenceNumber)
    {
        _requestReferenceNumber = referenceNumber;
        return this;
    }
    
    public StatusUpdateDto ValidateAndBuild(Action<StatusUpdateValidationBuilder> action)
    {
        if (!_isMapped)
            _statusUpdateDto = new StatusUpdateDto() { RequestReferenceNumber = _requestReferenceNumber };

        action(_validationBuilder);

        return _statusUpdateDto;
    }

    public StatusUpdateDto Build()
    {
        if (!_isMapped)
            _statusUpdateDto = new StatusUpdateDto() { RequestReferenceNumber = _requestReferenceNumber };

        return _statusUpdateDto;
    }


}