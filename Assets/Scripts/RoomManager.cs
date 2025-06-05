using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject gridRoomOne;
    public GameObject gridRoomTwo;
    public GameObject gridRoomThree;
    public GameObject gridBossRoom;

    public GameObject room1Enemies;
    public GameObject room2Enemies;
    public GameObject room3Enemies;
    public GameObject bossRoomEnemies;

    public Transform player;
    public PlayerResources playerResources;

    // Room spawn positions
    private readonly Vector3[] roomSpawnPositions = new Vector3[]
    {
        new Vector3(-0.87f, 3.12f, -0.07674628f),   // Room 1
        new Vector3(16.36f, 24.13f, -0.07674628f),  // Room 2
        new Vector3(18.7f, 25.27f, -0.07674628f),   // Room 3
        new Vector3(47.87f, 14.33f, -0.07674628f)   // Boss Room
    };

    // Gaki kill requirements per room
    private readonly int[] gakiKillsRequired = new int[] { 6, 10, 15 };

    private int currentRoom = 0; // 0: Room1, 1: Room2, 2: Room3, 3: Boss
    public int gakiKills = 0;

    private void Awake()
    {
        // Auto-get player and PlayerResources if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
        if (playerResources == null)
        {
            if (player != null)
                playerResources = player.GetComponent<PlayerResources>();
            else
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                    playerResources = playerObj.GetComponent<PlayerResources>();
            }
        }
    }

    public void RegisterGakiKill()
    {
        if (currentRoom < gakiKillsRequired.Length)
        {
            gakiKills++;
            if (gakiKills >= gakiKillsRequired[currentRoom])
            {
                AdvanceRoom();
            }
        }
    }

    private void AdvanceRoom()
    {
        gakiKills = 0;
        currentRoom++;

        // Deactivate all rooms/enemies
        gridRoomOne.SetActive(false);
        gridRoomTwo.SetActive(false);
        gridRoomThree.SetActive(false);
        gridBossRoom.SetActive(false);

        room1Enemies.SetActive(false);
        room2Enemies.SetActive(false);
        room3Enemies.SetActive(false);
        bossRoomEnemies.SetActive(false);

        // Activate the next room and enemies
        switch (currentRoom)
        {
            case 1: // To Room 2
                gridRoomTwo.SetActive(true);
                room2Enemies.SetActive(true);
                SetPlayerPosition(1);
                break;
            case 2: // To Room 3
                gridRoomThree.SetActive(true);
                room3Enemies.SetActive(true);
                SetPlayerPosition(2);
                break;
            case 3: // To Boss Room
                gridBossRoom.SetActive(true);
                bossRoomEnemies.SetActive(true);
                SetPlayerPosition(3);
                break;
            default:
                // Optionally handle end of game or loop
                break;
        }

        // Restore player health
        if (playerResources != null)
        {
            playerResources.health = playerResources.maxHealth;
            if (playerResources.healthBar != null)
                playerResources.healthBar.SetHealth(playerResources.maxHealth);
        }
    }

    private void SetPlayerPosition(int roomIndex)
    {
        if (player != null && roomIndex >= 0 && roomIndex < roomSpawnPositions.Length)
        {
            player.position = roomSpawnPositions[roomIndex];
        }
    }

    // Call this to initialize the first room (e.g. from Start)
    public void InitializeFirstRoom()
    {
        currentRoom = 0;
        gakiKills = 0;

        gridRoomOne.SetActive(true);
        gridRoomTwo.SetActive(false);
        gridRoomThree.SetActive(false);
        gridBossRoom.SetActive(false);

        room1Enemies.SetActive(true);
        room2Enemies.SetActive(false);
        room3Enemies.SetActive(false);
        bossRoomEnemies.SetActive(false);

        SetPlayerPosition(0);

        if (playerResources != null)
        {
            playerResources.health = playerResources.maxHealth;
            if (playerResources.healthBar != null)
                playerResources.healthBar.SetHealth(playerResources.maxHealth);
        }
    }
}
