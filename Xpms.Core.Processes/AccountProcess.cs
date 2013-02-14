using Xpms.Core.IDB;
using Xpms.Core.Models;

namespace Xpms.Core.Processes
{
    public class AccountProcess
    {
        public IRepository Repository { get; set; }

        public EmailProcess Email { get; set; }


    }
}