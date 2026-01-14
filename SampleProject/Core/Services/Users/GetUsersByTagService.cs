using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetUsersByTagService : IGetUsersByTagService
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByTagService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsersByTag(string tag)
        {
            // repo returns IQueryable<User> or IEnumerable<User> — either way works
            return _userRepository.Get()
                .Where(u => u.Tags.Contains(tag));
        }
    }
}