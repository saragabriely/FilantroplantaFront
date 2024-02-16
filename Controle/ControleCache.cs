using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControleCache
    {
        public ControleCache() { }

        public readonly IAppCache cache = new CachingService();

        public List<string> cacheKeys { get; set; }
        public long Token { get; set; }
        public Models.Pessoa pessoa { get; set; }

        private static ControleCache _instance;

        public static ControleCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ControleCache();
                }
                return _instance;
            }
        }

        public void InicializaPropriedadesCache()
        {
            Instance.cacheKeys = new List<string>();
            Instance.Token     = 0;
            Instance.pessoa    = new Models.Pessoa();
        }

        public void LimparCache()
        {
            foreach (var chave in Instance.cacheKeys)
            {
                cache.Remove(chave);
            }

            Instance.cacheKeys.Clear();
        }

        public void AdicionarChaveCache(string chave, object valor)
        {
            cache.Add(chave, valor);
            Instance.cacheKeys.Add(chave);
        }

        public void RemoverChaveCache(string chave)
        {
            if(Instance.cacheKeys.Contains(chave))
                cache.Remove(chave);
        }
    }
}
