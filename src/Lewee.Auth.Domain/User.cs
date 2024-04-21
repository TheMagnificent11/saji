using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// User
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="identityId">Identity ID</param>
    public User(string identityId)
        : base()
    {
        this.IdentityId = identityId;
    }

    /// <summary>
    /// Gets or sets the user ID from the identity provider.
    /// </summary>
    public string IdentityId { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets whether the user is an administrator.
    /// </summary>
    public bool IsAdministrator { get; protected set; }
}
