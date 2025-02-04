using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private GameObject _this;

   public void Delete()
    {
        Destroy(_this);
    }
}
