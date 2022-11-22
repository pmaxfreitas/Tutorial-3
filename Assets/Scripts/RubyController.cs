using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; } }
    public int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    public float speed = 4.0f;

    Rigidbody2D rigidbody2D;

    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    AudioSource audioSource;
    AudioSource bkgMusic;
    public AudioClip victory;
    public AudioClip loss;
    public AudioClip throwSound;
    public AudioClip hitSound;

    public AudioClip temp;

    public GameObject healthUpPrefab;
    public GameObject healthDownPrefab;

    public Text score;
    public Text gameOver;
    int scoreValue = 0;
    bool gameOverBool = false;

    public Text cogs;
    int cogsValue = 4;

    public static int level;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        bkgMusic = GameObject.Find("BkgMusic").GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(scoreValue == 6)
                {
                    SceneManager.LoadScene("StageTwo");
                    level = 2;
                }
                else if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if(gameOverBool == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            PlaySound(hitSound);

            GameObject healthDownObject = Instantiate(healthDownPrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        }

        if(amount > 0)
        {
            GameObject healthUpObject = Instantiate(healthUpPrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if(currentHealth <= 0)
        {
            gameOver.text = "You Lose!\n\nPress R to Restart";
            gameOverBool = true;
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            speed = 0;

            ChangeMusic(loss);
        }
    }

    void Launch()
    {
        if(cogsValue > 0)
        {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwSound);

        cogsValue--;
        cogs.text = "Cogs: " + cogsValue.ToString();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void ChangeScore(int scoreAmount)
    {
        if(scoreAmount > 0)
        {
            scoreValue = scoreValue + scoreAmount;
            score.text = "Robots Fixed: " + scoreValue.ToString() + "/6";
        }

        if(scoreValue == 6)
        {
            gameOver.text = "Talk to Jambi to visit stage two!";
        }

        if(scoreValue == 4 & level == 2)
        {
            gameOver.text = "You Win!\nCreated by Max Freitas\n\nPress R to Restart";

            ChangeMusic(victory);
        }
    }

    public void ChangeMusic(AudioClip music)
    {
        bkgMusic.Stop();
        bkgMusic.volume = 1;
        bkgMusic.clip = music;
        bkgMusic.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Cogs")
        {
            cogsValue += 4;
            Destroy(other);
        }
    }
}
