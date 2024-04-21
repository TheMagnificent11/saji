using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Tenant Created Domain Event
/// </summary>
public class TenantCreatedDomainEvent : DomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantCreatedDomainEvent"/> class.
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="correlationId">Correlation ID</param>
    public TenantCreatedDomainEvent(Guid tenantId, Guid correlationId)
        : base(correlationId)
    {
        this.TenantId = tenantId;
    }

    /// <summary>
    /// Gets the tenant ID.
    /// </summary>
    public Guid TenantId { get; }
}
