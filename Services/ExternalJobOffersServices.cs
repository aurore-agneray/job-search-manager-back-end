using System.Diagnostics;
using JobSearchManagerBackEnd.DTOs;

namespace JobSearchManagerBackEnd.Services;

/// <summary>
/// Defines all CRUD methods used to get job offers from external sources
/// </summary>
internal static class ExternalJobOffersServices
{
    /// <summary>
    /// Retrieves job offers from a Python script
    /// </summary>
    internal static IResult GetFromPythonScript()
    {
        List<JobOfferDTO> jobOffers = [];
        string[] offersData;

        try
        {
            // Chemin vers le script Python (relatif ou absolu)
            string scriptPath = @"get_job_offers_from_python.py";

            if (!File.Exists(scriptPath))
            {
                return Results.Problem("Python script is absent !", null, 500);
            }

            // Chemin vers l'environnement virtuel
            string venvPath = @".venv/bin/activate";

            if (!File.Exists(venvPath))
            {
                return Results.Problem("Python environment is absent !", null, 500);
            }

            // Commande à exécuter
            string command = $"source {venvPath} && python {scriptPath}";

            var start = new ProcessStartInfo
            {
                FileName = "/bin/bash",  // Utilise le shell Bash
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(start);

            if (process == null) return Results.StatusCode(500);

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0) return Results.Problem($"Python error: {error}");   

            Console.WriteLine(output);

            var offersLines = output.Split("\n");

            foreach (var line in offersLines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    offersData = output.Split(";;");

                    if (offersData.Count() == 3)
                    {
                        jobOffers.Add(
                            new JobOfferDTO {
                                Name = offersData[0].Trim(), 
                                Url = offersData[1].Trim(), 
                                Location = offersData[2].Trim()
                            }
                        );
                    }               
                }            
            }

            return Results.Ok(jobOffers);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error: {ex.Message}");
        }
    }
}