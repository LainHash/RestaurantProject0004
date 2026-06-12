using FluentValidation;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Update;

public class UpdateRestaurantTableValidator : AbstractValidator<UpdateRestaurantTableCommand>
{
    public UpdateRestaurantTableValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id bàn ăn không được để trống.");

        RuleFor(v => v.UpdateRestaurantTableDTO.TableNumber)
            .GreaterThan(0).WithMessage("Số bàn phải lớn hơn 0.");

        RuleFor(v => v.UpdateRestaurantTableDTO.FloorNumber)
            .GreaterThan(0).WithMessage("Số tầng phải lớn hơn 0.");

        RuleFor(v => v.UpdateRestaurantTableDTO.Capacity)
            .GreaterThan(0).WithMessage("Sức chứa phải lớn hơn 0.");

        RuleFor(v => v.UpdateRestaurantTableDTO.Shape)
            .NotEmpty().WithMessage("Hình dạng bàn không được để trống.")
            .MaximumLength(50).WithMessage("Hình dạng bàn chỉ có tối đa 50 ký tự.");

        RuleFor(v => v.UpdateRestaurantTableDTO.Status)
            .NotEmpty().WithMessage("Trạng thái bàn không được để trống.")
            .MaximumLength(50).WithMessage("Trạng thái bàn chỉ có tối đa 50 ký tự.");

        RuleFor(v => v.UpdateRestaurantTableDTO.Description)
            .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");
    }
}
