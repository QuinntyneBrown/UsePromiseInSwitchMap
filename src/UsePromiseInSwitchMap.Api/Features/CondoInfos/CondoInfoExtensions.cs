using System;
using UsePromiseInSwitchMap.Api.Models;

namespace UsePromiseInSwitchMap.Api.Features
{
    public static class CondoInfoExtensions
    {
        public static CondoInfoDto ToDto(this CondoInfo condoInfo)
        {
            return new ()
            {
                CondoInfoId = condoInfo.CondoInfoId
            };
        }
        
    }
}
