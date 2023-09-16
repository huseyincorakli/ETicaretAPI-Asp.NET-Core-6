using ETicaretAPI_V2.Application.DTOs.User;

namespace ETicaretAPI_V2.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public object  Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}
