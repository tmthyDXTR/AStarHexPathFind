using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SysHelper
{
    public static IEnumerator WaitForAndExecute(float waitTime, Action methodToExecute)
    {
        Debug.Log("Execute: " + methodToExecute + " in " + waitTime + " seconds");
        yield return new WaitForSeconds(waitTime);
        methodToExecute();
        Debug.Log("Executed: " + methodToExecute);
    }

}
