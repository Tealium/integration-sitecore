namespace Tealium.Sitecore.TagManagement.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITealiumMappingsProvider : IRefreshable
    {
        /// <summary>
        /// Gets the mapping.
        /// </summary>
        /// <value>
        /// The mapping.
        /// </value>
        IMapping Mapping { get; }
    }
}
