using FluentValidation;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(v => v.CreateCategoryDTO.Name)
            .NotEmpty().WithMessage("Tên Danh mục không được để trống.")
            .MaximumLength(100).WithMessage("Tên Danh mục chỉ có tối đa 100 ký tự.");
            
        RuleFor(v => v.CreateCategoryDTO.Description)
            .MaximumLength(500).WithMessage("Mô tả chỉ có tối đa 500 ký tự.");
    }
}
