using EmptyProject.Areas.CMS.MenuBack.Entities;

namespace EmptyProject.Areas.CMS.MenuBack.DTOs
{
    public class menuWithStateDTO : Menu
    {
        public bool IsSelected { get; set; }
    }
}
