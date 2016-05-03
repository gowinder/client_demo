using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(battle_field))]

public class battle_field_editor : Editor
{

    battle_field bf;
    GUIContent cont;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void OnInspectorGUI()
    {
        bf = (battle_field)target;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        cont = new GUIContent("start position x", "(0, 0)号六边形单边的心中点x");
        bf.start_pos_x = EditorGUILayout.FloatField(cont, bf.start_pos_x, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        cont = new GUIContent("start position x", "(0, 0)号六边形单边的心中点z");
        bf.start_pos_z = EditorGUILayout.FloatField(cont, bf.start_pos_z, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        cont = new GUIContent("tile side length", "六边形单边长度");
        bf.tile_side_len = EditorGUILayout.FloatField(cont, bf.tile_side_len, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        cont = new GUIContent("row count", "横向多少个六边形");
        bf.tile_count_x = EditorGUILayout.FloatField(cont, bf.tile_count_x, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        cont = new GUIContent("column count:", "纵向多少个六边形");
        bf.tile_count_z = EditorGUILayout.FloatField(cont, bf.tile_count_z, GUILayout.ExpandWidth(true));
        //~ gm.length=EditorGUILayout.FloatField(cont, gm.length, GUILayout.MinWidth(160));
        //~ gm.length=Mathf.Round(Mathf.Clamp(gm.length, 0, 50));
        //~ cont=new GUIContent("Actual:"+(gm.length*gm.gridSize).ToString("f2"), "after multiply the GridSize");
        //~ EditorGUILayout.LabelField(cont, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        cont = new GUIContent("Generate Grid", "generate hex grid");
        if (GUILayout.Button(cont)) 
            bf.generate_grid();

        cont = new GUIContent("remove all tile", "remove all hex grid");
        if (GUILayout.Button(cont))
            bf.clear_all_tile();
    }
}
