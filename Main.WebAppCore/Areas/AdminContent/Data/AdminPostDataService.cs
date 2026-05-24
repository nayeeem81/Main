using BusinessModel;

using DataTransferModel;

using IRepository;

namespace FineArtsWebApp
{
    public class AdminPostDataService : IAdminPostDataService
    {
        private readonly IAdminPostRepository _AdminPostRepository;
        private readonly IAdminPostMappingService _AdminPostMappingService;

        public AdminPostDataService(IAdminPostRepository adminPostRepository, IAdminPostMappingService adminPostMappingService)
        {
            _AdminPostRepository = adminPostRepository;
            _AdminPostMappingService  = adminPostMappingService;
        }

        public async Task<List<AdminPostViewModel>> GetAllAdminPosts()
        {
            var listPosts = await _AdminPostRepository.GetAllAdminContentPosts();

            List<AdminPostViewModel> objListPostVM = new List<AdminPostViewModel>();

            AdminPostViewModel objModel;

            foreach (AdminPostDataModel item in listPosts.ToList())
            {
                objModel = new AdminPostViewModel();
                _AdminPostMappingService.(item, objModel);
                objListPostVM.Add(objModel);
            }
            return objListPostVM;
        }

        public async Task<bool> SaveNewAdminPost(AdminPostDataModel objAdminPostDM)
        {
            AdminPostDataModel objAdminPostDataModel = _AdminPostMappingService.MapAdminPostViewModelToAdminPostEntity(objAdminPostDM);

            objAdminPostDataModel.SetModelBase( objAdminPostDM.ModelBase);
            objAdminPostDataModel.UserID = objAdminPostDM.UserID;
            

            List<AdminImageFileDataModel>objListFileDataModel =        
                _AdminPostMappingService
                .MapAdmiFileViweModelToAdminFileEntity(objAdminPostDM);

            var result = await _AdminPostRepository.SaveNewAdminPost(objAdminPostDataModel, objListFileDataModel);
            
            return result;
        }


        public async Task<AdminPostViewModel> GetAdminPostForEditPostID(int postID)
        {
            var postEntity = await _AdminPostRepository.GetAdminPostByPostID(postID);

            List<AdminImageFileViewModel> objListFiles = new List<AdminImageFileViewModel>();

            if (postEntity.ListAdminImageFiles != null && postEntity.ListAdminImageFiles.Count > 0)
            {
                postEntity.ListAdminImageFiles.ToList().ForEach(fileEntity =>
                {
                    AdminImageFileViewModel objFileVM = new AdminImageFileViewModel()
                    {
                        AdminImageFileID = fileEntity.AdminImageFileID,
                        ImageFileContent = fileEntity.ImageFileContent,
                        AdminPostID = fileEntity.AdminPostID
                    };
                    objListFiles.Add(objFileVM);
                });
            }


            List<AdminPostCommentViewModel> objListComments = new List<AdminPostCommentViewModel>();

            if (postEntity.ListAdminPostComments != null && postEntity.ListAdminPostComments.Count > 0)
            {
                postEntity.ListAdminPostComments.ToList().ForEach(commentEntity =>
                {
                    AdminPostCommentViewModel objCommentVM = new AdminPostCommentViewModel()
                    {
                        AdminPostCommentID = commentEntity.AdminPostCommentID,
                        Comment = commentEntity.Comment,
                        AdminPostID = commentEntity.AdminPostID
                    };
                    objListComments.Add(objCommentVM);
                });
            }

            AdminPostViewModel objModel = new AdminPostViewModel()
            {
                AdminPostID = postEntity.AdminPostID,
                PosterName = postEntity.PosterName,
                PostTitle = postEntity.Title,
                PosterContactNumber = postEntity.PosterContactNumber,
                WebsiteUrl = postEntity.WebsiteUrl,
                ShortNote = postEntity.ShortNote,
                SearchTag = postEntity.SearchTag,
                UserID = postEntity.UserID,
                PostTypeID = (int)postEntity.PostType,
                ListAdminPostFileImages = objListFiles,
                ListAdminPostComments = objListComments,
                HostCompanyName = postEntity.HostCompanyName,
                HostCountry = postEntity.HostCountry
            };

            return objModel;
        }

        public async Task<bool> UpdateAdminPost(AdminPostViewModel objPostVm)
        {
            var post = await _AdminPostRepository.GetAdminPostByPostID(objPostVm.AdminPostID);

            if (post != null)
            {
                post.ModifyBaseData(objPostVm.ModelBase);
                post.UserID = objPostVm.UserID;
                post.User = null;
            }

            List<AdminImageFile> images = new List<AdminImageFile>();

            images.AddRange(post.ListAdminImageFiles);

            objPostVm.ListAdminPostFileImages.ForEach(fileVM =>
            {
                var objFile = new AdminImageFile(fileVM.ImageFileContent);
                objFile.AdminPostID = post.AdminPostID;
                images.Add(objFile);
            });

            ///
            List<AdminPostComment> comments = new List<AdminPostComment>();

            comments.AddRange(post.ListAdminPostComments);

            objPostVm.ListAdminPostComments.ForEach(commentVM =>
            {
                var objComment = new AdminPostComment();
                objComment.AdminPostID = post.AdminPostID;
                objComment.Comment = commentVM.Comment;
                comments.Add(objComment);
            });

            post.PosterName = objPostVm.PosterName;
            post.Title = objPostVm.PostTitle;
            post.PosterContactNumber = objPostVm.PosterContactNumber;
            post.WebsiteUrl = objPostVm.WebsiteUrl;
            post.ShortNote = objPostVm.ShortNote;
            post.SearchTag = objPostVm.SearchTag;
            post.PostType = (EnumPostType)objPostVm.PostTypeID;
            post.ListAdminPostComments = comments;
            post.ListAdminImageFiles = images;
            post.AdminPostID = objPostVm.AdminPostID;


            var result = await _AdminPostRepository.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAdminPostImage(int id, int postId)
        {
            return await _AdminPostRepository.DeleteAdminPostImage(id, postId);
        }

        public async Task<bool> DeleteAdminPost(int postId)
        {
            var resultDelete = await _AdminPostRepository.DeleteAdminPost(postId);
            return resultDelete;
        }
    }
}
