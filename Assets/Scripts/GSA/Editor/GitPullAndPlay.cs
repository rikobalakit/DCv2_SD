using System;
using UnityEditor;
using System.Diagnostics;
using UnityEditor.SceneManagement;

public class GitPullMenuItem
{
    [MenuItem("Git/Pull and Reload")]
    public static void GitPull()
    {
        // Execute the "git pull" command
        RunGitCommand("pull");

        // Force Unity to refresh the assets and reload the scene
        AssetDatabase.Refresh();
        // Ensure the scripts are recompiled (AssetDatabase.Refresh() also triggers this)
    }

    [MenuItem("Git/PushLazy")]
    public static void GitPush()
    {
        // Run git add *
        RunGitCommand("add", "*");

        // Run git commit with a lazy message
        string commitMessage = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} Lazy Push";
        RunGitCommand("commit", $"-a -m \"{commitMessage}\"");

        // Run git push
        RunGitCommand("push", "");
    }

    private static void RunGitCommand(string command, string arguments = null)
    {
        // Execute the git command
        if (arguments == null)
        {
            Process.Start("git", command);
        }
        else
        {
            Process.Start("git", $"{command} {arguments}");
        }
    }
}
