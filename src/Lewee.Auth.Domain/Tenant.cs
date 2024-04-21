using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Tenant
/// </summary>
public class Tenant : AggregateRoot
{
    private readonly List<TenantUser> tenantUsers;

    internal Tenant(string name, string code)
        : base()
    {
        this.Name = name;
        this.Code = code;

        this.tenantUsers = [];
    }

     // EF constructor
    private Tenant()
    {
    }

    /// <summary>
    /// Gets or sets the tenant name.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Gets or sets the tenant code.
    /// </summary>
    public string Code { get; protected set; }

    /// <summary>
    /// Gets the tenant users.
    /// </summary>
    public IReadOnlyCollection<TenantUser> TenantUsers => this.tenantUsers.AsReadOnly();

    /// <summary>
    /// Creates a new tenant.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="code">Code</param>
    /// <param name="correlationId">Correlation ID</param>
    /// <returns>The created tenant</returns>
    public static Tenant Create(string name, string code, Guid correlationId)
    {
        var tenant = new Tenant(name, code);

        tenant.DomainEvents.Raise(new TenantCreatedDomainEvent(tenant.Id, correlationId));

        return tenant;
    }

    /// <summary>
    /// Adds a user to this tenant.
    /// </summary>
    /// <param name="user">User to add</param>
    /// <param name="correlationId">Correlation ID</param>
    public void AddUser(User user, Guid correlationId)
    {
        if (this.tenantUsers.Any(x => x.UserId == user.Id && x.IsActive && !x.IsDeleted))
        {
            return;
        }

        var tenantUser = new TenantUser(this, user);

        this.tenantUsers.Add(tenantUser);

        this.DomainEvents.Raise(new UserAddToTenantDomainEvent(this.Id, user.Id, correlationId));
    }

    /// <summary>
    /// Removes a user from this tenant.
    /// </summary>
    /// <param name="user">User to remove</param>
    /// <param name="correlationId">Correlation ID</param>
    public void RemoveUser(User user, Guid correlationId)
    {
        var tenantUser = this.tenantUsers.FirstOrDefault(x => x.UserId == user.Id && x.IsActive && !x.IsDeleted);

        if (tenantUser is null)
        {
            return;
        }

        tenantUser.End();

        this.DomainEvents.Raise(new UserRemovedFromTenantDomainEvent(this.Id, user.Id, correlationId));
    }
}
