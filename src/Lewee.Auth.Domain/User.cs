using Lewee.Domain;

namespace Lewee.Auth.Domain;

/// <summary>
/// User
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Gets or sets the user ID from the identity provider.
    /// </summary>
    public string IdentityId { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets whether the user is an administrator.
    /// </summary>
    public bool IsAdministrator { get; protected set; }
}
