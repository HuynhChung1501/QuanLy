using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Interface
{
    public interface IQuanLyRepositoryWrapper
    {
        IAuthUsersRepository AuthUser { get; }
        IAuth_AssignRepository Auth_AssignRepository { get; }
        IAuth_Assign_RoleRepository Auth_Assign_RoleRepository { get; }
        IAuth_PermissionsRepository Auth_PermissionsRepository { get; }
        IAuth_RolesRepository Auth_RolesRepository { get; }
        IAuth_UserRolesRepository Auth_UserRolesRepository { get; }
        IAuth_UsersRepository Auth_UsersRepository { get; }
    }
}
