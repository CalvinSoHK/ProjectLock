using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.Enums;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mon.MonData
{

#if UNITY_EDITOR
    [CustomEditor(typeof(TypeRelationSO))]
    public class TypeRelationSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TypeRelationSO obj = (TypeRelationSO)target;

            if (obj.targetType1 != MonType.None)
            {
                obj.displayRelations = obj.GetRelationOf(obj.targetType1);
            }

            if (GUILayout.Button("Update Type Relation"))
            {
                obj.UpdateRelation();
            }

            if (GUILayout.Button("Update Dict"))
            {

                obj.UpdateDict();
            }
        }
    }
#endif
}
