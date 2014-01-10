using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RapidXNA.Helpers
{
    public class QuadTreeRoot
    {
        List<QuadTree> Trees = new List<QuadTree>();
        public void UpdateItem(IQuadItem item)
        {
            for (int i = 0; i < Trees.Count; i++)
            {
                Trees[i].Remove(item);
                Trees[i].Add(item);
            }
        }

        public void Add(IQuadItem item)
        {
            item.BindRoot(this);

            bool found = false;
            for (int i = 0; i < Trees.Count; i++)
            {
                if (Trees[i].Bounds.Intersects(item.Position))
                {
                    found = true;
                    Trees[i].Add(item);
                }
            }

            if (!found)
            {
                int x = (int)((item.X + 512) / 1024);
                int y = (int)((item.Y + 512) / 1024);

                QuadTree newTree = new QuadTree();
                newTree.Bounds = new Rectangle(x * 1024, y * 1024, 1024, 1024);

                newTree.Add(item);
                Trees.Add(newTree);
            }
        }

        public void Remove(IQuadItem item)
        {
            for (int i = 0; i < Trees.Count; i++)
            {
                Trees[i].Remove(item);
            }
        }

        public List<IQuadItem> Find(Rectangle target)
        {
            List<IQuadItem> results = new List<IQuadItem>(),
                            removed = new List<IQuadItem>();

            for (int i = 0; i < Trees.Count; i++)
            {
                results.AddRange(Trees[i].Find(target));
            }

            for (int i = 0; i < results.Count; i++)
            {
                if (!removed.Contains(results[i]))
                {
                    removed.Add(results[i]);
                }
            }

            return removed;
        }
    }

    public class QuadTree
    {
        public QuadTree[] Children = new QuadTree[4];
        public Rectangle Bounds = new Rectangle();

        List<IQuadItem> Items = new List<IQuadItem>();

        public bool Contains(IQuadItem item)
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i] == item)
                    return true;

            return false;
        }

        public void Add(IQuadItem item)
        {
            if (Children[0] == null)
            {
                if (Items.Count > 1000)
                {
                    if (Bounds.Width > 256)
                    {
                        Children[0] = new QuadTree();
                        Children[0].Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width / 2, Bounds.Height / 2);
                        Children[1] = new QuadTree();
                        Children[1].Bounds = new Rectangle(Bounds.X + Bounds.Width / 2, Bounds.Y, Bounds.Width / 2, Bounds.Height / 2);
                        Children[2] = new QuadTree();
                        Children[2].Bounds = new Rectangle(Bounds.X, Bounds.Y + Bounds.Height / 2, Bounds.Width / 2, Bounds.Height / 2);
                        Children[3] = new QuadTree();
                        Children[3].Bounds = new Rectangle(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2, Bounds.Width / 2, Bounds.Height / 2);

                        for (int i = 0; i < Items.Count; i++)
                        {
                            Children[0].Add(Items[i]);
                            Children[1].Add(Items[i]);
                            Children[2].Add(Items[i]);
                            Children[3].Add(Items[i]);
                        }

                        Items.Clear();

                    }
                    else
                    {
                        if (Bounds.Intersects(item.Position))
                            Items.Add(item);
                    }
                }
                else
                {
                    if (Bounds.Intersects(item.Position))
                        Items.Add(item);
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

        public void Remove(IQuadItem item)
        {
            if (Children[0] == null)
            {
                if (Items.Contains(item))
                    Items.Remove(item);
            }
            else
            {
                Children[0].Remove(item);
                Children[1].Remove(item);
                Children[2].Remove(item);
                Children[3].Remove(item);
            }
        }

        public List<IQuadItem> Find(Rectangle target)
        {
            List<IQuadItem> results = new List<IQuadItem>();

            if (!Bounds.Intersects(target))
                return results;

            if (Children[0] == null)
            {
                results.AddRange(from item in Items where item.Position.Intersects(target) select item);
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

    public class IQuadItem
    {
        private Rectangle _position;
        public int X { get { return _position.X; } set { _position.X = value; PositionChanged(); } }
        public int Y { get { return _position.Y; } set { _position.Y = value; PositionChanged(); } }
        public int Width { get { return _position.Width; } set { _position.Width = value; PositionChanged(); } }
        public int Height { get { return _position.Height; } set { _position.Height = value; PositionChanged(); } }

        public Rectangle Position { get { return _position; } }

        private QuadTreeRoot Root;

        private void PositionChanged()
        {
            if (Root != null)
                Root.UpdateItem(this);
        }

        public void BindRoot(QuadTreeRoot root)
        {
            Root = root;
        }
    }


}
