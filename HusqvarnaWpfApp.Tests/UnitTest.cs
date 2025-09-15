using HusqvarnaTest.Services;

namespace Tests
{
    public class UnitTest
    {
        private static string s_filePath = "test.json";

        //TODO: The way I know I could make the test faster is by making the "FileHasChanged" method in the MonitorFileService an internal method and then exposing internals to the test project. By doing it this way
        //I could test the actual logic for checking if a file has changed or not without having to care about wating for the FileChanged event to trigger. But my understanding from the assignment PDF was that this wasnt the
        //expected way to solve this.
        [Fact]
        public async Task MonitorFileServiceTest()
        {
            //Create new empty test file
            if (File.Exists(s_filePath))
            {
                File.Delete(s_filePath);
            }
            await File.WriteAllTextAsync(s_filePath, string.Empty);

            var monitorFileService = new MonitorFileService(s_filePath, TimeSpan.FromMilliseconds(1));
            var monitoredChange = false;
            monitorFileService.FileChanged += (s, e) =>
            {
                monitoredChange = true;
            };

            await Task.Delay(2);
            Assert.False(monitoredChange);

            await File.WriteAllTextAsync(s_filePath, "null");

            await Task.Delay(2);
            Assert.True(monitoredChange);

            monitorFileService.Dispose();
            File.Delete(s_filePath);
        }
    }
}