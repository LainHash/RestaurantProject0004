using FluentValidation;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Create
{
    public class CreateRestaurantTableValidator : AbstractValidator<CreateRestaurantTableCommand>
    {
        public CreateRestaurantTableValidator()
        {
            RuleFor(v => v.CreateRestaurantTableDTO.TableNumber)
                .GreaterThan(0).WithMessage("Số bàn phải lớn hơn 0.");

            RuleFor(v => v.CreateRestaurantTableDTO.FloorNumber)
                .GreaterThan(0).WithMessage("Số tầng phải lớn hơn 0.");

            RuleFor(v => v.CreateRestaurantTableDTO.Capacity)
                .GreaterThan(0).WithMessage("Sức chứa phải lớn hơn 0.");

            RuleFor(v => v.CreateRestaurantTableDTO.Shape)
                .NotEmpty().WithMessage("Hình dạng bàn không được để trống.")
                .MaximumLength(50).WithMessage("Hình dạng bàn chỉ có tối đa 50 ký tự.");

            //RuleFor(v => v.CreateRestaurantTableDTO.Status)
            //    .NotEmpty().WithMessage("Trạng thái bàn không được để trống.")
            //    .MaximumLength(50).WithMessage("Trạng thái bàn chỉ có tối đa 50 ký tự.");

            RuleFor(v => v.CreateRestaurantTableDTO.Description)
                .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");
        }
    }
}
