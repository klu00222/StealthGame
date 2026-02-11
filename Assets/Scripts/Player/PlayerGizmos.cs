using UnityEditor;
using UnityEngine;

public class PlayerGizmos : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        EnemyData[] AllEnemies = FindObjectsByType<EnemyData>(FindObjectsSortMode.None);

        for (int i = 0; i < AllEnemies.Length; i++)
        {
            EnemyData Enemy = AllEnemies[i];
            Gizmos.DrawLine(Enemy.transform.position, transform.position);

            Vector3 LabelPosition = (Enemy.transform.position + transform.position) / 2;

            float DistanceEnemyPlayer = Vector2.Distance(Enemy.transform.position, transform.position);

            GUIStyle ColorLabel = new GUIStyle();
            ColorLabel.normal.textColor = Color.red;

            Handles.Label(LabelPosition, $"Distancia: {DistanceEnemyPlayer:F0} m", ColorLabel);
        }
    }
}
