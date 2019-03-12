using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolingTestScript : MonoBehaviour
{
    const string PoolTestKey = "PoolTestKey";
    List<Poolable> instances = new List<Poolable>();
    [SerializeField] GameObject prefab;

    private void Start()
    {
        if (PoolDataController.AddKey(PoolTestKey, prefab, 10, 15))
        {
            Debug.Log(string.Format("Creating pool with key: {0}", PoolTestKey));
        }
        else
        {
            Debug.Log("Pool already made.");
        }
    }

    private void OnDestroy()
    {
        //Don't put ReleaseInstances() here!!!
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Scene 1"))
            ChangeLevel("PoolTestScene1");
        if (GUI.Button(new Rect(10, 50, 100, 30), "Scene 2"))
            ChangeLevel("PoolTestScene2");
        if (GUI.Button(new Rect(10, 90, 100, 30), "Dequeue"))
        {
            Poolable obj = PoolDataController.Dequeue(PoolTestKey);
            float x = Random.Range(-10, 10);
            float y = Random.Range(0, 5);
            float z = Random.Range(0, 10);
            obj.transform.localPosition = new Vector3(x, y, z);
            obj.gameObject.SetActive(true);
            instances.Add(obj);
        }
        if (GUI.Button(new Rect(10, 130, 100, 30), "Enqueue"))
        {
            if (instances.Count > 0)
            {
                Poolable obj = instances[0];
                instances.RemoveAt(0);
                PoolDataController.Enqueue(obj);
            }
        }
    }

    void ChangeLevel(string level)
    {
        ReleaseInstances();
        SceneManager.LoadScene(level);
    }

    void ReleaseInstances()
    {
        for (int i = instances.Count - 1; i >= 0; i--)
        {
            PoolDataController.Enqueue(instances[i]);
        }
        instances.Clear();
    }
}
