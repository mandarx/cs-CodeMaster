namespace Game.Extensions;

public static class EnumerableExtensions {
    public static void ForEach<T> (this IEnumerable<T> that, Action<T> action) {
        foreach(var item in that)
            action(item);
    }
}