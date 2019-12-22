using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Helpers;
using System.Threading.Tasks;
using WebApi.Repositories;
using WebApi.Models.Users;
using AutoMapper;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<User> CreateAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            var tmpUser = await _userRepository.GetAsync(user.Username);

            if (tmpUser != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.AddAsync(user);

            return user;
        }

        public async Task UpdateAsync(User user, string password = null)
        {
            var tmpUser = await _userRepository.GetAsync(user.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(user.Username) && user.Username != tmpUser.Username)
            {
                // throw error if the new username is already taken
                if (tmpUser != null)
                    throw new AppException("Username " + user.Username + " is already taken");

                user.Username = tmpUser.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(user.FirstName))
                tmpUser.FirstName = user.FirstName;

            if (!string.IsNullOrWhiteSpace(user.LastName))
                tmpUser.LastName = user.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                tmpUser.PasswordHash = passwordHash;
                tmpUser.PasswordSalt = passwordSalt;
            }

            await _userRepository.UpdateAsync(tmpUser);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }

        // private helper methods

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