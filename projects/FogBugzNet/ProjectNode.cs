using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FogBugzNet
{
    class ProjectNode
    {
        public XmlNode Node;
        public Dictionary<int, XmlNode> MileStoneIdToNode = new Dictionary<int, XmlNode>();

    }
}
