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
    public IEnumerable<Game> rankings;

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
             else rankings = resp.data;

             if (rankings != null && rankings.Any())
                 foreach (var ranking in rankings)
                 {
                     print(ranking.Time);
                 }
         });
    }
}
