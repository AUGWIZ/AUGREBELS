using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SpotTheDifference
{
    public class SpotsManager : MonoBehaviour
    {
        [SerializeField] private int limit;
        private int score = 0;
        public void SpotButtonClicked(int index)
        {
            Debug.Log("Index" + index);
            Debug.Log("Spot Button Clicke");
            score += 1;
            Debug.Log("Score" + score);

            UIController.Instance.UpdateScoreUI(score);

            if (score == limit)
            {
                StartCoroutine("DelayToPopUpNewScreen");
            }
        }

        private IEnumerator DelayToPopUpNewScreen()
        {
            yield return new WaitForSeconds(3);
            UIController.Instance.CongraulationsScreen();

            UIController.Instance.ScreenType = DataModel.ScreenType.CongraulationsScreen;
            
        }
    }
}
