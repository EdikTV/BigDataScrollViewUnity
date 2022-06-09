using System.Collections.Generic;
using Services.Contracts;

namespace Services.Abstracts
{
    public interface IInfoService
    {
        void ConfigureData(string jsonPath);
        IEnumerable<UserInfo> GetPagedUserInfo(PagginationModel model);
    }
}