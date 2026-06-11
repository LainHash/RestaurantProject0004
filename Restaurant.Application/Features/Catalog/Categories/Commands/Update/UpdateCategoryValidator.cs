using FluentValidation;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id danh mục không được để trống.");

        RuleFor(v => v.UpdateCategoryDTO.Name)
            .NotEmpty().WithMessage("Tên danh mục không được để trống.")
            .MaximumLength(100).WithMessage("Tên danh mục chỉ có tối đa 100 ký tự.");

        RuleFor(v => v.UpdateCategoryDTO.Description)
            .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");
    }
}
