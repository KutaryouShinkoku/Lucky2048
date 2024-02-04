using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class general_event_sound : MonoBehaviour
{
    public AK.Wwise.Event eventName;
    // Start is called before the first frame update
    public void PlayEvent()
    {
        eventName.Post(gameObject);
    }


}
