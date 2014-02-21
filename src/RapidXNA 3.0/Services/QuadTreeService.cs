using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

/*TODO JMC http://www.xnawiki.com/index.php/QuadTree*/
/*Cleanup code and split classes
 Inherit from IRapidService*/
/*Note this is only for XNA*/
namespace RapidXNA.Services
{
    public class QuadTreeRoot
    {
        readonly List<QuadTree> _trees = new List<QuadTree>();
        public void UpdateItem(QuadItem item)
        {
            foreach (var t in _trees)
            {
                t.Remove(item);
                t.Add(item);
            }
        }

        public void Add(QuadItem item)
        {
            item.BindRoot(this);

            var found = false;
            foreach (var t in _trees.Where(t => t.Bounds.Intersects(item.Position)))
            {
                found = true;
                t.Add(item);
            }

            if (found) return;
            var x = (item.X + 512) / 1024;
            var y = (item.Y + 512) / 1024;

            var newTree = new QuadTree {Bounds = new Rectangle(x*1024, y*1024, 1024, 1024)};

            newTree.Add(item);
            _trees.Add(newTree);
        }

        public void Remove(QuadItem item)
        {
            foreach (var t in _trees)
            {
                t.Remove(item);
            }
        }

        public List<QuadItem> Find(Rectangle target)
        {
            List<QuadItem> results = new List<QuadItem>(),
                            removed = new List<QuadItem>();

            foreach (var t in _trees)
            {
                results.AddRange(t.Find(target));
            }

            foreach (var t in results.Where(t => !removed.Contains(t)))
            {
                removed.Add(t);
            }

            return removed;
        }
    }

    public class QuadTree
    {
        public QuadTree[] Children = new QuadTree[4];
        public Rectangle Bounds = new Rectangle();

        readonly List<QuadItem> _items = new List<QuadItem>();

        public bool Contains(QuadItem item)
        {
            return _items.Any(t => t == item);
        }

        public void Add(QuadItem item)
        {
            if (Children[0] == null)
            {
                if (_items.Count > 1000)
                {
                    if (Bounds.Width > 256)
                    {
                        Children[0] = new QuadTree
                        {
                            Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width/2, Bounds.Height/2)
                        };
                        Children[1] = new QuadTree
                        {
                            Bounds = new Rectangle(Bounds.X + Bounds.Width/2, Bounds.Y, Bounds.Width/2, Bounds.Height/2)
                        };
                        Children[2] = new QuadTree
                        {
                            Bounds =
                                new Rectangle(Bounds.X, Bounds.Y + Bounds.Height/2, Bounds.Width/2, Bounds.Height/2)
                        };
                        Children[3] = new QuadTree
                        {
                            Bounds =
                                new Rectangle(Bounds.X + Bounds.Width/2, Bounds.Y + Bounds.Height/2, Bounds.Width/2,
                                    Bounds.Height/2)
                        };

                        foreach (var t in _items)
                        {
                            Children[0].Add(t);
                            Children[1].Add(t);
                            Children[2].Add(t);
                            Children[3].Add(t);
                        }

                        _items.Clear();

                    }
                    else
                    {
                        if (Bounds.Intersects(item.Position))
                            _items.Add(item);
                    }
                }
                else
                {
                    if (Bounds.Intersects(item.Position))
                        _items.Add(item);
                }
                
            }
            else
            {
                Children[0].Add(item);
                Children[1].Add(item);
                Children[2].Add(item);
                Children[3].Add(item);
            }
        }

        public void Remove(QuadItem item)
        {
            if (Children[0] == null)
            {
                if (_items.Contains(item))
                    _items.Remove(item);
            }
            else
            {
                Children[0].Remove(item);
                Children[1].Remove(item);
                Children[2].Remove(item);
                Children[3].Remove(item);
            }
        }

        public List<QuadItem> Find(Rectangle target)
        {
            var results = new List<QuadItem>();

            if (!Bounds.Intersects(target))
                return results;

            if (Children[0] == null)
            {
                results.AddRange(from item in _items where item.Position.Intersects(target) select item);
            }
            else
            {
                results.AddRange(Children[0].Find(target));
                results.AddRange(Children[1].Find(target));
                results.AddRange(Children[2].Find(target));
                results.AddRange(Children[3].Find(target));
            }

            return results;
        }
    }

    public class QuadItem
    {
        private Rectangle _position;
        public int X { get { return _position.X; } set { _position.X = value; PositionChanged(); } }
        public int Y { get { return _position.Y; } set { _position.Y = value; PositionChanged(); } }
        public int Width { get { return _position.Width; } set { _position.Width = value; PositionChanged(); } }
        public int Height { get { return _position.Height; } set { _position.Height = value; PositionChanged(); } }

        public Rectangle Position { get { return _position; } }

        private QuadTreeRoot _root;

        private void PositionChanged()
        {
            if (_root != null)
                _root.UpdateItem(this);
        }

        public void BindRoot(QuadTreeRoot root)
        {
            _root = root;
        }
    }


}
