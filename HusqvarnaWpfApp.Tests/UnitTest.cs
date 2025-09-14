using HusqvarnaTest.Services;

namespace Tests
{
    public class UnitTest
    {
        private static string _filePath = "test.json";

        [Fact]
        public void MonitorFileServiceTest()
        {
            //Create new empty test file
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            File.WriteAllText(_filePath, string.Empty);

            var fileService = new FileService();
            var monitorFileService = new MonitorFileService(_filePath, fileService);

            //File has not changed
            (bool fileHasChanged, DateTime writeTime) = monitorFileService.FileHasChanged();
            Assert.False(fileHasChanged);

            //File has changed
            File.WriteAllText(_filePath, "null");
            (fileHasChanged, writeTime) = monitorFileService.FileHasChanged();
            Assert.True(fileHasChanged);

            File.Delete(_filePath);
        }
    }
}