namespace rod.Commons.System.Collections
{
    /// <summary>
    /// Interface for an object which can be identified by SimplifiedKey
    /// </summary>
    /// <typeparam name="V">Value type for simlified key</typeparam>
    /// <typeparam name="K">Simplified key type</typeparam>
    public interface SimplifiedKeyIdentifiable<V, K>
        where V: struct
        where K: SimplifiedKey<V>
    {
        K Key { get; }
    }
}