using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace WaffleCat.Core
{
    //A custom manager to allow for default resources in case of bad loading
    public class WaffleCatContentManager : ContentManager
    {
        private Dictionary<Type, object> defaultResources = new Dictionary<Type, object>();

        public WaffleCatContentManager(IServiceProvider services)
            : base(services) { }

        /// <summary>
        /// Attempt to load an asset. If the asset is not found, load the default instead.
        /// </summary>
        /// <typeparam name="T">The content type to load.</typeparam>
        /// <param name="assetName">The path to the asset to be loaded.</param>
        /// <returns>The loaded asset.</returns>
        public override T Load<T>(String assetName)
        {
            try
            {
                return base.Load<T>(assetName);
            }
            catch (ContentLoadException c)
            {
                Log.Write("Could not find asset " + assetName);
                try
                {
                    return GetDefault<T>();
                }
                catch (KeyNotFoundException key)
                {
                    Log.WriteError("Couldn't find a default resource for " + assetName);
                    throw c;
                }
            }
        }

        public void SetDefault<T>(string filename)
        {
            defaultResources[typeof(T)] = this.Load<T>(filename);
        }

        public T GetDefault<T>()
        {
            return (T)defaultResources[typeof(T)];
        }
    }
}
