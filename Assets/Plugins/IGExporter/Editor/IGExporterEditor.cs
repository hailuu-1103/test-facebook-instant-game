using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections.Generic;
using System.IO;

namespace QMG
{

    public class IGExporterEditor
    {

        [PostProcessBuild()]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                //只有WebGL才需要处理
                return;
            }

            Debug.Log("Application.version=" + Application.unityVersion);

            string templateVer = "2018";
            if (Application.unityVersion.StartsWith("2018."))
            {
                templateVer = "2018";
            }
            else if (Application.unityVersion.StartsWith("2019."))
            {
                templateVer = "2019";
            }
            else
            {
                //未处理的
            }

            Debug.Log("copying|template|files|from|ver=" + templateVer);

            Dictionary<string, string> copyMap = new Dictionary<string, string>();
            copyMap.Add(Application.dataPath + "/Plugins/IGExporter/Editor/TemplateFile/ver_" + templateVer + "/" + "index.html", pathToBuildProject + "/index.html");
            copyMap.Add(Application.dataPath + "/Plugins/IGExporter/Editor/TemplateFile/ver_" + templateVer + "/" + "fbapp-config.json", pathToBuildProject + "/fbapp-config.json");
            copyMap.Add(Application.dataPath + "/Plugins/IGExporter/Editor/TemplateFile/ver_" + templateVer + "/Build/" + "UnityLoader.js.tpl", pathToBuildProject + "/Build/UnityLoader.js");
            copyMap.Add(Application.dataPath + "/Plugins/IGExporter/Editor/TemplateFile/ver_" + templateVer + "/TemplateData/" + "UnityProgress.js.tpl", pathToBuildProject + "/TemplateData/UnityProgress.js");

            int copyCount = 0;
            int skipCount = 0;
            foreach (var item in copyMap)
            {
                if (item.Key.Contains("/fbapp-config.json"))
                {
                    if (File.Exists(item.Value))
                    {
                        skipCount++;
                        continue;
                    }
                }

                copyCount++;
                File.Copy(item.Key, item.Value, true);
            }

            Debug.Log("IGExporterEditor|OnPostprocessBuild|ok|copy|files|copyCount=" + copyCount + "|skipCount=" + skipCount);
        }

    }

}
