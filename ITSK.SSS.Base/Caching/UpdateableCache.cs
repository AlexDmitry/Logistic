using ITSK.SSS.Base.Enums;
using ITSK.SSS.Base.Logging;
using ITSK.SSS.Base.Threads;
using System;
using System.Collections.Generic;

namespace ITSK.SSS.Base.Caching
{
    /// <summary>
    /// Обновляемый кэш, который обновляется при помощи переданного в конструктор метода обновления
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class UpdateableCache<TKey, TData>
    {
        public TData this[TKey key]
        {
            get
            {
                lock (this)
                {
                    if (_store == null) return default(TData);
                    TData result;
                    _store.TryGetValue(key, out result);
                    return result;
                }
            }
        }

        protected IEnumerable<TData> GetValues()
        {
            lock (this)
            {
                return _store == null ? null : _store.Values;
            }
        }

        private Dictionary<TKey, TData> _store;

        private DateTime? _lastCheckDateTime;
        private readonly Func<DateTime?, Dictionary<TKey, TData>, Dictionary<TKey, TData>> _updateCache;
        private readonly CycledThread _checkThread;
        private readonly IAssistLogger _logger;

        public UpdateableCache(
            Func<DateTime?, Dictionary<TKey, TData>, Dictionary<TKey, TData>> updateCache,
            TimeSpan checkChangesTime,
            IAssistLogger logger = null,
            DateTime? lastCheckDateTime = null)
        {
            _lastCheckDateTime = lastCheckDateTime;
            _logger = logger;
            _updateCache = updateCache;

            _checkThread = new CycledThread(CheckChanges, checkChangesTime);
            _checkThread.Start();
        }

        private void CheckChanges()
        {
            var newLastCheckDateTime = DateTime.UtcNow;

            try
            {
                var newData = _updateCache(_lastCheckDateTime, _store == null ? _store : new Dictionary<TKey, TData>(_store));
                lock (this)
                {
                    _store = newData;
                }
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(LogCategory.SssBase, ex);
                }
            }

            _lastCheckDateTime = newLastCheckDateTime;
        }
    }
}
