using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject gridLobbyRoom;      // NEW: Lobby grid
    public GameObject gridRoomOne;
    public GameObject gridRoomTwo;
    public GameObject gridRoomThree;
    public GameObject gridBossRoom;

    public GameObject lobbyEnemies;       // Optional: Lobby enemies
    public GameObject room1Enemies;
    public GameObject room2Enemies;
    public GameObject room3Enemies;
    public GameObject bossRoomEnemies;

    public Transform player;
    public PlayerResources playerResources;

    // Room spawn positions: 0 = Lobby, 1 = Room1, 2 = Room2, 3 = Room3, 4 = Boss
    private readonly Vector3[] roomSpawnPositions = new Vector3[]
    {
        new Vector3(-10f, 3.12f, -0.07674628f),   // Lobby (adjust as needed)
        new Vector3(-0.87f, 3.12f, -0.07674628f), // Room 1
        new Vector3(19.08f, 30.01f, -0.07674628f),// Room 2
        new Vector3(18.7f, 30f, -0.07674628f),    // Room 3
        new Vector3(47.87f, 20f, -0.07674628f)    // Boss Room
    };

    // Gaki kill requirements per room (Room 1, 2, 3)
    private readonly int[] gakiKillsRequired = new int[] { 6, 10, 15 };

    private int currentRoom = 0; // 0: Lobby, 1: Room1, 2: Room2, 3: Room3, 4: Boss
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

    void Update()
    {
        // Debug: Press G to add 1 Gaki kill
        if (Input.GetKeyDown(KeyCode.G))
        {
            gakiKills += 1;
            Debug.Log($"[DEBUG] Added 6 Gaki kills. Current: {gakiKills}");
            if (currentRoom > 0 && currentRoom < gakiKillsRequired.Length + 1 && gakiKills >= gakiKillsRequired[currentRoom - 1])
            {
                AdvanceRoom();
            }
        }

        // LOBBY EXIT LOGIC
        if (currentRoom == 0 && player != null && player.position.x > 20f)
        {
            AdvanceRoom(); // Move to Room 1
        }
    }

    public void RegisterGakiKill()
    {
        if (currentRoom > 0 && currentRoom < gakiKillsRequired.Length + 1)
        {
            gakiKills++;
            if (gakiKills >= gakiKillsRequired[currentRoom - 1])
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
        if (gridLobbyRoom != null) gridLobbyRoom.SetActive(false);
        gridRoomOne.SetActive(false);
        gridRoomTwo.SetActive(false);
        gridRoomThree.SetActive(false);
        gridBossRoom.SetActive(false);

        if (lobbyEnemies != null) lobbyEnemies.SetActive(false);
        room1Enemies.SetActive(false);
        room2Enemies.SetActive(false);
        room3Enemies.SetActive(false);
        bossRoomEnemies.SetActive(false);

        // Activate the next room and enemies
        switch (currentRoom)
        {
            case 1: // To Room 1
                gridRoomOne.SetActive(true);
                room1Enemies.SetActive(true);
                SetPlayerPosition(1);
                break;
            case 2: // To Room 2
                gridRoomTwo.SetActive(true);
                room2Enemies.SetActive(true);
                SetPlayerPosition(2);
                break;
            case 3: // To Room 3
                gridRoomThree.SetActive(true);
                room3Enemies.SetActive(true);
                SetPlayerPosition(3);
                break;
            case 4: // To Boss Room
                gridBossRoom.SetActive(true);
                bossRoomEnemies.SetActive(true);
                SetPlayerPosition(4);
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

            // Also update respawn position if PlayerDeath is present
            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.respawnPosition = roomSpawnPositions[roomIndex];
            }
        }
    }

    // Call this to initialize the lobby room (e.g. from Start)
    public void InitializeLobbyRoom()
    {
        currentRoom = 0;
        gakiKills = 0;

        if (gridLobbyRoom != null) gridLobbyRoom.SetActive(true);
        gridRoomOne.SetActive(false);
        gridRoomTwo.SetActive(false);
        gridRoomThree.SetActive(false);
        gridBossRoom.SetActive(false);

        if (lobbyEnemies != null) lobbyEnemies.SetActive(true);
        room1Enemies.SetActive(false);
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
