namespace Repository.Dataloader.DataUnit
{
    public interface IDataUnit
    {
        void Run(bool clearExisting = true);
    }
}
