using katio.Data.Dto;
using katio.Data.Models;

namespace katio.Business.Interfaces;

public interface IUserService
{
    Task<BaseMessage<User>> Index();
    Task<BaseMessage<User>> GetUserById(int Id);
    Task<BaseMessage<User>> GetUserByName(string Name);
    Task<BaseMessage<User>> GetUserByLastName(string LastName);
    Task<BaseMessage<User>> GetUserByEmail(string Email);
    Task<BaseMessage<User>> CreateUser(User user);
    Task<BaseMessage<User>> UpdateUser(User user);
    Task<BaseMessage<User>> DeleteUser(int id);

    
}


