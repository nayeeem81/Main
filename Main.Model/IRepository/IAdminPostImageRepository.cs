using Domain.Model;
using Main.Common.Enums;

namespace IRepository;

public interface IAdminPostImageRepository
{
    Task<List<AdminPost>>
        GetSelectAdminPosts ( EnumCompanyName company );
}