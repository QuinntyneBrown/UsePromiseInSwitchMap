using System;
using UsePromiseInSwitchMap.Api.Models;

namespace UsePromiseInSwitchMap.Api.Features
{
    public static class AddressExtensions
    {
        public static AddressDto ToDto(this Address address)
        {
            return new ()
            {
                AddressId = address.AddressId
            };
        }
        
    }
}
