using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlphabetReceptors : MonoBehaviour
{
    GameObject _receptorObject;
    [SerializeField] List<GameObject> _receptorsObjects = new List<GameObject>();
    Vector3 _initialReceptorPosition;

    private void Awake()
    {
       /* _receptorObject = transform.Find("Receptor").gameObject;
        _receptorsObjects.Add(_receptorObject);
        _initialReceptorPosition = _receptorObject.transform.position;*/

        DataManager _currDataManager = DataManager.Instance;
    }
    private void Start()
    {
        GetReceptors();
    }

    public void InstantiateReceptors()
    {

    }

    public void GetReceptors()
    {
        var _receptors = GetComponentsInChildren<ReceptorItem>();

        for (int i = 0; i < _receptors.Length; i++)
        {
            _receptors[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = DataManager.Instance.Letters[i];
            _receptors[i].GetComponent<ReceptorItem>().ValueToReceive = DataManager.Instance.Letters[i].ToLower();
        }
    }
}
