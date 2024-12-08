public interface IDemagable{
    void takeDamage(int amount);
}

public interface IAttacker{
    void attack(IDemagable target);
}