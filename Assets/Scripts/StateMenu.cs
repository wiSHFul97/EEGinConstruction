using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMenu : MonoBehaviour
{
    private GameObject segmentGO;

    void getGameObject(GameObject g)
    {
        this.segmentGO = g;
    }


    public void changeStateToINSTALLED()
    {
        this.segmentGO.SendMessage("changeState", SegmentState.STATE.INSTALLED);
    }

    public void changeStateToUNINSTALLED()
    {
        this.segmentGO.SendMessage("changeState", SegmentState.STATE.UNINSTALLED);
    }

    public void changeStateToPREPARED_TO_INSTALL()
    {
        this.segmentGO.SendMessage("changeState", SegmentState.STATE.PREPARED_TO_INSTALL);
    }
}
