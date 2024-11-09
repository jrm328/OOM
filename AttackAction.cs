using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackAction : AIAction
{
    public override void TackeAction()
    {
        aiMovementData.Direction = Vector2.zero;
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Move(aiMovementData.Direction, aiMovementData.PointOfInterest);
        aiActionData.Attack = true;
        enemyBrain.Attack();

    }
}
