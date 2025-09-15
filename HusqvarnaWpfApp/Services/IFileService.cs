
namespace HusqvarnaTest.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Reads the content of a JSON file.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON content into.</typeparam>
        /// <param name="filePath">The path to the JSON file to read.</param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> if deserialization succeeds; 
        /// otherwise null (or the default value for the type).
        /// </returns>
        public T? GetJsonFileData<T>(string filePath);
    }
}
