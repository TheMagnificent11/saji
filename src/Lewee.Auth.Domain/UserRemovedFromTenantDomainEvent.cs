using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// User Removed From Tenant Domain Event
/// </summary>
public class UserRemovedFromTenantDomainEvent : DomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRemovedFromTenantDomainEvent"/> class.
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="userRemovedId">ID of the user removed</param>
    /// <param name="correlationId">Correlation ID</param>
    public UserRemovedFromTenantDomainEvent(Guid tenantId, Guid userRemovedId, Guid correlationId)
        : base(correlationId)
    {
        this.TenantId = tenantId;
        this.UserRemovedId = userRemovedId;
    }

    /// <summary>
    /// Gets the tenant ID.
    /// </summary>
    public Guid TenantId { get; }

    /// <summary>
    /// Gets the user ID of the user that was removed.
    /// </summary>
    /// <remarks>
    /// UserId is the ID of the user that took the action to remove the user from the tenant.
    /// </remarks>
    public Guid UserRemovedId { get; }
}
