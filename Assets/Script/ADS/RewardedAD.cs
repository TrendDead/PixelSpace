using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAD : MonoBehaviour
{

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
			if (PlayerPrefs.GetInt ("noads") != 1) {
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show ("rewardedVideo", options);
			}
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                int coins =PlayerPrefs.GetInt("coins");
                coins += 30;
                PlayerPrefs.SetInt("coins", coins);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}

