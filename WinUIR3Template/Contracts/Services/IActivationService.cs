namespace WinUIR3Template.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
