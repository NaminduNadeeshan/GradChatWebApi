using GradChat.Data.Entity;
using GradChat.Data.Repo.UserRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradChat.Data.Repo.UserRepository
{
  public class UserRepo : IUserRepo
  {
    private GradChatDbContext _context;

    public UserRepo(GradChatDbContext context)
    {
        _context = context;
    }

    public User Authenticate(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        return null;

      var user = _context.User.SingleOrDefault(x => x.UserName == userName);

      //check username existing
      if (userName == null)
        return null;

      // check password is correct
      if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return user;
    }

    public User Create(User user, string password)
    {
       if (string.IsNullOrWhiteSpace(password))
        throw new Exception("Password is required");

      if (_context.User.Any(x => x.UserName == user.UserName))
        throw new Exception($"Username {user.UserName} is already taken");

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      _context.User.Add(user);
      _context.SaveChanges();

      return user;
    }

    public IQueryable<User> GetById(int Id)
    {
      var user = _context.User.Where(u => u.Id == Id);

      return user;
    }

    public User Update(User user, string password)
    {

      var users = _context.User.Find(user.Id);

      if (user == null)
        throw new Exception("User not found");

      if (user.UserName != user.UserName)
      {
        // username has changed so check if the new username is already taken
        if (_context.User.Any(x => x.UserName == user.UserName))
          throw new Exception($"Username {user.UserName}  is already taken");
      }

      // update user properties
      users.FirstName = user.FirstName;
      users.LastName = user.LastName;
      users.UserName = user.UserName;

      // update password if it was entered
      if (!string.IsNullOrWhiteSpace(password))
      {
        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
      }

      _context.User.Update(users);
      _context.SaveChanges();

      return user;
    }

    // verifieng the password hash
    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i]) return false;
        }
      }

      return true;
    }

    // create the password hash

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }
  }
}
