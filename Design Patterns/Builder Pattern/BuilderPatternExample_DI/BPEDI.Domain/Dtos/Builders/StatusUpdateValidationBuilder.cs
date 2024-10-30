using BPEDI.Domain.Validators;

namespace BPEDI.Domain.Dtos.Builders;

public class StatusUpdateValidationBuilder
{
    private readonly StatusUpdateDto _statusUpdateDto;
    private readonly StatusUpdateRefExistsValidator _refExistsValidator;
    private readonly StatusUpdateCorrectStateValidator _correctStateValidator;

    private StatusUpdateValidationBuilder(StatusUpdateDto statusUpdateDto)
    {
        _refExistsValidator = new StatusUpdateRefExistsValidator();
        _correctStateValidator = new StatusUpdateCorrectStateValidator();
        _statusUpdateDto = statusUpdateDto;
    }
    
    public static StatusUpdateValidationBuilder Empty(StatusUpdateDto dto) => new(dto);
    
    public StatusUpdateValidationBuilder ReferenceMustExist()
    {
        var validationResult = _refExistsValidator.Validate(_statusUpdateDto);
        return this;
    }

    public StatusUpdateValidationBuilder ReferenceInCorrectState()
    {
        var validationResult = _correctStateValidator.Validate(_statusUpdateDto);
        return this;
    }
}