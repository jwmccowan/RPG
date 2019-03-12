using UnityEngine;
using UnityEditor;

public class AssetCreator : MonoBehaviour
{
    [MenuItem("Assets/Create/Converation Data")]
    public static void CreateConversationData()
    {
        ScriptableObjectUtility.CreateAsset<ConversationData>();
    }
}
