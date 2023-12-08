using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour, IUnits
{
    private bool isIdle = true;
    public float moveSpeed = 2f; // Movement speed.
    public float miningDuration = 2f; // Time spent "mining" at the rock, adjustable.
    private Coroutine currentMoveCoroutine; // Keep track of the current moving coroutine.

    public bool IsIdle()
    {
        return isIdle;
    }

    public void MoveTo(Vector3 position, float stopDistance, System.Action onArrivedAtPosition)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(MoveToRoutine(position, stopDistance, onArrivedAtPosition));
    }

    private IEnumerator MoveToRoutine(Vector3 position, float stopDistance, System.Action onArrivedAtPosition)
    {
        isIdle = false;

        while (Vector3.Distance(transform.position, position) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isIdle = true;
        onArrivedAtPosition?.Invoke();
    }

    public void PlayAnimationMine(Vector3 lookAtPosition, System.Action onAnimationCompleted)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(PlayAnimationMineRoutine(lookAtPosition, onAnimationCompleted));
    }

    private IEnumerator PlayAnimationMineRoutine(Vector3 lookAtPosition, System.Action onAnimationCompleted)
    {
        isIdle = false;

        // Optionally look at the mining position
        transform.LookAt(new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z));

        yield return new WaitForSeconds(miningDuration);

        isIdle = true;
        onAnimationCompleted?.Invoke();
    }
}
