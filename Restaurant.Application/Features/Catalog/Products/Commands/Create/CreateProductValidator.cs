using FluentValidation;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(v => v.CreateProductDTO.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được để trống.")
            .MaximumLength(100).WithMessage("Tên sản phẩm chỉ có tối đa 100 ký tự.");

        RuleFor(v => v.CreateProductDTO.Description)
            .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");

        RuleFor(v => v.CreateProductDTO.Price)
            .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0.");

        RuleFor(v => v.CreateProductDTO.Unit)
            .NotEmpty().WithMessage("Đơn vị tính không được để trống.")
            .MaximumLength(50).WithMessage("Đơn vị tính chỉ có tối đa 50 ký tự.");

        RuleFor(v => v.CreateProductDTO.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Số lượng không được âm.");

        RuleFor(v => v.CreateProductDTO.CategoryName)
            .NotEmpty().WithMessage("Tên danh mục không được để trống.")
            .MaximumLength(100).WithMessage("Tên danh mục chỉ có tối đa 100 ký tự.");
    }
}

