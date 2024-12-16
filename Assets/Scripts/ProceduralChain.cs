using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProceduralChain : MonoBehaviour
{
    [SerializeField] private float shootDuration;
    [SerializeField] private float chainOffsetFromPlayer = 1f;
    [SerializeField] private float chainOffsetFromPlayerY = 0.5f;
    internal bool didThrowed = false;
    public Transform player; // Player's transform
    public GameObject chainLinkPrefab; // Prefab for a single chain link (RigidBody with collider)
    public float chainLinkLength = 1f; // Length of each chain link
    private GameObject[] chainLinks; // Array to hold the chain links


    public void DetectHookshotCollision(Vector3 grabPoint, GameObject grabbedObject)
    {
        // Create chain links between the player and the hit point
        CreateChain(grabPoint);

        // Attach the first chain link to the player
        SpringJoint firstLinkJoint = chainLinks[0].GetComponent<SpringJoint>();
        firstLinkJoint.connectedBody = player.GetComponent<Rigidbody>(); // Attach to player

        // Attach the last chain link to the wall
        //SpringJoint lastLinkJoint = chainLinks[chainLinks.Length - 1].GetComponent<SpringJoint>();
        //lastLinkJoint.connectedAnchor = grabPoint; // Attach the last link to the wall
        Destroy(chainLinks[chainLinks.Length - 1].GetComponent<Collider>());

        //chainLinks[chainLinks.Length - 1].GetComponent<Rigidbody>().AddForce(transform.forward * 100f , ForceMode.Impulse);
        StartCoroutine(SimulateThrow(grabPoint));
    }


    void CreateChain(Vector3 targetPoint)
    {
        // Calculate the number of links required based on the distance between the player and the hit point
        Vector3 chainRotation = Vector3.zero;
        float distance = Vector3.Distance(player.position, targetPoint);
        int numberOfLinks = Mathf.CeilToInt(distance / chainLinkLength);

        chainLinks = new GameObject[numberOfLinks];

        // Create chain links and position them
        for (int i = 0; i < numberOfLinks; i++)
        {
            chainRotation += new Vector3(0, 90, 0);

            Vector3 calculatedOffsetZ = player.transform.forward * chainOffsetFromPlayer;
            Vector3 calculatedOffsetY = -player.transform.up * chainOffsetFromPlayerY;
            Vector3 calculatedOffset = calculatedOffsetZ + calculatedOffsetY;

            Vector3 bottomTarget = player.position + (Vector3.down * distance);

            Vector3 linkPosition = Vector3.Lerp(player.position + calculatedOffset, bottomTarget, (float)i / (numberOfLinks - 1));
            GameObject chainLink = Instantiate(chainLinkPrefab, linkPosition, Quaternion.Euler(chainRotation));

            //chainLink.transform.LookAt(targetPoint);

            // Add a hinge joint to allow bending
            SpringJoint joint = chainLink.GetComponent<SpringJoint>();   

            if (i > 0)
            {
                // Connect this link to the previous one
                joint.connectedBody = chainLinks[i - 1].GetComponent<Rigidbody>();
            }
            
            //joint.enablePreprocessing = false;

            // Store the chain link
            chainLinks[i] = chainLink;
        }

        chainLinks[chainLinks.Length - 1].GetComponent<Rigidbody>().isKinematic = true;
    }


    private IEnumerator SimulateThrow(Vector3 targetPos)
    {
        float elapsedTime = 0f;
        GameObject lastLink = chainLinks[chainLinks.Length - 1];

        while (elapsedTime < shootDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / shootDuration; // Zamana bağlı bir interpolasyon faktörü
            Vector3 newPosition;
            newPosition = Vector3.Lerp(lastLink.transform.position, targetPos, t);
            SetLastChainPosition(newPosition);

            yield return null; // Bir sonraki kareyi bekle
        }

        SetLastChainPosition(targetPos);
        didThrowed = true;
    }


    public void SetLastChainPosition(Vector3 grabPos)
    {
        GameObject lastLink = chainLinks[chainLinks.Length - 1];
        lastLink.transform.position = grabPos;
    }


    public void DestroyChain()
    {
        if(chainLinks == null) return;

        foreach (GameObject chain in chainLinks)
        {
            Destroy(chain);
        }

        StopAllCoroutines();
        didThrowed = false;
    }
}
