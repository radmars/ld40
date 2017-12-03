using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdPulser : MonoBehaviour {

    [SerializeField]
    public GameObject[] bones;
    // Use this for initialization

    private float[] _sins;
    private float[] _rates;

    void Start () {

        _sins = new float[bones.Length];
        _rates = new float[bones.Length];

        for ( int i=0; i<_sins.Length; i++) {
            _sins[i] = Random.Range(0, Mathf.PI * 2f);
            _rates[i] = Random.Range(2f,5f);
        }
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;

        for (int i = 0; i < _sins.Length; i++) {
            _sins[i] += dt * _rates[i];
            if (_sins[i] >= Mathf.PI * 2) {
                _sins[i] -= Mathf.PI * 2;
            }

            float sy = 0.9f + Mathf.Sin(_sins[i])*0.1f;
            float sz = 0.9f + Mathf.Cos(_sins[i]) * 0.1f;
            bones[i].transform.localScale = new Vector3(1, sy, sz); 
        }
    }

}
