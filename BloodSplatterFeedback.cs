using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodSplatterFeedback : Feedback
{

    [SerializeField]
    private GameObject bloodSplatterPrefab = null;  // Blood splatter prefab to instantiate

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
    }

    public override void CreateFeedback()
    {
        // Instantiate the blood splatter at the given position
        GameObject bloodSplatter = Instantiate(bloodSplatterPrefab, transform.position, Quaternion.identity);

        
    }
}
