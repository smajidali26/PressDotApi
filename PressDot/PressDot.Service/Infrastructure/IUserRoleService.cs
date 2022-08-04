using PressDot.Core.Domain;

namespace PressDot.Service.Infrastructure
{
    public interface IUserRoleService : IService<UserRole>
    {
        bool IsValidRoleId(int roleId);
    }
}
