using Model;
using Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetRankingScript : MonoBehaviour
{
    public RESTService rest;
    public ModalService modal;
    private int _roomCode = 1;
    RankingHolder RankingHolder;

    // Components
    public GameObject RankingList;
    public GameObject RankingList1;
    public GameObject RankingList2;
    public GameObject RankingList3;
    public GameObject RankingList4;
    public GameObject RankingList5;
    public GameObject RankingList6;
    public GameObject RankingList7;
    public GameObject RankingList8;
    public GameObject RankingList9;
    public GameObject RankingListUser;
    // End Components
    private IEnumerable<GameObject> _rankingListsArray;


    // Start is called before the first frame update
    void Start()
    {
        _rankingListsArray = new GameObject[] { RankingList, RankingList1, RankingList2, RankingList3, RankingList4,
            RankingList5, RankingList6, RankingList7, RankingList8, RankingList9 };

        GetRankings();
    }

    public void GetRankings()
    {
        print("GET RANKINGS");
        rest.GetRanking(_roomCode, resp =>
         {
             if (resp.IsError())
                 modal.ShowModal(resp.message);
             else RankingHolder = resp.data;

             print("Response");
             print(JsonUtility.ToJson(RankingHolder));

             if (RankingHolder != null && RankingHolder.Ranking != null && RankingHolder.Ranking.Any())
                 SetValues();
         });
    }

    public void SetValues()
    {
        print("SetValues");
        print(JsonUtility.ToJson(RankingHolder));
        for (int i = RankingHolder.Count - 1; i < _rankingListsArray.Count(); i++)
        {
            var list = _rankingListsArray.ElementAt(i);
            Destroy(list);
        }
    }
}
