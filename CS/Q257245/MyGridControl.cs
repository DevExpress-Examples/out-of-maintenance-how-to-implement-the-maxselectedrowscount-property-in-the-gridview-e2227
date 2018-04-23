using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;

namespace DXSample {
    public class MyGridControl :GridControl {
        public MyGridControl() : base() { }

        protected override void RegisterAvailableViewsCore(InfoCollection collection) {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridViewInfoRegistrator());
        }
    }

    public class MyGridView :GridView {
        public MyGridView() : base() { }
        public MyGridView(GridControl grid) : base(grid) { }

        internal const string MyGridViewName = "MyGridView";
        protected override string ViewName { get { return MyGridViewName; } }

        private int fMaxSelectedRowsCount = -1;
        public int MaxSelectedRowsCount {
            get { return fMaxSelectedRowsCount; }
            set {
                if (fMaxSelectedRowsCount == value) return;
                fMaxSelectedRowsCount = value;
                ClearSelection();
            }
        }

        protected override void InvertFocusedRowSelectionCore(BaseHitInfo hitInfo) {
            if (OptionsSelection.MultiSelectMode == GridMultiSelectMode.RowSelect && MaxSelectedRowsCount > 0 && 
                MaxSelectedRowsCount <= SelectedRowsCount && !IsRowSelected(((GridHitInfo)hitInfo).RowHandle)) 
                return;
            base.InvertFocusedRowSelectionCore(hitInfo);
        }

        protected override void SelectAnchorRangeCore(bool controlPressed, bool allowCells) {
            if (MaxSelectedRowsCount > 0 && OptionsSelection.MultiSelectMode == GridMultiSelectMode.RowSelect) {
                int anchor = GetVisibleIndex(SelectionAnchorRowHandle);
                int focused = GetVisibleIndex(FocusedRowHandle);
                if (controlPressed) {
                    int selected = SelectedRowsCount;
                    int start = Math.Min(anchor, focused);
                    int end = Math.Max(anchor, focused);
                    for (int i = start; i < end; i++) if (!IsRowSelected(i)) selected++;
                    if (selected >= MaxSelectedRowsCount) return;
                } else if (Math.Abs(anchor - focused) >= MaxSelectedRowsCount) return;
            }
            base.SelectAnchorRangeCore(controlPressed, allowCells);
        }
    }

    public class MyGridViewInfoRegistrator :GridInfoRegistrator {
        public MyGridViewInfoRegistrator() : base() { }

        public override string ViewName { get { return MyGridView.MyGridViewName; } }

        public override BaseView CreateView(GridControl grid) {
            return new MyGridView(grid);
        }
    }
}