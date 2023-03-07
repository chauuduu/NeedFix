
using Domain.Staffs;

namespace Application.Service.StaffServices
{
    public interface IStaffService
    {
        string Token(string email, string password);
        void Login(string email, string password);
        void Logout();
        void Register(string citizenCode, string fullName, DateTime birthday, string phone, string address, int roleId, string email, string password);

    }
}
