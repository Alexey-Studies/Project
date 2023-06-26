using System;
using System.Collections.Generic;
using System.Xml;

namespace WpfAppProject.Interfaces
{
    public interface ISave : ISaveLoad
    {
        Dictionary<DateTime, Dictionary<DateTime, Dictionary<string, bool>>> GetData();
        XmlDocument PrepareData();
        void Save();
    }
}
