using System;
using System.Collections.Generic;

namespace WpfAppProject.Interfaces
{
    public interface ILoad : ISaveLoad
    {
        void Load();
        Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData();
    }
}
