using HusqvarnaTest.Services;

namespace Tests
{
    public class UnitTest
    {
        private static string _filePath = "test.json";

        [Fact]
        //TODO: Sometimes (for some reason) the test takes a little longer to run the first time I run it, if I run it a second time (and all times after that) its much faster and Im not sure why that is or how to solve it
        public void MonitorFileServiceTest()
        {
            //Create new empty test file
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            File.WriteAllText(_filePath, string.Empty);

            var monitorFileService = new MonitorFileService(_filePath);

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