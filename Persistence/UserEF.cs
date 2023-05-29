using Npgsql;
using art_gallery_api;
using art_gallery_api.Persistence;
using art_gallery_api.Models;
using Microsoft.EntityFrameworkCore;

public class UserEF : IRepository, IUserDataAccess
{
    private IRepository _repo => this;

    private GalleryContext context;

    public UserEF(GalleryContext context)
    {
        this.context = context;
    }

    public List<User> GetUsers()
    {
        return context.Users.ToList();
    }

    // public List<Artifact> GetSquareMapsOnly()
    // {
    //     var listOfMaps = context.Maps.Where(x => x.IsSquare==true).Select(x => x.Id);
    //     return context.Maps.Where(x => listOfMaps.Contains(x.Id)).ToList();
    // }

    public List<User> GetAdminUsersOnly()
    {
        var listOfUsers = context.Users.Where(x => x.Role.Equals("admin")).Select(x => x.UserId);
        return context.Users.Where(x => listOfUsers.Contains(x.UserId)).ToList();
    }

    public List<User> GetMemebersOnly()
    {
        var listOfUsers = context.Users.Where(x => x.HaveMembership==true).Select(x => x.UserId);
        return context.Users.Where(x => listOfUsers.Contains(x.UserId)).ToList();
    }

    public User InsertUser(User newUser)
    {
        // newArtifact.CreatedDate = DateTime.Now;
        // newArtifact.AcquiredDate = DateTime.Now;
        newUser.Createddate = DateTime.Now;
        newUser.Modifieddate = DateTime.Now;
        context.Users.Add(newUser);
        context.SaveChanges();
        var users = GetUsers();
        var result = users.Find(x => x.UserId==newUser.UserId);

        return result;
    }

    public User RemoveUser(int id)
    {
        var users = context.Users.ToList();
        User removingUser = users.Find(x => x.UserId==id);
        context.Users.Remove(removingUser);
        context.SaveChanges();
        return removingUser;
    }

    public User UpdateUser(User user, User updatedUser)
    {
        // temp variables
        string temp_first_name;
        string temp_last_name;
        string temp_email;
        string? temp_role = null;
        bool have_membership;

        if(!updatedUser.Firstname.Equals(user.Firstname))
        {
            temp_first_name = updatedUser.Firstname;
        }
        else
        {
            temp_first_name = user.Firstname;
        }

        if(!updatedUser.Lastname.Equals(user.Lastname))
        {
            temp_last_name = updatedUser.Lastname;
        }
        else
        {
            temp_last_name = user.Lastname;
        }

        if(!updatedUser.Email.Equals(user.Email))
        {
            temp_email = updatedUser.Email;
        }
        else
        {
            temp_email = user.Email;
        }

        if(updatedUser.Role!=null)
        {
            if(!updatedUser.Role.Equals(user.Role))
            {
                temp_role = updatedUser.Role;
            }
            else
            {
                temp_role = user.Role;
            }
        }

        if(updatedUser.HaveMembership!=user.HaveMembership)
        {
            have_membership = updatedUser.HaveMembership;
        }
        else
        {
            have_membership = user.HaveMembership;
        }

        var searchUser = context.Users.FirstOrDefault(x => x.UserId==user.UserId);

        if(searchUser!=null)
        {
            searchUser.Firstname = temp_first_name;
            searchUser.Lastname = temp_last_name;
            searchUser.Email = temp_email;
            searchUser.Role = temp_role;
            searchUser.HaveMembership = have_membership;
            searchUser.Modifieddate = DateTime.Now;
        }
        
        context.SaveChanges();

        return searchUser;
    }
}