using Common;
using Model;

namespace Main.Service;

    public class AdminPostMappingService : IAdminPostMappingService
    {
        public AdminPostMappingService() { }

        public AdminPost MapAdminPostViewModelToAdminPostEntity(AdminPostViewModel objAdminPostVM)
        {
            return new AdminPost()
            {
                PosterName = objAdminPostVM.PosterName,
                Title = objAdminPostVM.PostTitle,
                PostType = (EnumPostType)objAdminPostVM.PostTypeID,
                WebsiteUrl = objAdminPostVM.WebsiteUrl,
                SearchTag = objAdminPostVM.SearchTag,
                ShortNote = objAdminPostVM.ShortNote,
                ListAdminImageFiles = new List<AdminImageFile>(),
                ListAdminPostComments = new List<AdminPostComment>(),
                PosterContactNumber = objAdminPostVM.PosterContactNumber 
            };
        }

        public List<AdminImageFile> MapAdmiFileViweModelToAdminFileEntity(AdminPostViewModel adminFileVM)
        {
            List<AdminImageFile> objListFileEntity = new List<AdminImageFile>();
            adminFileVM.ListAdminPostFileImages.ForEach(fileVM =>
            {
                objListFileEntity.Add(new AdminImageFile(fileVM.ImageFileContent));
            });
            return objListFileEntity;
        }

        public void MapAdminPostEntityToAdminPostViewModelListModel(AdminPost postEntity, AdminPostViewModel postViewModel)
        {
            postViewModel.AdminPostID = postEntity.AdminPostID;
            postViewModel.PostTitle = postEntity.Title;
            postViewModel.PosterContactNumber = postEntity.PosterContactNumber;
            postViewModel.PosterName = postEntity.PosterName;
            postViewModel.WebsiteUrl = postEntity.WebsiteUrl;
            postViewModel.PostTypeID = (int)postEntity.PostType;
            postViewModel.SearchTag = postEntity.SearchTag;
            postViewModel.ShortNote = postEntity.ShortNote;
            postViewModel.EnumAdminPostTypeDescription = EnumDescriptionHelper.GetDescription(postEntity.PostType);
        }
    }

