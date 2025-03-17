using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonSystem))]
public class DungeonSaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Label("데이터 초기화");
        if(GUILayout.Button("초기화"))
        {
            ((DungeonSystem)target).SaveData(StageType.Stage1);
        }

        if(GUILayout.Button("2스테이지"))
        {
            ((DungeonSystem)target).SaveData(StageType.Stage2);
        }

        if (GUILayout.Button("3스테이지"))
        {
            ((DungeonSystem)target).SaveData(StageType.Stage3);
        }
    }

}
