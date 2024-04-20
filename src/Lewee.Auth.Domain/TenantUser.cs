using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Tenant User
/// </summary>
public class TenantUser : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUser"/> class.
    /// </summary>
    /// <param name="tenant">Tenant</param>
    /// <param name="user">User</param>
    public TenantUser(Tenant tenant, User user)
    {
        this.Tenant = tenant;
        this.User = user;

        this.TenantId = tenant.Id;
        this.UserId = user.Id;

        this.StartDateTimeUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets the tenant.
    /// </summary>
    public Tenant Tenant { get; protected set; }

    /// <summary>
    /// Gets or sets the tenant ID.
    /// </summary>
    public Guid TenantId { get; protected set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public User User { get; protected set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; protected set; }

    /// <summary>
    /// Gets or sets the start date time.
    /// </summary>
    public DateTime StartDateTimeUtc { get; protected set; }

    /// <summary>
    /// Gets or sets the end date time.
    /// </summary>
    public DateTime? EndDateTimeUtd { get; protected set; }

    /// <summary>
    /// Ends the tenant membership.
    /// </summary>
    public void End()
    {
        this.EndDateTimeUtd = DateTime.UtcNow;
    }
}
