namespace EmptyProject.Areas.CMS.MenuBack.DTOs
{
    public class itemForFolderForDashboard
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int MenuFatherId { get; set; }
        public int Order { get; set; }
        public string URLPath { get; set; }
        public string IconURLPath { get; set; }
        public bool Active { get; set; }
        public int UserCreationId { get; set; }
        public int UserLastModificationId { get; set; }
        public DateTime DateTimeCreation { get; set; }
        public DateTime DateTimeLastModification { get; set; }
    }
}
