using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SSManager : MonoBehaviour {

    private Transform flyingObjectsContainer;
    private SSMedalProgressManager medalProgressManager;
    private Transform[] coins;
    private Transform[] sacks;

    private int trueSackIndex;
    private bool chooseEnabled;
    private int swapCount;
    private int currentSwapCount;

    private void GetChildren () {
        flyingObjectsContainer = transform.Find("FlyingObjectsContainer");
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<SSMedalProgressManager>();
        Transform sackContainer = transform.Find("SackContainer");
        coins = new Transform[sackContainer.childCount];
        sacks = new Transform[sackContainer.childCount];
        for (int i = 0 ; i < sackContainer.childCount ; i++) {
            coins[i] = sackContainer.GetChild(i).GetChild(0);
            sacks[i] = sackContainer.GetChild(i).GetChild(1);
        }
    }

    void Awake () {
        GetChildren();

        medalProgressManager.SetDifficulty(new int[] { 3, 4, 4, 4 }, new int[] { 0, 0, 0, 0 });
        medalProgressManager.gameObject.SetActive(true);
        transform.Find("LifeCount").gameObject.SetActive(true);
    }

    void Start () {
        PrepareNewRound();
        Invoke("StartNewRound", 1f);
    }

    private void StartNewRound () {
        StartCoroutine(StartRound());
    }

    private void PrepareNewRound () {
        int sackCount = 3;
        trueSackIndex = Random.Range(0, sackCount);
        chooseEnabled = false;
    }

    private IEnumerator StartRound () {
        yield return StartCoroutine(ShowSack(trueSackIndex, 0.5f));
        SwapSacks();
    }

    private void SwapEnded () {
        currentSwapCount++;
        if (currentSwapCount < swapCount) {
            SwapSacks();
        } else {
            chooseEnabled = true;
        }
    }

    private void SwapSacks () {
        float swapTime;
        int[] indexes = new int[sacks.Length];
        for (int i = 0 ; i < indexes.Length ; i++) {
            indexes[i] = i;
        }
        RandomizeIntArray(indexes);

        switch (medalProgressManager.Difficulty().currentMedal) {
            case SSDifficulty.Medal.None:
                swapTime = 0.3f;
                swapCount = Random.Range(4, 6);
                break;
            case SSDifficulty.Medal.Bronze:
                swapTime = 0.25f;
                swapCount = Random.Range(6, 9);
                break;
            case SSDifficulty.Medal.Silver:
                swapTime = 0.2f;
                swapCount = Random.Range(7, 11);
                break;
            case SSDifficulty.Medal.Gold:
            case SSDifficulty.Medal.Platinium:
            default:
                swapTime = 0.15f;
                swapCount = Random.Range(9, 13);
                break;
        }
        StartCoroutine(SwapSacks(indexes[0], indexes[1], swapTime));
    }

    private IEnumerator SwapSacks (int index1, int index2, float time) {
        Transform sack1 = sacks[index1].parent;
        Transform sack2 = sacks[index2].parent;
        Vector2 position1 = sack1.position;
        Vector2 position2 = sack2.position;

        StartCoroutine(MoveToPosition(sack1, position2, time));
        StartCoroutine(MoveToPosition(sack2, position1, time));
        yield return new WaitForSeconds(time);
        SwapEnded();

    }

    public void CheckSack (int currentSackIndex) {
        if (chooseEnabled) {
            chooseEnabled = false;
            StartCoroutine(SackChosen(currentSackIndex));
        }
    }

    private IEnumerator SackChosen (int currentSackIndex) {
        StartCoroutine(ShowSack(currentSackIndex, 0.5f));
        if (currentSackIndex == trueSackIndex) {
            yield return new WaitForSeconds(1.5f);
            medalProgressManager.Scored();
        } else {
            StartCoroutine(ShowSack(trueSackIndex, 0.5f));
            yield return new WaitForSeconds(1.5f);
            medalProgressManager.ReduceLife();
        }
    }

    private IEnumerator ShowSack (int sackIndex, float showTime) {
        Transform sackTransform = sacks[sackIndex];
        GameObject trueCoinObject = coins[trueSackIndex].gameObject;
        trueCoinObject.SetActive(true);
        StartCoroutine(MoveToPosition(sackTransform,
        new Vector2(sackTransform.position.x, sackTransform.position.y + 50), 0.5f));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(showTime);
        StartCoroutine(MoveToPosition(sackTransform,
            new Vector2(sackTransform.position.x, sackTransform.position.y - 50), 0.5f));
        yield return new WaitForSeconds(0.5f);
        trueCoinObject.SetActive(false);
    }

    public IEnumerator MoveToPosition (Transform transform, Vector2 position, float timeToMove) {
        Vector2 currentPos = transform.position;
        float t = 0f;
        while (t < 1) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(currentPos, position, t);
            yield return null;
        }
    }

    private void RandomizeIntArray (int[] arr) {
        for (var i = arr.Length - 1 ; i > 0 ; i--) {
            var r = Random.Range(0, i);
            var tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
