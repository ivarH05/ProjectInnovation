/*using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(Move))]
public class MoveDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get all our child properties
        SerializedProperty nameProp = property.FindPropertyRelative("name");
        SerializedProperty spriteProp = property.FindPropertyRelative("sprite");
        SerializedProperty moveTypeProp = property.FindPropertyRelative("moveType");
        SerializedProperty controlTypeProp = property.FindPropertyRelative("controlType");
        SerializedProperty aerialProp = property.FindPropertyRelative("aerial");
        SerializedProperty groundProp = property.FindPropertyRelative("ground");
        SerializedProperty knockedMoveProp = property.FindPropertyRelative("knockedMove");
        SerializedProperty preperationFramesProp = property.FindPropertyRelative("preperationFrames");
        SerializedProperty executionFramesProp = property.FindPropertyRelative("executionFrames");
        SerializedProperty recoveryFramesProp = property.FindPropertyRelative("recoveryFrames");
        SerializedProperty baseDamageProp = property.FindPropertyRelative("baseDamage");
        SerializedProperty onGetBlockedModifierProp = property.FindPropertyRelative("onGetBlockedModifier");
        SerializedProperty onDealDamageStunProp = property.FindPropertyRelative("onDealDamageStun");
        SerializedProperty hitboxProp = property.FindPropertyRelative("hitboxes");
        SerializedProperty fullBodyProp = property.FindPropertyRelative("fullBody");
        SerializedProperty moveHeightProp = property.FindPropertyRelative("moveHeight");
        SerializedProperty moveBehaviourProp = property.FindPropertyRelative("moveBehaviour");

        string title = string.IsNullOrEmpty(nameProp.stringValue) ? "New Move" : nameProp.stringValue;
        label = new GUIContent(title);

        // Begin drawing the property
        EditorGUI.BeginProperty(position, label, property);

        // Setup layout variables
        float y = position.y;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;
        Rect rect;

        // --- General Section ---
        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.LabelField(rect, "General", EditorStyles.boldLabel);
        y += lineHeight + spacing * 2;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, nameProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, spriteProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, moveTypeProp);
        y += lineHeight + spacing;

        // --- Usability Section ---
        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.LabelField(rect, "Usability", EditorStyles.boldLabel);
        y += lineHeight + spacing * 2;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, controlTypeProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, aerialProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, groundProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, knockedMoveProp);
        y += lineHeight + spacing;

        // --- Timing Section ---
        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.LabelField(rect, "Timing", EditorStyles.boldLabel);
        y += lineHeight + spacing * 2;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, preperationFramesProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, executionFramesProp);
        y += lineHeight + spacing;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, recoveryFramesProp);
        y += lineHeight + spacing;

        // Determine the current MoveType
        MoveType moveType = (MoveType)moveTypeProp.enumValueIndex;

        // --- Attack Options (for ATTACK and HYBRID) ---
        if (moveType == MoveType.ATTACK || moveType == MoveType.HYBRID)
        {
            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.LabelField(rect, "Attack Options", EditorStyles.boldLabel);
            y += lineHeight + spacing * 2;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(rect, baseDamageProp);
            y += lineHeight + spacing;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(rect, onGetBlockedModifierProp);
            y += lineHeight + spacing;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(rect, onDealDamageStunProp);
            y += lineHeight + spacing;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(position, property, label, true);

            y += EditorGUI.GetPropertyHeight(hitboxProp, true) + spacing;

        }

        // --- Block Options (for BLOCK and HYBRID) ---
        if (moveType == MoveType.BLOCK || moveType == MoveType.HYBRID)
        {
            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.LabelField(rect, "Block Options", EditorStyles.boldLabel);
            y += lineHeight + spacing * 2;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(rect, fullBodyProp);
            y += lineHeight + spacing;
        }

        // --- Shared Options (for ATTACK, BLOCK and HYBRID) ---
        if (moveType == MoveType.ATTACK || moveType == MoveType.BLOCK || moveType == MoveType.HYBRID)
        {
            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.LabelField(rect, "Attack and Block", EditorStyles.boldLabel);
            y += lineHeight + spacing * 2;

            rect = new Rect(position.x, y, position.width, lineHeight);
            EditorGUI.PropertyField(rect, moveHeightProp);
            y += lineHeight + spacing;
        }

        // --- Behaviour (Always Shown) ---
        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.LabelField(rect, "Behaviour", EditorStyles.boldLabel);
        y += lineHeight + spacing * 2;

        rect = new Rect(position.x, y, position.width, lineHeight);
        EditorGUI.PropertyField(rect, moveBehaviourProp);
        y += lineHeight + spacing;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0f;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;
        int lines = 0;

        // Always visible sections:
        // General: header + 3 fields
        lines += 1 + 3;
        // Usability: header + 4 fields
        lines += 1 + 4;
        // Timing: header + 3 fields
        lines += 1 + 3;

        // Conditional sections:
        SerializedProperty moveTypeProp = property.FindPropertyRelative("moveType");
        MoveType moveType = (MoveType)moveTypeProp.enumValueIndex;
        if (moveType == MoveType.ATTACK || moveType == MoveType.HYBRID)
        {
            // Attack Options: header + 4 fields
            lines += 1 + 4;
            SerializedProperty hitboxProp = property.FindPropertyRelative("hitboxes");
            height += EditorGUI.GetPropertyHeight(hitboxProp, true) + spacing;
        }
        if (moveType == MoveType.BLOCK || moveType == MoveType.HYBRID)
        {
            // Block Options: header + 1 field
            lines += 1 + 1;
        }
        if (moveType == MoveType.ATTACK || moveType == MoveType.BLOCK || moveType == MoveType.HYBRID)
        {
            // Attack and Block: header + 1 field
            lines += 1 + 1;
        }
        // Behaviour: header + 1 field
        lines += 1 + 1;

        height += lines * lineHeight + lines * spacing + 10;
        return height;
    }
}
*/