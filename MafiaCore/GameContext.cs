using System;
using System.Collections.Generic;

namespace MafiaCore
{
    public class GameContext : Context
    {
        public List<Player> Players = new List<Player>();
        public Random rng = new Random();
    }
}
