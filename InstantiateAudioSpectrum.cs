using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAudioSpectrum : MonoBehaviour
{
    [SerializeField] public GameObject _sampleSpherePrefab;
    [SerializeField] public float _maxScale;
    [SerializeField] public GameObject parentP;
    [SerializeField] public float circleRadius;
    [SerializeField] public float smoothDampValue;

    private GameObject[] _sampleSphere = new GameObject[64];
    private Vector3 velocity = Vector3.zero;
    private int v = 1;

   
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 64; i++)
        {
            GameObject _instanceSampleSphere = (GameObject)Instantiate(_sampleSpherePrefab, parentP.transform.position, parentP.transform.rotation);
            _instanceSampleSphere.transform.parent = this.transform;
            _instanceSampleSphere.name = "sampleSphere" + i;
            this.transform.eulerAngles = new Vector3(0, (-1.40625f * i) * 4, 0);
            _instanceSampleSphere.transform.position = parentP.transform.position;
            _sampleSphere[i] = _instanceSampleSphere;
            _sampleSphere[i].transform.position = new Vector3(_sampleSphere[i].transform.position.x, _sampleSphere[i].transform.position.y, _sampleSphere[i].transform.position.z + circleRadius);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 30.0f * Time.fixedDeltaTime * (AudioSpectrum._samples[1] * 20));
    }

    // Update is called once per frame
    void Update()
    {

        for (int i=0; i < 32; i++)
        {
            if (_sampleSphere != null)
            {
                _sampleSphere[i].transform.localScale = new Vector3(10, (AudioSpectrum._samples[i] / (2 + i / 15) * _maxScale) + 10, 10);
                
                _sampleSphere[i].transform.localPosition = new Vector3(_sampleSphere[i].transform.localPosition.x,Mathf.SmoothDamp(_sampleSphere[i].transform.localPosition.y, (AudioSpectrum._samples[i] * _maxScale * i / (2 + i / 15)), ref velocity.y, smoothDampValue), _sampleSphere[i].transform.localPosition.z);
            }
        }

        for (int i = 32; i >= 32 && i < 64; i++)
        {
            if (_sampleSphere != null)
            {
                
                _sampleSphere[i].transform.localScale = new Vector3(10, (AudioSpectrum._samples[i - (v + ((i - 32) * 2))] / (2 + (i - (v + ((i - 32) * 2))) / 15) * _maxScale) + 10, 10);

                _sampleSphere[i].transform.localPosition = new Vector3(_sampleSphere[i].transform.localPosition.x, Mathf.SmoothDamp(_sampleSphere[i].transform.localPosition.y, (AudioSpectrum._samples[i - (v + ((i - 32) * 2))] * _maxScale * (i - (v + ((i - 32) * 2))) / (2 + (i - (v + ((i - 32) * 2))) / 15)),ref velocity.y, smoothDampValue), _sampleSphere[i].transform.localPosition.z);

            }
        }

        
    }
}
