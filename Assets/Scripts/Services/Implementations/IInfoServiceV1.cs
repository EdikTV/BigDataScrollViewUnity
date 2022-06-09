using System.Collections.Generic;
using System.Linq;
using Enums;
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

        public IEnumerable<UserInfo> GetPagedUserInfo(PagginationModel model)
        {
            var userInfos = new List<UserInfo>();

            for (var i = model.SkipCount; i < model.SkipCount + model.TakeCount; i++)
            {
                var offset = -1; // Так как Count это количество, то значение индекса будет на 1 меньше.
                
                if (i > _itemData.Count + offset) 
                {
                    return userInfos;
                }
                
                var itemData = _itemData[i];

                userInfos.Add(new UserInfo
                {
                    Email = itemData[nameof(DataFields.email)].ToString(),
                    Name = itemData[nameof(DataFields.first_name)].ToString()
                });
               
            }

            return userInfos.ToArray();
        }
    }
}