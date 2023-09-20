using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisualArray;

    /// <summary>
    /// Start is called before the first frame
    /// </summary>
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            foreach (GameObject selectedCounterVisual in selectedCounterVisualArray)
            {
                selectedCounterVisual.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject selectedCounterVisual in selectedCounterVisualArray)
            {
                selectedCounterVisual.SetActive(false);
            }
        }
    }
}
