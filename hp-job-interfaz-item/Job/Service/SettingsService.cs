using Domain.Model.Properties;
using Domain.Service;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly Settings _setting;
        public SettingsService(IOptions<Settings> setting)
        {
            _setting = setting.Value;
        }

        public Settings GetSettings()
        {
            return _setting;
        }
    }
}
