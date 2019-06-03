using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class RankingHolder
    {
        public int Count;
        public Game[] Ranking;
        public Game UserGame;
        public int Offset;
        public int NumberUser;
    }
}


