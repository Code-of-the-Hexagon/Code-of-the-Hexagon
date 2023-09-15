using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelect : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField]
    private Material _defaultMaterial;
    [SerializeField]
    private Material _selectedMaterial;
    public SelectedCellText LabelScript { get; set; }

    private string _label;

    public void OnEnable()
    {
        SetMaterial(_defaultMaterial);
    }

    private void OnMouseEnter()
    {
        LabelScript.ChangeSelectedCount(1);
        LabelScript.DisplayText(_label);
        SetMaterial(_selectedMaterial);
    }

    private void OnMouseExit()
    {
        LabelScript.ChangeSelectedCount(-1);
        SetMaterial(_defaultMaterial);
    }

    public void SetLabel(string label)
    {
        _label = label;
    }

    private void SetMaterial(Material material)
    {
        var materials = _renderer.materials;
        materials[0] = material;
        _renderer.materials = materials;
    }
}
