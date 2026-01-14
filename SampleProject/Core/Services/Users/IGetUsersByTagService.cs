using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IGetUsersByTagService
    {
        IEnumerable<User> GetUsersByTag(string tag);
    }
}
