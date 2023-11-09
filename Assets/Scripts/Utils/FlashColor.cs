using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color color = Color.red;
    public float duration = .3f;


    private Tween _currentTween;


    private void OnValidate()
    {

        spriteRenderers = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    {

        if (_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(item => { item.color = Color.white; });
        }

        foreach (var sprite in spriteRenderers)
        {
            _currentTween = sprite.DOColor(color, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

}
