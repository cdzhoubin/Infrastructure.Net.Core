using Microsoft.Extensions.Configuration;
using System.Xml;
using Zhoubin.Infrastructure.Common.Config;

namespace Zhoubin.Infrastructure.Common.Test.Config
{
    public sealed class Entity : ConfigEntityBase
    {
        public string Propety { get; private set; }
        public string Propety1 { get; private set; }

        protected override void SetProperty(IConfigurationSection node)
        {
            switch (node.Key)
            {
                case "Propety":
                    Propety = node.Value;
                    break;
                case "Propety1":
                    Propety1 = node.Value;
                    break;
            }
        }
    }
}
