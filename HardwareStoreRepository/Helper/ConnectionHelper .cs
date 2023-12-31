﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Helper
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
               ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(ConfigurationManger.AppSetting["RedisURL"]);
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }

}
