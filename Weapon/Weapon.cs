using System;

class Weapon
{
    private readonly int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        if (damage > 0 && bullets > 0)
        {
            _damage = damage;
            _bullets = bullets;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public void Fire(Player player)
    {
        if (player != null)
        {
            if (_bullets > 0)
            {
                player.TakeDamage(_damage);
                _bullets--;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        else
        {
            throw new ArgumentNullException();
        }
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
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}

class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        if (_weapon != null)
            _weapon = weapon;
        else
            throw new ArgumentNullException();
    }

    public void OnSeePlayer(Player player)
    {
        if (player != null)
            _weapon.Fire(player);
        else
            throw new ArgumentNullException();
    }
}