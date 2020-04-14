using System;
using System.Collections.Generic;
using System.Linq;
using backend.Data;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public interface IUserService
    {
        ApplicationUser Authenticate(string username, string password);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        ApplicationUser Create(ApplicationUser user, string password);
        void Update(ApplicationUser user, string password = null);
        void Delete(string id);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(u => u.UserName == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!_signInManager.PasswordSignInAsync(user.UserName, password, true, false).Result.Succeeded)
                return null;

            // authentication successful
            return user;
        }

        
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public ApplicationUser Create(ApplicationUser user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.UserName == user.UserName))
                throw new AppException("Username \"" + user.UserName + "\" is already taken");

            _userManager.CreateAsync(user, password).Wait();
            _context.SaveChanges();
            
            return user;
        }

        public void Update(ApplicationUser userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("Username " + userParam.UserName + " is already taken");

                user.UserName = userParam.UserName;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                _userManager.AddPasswordAsync(user, password);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        
    }
}