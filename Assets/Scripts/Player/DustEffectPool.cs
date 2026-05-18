using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectPool : MonoBehaviour
{
    [SerializeField] GameObject dustPrefab;
    [SerializeField] int poolSize = 5;

    List<GameObject> pool;

    void Awake()
    {
        InitializePool();
    }

    void OnEnable()
    {
        PlayerMovement.OnLandingEvent += OnPlayerLanded;
    }

    void OnDisable()
    {
        PlayerMovement.OnLandingEvent -= OnPlayerLanded;
    }

    void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(dustPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void OnPlayerLanded(float _, Vector2 position)
    {
        GameObject dust = GetAvailableFromPool();
        if (dust == null) return;

        dust.transform.position = position;
        dust.SetActive(true);
        StartCoroutine(DisableAfterDelay(dust));
    }

    GameObject GetAvailableFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) return obj;
        }
        return null;
    }

    IEnumerator DisableAfterDelay(GameObject dust)
    {
        yield return new WaitForSecondsRealtime(1f);
        dust.SetActive(false);
        dust.transform.position = transform.position;
    }
}