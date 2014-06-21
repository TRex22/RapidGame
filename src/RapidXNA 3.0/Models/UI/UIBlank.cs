using Microsoft.Xna.Framework;
using RapidXNA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RapidXNA.Models.UI
{
    /// <summary>
    /// The base class for all UI items.
    /// 
    /// Contains the scaffolding needed for management and consistent usage.
    /// </summary>
    public class UIBlank
    {
        private int _sortOrder = 0;

        public int SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        private bool _isActive = true;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public virtual void Update(GameTime gt, UIService _ui) { }

        public virtual void Draw(GameTime gt, UIService _ui) { }

        /// Notes:
        /// - need to rethink the UIService passthrough.
    }
}
