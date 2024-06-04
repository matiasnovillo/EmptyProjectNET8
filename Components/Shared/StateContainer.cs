using EmptyProject.Areas.CMS.UserBack.Entities;

namespace EmptyProject.Components.Shared
{
    public class StateContainer
    {
        public User User { get; set; } = new User();

        public bool ShowOrHideSideNav { get; set; } = true;
    }
}
