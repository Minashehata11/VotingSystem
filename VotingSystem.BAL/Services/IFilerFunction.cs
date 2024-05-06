using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.VotingSystem.Services
{
    public interface IFilerFunction
    {
        // searching function return boolean , true if national number exists in xml file 
        public  bool StringInXml(string xmlFilePath, string searchString);
      
    }
}
