using System.Collections.Generic;
using System.Linq;
using LitJson;
using Services.Abstracts;
using Services.Contracts;
using UnityEngine;

namespace Services.Implementations
{
    public class InfoServiceV1 : IInfoService
    {
        private JsonData _itemData;

        public void ConfigureData (string jsonPath) 
            => _itemData = JsonMapper.ToObject(jsonPath);

        public IEnumerable<UserInfo> GetPagedUserInfo(PagedModel model)
        {
            var userInfos = new List<UserInfo>();

            for (var i = model.SkipCount; i < model.SkipCount + model.TakeCount; i++)
            {
                if (i > _itemData.Count - 1) 
                {
                    return userInfos;
                }
                
                var itemData = _itemData[i];

                userInfos.Add(new UserInfo
                {
                    Email = itemData["email"].ToString(),
                    Name = itemData["first_name"].ToString()
                });
               
            }

            return userInfos.ToArray();
        }
    }
}