using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace _Tween.Scripts.Examples.DOTween
{
    [RequireComponent(typeof(Renderer))]
    internal class TweenCubeColor : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Color _endColor;


        [ContextMenu(nameof(Play))]
        public void Play()
        {
            Material material = GetComponent<Renderer>().material;
            material.DOColor(_endColor, _duration);
        }
        
    }
}
