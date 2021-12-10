using System;
using System.Collections.Generic;

namespace MafiaCore
{
    [Serializable]
    public class GameVariant
    {
        public List<Role> Roles = new List<Role>();

        public List<Team> ComputeTeams()
        {
            HashSet<Team> unique = new HashSet<Team>();
            foreach (Role role in Roles)
            {
                unique.Add(role.Team);
            }
            return new List<Team>(unique);
        }
    }
}
