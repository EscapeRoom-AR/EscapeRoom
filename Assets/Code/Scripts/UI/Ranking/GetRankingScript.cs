using Model;
using Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    // User
    private IEnumerable<GameObject> _rankingListsArray;
    // Pagination
    private int _offset = 0;
    public Button NextButton;
    public Button PreviousButton;
    private bool _loading = true;
    public GameObject Background;

    // Start is called before the first frame update
    void Start()
    {
        _rankingListsArray = new GameObject[] { RankingList, RankingList1, RankingList2, RankingList3, RankingList4,
            RankingList5, RankingList6, RankingList7, RankingList8, RankingList9 };

        GetRankings();
    }

    public void GetRankings()
    {
        _loading = true;
        Background.SetActive(_loading);
        rest.GetRanking(_roomCode, _offset, resp =>
          {
              if (resp.IsError())
                  modal.ShowModal(resp.message);
              else RankingHolder = resp.data;

              if (RankingHolder != null && RankingHolder.Ranking != null && RankingHolder.Ranking.Any())
                  SetValues();
          });
    }

    public void SetValues()
    {

        if (RankingHolder.Count == 10)
        {
            NextButton.interactable = true;
        }
        else NextButton.interactable = false;

        if (_offset <= 0)
        {
            PreviousButton.interactable = false;
        }
        else PreviousButton.interactable = true;


        for (int i = RankingHolder.Count; i < _rankingListsArray.Count(); i++)
        {
            var list = _rankingListsArray.ElementAt(i);
            list.SetActive(false);
        }

        for (int i = 0; i < RankingHolder.Count; i++)
        {
            var list = _rankingListsArray.ElementAt(i);
            list.SetActive(true);
            var user = RankingHolder.Ranking.ElementAt(i).User;
            var username = user.Username;
            var score = RankingHolder.Ranking.ElementAt(i).Score;
            var number = RankingHolder.Offset + i + 1;

            list.transform.Find("Username").GetComponent<Text>().text = username;
            list.transform.Find("Number").GetComponent<Text>().text = number.ToString();
            list.transform.Find("Score").GetComponent<Text>().text = score.ToString();
            var image = list.transform.Find("UserImage").Find("Mask").Find("Icon").GetComponent<Image>();
            var imagePath = user.Image;
            if (!string.IsNullOrWhiteSpace(imagePath))
                rest.GetImage(imagePath, sprite => image.sprite = sprite);
        }

        var userGameUsernameComponent = RankingListUser.transform.Find("Username").GetComponent<Text>();
        var userGameNumberComponent = RankingListUser.transform.Find("Number").GetComponent<Text>();
        var userGameScoreComponent = RankingListUser.transform.Find("Score").GetComponent<Text>();

        if (RankingHolder.UserGame != null && RankingHolder.UserGame.Code != 0)
        {
            userGameUsernameComponent.text = RankingHolder.UserGame.User.Username;
            userGameNumberComponent.text = RankingHolder.NumberUser.ToString();
            userGameScoreComponent.text = RankingHolder.UserGame.Score.ToString();
        }
        else
        {
            Destroy(RankingListUser);
            //userGameUsernameComponent.text = "No records of you";
            //userGameNumberComponent.text = "";
            //userGameScoreComponent.text = "";
        }
        _loading = false;
        Background.SetActive(_loading);
    }

    public void NextButtonClick()
    {
        _offset += RankingHolder.Count;
        GetRankings();
    }

    public void PreviousButtonClick()
    {
        _offset -= RankingHolder.Count;
        GetRankings();
    }
}
