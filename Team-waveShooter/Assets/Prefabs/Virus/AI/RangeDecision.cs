using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Range")]
public class RangeDecision : Decision
{
  public bool inRange;

  public override bool Decide(StateController controller)
  {
    return RangeCheck(controller);
  }

  private bool RangeCheck(StateController controller)
  {
    return inRange;
  }

  void OnColliderEnter(Collider other)
  {
    if(other.tag == "Player")
      inRange = true;
  }
}
