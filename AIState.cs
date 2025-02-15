using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAIBrain enemyBrain = null;

    [SerializeField]
    private List<AIAction> actions = null;

    [SerializeField]
    private List<AITransition> transitions = null;

    private void Awake()
    {
        enemyBrain = transform.root.GetComponent<EnemyAIBrain>();
    }

    public void UpdateState()
    {
        foreach (var action in actions)
        {
            action.TackeAction();
        }
        foreach (var transition in transitions) 
        {
            bool results = false;
            foreach (var decision in transition.Decisions)
            {
                results = decision.MakeADecision();
                if (results == false)
                    break;
            }
            if (results)
            {
                if (transition.PositiveResult != null)
                {
                    enemyBrain.ChangeToState(transition.PositiveResult);
                    return;
                }
            }
            else
            {
                if (transition.NegativeResult != null)
                {
                    enemyBrain.ChangeToState(transition.NegativeResult);
                    return;
                }
            }
        }
    }
}
