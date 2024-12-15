using System.Collections;

namespace WebApi.Tests.InlineData;

public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "en" };
        yield return new object[] { "pt-BR" };
        yield return new object[] { "fr" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
