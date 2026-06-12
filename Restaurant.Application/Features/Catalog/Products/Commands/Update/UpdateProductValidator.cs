using FluentValidation;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(v => v.UpdateProductDTO.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được để trống.")
            .MaximumLength(100).WithMessage("Tên sản phẩm chỉ có tối đa 100 ký tự.");

            RuleFor(v => v.UpdateProductDTO.Description)
                .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");

            RuleFor(v => v.UpdateProductDTO.Price)
                .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0.");

            RuleFor(v => v.UpdateProductDTO.Unit)
                .NotEmpty().WithMessage("Đơn vị tính không được để trống.")
                .MaximumLength(50).WithMessage("Đơn vị tính chỉ có tối đa 50 ký tự.");

            RuleFor(v => v.UpdateProductDTO.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng không được âm.");

            RuleFor(v => v.UpdateProductDTO.CategoryName)
                .NotEmpty().WithMessage("Tên danh mục không được để trống.")
                .MaximumLength(100).WithMessage("Tên danh mục chỉ có tối đa 100 ký tự.");
        }
    }
}
