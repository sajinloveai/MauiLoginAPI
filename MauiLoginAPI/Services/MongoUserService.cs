using MauiLoginAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiLoginAPI.Services
{
    public class MongoUserService
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:Database"]);
            _users = database.GetCollection<User>("Users");
        }

        // Get all users
        public async Task<List<User>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        // Get a user by username
        public async Task<User> GetByUsernameAsync(string username) =>
            await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

        // Register a new user
        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        // ✅ Authenticate user (used for login)
        public async Task<User> Authenticate(string username, string password) =>
            await _users.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
    }
}
    