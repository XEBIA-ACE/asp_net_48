namespace AccountManagement.Application.Ports;

/// <summary>
/// Port for the service's main datastore connection. Adapters live in the Infrastructure layer.
/// </summary>
public interface IDatabaseConnection
{
    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}
