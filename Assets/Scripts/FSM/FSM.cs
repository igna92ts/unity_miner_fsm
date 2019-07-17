using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {
    public static string nullState = "null_state";
    public static string nullTransition = "null_transition";
    private List<FSMState> states;
 
    private string currentStateID;
    public string CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }
 
    public FSM()
    {
        states = new List<FSMState>();
    }
 
    public void AddState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }
 
        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }
 
        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() + 
                               " because state has already been added");
                return;
            }
        }
        states.Add(s);
    }
 
    public void PerformTransition(string trans)
    {
        if (trans == nullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }
 
        string id = currentState.GetOutputState(trans);
        Debug.Log("Got transition " + trans);
        Debug.Log("And output state is " + id);
        if (id == nullState)
        {
            Debug.LogError("FSM ERROR: State " + currentStateID.ToString() +  " does not have a target state " + 
                           " for transition " + trans.ToString());
            return;
        }
 
        currentStateID = id;
        bool found = false;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                found = true;
                currentState.DoBeforeLeaving();
                currentState = state;
                currentState.DoBeforeEntering();
                break;
            }
        }
        if (!found) {
            Debug.LogError("FSM ERROR: state " + currentStateID + " not found. Maybe you are missing the implementation");
        }
    }
}
