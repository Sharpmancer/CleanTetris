namespace Features.Persistence.App
{
    public interface ISaveDataAssembleStrategy : ISaveDataAssemblyTypeProvider
    {
        object AssembleSaveData();
        void DisassembleSaveData(object saveData);
    }
}