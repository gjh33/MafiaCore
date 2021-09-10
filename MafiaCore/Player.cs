using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public class Player
    {
        public Role Role;
        public Context Context = new Context();

        public void AssignRole(Role role)
        {
            Role = role;
        }
    }
}
