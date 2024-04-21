using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// User Add To Tenant Domain Event
/// </summary>
public class UserAddToTenantDomainEvent : DomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserAddToTenantDomainEvent"/> class.
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="userAddedId">ID of the user added</param>
    /// <param name="correlationId">Correlation ID</param>
    public UserAddToTenantDomainEvent(Guid tenantId, Guid userAddedId, Guid correlationId)
        : base(correlationId)
    {
        this.TenantId = tenantId;
        this.UserAddId = userAddedId;
        this.TenantId = tenantId;
    }

    /// <summary>
    /// Gets the tenant ID.
    /// </summary>
    public Guid TenantId { get; }

    /// <summary>
    /// Gets the user ID of the user added to the tenant.
    /// </summary>
    /// <remarks>
    /// UserId is the ID of the user that took the action to add the user to the tenant.
    /// </remarks>
    public Guid UserAddId { get; }
}
