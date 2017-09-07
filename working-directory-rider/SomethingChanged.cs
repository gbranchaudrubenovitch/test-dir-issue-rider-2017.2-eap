using System;
using System.IO;
using NUnit.Framework;

namespace working_directory_rider
{
    [TestFixture]
    public class SomethingChanged
    {
        [Test]
        public void TestDirShouldNotBeAppData()
        {
            var testDir = TestContext.CurrentContext.TestDirectory;
            Assert.That(testDir, Does.Not.Contain("AppData"));
        }

        [Test]
        public void TheProjectFileShouldBeFindableFromTestDirectory()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            var projectPath = DiscoverProject("working-directory-rider");
            Assert.That(projectPath, Is.Not.Null.Or.Empty);
        }

        /// <summary>
        /// Simplified version of a method we use to start our webmvc project for system tests.
        /// Taken from the SpecsFor.Mvc lib. 
        /// </summary>
        private static string DiscoverProject(string projectName)
        {
            var currentFolder = Environment.CurrentDirectory;
            Console.WriteLine("Discovering path to project {0} for Specs4MVC tests. Starting in {1}", projectName, currentFolder);
            string projectPath = null;
            var directoryInfo = new DirectoryInfo(currentFolder);

            while (projectPath == null)
            {
                var solutionFiles = directoryInfo.GetFiles("*.sln", SearchOption.AllDirectories);
                foreach (var solutionFile in solutionFiles)
                {
                    Console.WriteLine("Found a possible solution file {0}", solutionFile.FullName);

                    projectPath = Path.Combine(solutionFile.Directory.FullName, projectName);
                    if (Directory.Exists(projectPath))
                    {
                        Console.WriteLine("Found suitable project path {0}", projectPath);
                        break;
                    }

                    Console.WriteLine("Assumed project path {0} does not exist. Continuing search...", projectPath);
                    projectPath = null;
                }

                if (projectPath != null) continue;

                if (directoryInfo.Parent == null)
                {
                    throw new InvalidOperationException(string.Format("Unable to find project file {0}. Started at {1}, Searched all the way up to {2}", projectName, currentFolder, directoryInfo.FullName));
                }
                directoryInfo = directoryInfo.Parent;
            }

            return projectPath;
        }
    }
}