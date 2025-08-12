using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutoUITriggerBase : MonoBehaviour
{
    [SerializeField] protected GameObject uiPrefab;
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(StringNameSpace.Tags.Player)) return;
        ShowUI();
    }
    protected abstract void ShowUI();
}
