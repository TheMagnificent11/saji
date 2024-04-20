using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Tenant
/// </summary>
public class Tenant : AggregateRoot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tenant"/> class.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="code">Code</param>
    public Tenant(string name, string code)
    {
        this.Name = name;
        this.Code = code;
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
}
