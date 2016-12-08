using System;
using System.Collections.Generic;
using System.Linq;

namespace Typing_Speed_Trainer
{
    public class LastVisitedUrisCollection
    {
        private readonly int _size;
        private readonly Queue<Tuple<string, Uri>> _lastVisited;

        public LastVisitedUrisCollection(int size)
        {
            _size = size;
            _lastVisited = new Queue<Tuple<string, Uri>>(_size);
        }

        public void Append(Uri uri)
        {
            if (_lastVisited.Count >= _size)
            {
                _lastVisited.Dequeue();
            }

            var entry = new Tuple<string, Uri>(uri.Host, uri);
            if (GetUri(uri.Host) == null)
            {
                _lastVisited.Enqueue(new Tuple<string, Uri>(uri.Host, uri));
            }
        }

        public Uri GetUri(string host)
        {
            return (from tuple in _lastVisited where tuple.Item1 == host select tuple.Item2).FirstOrDefault();
        }

        public List<string> Uris()
        {
            return _lastVisited.Select(tuple => tuple.Item1).ToList();
        }
    }
}