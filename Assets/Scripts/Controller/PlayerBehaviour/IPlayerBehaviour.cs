public interface IPlayerBehaviour
{
    IPlayerBehaviour UpdateBehaviour(IInputComponent input);
    void SetNextBehaviour(IPlayerBehaviour nextBehaviour);
    IPlayerBehaviour GetNextBehaviour();

    bool ReceiveDamageOnHit();
}