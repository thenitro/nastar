using System.Collections.Generic;

namespace Nastar
{
    public class SparseMatrix<T>
    {
        private int _minX;
        private int _minY;
    
        private int _maxX;
        private int _maxY;
    
        private Dictionary<int, Dictionary<int, T>> _rows;
    
        public SparseMatrix()
        {
            _minX = 0;
            _minY = 0;
            _maxX = 0;
            _maxY = 0;
    
            _rows = new Dictionary<int, Dictionary<int, T>>();
        }
    
        public int MinX
        {
            get { return _minX; }
        }
    
        public int MinY
        {
            get { return _minY; }
        }
    
        public int MaxX
        {
            get { return _maxX; }
        }
    
        public int MaxY
        {
            get { return _maxY; }
        }
    
        public void Add(int x, int y, T node)
        {
            var cols = TakeCol(x);
            cols[y] = node;
    
            if (y <= _minY)
            {
                _minY = y;
            }
    
            if (y >= _maxY)
            {
                _maxY = y + 1;
            }
        }
    
        public T Take(int x, int y)
        {
            if (!Has(x, y))
            {
                return default(T);
            }
    
            return _rows[x][y];
        }
    
        public bool Has(int x, int y)
        {
            if (x < _minX || y < _minY || x > _maxX || y > _maxY)
            {
                return false;
            }
    
            if (!_rows.ContainsKey(x))
            {
                return false;
            }
    
            return _rows[x].ContainsKey(y);
        }
    
        public List<T> ToList()
        {
            var result = new List<T>();
    
            for (int x = MinX; x < MaxX; x++)
            {
                for (int y = MinY; y < MaxY; y++)
                {
                    if (Has(x, y))
                    {
                        result.Add(Take(x, y));
                    }
                }
            }
    
            return result;
        }
    
        private Dictionary<int, T> TakeCol(int x)
        {
            Dictionary<int, T> cols;
    
            if (_rows.ContainsKey(x))
            {
                cols = _rows[x];
            }
            else
            {
                cols = new Dictionary<int, T>();
                _rows[x] = cols;
            }
    
            if (x <= _minX)
            {
                _minX = x;
            }
    
            if (x >= _maxX)
            {
                _maxX = x + 1;
            }
    
            return cols;
        }
    }
}