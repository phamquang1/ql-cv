using System.IO;
using System.Text;

namespace NCCTalentManagement.Utils
{
    public class UTF8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8; 
    }
}
