using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.VotingSystem.Services
{
    public class FilterFunction:IFilerFunction
    {
        // searching function return boolean , true if national number exists in xml file 
        public  bool StringInXml(string xmlFilePath, string searchString)
        {

            if (string.IsNullOrEmpty(xmlFilePath) || string.IsNullOrEmpty(searchString))
            {

                return false; // Handle empty inputs 
            }

            try
            {
                // Use XDocument for clean and efficient XML handling
                var doc = XDocument.Load(xmlFilePath);
                return doc.ToString().Contains(searchString); // return true if find or false if not or through exceptions
            }
            catch (Exception ex)
            {
                // Handle potential exceptions (e.g., file not found, invalid XML)
                Console.WriteLine($"Error: {ex.Message}");

                return false;
            }

        }
    }
}
