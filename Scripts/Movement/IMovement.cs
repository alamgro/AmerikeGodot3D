public interface IMovement
{
    float MoveSpeed { get; set; }
    float JumpForce { get; set; }
    void TranslateXZ(float deltaTime);
    void Jump();
}
