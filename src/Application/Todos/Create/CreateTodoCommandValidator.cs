using FluentValidation;

namespace Application.Todos.Create;

internal sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Priority)
            .IsInEnum();
        
        RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(c => c.DueDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .When(x => x.DueDate.HasValue);
    }
}
