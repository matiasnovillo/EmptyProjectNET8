using EmptyProject.Areas.CMS.MenuBack.Entities;
using EmptyProject.Areas.CMS.MenuBack.Repositories;
using EmptyProject.Areas.CMS.RoleBack.Entities;
using EmptyProject.Areas.CMS.RoleBack.Repositories;
using EmptyProject.Areas.CMS.RoleMenuBack.Entities;
using EmptyProject.Areas.CMS.RoleMenuBack.Repositories;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.CMS.UserBack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProject.Controllers
{
    [ApiController]
    public class SeedTablesValuesController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly RoleMenuRepository _rolemenuRepository;
        private readonly MenuRepository _menuRepository;

        public SeedTablesValuesController(UserRepository userRepository,
            RoleRepository roleRepository,
            RoleMenuRepository rolemenuRepository,
            MenuRepository menuRepository) 
        { 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _rolemenuRepository = rolemenuRepository;
            _menuRepository = menuRepository;
        }

        [HttpGet("api/SeedTables")]
        public ActionResult Get()
        {
            #region User
            User User = new()
            {
                UserId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                Email = "novillo.matias1@gmail.com",
                Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw==",
                RoleId = 1,
                ProfilePicture = "/img/CMSCore/User.png"
            };
            _userRepository.Add(User);
            #endregion

            #region Role
            Role Role = new()
            {
                RoleId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                Name = "Root",
            };
            _roleRepository.Add(Role);
            #endregion

            #region RoleMenu
            RoleMenu RoleMenu1 = new() 
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 1
            };
            _rolemenuRepository.Add(RoleMenu1);

            RoleMenu RoleMenu2 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 2
            };
            _rolemenuRepository.Add(RoleMenu2);

            RoleMenu RoleMenu3 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 3
            };
            _rolemenuRepository.Add(RoleMenu3);

            RoleMenu RoleMenu4 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 4
            };
            _rolemenuRepository.Add(RoleMenu4);

            RoleMenu RoleMenu5 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 5
            };
            _rolemenuRepository.Add(RoleMenu5);

            RoleMenu RoleMenu6 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 6
            };
            _rolemenuRepository.Add(RoleMenu6);

            RoleMenu RoleMenu7 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 7
            };
            _rolemenuRepository.Add(RoleMenu7);

            RoleMenu RoleMenu8 = new()
            {
                RoleMenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                RoleId = 1,
                MenuId = 8
            };
            _rolemenuRepository.Add(RoleMenu8);
            #endregion

            #region Menu
            Menu MenuSistema = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "",
                MenuFatherId = 0,
                Order = 100,
                Name = "Sistema",
                URLPath = ""
            };
            _menuRepository.Add(MenuSistema);

            Menu MenuFallas = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "F",
                MenuFatherId = 1,
                Order = 0,
                Name = "Fallas",
                URLPath = "/System/FailurePage"
            };
            _menuRepository.Add(MenuFallas);

            Menu MenuParametros = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "P",
                MenuFatherId = 1,
                Order = 0,
                Name = "Parametros",
                URLPath = "/System/ParameterPage"
            };
            _menuRepository.Add(MenuParametros);

            Menu MenuCMS = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "",
                MenuFatherId = 0,
                Order = 200,
                Name = "CMS",
                URLPath = ""
            };
            _menuRepository.Add(MenuCMS);

            Menu MenuUsuarios = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "U",
                MenuFatherId = 4,
                Order = 0,
                Name = "Usuarios",
                URLPath = "/CMS/UserPage"
            };
            _menuRepository.Add(MenuUsuarios);

            Menu MenuRoles = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "R",
                MenuFatherId = 4,
                Order = 0,
                Name = "Roles",
                URLPath = "/CMS/RolePage"
            };
            _menuRepository.Add(MenuRoles);

            Menu MenuMenues = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "M",
                MenuFatherId = 4,
                Order = 0,
                Name = "Menues",
                URLPath = "/CMS/MenuPage"
            };
            _menuRepository.Add(MenuMenues);

            Menu MenuPermisos = new()
            {
                MenuId = 0,
                Active = true,
                DateTimeCreation = DateTime.Now,
                DateTimeLastModification = DateTime.Now,
                UserCreationId = 1,
                UserLastModificationId = 1,
                IconURLPath = "P",
                MenuFatherId = 4,
                Order = 0,
                Name = "Permisos",
                URLPath = "/CMS/Permission"
            };
            _menuRepository.Add(MenuPermisos);
            #endregion

            return Ok();
        }
    }
}
