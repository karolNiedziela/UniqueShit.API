namespace UniqueShit.Application.Features.Models.Commands
{
    public sealed record CreateModelResponse(int Id, string Name, int ProductCategoryId, int BrandId);
}
