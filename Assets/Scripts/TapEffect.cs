using UnityEngine;

public class TapEffect : MonoBehaviour
{
    [SerializeField]　ParticleSystem tapEffect;
    [SerializeField]　Camera _camera;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // z方向に値を足して、カメラより少し奥に描画する。
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Play();
        }
    }
}