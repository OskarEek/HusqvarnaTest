using HusqvarnaTest.Services;

namespace Tests
{
    public class UnitTest
    {
        private static string _filePath = "test.json";

        //TODO: The way I know I could make the test faster is by making the "FileHasChanged" method in the MonitorFileService an internal method and then exposing internals to the test project. By doing it this way
        //I could test the actual logic for checking if a file has changed or not without having to care about wating for the FileChanged event to trigger. But my understanding from the assignment PDF was that this wasnt the
        //expected way to solve this.
        [Fact]
        public async Task MonitorFileServiceTest()
        {
            //Create new empty test file
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            await File.WriteAllTextAsync(_filePath, string.Empty);

            var monitorFileService = new MonitorFileService(_filePath, TimeSpan.FromMilliseconds(1));
            var monitoredChange = false;
            monitorFileService.FileChanged += (s, e) =>
            {
                monitoredChange = true;
            };

            await Task.Delay(2);
            Assert.False(monitoredChange);

            await File.WriteAllTextAsync(_filePath, "null");

            await Task.Delay(2);
            Assert.True(monitoredChange);

            monitorFileService.Dispose();
            File.Delete(_filePath);
        }
    }
}