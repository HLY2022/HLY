using HLY.WEB.Data.IServices;
using HLY.WEB.Data.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HLY.WEB.Data.Services
{
    public class UsersService: IUsersService
    {
        private DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;
        }
        public Users Authenticate(string Code, string Password)
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Password))
                return null;
            //var userlist = _context.Users.ToList();
            var user = _context.Users.SingleOrDefault(x => x.Code == Code);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(Password, user.PasswordHash))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }

        public Users GetById(int guid)
        {
            return _context.Users.Find(guid);
        }

        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Code == user.Code))
                throw new Exception("Username '" + user.Code + "' is already taken");

            string passwordHash;
            CreatePasswordHash(password, out passwordHash);
            user.PasswordHash = passwordHash;
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Users userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Guid);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Code != user.Code)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Code == userParam.Code))
                    throw new Exception("Username " + userParam.Code + " is already taken");
            }

            // update user properties
            user.Code = userParam.Code;
            user.Name = userParam.Name;
            user.Email = userParam.Email;
            user.Mobile = userParam.Mobile;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                string passwordHash;
                CreatePasswordHash(password, out passwordHash);
                user.PasswordHash = passwordHash;
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string Password, out string passwordHash)
        {
            if (Password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password)));
            }
        }

        private static bool VerifyPasswordHash(string Password, string PasswordHash)
        {
            if (Password == null) throw new ArgumentNullException("password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password)));
                if(PasswordHash != computedHash)
                    return true;
            }

            return true;
        }
    }
}
