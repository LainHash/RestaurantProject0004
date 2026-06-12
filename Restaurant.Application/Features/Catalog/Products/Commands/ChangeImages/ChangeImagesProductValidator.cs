using FluentValidation;

namespace Restaurant.Application.Features.Catalog.Products.Commands.ChangeImages
{
    public class ChangeImagesProductValidator : AbstractValidator<ChangeImagesProductCommand>
    {
        public ChangeImagesProductValidator()
        {
            // Mỗi URL trong danh sách thêm phải hợp lệ và không rỗng
            RuleForEach(v => v.ChangeImagesProductDTO.ImagesToAdd)
                .NotEmpty().WithMessage("URL ảnh thêm mới không được để trống.")
                .MaximumLength(2048).WithMessage("URL ảnh thêm mới không được vượt quá 2048 ký tự.");

            // Mỗi URL trong danh sách xóa phải hợp lệ và không rỗng
            RuleForEach(v => v.ChangeImagesProductDTO.ImagesToRemove)
                .NotEmpty().WithMessage("URL ảnh cần xóa không được để trống.")
                .MaximumLength(2048).WithMessage("URL ảnh cần xóa không được vượt quá 2048 ký tự.");

            // Nếu NewPrimaryImageUrl được cung cấp thì không được rỗng và phải hợp lệ
            When(v => v.ChangeImagesProductDTO.NewPrimaryImageUrl != null, () =>
            {
                RuleFor(v => v.ChangeImagesProductDTO.NewPrimaryImageUrl)
                    .NotEmpty().WithMessage("URL ảnh primary không được để trống nếu được cung cấp.")
                    .MaximumLength(2048).WithMessage("URL ảnh primary không được vượt quá 2048 ký tự.");
            });

            // Phải có ít nhất một thao tác (thêm, xóa hoặc đổi primary)
            RuleFor(v => v.ChangeImagesProductDTO)
                .Must(dto =>
                    dto.ImagesToAdd.Count > 0 ||
                    dto.ImagesToRemove.Count > 0 ||
                    dto.NewPrimaryImageUrl != null)
                .WithMessage("Phải có ít nhất một thao tác: thêm ảnh, xóa ảnh hoặc đổi ảnh primary.");
        }
    }
}
