using BusinessModel;
using Main.Model;

namespace IService;

public interface IAdminPostMappingService
    {
        AdminPost MapAdminPostViewModelToAdminPostEntity(AdminPostDataModel objAdminPostVM);

        void MapAdminPostEntityToAdminPostViewModelListModel(AdminPost postEntity, AdminPostDataModel postViewModel);

        List<AdminImageFile> MapAdmiFileViweModelToAdminFileEntity(AdminPostDataModel adminFileVM);
    }

