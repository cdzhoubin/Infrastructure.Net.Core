using Microsoft.Extensions.Configuration;
using System.Xml;
using Zhoubin.Infrastructure.Common.Config;

namespace Zhoubin.Infrastructure.Common.Test.Config
{
    public sealed class Entity : ConfigEntityBase
    {
        public string Propety { get { return GetValue<string>("Propety"); } set { SetValue("Propety", value); } }
        public string Propety1 { get { return GetValue<string>("Propety1"); } set { SetValue("Propety1", value); } }
    }
}
