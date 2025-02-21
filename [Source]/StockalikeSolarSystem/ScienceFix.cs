using System.Linq;
using UnityEngine;


namespace SASSPlugin
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class ScienceFix : MonoBehaviour
    {
        void Awake()
        {
            if (AssemblyLoader.loadedAssemblies.FirstOrDefault(a => a.name == "GalacticNeighborhood") != null) return;

            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                if (!config.HasNode("RESULTS") || !config.HasNode("STOCK_RESULTS") || !config.GetNode("STOCK_RESULTS").HasNode("RESULTS")) continue;
                ConfigNode results = config.GetNode("RESULTS");
                ConfigNode stockResults = config.GetNode("STOCK_RESULTS").GetNode("RESULTS");
                string[] stockPlanets = new[] { "Sun", "Moho", "Eve", "Gilly", "Kerbin", "Mun", "Minmus", "Duna", "Ike", "Dres", "Jool", "Laythe", "Vall", "Tylo", "Bop", "Pol", "Eeloo" };

                foreach (ConfigNode.Value def in results.values)
                {
                    if (def.name.StartsWith("Polta"))
                        def.name = "OPM" + def.name;
                }
                foreach (string planet in stockPlanets)
                {
                    results.RemoveValuesStartWith(planet);
                }
                results.AddData(stockResults);
            }
        }
    }
}
