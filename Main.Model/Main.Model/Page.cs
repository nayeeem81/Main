using System.ComponentModel.DataAnnotations;
using Main.Common;
using Main.Common.EnumClasses;
using Main.Common.Model;

namespace Main.Model
{ 
    public class Page : BaseEntity
    {
        public Page()
        {
            ListPageContents = new List<PageContent>();
        }


        public Page(EnumPublicPage enumPublicPage)
        {
            EnumPublicPage = enumPublicPage;

            ListPageContents = new List<PageContent>();
        }


        //Ony used for seeding
        //Seed constructr hard coded
        public Page(EnumPublicPage enumPublicPage, int pageId)
        {
            PageID = pageId;
            EnumPublicPage = enumPublicPage;
            ListPageContents = new List<PageContent>();
            
            ModifiedBy = 1;
            CreatedBy = 1;
            CreatedDate = DateTime.MinValue;
            ModifiedDate = DateTime.MinValue;

            HostCountry = EnumCountry.Bangladesh;
            HostCompanyName = EnumCompanyName.FineArts;
            IsActive = true;
        }



        [Key]
        public int PageID { get; set; }


        [Required]
        public EnumPublicPage EnumPublicPage { get; set; }


        public virtual ICollection<PageContent> ListPageContents { get; set; } = new HashSet<PageContent>();


        public void SavePageContent(PageContent pageContent)
        {
            ListPageContents ??= [];

            if (pageContent != null && ListPageContents.Count (a => a.PageID == pageContent.PageID) == 0 )
            {
                ListPageContents.Add(pageContent);
            }
        }


        public PageContent GetNewOrExistingPageContent ( int pageId, ModelBase modelBase)
        {
            ListPageContents ??= [];

            int count = ListPageContents.Count;

            if ( count > 0 )
            {
                PageContent objOldPageContent = ListPageContents.Single<PageContent> ( a => a.PageID == pageId);

                if ( objOldPageContent != null )
                {
                    objOldPageContent.ModifyBaseData ( modelBase );

                    return objOldPageContent;
                }
                else
                {
                    PageContent objNewPageContent = new ( pageId );

                    objNewPageContent.CreateBaseData ( modelBase );

                    return objNewPageContent;
                }
            }
            else
            {
                PageContent objNewPageContent = new ( pageId );

                objNewPageContent.CreateBaseData ( modelBase );

                return objNewPageContent;
            }
        }
    }
}
