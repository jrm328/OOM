using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

public class BenchmarkManager : MonoBehaviour
{
    public float reportInterval = 60.0f;  // Time in seconds between each report
    public bool autoGenerateReport = true;

    private int frameCount = 0;
    private float fpsSum = 0f;
    private float averageFPS;
    private float memoryUsage;
    private float cpuTime;

    private void Start()
    {
        // Start coroutine to auto-generate reports at specified intervals
        if (autoGenerateReport)
        {
            StartCoroutine(AutoGenerateReport());
        }
    }

    private void Update()
    {
        // Collect benchmark data every frame
        float deltaTime = Time.unscaledDeltaTime;
        frameCount++;
        fpsSum += 1.0f / deltaTime;

        // Optional: Add CPU and memory data collection here
        memoryUsage = System.GC.GetTotalMemory(false) / (1024 * 1024);  // Memory usage in MB
        cpuTime = deltaTime;  // Approximate CPU time per frame
    }

    private IEnumerator AutoGenerateReport()
    {
        while (autoGenerateReport)
        {
            yield return new WaitForSeconds(reportInterval);

            // Generate and save the benchmark report
            GenerateReport();
        }
    }

    public async void GenerateReport()
    {
        // Calculate average FPS
        if (frameCount > 0)
        {
            averageFPS = fpsSum / frameCount;
        }

        // Create the report
        string report = $"Benchmark Report\n" +
                        $"-----------------\n" +
                        $"Average FPS: {averageFPS:F2}\n" +
                        $"Memory Usage: {memoryUsage:F2} MB\n" +
                        $"CPU Time: {cpuTime * 1000:F2} ms/frame\n" +
                        $"Device Model: {SystemInfo.deviceModel}\n" +
                        $"Device Type: {SystemInfo.deviceType}\n" +
                        $"Graphics Device: {SystemInfo.graphicsDeviceName}\n" +
                        $"Operating System: {SystemInfo.operatingSystem}\n" +
                        $"Game Version: {Application.version}\n" +
                        $"Timestamp: {System.DateTime.Now}\n";

        // Generate file path and save the report asynchronously
        string path = Application.dataPath + "/BenchmarkReport_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
        await SaveReportAsync(path, report);

        Debug.Log($"Benchmark report saved to {path}");

        // Reset counters for the next report
        fpsSum = 0f;
        frameCount = 0;
    }

    private async Task SaveReportAsync(string path, string report)
    {
        try
        {
            await File.WriteAllTextAsync(path, report);
        }
        catch (IOException ex)
        {
            Debug.LogError($"Failed to save report: {ex.Message}");
        }
    }
}
