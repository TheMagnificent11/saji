using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// Role
/// </summary>
public class Role : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class"
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="description">Description</param>
    public Role(string name, string? description = null)
        : base()
    {
        this.Name = name;
        this.Description = description;
    }

    // EF constructor
    private Role()
    {
    }

    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Gets or sets the role description.
    /// </summary>
    public string? Description { get; protected set; }
}
