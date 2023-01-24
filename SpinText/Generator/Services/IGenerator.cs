using SpinText.Blocks.DB;

namespace SpinText.Generator.Services;

public interface IGenerator
{
    IEnumerable<string> GenerateHT(/* TODO: STVars */ object data, IEnumerable<BlockData> blocks);
}

public class Generator : IGenerator
{
    public IEnumerable<string> GenerateHT(object data, IEnumerable<BlockData> blocks) => throw new NotImplementedException();
}
