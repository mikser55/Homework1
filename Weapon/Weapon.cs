class Weapon
{
    private int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        _damage = damage;
        _bullets = bullets;
    }

    public void Fire(Player player)
    {
        player.TakeDamage(_damage);
        _bullets--;
    }
}

class Player
{
    private int _health;

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health -= damage;

            if (_health < 0)
                _health = 0;
        }
    }
}

class Bot
{
    private Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}