using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestinationViewer : MonoBehaviour
{
    public QuestManager questManager;
    public AssignmentViewer assignmentViewer;
    public GameObject WorldMap;

    public void InitDestinationViewer()
    {
        WorldMap.SetActive(true);
    }

    public void CloseDestinationViewer()
    {
        WorldMap.SetActive(false);
    }

    public void OnDestinationClick()
    {

    }
}
