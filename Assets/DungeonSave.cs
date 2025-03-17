using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSave : MonoBehaviour, IEventTrigger
{
    [SerializeField] private StageType saveStageLevel;
    private DungeonSystem dungeonSystem;

    private bool completeSave;

    void Start()
    {
        completeSave = false;
        dungeonSystem = FindObjectOfType<DungeonSystem>();    
    }

    public void EventTrigger()
    {
        if (completeSave)
            return;

        dungeonSystem.SaveData(saveStageLevel);
        completeSave = true;
    }
}
