namespace art_gallery_api.Persistence;
using art_gallery_api.Models;
public interface IUserDataAccess
{
    List<User> GetUsers();
    List<User> GetAdminUsersOnly();
    List<User> GetMemebersOnly();
    User InsertUser(User newUser);
    User RemoveUser(int id);
    User UpdateUser(User user, User updatedUser);
}