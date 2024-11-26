namespace Catalog.Api.Features.Authors.UpdateAuthor;

public record UpdateAuthorCommand(
    Guid Id, string Name) : ICommand<UpdateAuthorResult>;

public record UpdateAuthorResult(bool Success);

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}

public class UpdateAuthorCommandHandler(
    ApplicationDbContext context) : ICommandHandler<UpdateAuthorCommand, UpdateAuthorResult>
{
    public async Task<UpdateAuthorResult> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
    {
        var authorId = command.Id;

        var author = await context.Authors.FindAsync([authorId], cancellationToken: cancellationToken);

        if (author is null)
        {
            return new UpdateAuthorResult(false);
        }

        author.Name = command.Name;

        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);

        return new UpdateAuthorResult(true);
    }
}
