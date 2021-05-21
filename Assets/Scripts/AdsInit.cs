using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour
{
    private string _gameId = "4135281";
    private bool _testMode = true;


    private void Start()
    {
        Advertisement.Initialize(_gameId, _testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    private IEnumerator ShowBannerWhenReady()
    {
        while(!Advertisement.IsReady("MainBottom"))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show("MainBottom");
    }
}
