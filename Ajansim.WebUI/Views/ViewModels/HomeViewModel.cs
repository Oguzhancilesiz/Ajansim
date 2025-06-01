using Ajansim.Entities;

namespace Ajansim.WebUI.Views.ViewModels
{
    public class HomeViewModel
    {
        public List<Service> Services { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }
        public List<FAQ> FAQs { get; set; }
        //public List<Label> Brands { get; set; } eklenecek

        public Page? AboutPage { get; set; }
        public Page? ContactPage { get; set; }
        public Page? IndexPage { get; set; }

        //public SiteInfo? SiteInfo { get; set; }
        public Media? SiteLogo { get; set; }
    }
}
