using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Tenant User Role
/// </summary>
public class TenantUserRole : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TenantUserRole"/> class.
    /// </summary>
    /// <param name="tenant">Tenant</param>
    /// <param name="user">User</param>
    /// <param name="role">Role</param>
    public TenantUserRole(Tenant tenant, User user, Role role)
        : base()
    {
        this.Tenant = tenant;
        this.User = user;
        this.Role = role;

        this.TenantId = tenant.Id;
        this.UserId = user.Id;
        this.RoleId = role.Id;

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
    /// Gets or sets the role.
    /// </summary>
    public Role Role { get; protected set; }

    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    public Guid RoleId { get; protected set; }

    /// <summary>
    /// Gets or sets the start date time.
    /// </summary>
    public DateTime StartDateTimeUtc { get; protected set; }

    /// <summary>
    /// Gets or sets the end date time.
    /// </summary>
    public DateTime? EndDateTimeUtd { get; protected set; }

    /// <summary>
    /// Ends the user role.
    /// </summary>
    public void End()
    {
        this.EndDateTimeUtd = DateTime.UtcNow;
    }
}
