using Services;
using System.Linq;
using UnityEngine;

public class GetRankingScript : MonoBehaviour
{
    public RESTService rest;
    public ModalService modal;
    private int _roomCode = 1;
    public RankingHolder RankingHolder;

    // Components
    public GameObject RankingList1;
    public GameObject RankingList2;
    public GameObject RankingList3;
    public GameObject RankingList4;
    public GameObject RankingList5;
    public GameObject RankingList6;
    public GameObject RankingList7;
    public GameObject RankingList8;
    public GameObject RankingList9;
    public GameObject RankingList10;
    // End Components


    // Start is called before the first frame update
    void Start()
    {
        // TODO remove this, just testing
        GetRankings();
    }

    public void GetRankings()
    {
        print("GET RANKINGS");
        rest.GetRanking(_roomCode, resp =>
         {
             print(resp.message); // remove
             if (resp.IsError())
                 modal.ShowModal(resp.message);
             else RankingHolder = resp.data;

             if (RankingHolder != null && RankingHolder.Ranking.Any())
                 foreach (var ranking in RankingHolder.Ranking)
                 {
                     print(ranking.Score);
                 }
         });
    }
}
