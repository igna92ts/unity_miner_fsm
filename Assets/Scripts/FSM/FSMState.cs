using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState {
    protected Dictionary<string, string> transitionMap = new Dictionary<string, string>();
    protected string stateID;
    public string ID { get { return stateID; } }
    public void AddTransition(string trans, string id)
    {
        // Check if anyone of the args is invalid
        if (trans == FSM.nullTransition) {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
            return;
        }
        if (id == FSM.nullState) {
            Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
            return;
        }
        if (transitionMap.ContainsKey(trans)) {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() + "Impossible to assign to another state");
            return;
        }
        transitionMap.Add(trans, id);
    }

    public string GetOutputState(string trans) {
        // Check if the map has this transition
        if (transitionMap.ContainsKey(trans))
        {
            return transitionMap[trans];
        }
        return FSM.nullState;
    }
 
    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { } 
    public abstract void Condition(GameObject owner);
    public abstract void Execute(GameObject owner);
}
