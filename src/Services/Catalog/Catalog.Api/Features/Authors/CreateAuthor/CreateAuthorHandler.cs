namespace Catalog.Api.Features.Authors.CreateAuthor;

public record CreateAuthorCommand(
    string Name) : ICommand<CreateAuthorResult>;

public record CreateAuthorResult(
    Guid Id);

public class CreateAuthorCommandHandler(
    ApplicationDbContext context) : ICommandHandler<CreateAuthorCommand, CreateAuthorResult>
{
    public async Task<CreateAuthorResult> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = command.Name
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateAuthorResult(author.Id);
    }
}
