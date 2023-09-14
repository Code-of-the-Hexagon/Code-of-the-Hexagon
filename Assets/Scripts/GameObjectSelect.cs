using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelect : MonoBehaviour
{
    public MeshRenderer Renderer;
    public Material DefaultMaterial;
    public Material SelectedMaterial;
    public SelectedCellText LabelScript;

    private string _label;

    public void OnEnable()
    {
        SetMaterial(DefaultMaterial);
    }

    private void OnMouseEnter()
    {
        LabelScript.ChangeSelectedCount(1);
        LabelScript.DisplayText(_label);
        SetMaterial(SelectedMaterial);
    }

    private void OnMouseExit()
    {
        LabelScript.ChangeSelectedCount(-1);
        SetMaterial(DefaultMaterial);
    }

    private void SetMaterial(Material material)
    {
        var materials = Renderer.materials;
        materials[0] = material;
        Renderer.materials = materials;
    }

    public void SetLabel(string label)
    {
        _label = label;
    }
}
