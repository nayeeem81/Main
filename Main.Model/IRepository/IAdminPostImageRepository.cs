using Domain.Model;
using Main.Common.Enums;

namespace IRepository;

public interface IAdminPostImageRepository
{
    Task<List<PanelPost>> GetSelectAdminPosts (
        EnumCompanyName company );
}