using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelect : MonoBehaviour
{
    public SelectedCellText LabelScript { get; set; }

    [SerializeField]
    private List<MeshRenderer> _rendererList = new();
    [SerializeField]
    private Material _selectedMaterial;

    private List<Material> _defaultMaterialList;
    private string _label;
    private int _enterCount = 0;

    private void OnMouseEnter()
    {
        _enterCount++;
        LabelScript.ChangeSelectedCount(1);
        LabelScript.DisplayText(_label);
        SetAllMaterials(_selectedMaterial, _enterCount == 1);
    }

    private void OnMouseExit()
    {
        LabelScript.ChangeSelectedCount(-1);
        SetAllMaterialsToDefault();
    }

    public void SetLabel(string label)
    {
        _label = label;
    }

    private void SetAllMaterialsToDefault()
    {
        if (_defaultMaterialList.Count != _rendererList.Count)
        {
            throw new Exception("defaultMaterialList is not full");
        }
        for (var i = 0; i < _rendererList.Count; i++)
        {
            SetMaterial(i, _defaultMaterialList[i]);
        }
    }

    private void SetAllMaterials(Material material, bool updateDefaultMaterials = false)
    {
        if (updateDefaultMaterials)
        {
            _defaultMaterialList = new List<Material>();
        }
        
        for (var i = 0; i < _rendererList.Count; i++)
        {
            if (updateDefaultMaterials)
            {
                _defaultMaterialList.Add(GetMaterial(i));
            }

            SetMaterial(i, material);
        }
    }

    private void SetMaterial(int meshIndex, Material material)
    {
        _rendererList[meshIndex].materials = new [] { material };
    }

    private Material GetMaterial(int meshIndex) =>
        _rendererList[meshIndex].materials[0];
}
