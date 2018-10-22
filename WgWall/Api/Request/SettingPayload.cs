using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgWall.Api.Request.Interface;
using WgWall.Data.Model;

namespace WgWall.Api.Request
{
    public class SettingPayload : IWriteIntoPayload<Setting>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public void WriteInto(Setting entity)
        {
            entity.Key = Key;
            entity.Value = Value;
        }
    }
}
