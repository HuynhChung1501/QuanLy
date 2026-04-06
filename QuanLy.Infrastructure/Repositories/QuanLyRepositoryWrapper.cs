using QuanLy.Domain.Interface;
using QuanLy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Infrastructure.Repositories
{
    public class QuanLyRepositoryWrapper : IQuanLyRepositoryWrapper
    {
        private readonly DASContext _repoContext;
        public QuanLyRepositoryWrapper(DASContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        private IAuthUsersRepository _authUser;
        public IAuthUsersRepository AuthUser
        {
            get
            {
                if (_authUser == null)
                {
                    _authUser = new AuthUsersRepository(_repoContext);
                }
                return _authUser;
            }
        }
        private IAuth_AssignRepository _iAuth_AssignRepository;
        public IAuth_AssignRepository Auth_AssignRepository
        {
            get
            {
                if (_iAuth_AssignRepository == null)
                {
                    _iAuth_AssignRepository = new Auth_AssignRepository(_repoContext);
                }
                return _iAuth_AssignRepository;
            }
        }

        private IAuth_Assign_RoleRepository _iAuth_Assign_RoleRepository;
        public IAuth_Assign_RoleRepository Auth_Assign_RoleRepository
        {
            get
            {
                if (_iAuth_Assign_RoleRepository == null)
                {
                    _iAuth_Assign_RoleRepository = new Auth_Assign_RoleRepository(_repoContext);
                }
                return _iAuth_Assign_RoleRepository;
            }
        }

        private IAuth_PermissionsRepository _iAuth_PermissionsRepository;
        public IAuth_PermissionsRepository Auth_PermissionsRepository
        {
            get
            {
                if (_iAuth_PermissionsRepository == null)
                {
                    _iAuth_PermissionsRepository = new Auth_PermissionsRepository(_repoContext);
                }
                return _iAuth_PermissionsRepository;
            }
        }
        private IAuth_RolesRepository _iAuth_RolesRepository;
        public IAuth_RolesRepository Auth_RolesRepository
        {
            get
            {
                if (_iAuth_RolesRepository == null)
                {
                    _iAuth_RolesRepository = new Auth_RolesRepository(_repoContext);
                }
                return _iAuth_RolesRepository;
            }
        }
        private IAuth_UserRolesRepository _iuth_UserRolesRepository;
        public IAuth_UserRolesRepository Auth_UserRolesRepository
        {
            get
            {
                if (_iuth_UserRolesRepository == null)
                {
                    _iuth_UserRolesRepository = new Auth_UserRolesRepository(_repoContext);
                }
                return _iuth_UserRolesRepository;
            }
        }

        private IAuth_UsersRepository _iAuth_UsersRepository;
        public IAuth_UsersRepository Auth_UsersRepository
        {
            get
            {
                if (_iAuth_UsersRepository == null)
                {
                    _iAuth_UsersRepository = new Auth_UsersRepository(_repoContext);
                }
                return _iAuth_UsersRepository;
            }
        }


    }
}
