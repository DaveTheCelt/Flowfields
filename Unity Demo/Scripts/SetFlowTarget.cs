using UnityEngine;
namespace Flowfield.Demo
{
    public class SetFlowTarget : MonoBehaviour
    {
        [SerializeField]
        AnimationCurve _easing;
        SeekFlow _seeker;
        float _delta;
        Vector3 _target;
        Vector3 _start;

        private float LerpTime => _seeker.RecalculateRate / 4f;

        private void Awake()
        {
            _seeker = GetComponentInParent<SeekFlow>();
            _seeker.OnRefresh += ApplyTarget;
        }

        private void ApplyTarget()
        {
            _delta = 0;
            _start = transform.position;
            _target = _seeker.TargetPoint;
            enabled = true;
        }

        void Update()
        {
            _delta += Time.deltaTime;
            float t = _delta / LerpTime;
            t = _easing == null ? t : _easing.Evaluate(t);
            transform.position = Vector3.Lerp(_start, _target, t);
            enabled = _delta < LerpTime;
        }
    }
}