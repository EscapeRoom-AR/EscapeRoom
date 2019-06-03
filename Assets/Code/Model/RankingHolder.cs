using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RankingHolder : MonoBehaviour
{
    public IEnumerable<Game> Ranking { get; set; }
    public int Count { get; set; }
    public Game UserGame { get; set; }
}
